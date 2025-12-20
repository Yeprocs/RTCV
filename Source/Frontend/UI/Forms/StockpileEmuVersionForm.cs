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

namespace RTCV.UI
{
    public partial class StockpileEmuVersionForm : ColorizedForm
    {
        public string SelectedVersion;
        public StockpileEmuVersionForm(bool detected = true)
        {
            InitializeComponent();

            string detectedText = "RTC has detected this stockpile does not have an associated emulator system and version. Please select the system and version this stockpile was created in.";
            string defaultText = "Please select the system and version the selected stockpile items were created in.";


            lbText.Text = detected ? detectedText : defaultText;

            var directories = Directory.GetDirectories(new DirectoryInfo(RtcCore.RtcDir).Parent.Parent.FullName);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedVersion = (string)cbEmuVersion.SelectedItem;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
