namespace SpecimenDotnetproject
{
    partial class PromoScreenForm
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
            this.PromoScreen_listBox = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PromoScreentoolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UploadPromoScreen_button = new System.Windows.Forms.Button();
            this.DeletePromoscreen_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PromoScreenNO_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.PromoScreenDuration_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.PromoScreen_comboBox = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PromoScreenNO_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoScreenDuration_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // PromoScreen_listBox
            // 
            this.PromoScreen_listBox.FormattingEnabled = true;
            this.PromoScreen_listBox.ItemHeight = 15;
            this.PromoScreen_listBox.Location = new System.Drawing.Point(7, 6);
            this.PromoScreen_listBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PromoScreen_listBox.Name = "PromoScreen_listBox";
            this.PromoScreen_listBox.Size = new System.Drawing.Size(232, 124);
            this.PromoScreen_listBox.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PromoScreentoolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 303);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(246, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // PromoScreentoolStripStatusLabel
            // 
            this.PromoScreentoolStripStatusLabel.Name = "PromoScreentoolStripStatusLabel";
            this.PromoScreentoolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.PromoScreentoolStripStatusLabel.Text = "Status";
            // 
            // UploadPromoScreen_button
            // 
            this.UploadPromoScreen_button.Location = new System.Drawing.Point(6, 263);
            this.UploadPromoScreen_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UploadPromoScreen_button.Name = "UploadPromoScreen_button";
            this.UploadPromoScreen_button.Size = new System.Drawing.Size(232, 30);
            this.UploadPromoScreen_button.TabIndex = 2;
            this.UploadPromoScreen_button.Text = "Upload";
            this.UploadPromoScreen_button.UseVisualStyleBackColor = true;
            this.UploadPromoScreen_button.Click += new System.EventHandler(this.UploadPromoScreen_button_Click);
            // 
            // DeletePromoscreen_button
            // 
            this.DeletePromoscreen_button.Location = new System.Drawing.Point(6, 131);
            this.DeletePromoscreen_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeletePromoscreen_button.Name = "DeletePromoscreen_button";
            this.DeletePromoscreen_button.Size = new System.Drawing.Size(232, 30);
            this.DeletePromoscreen_button.TabIndex = 2;
            this.DeletePromoscreen_button.Text = "Delete";
            this.DeletePromoscreen_button.UseVisualStyleBackColor = true;
            this.DeletePromoscreen_button.Click += new System.EventHandler(this.DeletePromoscreen_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "PromoScreen No";
            // 
            // PromoScreenNO_numericUpDown
            // 
            this.PromoScreenNO_numericUpDown.Location = new System.Drawing.Point(125, 172);
            this.PromoScreenNO_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PromoScreenNO_numericUpDown.Name = "PromoScreenNO_numericUpDown";
            this.PromoScreenNO_numericUpDown.Size = new System.Drawing.Size(112, 23);
            this.PromoScreenNO_numericUpDown.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Duration(sec)";
            // 
            // PromoScreenDuration_numericUpDown
            // 
            this.PromoScreenDuration_numericUpDown.Location = new System.Drawing.Point(121, 202);
            this.PromoScreenDuration_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PromoScreenDuration_numericUpDown.Name = "PromoScreenDuration_numericUpDown";
            this.PromoScreenDuration_numericUpDown.Size = new System.Drawing.Size(116, 23);
            this.PromoScreenDuration_numericUpDown.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "File Orientation";
            // 
            // PromoScreen_comboBox
            // 
            this.PromoScreen_comboBox.FormattingEnabled = true;
            this.PromoScreen_comboBox.Location = new System.Drawing.Point(119, 232);
            this.PromoScreen_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PromoScreen_comboBox.Name = "PromoScreen_comboBox";
            this.PromoScreen_comboBox.Size = new System.Drawing.Size(117, 23);
            this.PromoScreen_comboBox.TabIndex = 8;
            // 
            // PromoScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 325);
            this.Controls.Add(this.PromoScreen_comboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PromoScreenDuration_numericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PromoScreenNO_numericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeletePromoscreen_button);
            this.Controls.Add(this.UploadPromoScreen_button);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.PromoScreen_listBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromoScreenForm";
            this.Text = "Promo Screen ";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PromoScreenNO_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoScreenDuration_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox PromoScreen_listBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button UploadPromoScreen_button;
        private System.Windows.Forms.Button DeletePromoscreen_button;
        private System.Windows.Forms.ToolStripStatusLabel PromoScreentoolStripStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PromoScreenNO_numericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PromoScreenDuration_numericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox PromoScreen_comboBox;
    }
}