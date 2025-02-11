namespace RTCV.UI.Components.Controls
{
    partial class Toast
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lbMessage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbChevron = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pbProgress);
            this.panel1.Controls.Add(this.lbMessage);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 60);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "color:dark1";
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(6, 40);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(206, 14);
            this.pbProgress.TabIndex = 118;
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbMessage.ForeColor = System.Drawing.Color.White;
            this.lbMessage.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbMessage.Location = new System.Drawing.Point(2, 22);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(134, 13);
            this.lbMessage.TabIndex = 117;
            this.lbMessage.Text = "Example notification text";
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel2.Controls.Add(this.lbChevron);
            this.panel2.Controls.Add(this.lbTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 20);
            this.panel2.TabIndex = 0;
            this.panel2.Tag = "color:dark3";
            // 
            // lbChevron
            // 
            this.lbChevron.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbChevron.Font = new System.Drawing.Font("Segoe UI Symbol", 11F);
            this.lbChevron.ForeColor = System.Drawing.Color.White;
            this.lbChevron.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbChevron.Location = new System.Drawing.Point(197, -2);
            this.lbChevron.Name = "lbChevron";
            this.lbChevron.Size = new System.Drawing.Size(21, 20);
            this.lbChevron.TabIndex = 119;
            this.lbChevron.Text = "v";
            this.lbChevron.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbChevron.Click += new System.EventHandler(this.lbChevron_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbTitle.Location = new System.Drawing.Point(2, 2);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(139, 15);
            this.lbTitle.TabIndex = 118;
            this.lbTitle.Text = "Example notification title";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Toast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.panel1);
            this.Name = "Toast";
            this.Size = new System.Drawing.Size(220, 62);
            this.Tag = "color:light1";
            this.Load += new System.EventHandler(this.Toast_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lbChevron;
    }
}
