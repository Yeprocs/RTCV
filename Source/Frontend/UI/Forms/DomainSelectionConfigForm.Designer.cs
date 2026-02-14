namespace RTCV.UI
{
    partial class DomainSelectionConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomainSelectionConfigForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LoadFromFileButton = new System.Windows.Forms.Button();
            this.SaveToFileButton = new System.Windows.Forms.Button();
            this.resetToDefaultButton = new System.Windows.Forms.Button();
            this.gbUnitSource = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbUnitSource.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.LoadFromFileButton);
            this.panel1.Controls.Add(this.SaveToFileButton);
            this.panel1.Controls.Add(this.resetToDefaultButton);
            this.panel1.Controls.Add(this.gbUnitSource);
            this.panel1.Location = new System.Drawing.Point(8, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 513);
            this.panel1.TabIndex = 192;
            this.panel1.Tag = "color:normal";
            // 
            // LoadFromFileButton
            // 
            this.LoadFromFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.LoadFromFileButton.FlatAppearance.BorderSize = 0;
            this.LoadFromFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadFromFileButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.LoadFromFileButton.ForeColor = System.Drawing.Color.White;
            this.LoadFromFileButton.Location = new System.Drawing.Point(138, 474);
            this.LoadFromFileButton.Name = "LoadFromFileButton";
            this.LoadFromFileButton.Size = new System.Drawing.Size(122, 30);
            this.LoadFromFileButton.TabIndex = 163;
            this.LoadFromFileButton.TabStop = false;
            this.LoadFromFileButton.Tag = "color:light1";
            this.LoadFromFileButton.Text = "Load From File";
            this.LoadFromFileButton.UseVisualStyleBackColor = false;
            this.LoadFromFileButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LoadFromFileButton_MouseClick);
            // 
            // SaveToFileButton
            // 
            this.SaveToFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.SaveToFileButton.FlatAppearance.BorderSize = 0;
            this.SaveToFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveToFileButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.SaveToFileButton.ForeColor = System.Drawing.Color.White;
            this.SaveToFileButton.Location = new System.Drawing.Point(10, 474);
            this.SaveToFileButton.Name = "SaveToFileButton";
            this.SaveToFileButton.Size = new System.Drawing.Size(122, 30);
            this.SaveToFileButton.TabIndex = 162;
            this.SaveToFileButton.TabStop = false;
            this.SaveToFileButton.Tag = "color:light1";
            this.SaveToFileButton.Text = "Save to File";
            this.SaveToFileButton.UseVisualStyleBackColor = false;
            this.SaveToFileButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SaveToFileButton_MouseClick);
            // 
            // resetToDefaultButton
            // 
            this.resetToDefaultButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.resetToDefaultButton.FlatAppearance.BorderSize = 0;
            this.resetToDefaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetToDefaultButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.resetToDefaultButton.ForeColor = System.Drawing.Color.White;
            this.resetToDefaultButton.Location = new System.Drawing.Point(622, 474);
            this.resetToDefaultButton.Name = "resetToDefaultButton";
            this.resetToDefaultButton.Size = new System.Drawing.Size(122, 30);
            this.resetToDefaultButton.TabIndex = 151;
            this.resetToDefaultButton.TabStop = false;
            this.resetToDefaultButton.Tag = "color:light1";
            this.resetToDefaultButton.Text = "Reset to Default";
            this.resetToDefaultButton.UseVisualStyleBackColor = false;
            this.resetToDefaultButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.resetToDefaultButton_MouseClick);
            // 
            // gbUnitSource
            // 
            this.gbUnitSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUnitSource.BackColor = System.Drawing.Color.Transparent;
            this.gbUnitSource.Controls.Add(this.panel2);
            this.gbUnitSource.Controls.Add(this.tableLayoutPanel3);
            this.gbUnitSource.Font = new System.Drawing.Font("Segoe UI", 1F);
            this.gbUnitSource.ForeColor = System.Drawing.Color.White;
            this.gbUnitSource.Location = new System.Drawing.Point(4, 3);
            this.gbUnitSource.Name = "gbUnitSource";
            this.gbUnitSource.Size = new System.Drawing.Size(746, 465);
            this.gbUnitSource.TabIndex = 161;
            this.gbUnitSource.TabStop = false;
            this.gbUnitSource.Tag = "";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(6, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(735, 395);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(735, 52);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Tag = "color:dark2";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 214F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 8);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(734, 46);
            this.tableLayoutPanel3.TabIndex = 1;
            this.tableLayoutPanel3.Tag = "color:dark3";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(545, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 2, 17, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Set as Auto Domain";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(378, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Show in RTC";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(100, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Domain Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DomainSelectionConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(790, 537);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(806, 288);
            this.Name = "DomainSelectionConfigForm";
            this.Tag = "color:dark1";
            this.Text = "Domain Selection Config";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnShown);
            this.panel1.ResumeLayout(false);
            this.gbUnitSource.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbUnitSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button resetToDefaultButton;
        private System.Windows.Forms.Button LoadFromFileButton;
        private System.Windows.Forms.Button SaveToFileButton;
    }
}
