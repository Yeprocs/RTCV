using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTCV.UI.Modular;
using RTCV.Common;

namespace RTCV.UI
{
    public partial class UpdateEmuVersionForm : ColorizedForm
    {
        private string defaultString = "--select--";

        public string SelectedVersion;
        public UpdateEmuVersionForm(bool detected = true)
        {
            InitializeComponent();

            TopLevel = true;
            TopMost = true;

            string detectedText = "RTC has detected some entries in this file do not have an associated emulator system and version. Please select the system and version the entries were created in.";
            string defaultText = "Please select the system and version the selected files were created in.";


            lbText.Text = detected ? detectedText : defaultText;

            var directories = Directory.GetDirectories(new DirectoryInfo(RtcCore.RtcDir).Parent.Parent.FullName);

            cbEmuVersion.Items.Add("--select--");
            cbEmuVersion.SelectedIndex = 0;

            btnOk.Enabled = false;

            foreach(var dir in directories)
            {
                var dirName = new DirectoryInfo(dir).Name;
                if (dirName != "Launcher" && dirName != "RTCV")
                { 
                    cbEmuVersion.Items.Add(dirName);
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            cbEmuVersion.DroppedDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((string)cbEmuVersion.SelectedItem != defaultString && (string)cbEmuVersion.SelectedItem != null)
                SelectedVersion = (string)cbEmuVersion.SelectedItem;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void UpdateEmuVersionForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void cbEmuVersion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnOk.Enabled = cbEmuVersion.SelectedIndex == 0 ? false : true;
        }
    }
}
