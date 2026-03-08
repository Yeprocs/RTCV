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
        private bool settingChangedInForm = false;
        public DomainSelectionConfigForm()
        {
            InitializeComponent();
        }

        public void UpdateDomainsList()
        {
            // If we end up clicking on a setting in the form directly, we don't need to update the entire table
            if (settingChangedInForm)
            {
                settingChangedInForm = false;
                return;
            }

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

            var vmds = MemoryDomains.VmdPool;
            foreach (KeyValuePair<string, VirtualMemoryDomain> vmd in vmds)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

                tableLayoutPanel1.Controls.Add(new Label { Text = vmd.Key, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 14), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, Margin = new Padding(2, 2, 2, 2) }, 0, i);

                var showInRtcCheckbox = new CheckBox { Checked = vmds[vmd.Key].Visible, CheckAlign = ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, AutoSize = true };
                showInRtcCheckbox.Click += new System.EventHandler((sender, e) => OnShowInRtcCheckbox(sender, e, vmd.Key));
                tableLayoutPanel1.Controls.Add(showInRtcCheckbox, 1, i);

                var autoDomainCheckbox = new CheckBox { Checked = vmds[vmd.Key].AutoDomainSelect, CheckAlign = ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, AutoSize = true };
                autoDomainCheckbox.Click += new System.EventHandler((sender, e) => OnAutoDomainCheckbox(sender, e, vmd.Key));
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
            this.Refresh();
        }

        private void OnShowInRtcCheckbox(object sender, EventArgs e, string domainName)
        {
            settingChangedInForm = true;
            CheckBox checkBox = sender as CheckBox;

            if (domainName.Contains("[V]"))
                MemoryDomains.VmdPool[domainName].Visible = checkBox.Checked;
            else
                MemoryDomains.MemoryInterfaces[domainName].Visible = checkBox.Checked;
            LocalNetCoreRouter.Route(NetCore.Endpoints.UI, NetCore.Commands.Remote.EventDomainsUpdated, new object[] { false }, true);
        }

        private void OnAutoDomainCheckbox(object sender, EventArgs e, string domainName)
        {
            settingChangedInForm = true;
            CheckBox checkBox = sender as CheckBox;

            if (domainName.Contains("[V]"))
                MemoryDomains.VmdPool[domainName].AutoDomainSelect = checkBox.Checked;
            else
                MemoryDomains.MemoryInterfaces[domainName].AutoDomainSelect = checkBox.Checked;

            var blacklistedDomains = MemoryDomains.MemoryInterfaces.Keys.Where(key => MemoryDomains.MemoryInterfaces[key].AutoDomainSelect == false);
            blacklistedDomains = blacklistedDomains.Concat(MemoryDomains.VmdPool.Keys.Where(key => MemoryDomains.VmdPool[key].AutoDomainSelect == false));

            AllSpec.VanguardSpec.Update(VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS, blacklistedDomains.ToArray());
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
                string configFileName = new DirectoryInfo(CorruptCore.RtcCore.EmuDir).Name + "_DOMAINS_CONFIG";
                string defaultConfigFileName = "DEFAULT_" + configFileName;
                string systemCore = AllSpec.VanguardSpec[VSPEC.SYSTEMCORE].ToString();

                if (Params.IsParamSet(defaultConfigFileName))
                {
                    var jsonString = File.ReadAllText(Path.Combine(Params.ParamsDir, configFileName));
                    var defaultJsonString = File.ReadAllText(Path.Combine(Params.ParamsDir, defaultConfigFileName));
                    var config = JsonConvert.DeserializeObject<DomainConfigRoot>(jsonString);
                    var defaultConfig = JsonConvert.DeserializeObject<DomainConfigRoot>(defaultJsonString);

                    List<string> defaultDomains = new List<string>();
                    if (defaultConfig.DomainConfigSystem.ContainsKey(systemCore))
                    {
                        foreach (string domain in defaultConfig.DomainConfigSystem[systemCore].DomainConfig.Keys)
                        {
                            if (MemoryDomains.MemoryInterfaces.ContainsKey(domain))
                            {
                                MemoryDomains.MemoryInterfaces[domain].Visible = defaultConfig.DomainConfigSystem[systemCore].DomainConfig[domain].VISIBLE;
                                MemoryDomains.MemoryInterfaces[domain].AutoDomainSelect = defaultConfig.DomainConfigSystem[systemCore].DomainConfig[domain].AUTOSELECT;
                            }
                        }
                        config.DomainConfigSystem.Remove(systemCore);
                        jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);

                        Params.SetParam(configFileName, jsonString);
                    }
                    foreach (string vmd in MemoryDomains.VmdPool.Keys)
                    {
                        MemoryDomains.VmdPool[vmd].Visible = true;
                        MemoryDomains.VmdPool[vmd].AutoDomainSelect = true;
                    }

                    LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.UI, RTCV.NetCore.Commands.Remote.EventDomainsUpdated, new object[] { false }, true);

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
            foreach (KeyValuePair<string, VirtualMemoryDomain> vmd in MemoryDomains.VmdPool)
            {
                configSystem.DomainConfig[vmd.Key] = new DomainConfig(vmd.Value.Visible, vmd.Value.AutoDomainSelect);
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

            List<string> blacklistedDomains = new List<string>();
            if (jsonString.DomainConfigSystem.ContainsKey(systemCore))
            {
                bool refreshVMDs = false;
                List<string> failedLoadVMDs = new List<string>();
                foreach (string domain in jsonString.DomainConfigSystem[systemCore].DomainConfig.Keys)
                {
                    // If it's a vmd domain, try to find it in the vmd directory and load it if possible
                    if (domain.Contains("[V]"))
                    {
                        string vmdName = domain.Remove(0, 3) + ".vmd";
                        string path = Path.Combine(RtcCore.VmdsDir, vmdName);
                        // If the vmd is already loaded into RTC, we can skip trying to find and load
                        if (!MemoryDomains.VmdPool.ContainsKey(domain))
                            {
                            if (File.Exists(path))
                            {
                                S.GET<VmdPoolForm>().loadVmd(path, false);
                                refreshVMDs = true;
                            }
                            else
                            {
                                failedLoadVMDs.Add(vmdName);
                            }
                        }

                        if (!failedLoadVMDs.Contains(vmdName))
                        {
                            MemoryDomains.VmdPool[domain].Visible = jsonString.DomainConfigSystem[systemCore].DomainConfig[domain].VISIBLE;
                            MemoryDomains.VmdPool[domain].AutoDomainSelect = jsonString.DomainConfigSystem[systemCore].DomainConfig[domain].AUTOSELECT;
                        }
                    }
                    else
                    {
                        if (MemoryDomains.MemoryInterfaces.ContainsKey(domain))
                        {
                            MemoryDomains.MemoryInterfaces[domain].Visible = jsonString.DomainConfigSystem[systemCore].DomainConfig[domain].VISIBLE;
                            MemoryDomains.MemoryInterfaces[domain].AutoDomainSelect = jsonString.DomainConfigSystem[systemCore].DomainConfig[domain].AUTOSELECT;
                        }
                    }
                }

                if (failedLoadVMDs.Count > 0)
                {
                    string missingVMDsString = "";
                    foreach (string vmd in failedLoadVMDs)
                    {
                        missingVMDsString += vmd + "\n";
                    }
                    string missingVMDsMessage = "The following VMD files could not be found while loading the domains config: \n\n" +
                                                      String.Join(Environment.NewLine, missingVMDsString + "\n" +
                                                      "They will not be loaded.");
                    MessageBox.Show(missingVMDsMessage, "Missing VMD files", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }

                if (refreshVMDs)
                    S.GET<VmdPoolForm>().RefreshVMDs();
            }

            Params.RemoveParam(systemCore + "_DOMAINS_CONFIG");
            Params.RemoveParam("VMD_DOMAINS_CONFIG");

            LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.UI, RTCV.NetCore.Commands.Remote.EventDomainsUpdated, new object[] { false }, true);

            UpdateDomainsList();
        }
    }
}
