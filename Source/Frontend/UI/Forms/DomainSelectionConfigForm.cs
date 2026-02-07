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

    public partial class DomainSelectionConfigForm : Modular.ColorizedForm
    {
        public DomainSelectionConfigForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
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

                tableLayoutPanel1.Controls.Add(new Label { Text = domain.Key, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 14), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right , Padding = new Padding(3) }, 0, i);

                var showInRtcCheckbox = new CheckBox { Checked = domains[domain.Key].Visible, CheckAlign = ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, Padding = new Padding(3) };
                showInRtcCheckbox.Click += new System.EventHandler((sender, e) => OnShowInRtcCheckbox(sender, e, domain.Key));
                tableLayoutPanel1.Controls.Add(showInRtcCheckbox, 1, i);

                var autoDomainCheckbox = new CheckBox { Checked = domains[domain.Key].AutoDomainSelect, CheckAlign = ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, Padding = new Padding(3) };
                autoDomainCheckbox.Click += new System.EventHandler((sender, e) => OnAutoDomainCheckbox(sender, e, domain.Key));
                tableLayoutPanel1.Controls.Add(autoDomainCheckbox, 2, i);
                
                i++;
            }
            
            tableLayoutPanel1.ResumeLayout();
            tableLayoutPanel1.Visible = true;
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
    }
}
