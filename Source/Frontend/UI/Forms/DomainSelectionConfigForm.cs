namespace RTCV.UI
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using System.Windows.Forms;
    using RTCV.CorruptCore;
    using RTCV.NetCore;
    using RTCV.Common;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using System.IO;
    using System.Configuration.Internal;

    public partial class DomainSelectionConfigForm : Modular.ColorizedForm
    {
        public DomainSelectionConfigForm()
        {
            InitializeComponent();
        }

        public void UpdateDomainsList()
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.SuspendLayout();

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27));

            var domains = MemoryDomains.MemoryInterfaces;
            var i = 0;
            foreach (KeyValuePair<string, MemoryDomainProxy> domain in domains)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

                tableLayoutPanel1.Controls.Add(new Label { Text = domain.Key, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 14), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, Margin = new Padding(2, 2, 2, 2) }, 0, i);

                var showInRtcCheckbox = new CheckBox { Checked = domains[domain.Key].Visible, CheckAlign = ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, AutoSize = true };
                showInRtcCheckbox.Click += new System.EventHandler((sender, e) => OnShowInRtcCheckbox(sender, e, domain.Key));
                tableLayoutPanel1.Controls.Add(showInRtcCheckbox, 1, i);

                var autoDomainCheckbox = new CheckBox { Checked = domains[domain.Key].AutoDomainSelect, CheckAlign = ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, AutoSize = true };
                autoDomainCheckbox.Click += new System.EventHandler((sender, e) => OnAutoDomainCheckbox(sender, e, domain.Key));
                tableLayoutPanel1.Controls.Add(autoDomainCheckbox, 2, i);

                i++;
            }


            tableLayoutPanel1.ResumeLayout();
            tableLayoutPanel1.Visible = true;

        }

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateDomainsList();
        }

        private void OnShown(object sender, EventArgs e)
        {
            UpdateDomainsList();
        }

        private void OnShowInRtcCheckbox(object sender, EventArgs e, string domainName)
        {
            CheckBox checkBox = sender as CheckBox;

            MemoryDomains.MemoryInterfaces[domainName].Visible = checkBox.Checked;
            LocalNetCoreRouter.Route(NetCore.Endpoints.UI, NetCore.Commands.Remote.EventDomainsUpdated, new object[] { false }, true);
        }

        private void OnAutoDomainCheckbox(object sender, EventArgs e, string domainName)
        {
            CheckBox checkBox = sender as CheckBox;

            MemoryDomains.MemoryInterfaces[domainName].AutoDomainSelect = checkBox.Checked;

            string[] blacklistedDomains = MemoryDomains.MemoryInterfaces.Keys.Where(key => MemoryDomains.MemoryInterfaces[key].AutoDomainSelect == false).ToArray();

            AllSpec.VanguardSpec.Update(VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS, blacklistedDomains);
            LocalNetCoreRouter.Route(NetCore.Endpoints.UI, NetCore.Commands.Remote.EventDomainsUpdated, new object[] { false }, true);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void resetToDefaultButton_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to reset to the default domains configuration?", "Reset Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string configFileName = "DEFAULT_" + new DirectoryInfo(CorruptCore.RtcCore.EmuDir).Name + "_DOMAINS_CONFIG";
                string systemCore = AllSpec.VanguardSpec[VSPEC.SYSTEMCORE].ToString();

                if (Params.IsParamSet(configFileName))
                {
                    var configFile = File.ReadAllText(Path.Combine(Params.ParamsDir, configFileName));
                    var jsonString = JsonConvert.DeserializeObject<DomainConfigRoot>(configFile);

                    List<string> defaultDomains = new List<string>();
                    if (jsonString.DomainConfigSystem.ContainsKey(systemCore))
                    {
                        foreach (string domain in jsonString.DomainConfigSystem[systemCore].DomainConfig.Keys)
                        {
                            if (!jsonString.DomainConfigSystem[systemCore].DomainConfig[domain].AUTOSELECT)
                                defaultDomains.Add(domain);
                        }
                    }

                    AllSpec.VanguardSpec.Update(VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS, defaultDomains.ToArray());

                    Params.RemoveParam(configFileName);

                    LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.UI, RTCV.NetCore.Commands.Remote.EventDomainsUpdated, new object[] { true, true }, true);

                    UpdateDomainsList();
                }
            }
        }
        private void SaveToFileButton_MouseClick(object sender, MouseEventArgs e)
        {
            string currentFilename = "";
            string systemCore = AllSpec.VanguardSpec[VSPEC.SYSTEMCORE].ToString();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                DefaultExt = "dmc",
                Title = "Save Domains Config File",
                Filter = "DMC files|*.dmc",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFilename = saveFileDialog1.FileName;
            }
            else
            {
                return;
            }

            DomainConfigRoot savedConfig = new DomainConfigRoot();
            DomainConfigSystem configSystem = new DomainConfigSystem();
            foreach (KeyValuePair<string, MemoryDomainProxy> domain in MemoryDomains.MemoryInterfaces)
            {
                configSystem.DomainConfig[domain.Key] = new DomainConfig(domain.Value.Visible, domain.Value.AutoDomainSelect);
            }
            savedConfig.DomainConfigSystem[systemCore] = configSystem;

            string jsonString = JsonConvert.SerializeObject(savedConfig, Formatting.Indented);
            Params.SetParam(currentFilename, jsonString);
        }

        private void LoadFromFileButton_MouseClick(object sender, MouseEventArgs e)
        {
            string currentFilename = "";
            string systemCore = AllSpec.VanguardSpec[VSPEC.SYSTEMCORE].ToString();
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    DefaultExt = "dmc",
                    Title = "Open Domains Config File",
                    Filter = "DMC files|*.dmc",
                    RestoreDirectory = true
                };
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    currentFilename = OpenFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The DMC file {currentFilename} could not be loaded.");
            }

            var configFile = File.ReadAllText(Path.Combine(Params.ParamsDir, currentFilename));
            var jsonString = JsonConvert.DeserializeObject<DomainConfigRoot>(configFile);

            List<string> defaultDomains = new List<string>();
            if (jsonString.DomainConfigSystem.ContainsKey(systemCore))
            {
                foreach (string domain in jsonString.DomainConfigSystem[systemCore].DomainConfig.Keys)
                {
                    if (!jsonString.DomainConfigSystem[systemCore].DomainConfig[domain].AUTOSELECT)
                        defaultDomains.Add(domain);
                }
            }

            AllSpec.VanguardSpec.Update(VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS, defaultDomains.ToArray());

            Params.RemoveParam(systemCore + "_DOMAINS_CONFIG");

            LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.UI, RTCV.NetCore.Commands.Remote.EventDomainsUpdated, new object[] { true, true }, true);

            UpdateDomainsList();
        }
    }
}
