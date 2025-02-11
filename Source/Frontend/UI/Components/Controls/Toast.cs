using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using RTCV.CorruptCore;
using RTCV.NetCore;

namespace RTCV.UI.Components.Controls
{
    public partial class Toast : UserControl
    {
        private bool _collapsed;
        public Toast()
        {
            InitializeComponent();
        }
        public Toast(string title, string message)
        {
            InitializeComponent();
            this.lbTitle.Text = title;
            this.lbMessage.Text = message;
            this.lbChevron.Text = "\ue015"; // the designer can't handle funky unicode characters
            RtcCore.ProgressBarHandler += UpdateProgress;
            Colors.SetRTCColor(Colors.GeneralColor, this);
        }


        //why does winforms implement its own hand cursor? why??
        //https://superuser.com/a/1501044
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);
        
        private static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            // Note: force the cursor to own the handle so it gets released properly
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(curs, true);
            return curs;
        }

        private void Toast_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors", "Hand", null).ToString();
                var hand = LoadCustomCursor(path);
                this.lbChevron.Cursor = hand;
            }
            catch (Exception _)
            {
                this.lbChevron.Cursor = Cursors.Hand;
            }
        }
        
        private void UpdateProgress(object sender, ProgressBarEventArgs e)
        {
            SyncObjectSingleton.FormBeginExecute(() =>
            {
                this.lbMessage.Text = e.CurrentTask;
                if ((int)e.Progress > 100)
                {
                    e.Progress = 100;
                }

                this.pbProgress.Value = (int)e.Progress;
            });
        }
        
        public void Close()
        {
            this.Dispose();
        }

        private void lbChevron_Click(object sender, EventArgs e)
        {
            this._collapsed = !this._collapsed;
            if (this._collapsed)
            {
                this.Height = 22;
                this.lbChevron.Text = "\ue013"; // 
            }
            else
            {
                this.Height = 62;
                this.lbChevron.Text = "\ue015"; // 
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            RtcCore.ProgressBarHandler -= UpdateProgress;
            base.Dispose(disposing);
        }
    }
}
