namespace RTCV.UI.Components.Controls
{
    partial class Range
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbMin = new RTCV.UI.Components.Controls.HexTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMax = new RTCV.UI.Components.Controls.HexTextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMin
            // 
            this.tbMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbMin.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMin.ForeColor = System.Drawing.Color.White;
            this.tbMin.Location = new System.Drawing.Point(37, 1);
            this.tbMin.MaxLength = 16;
            this.tbMin.Name = "tbMin";
            this.tbMin.Nullable = true;
            this.tbMin.Size = new System.Drawing.Size(100, 20);
            this.tbMin.TabIndex = 1;
            this.tbMin.Tag = "color:dark1";
            this.tbMin.Text = "FFFFFFFFFFFFFFFF";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 145;
            this.label9.Text = "From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(138, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 146;
            this.label1.Text = "to";
            // 
            // tbMax
            // 
            this.tbMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbMax.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMax.ForeColor = System.Drawing.Color.White;
            this.tbMax.Location = new System.Drawing.Point(155, 1);
            this.tbMax.MaxLength = 16;
            this.tbMax.Name = "tbMax";
            this.tbMax.Nullable = true;
            this.tbMax.Size = new System.Drawing.Size(100, 20);
            this.tbMax.TabIndex = 147;
            this.tbMax.Tag = "color:dark1";
            this.tbMax.Text = "FFFFFFFFFFFFFFFF";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnDelete.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnDelete.Location = new System.Drawing.Point(260, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(21, 20);
            this.btnDelete.TabIndex = 182;
            this.btnDelete.TabStop = false;
            this.btnDelete.Tag = "color:dark2";
            this.btnDelete.Text = "X";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // Range
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbMin);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Range";
            this.Size = new System.Drawing.Size(287, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HexTextBox tbMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private HexTextBox tbMax;
        private System.Windows.Forms.Button btnDelete;
    }
}
