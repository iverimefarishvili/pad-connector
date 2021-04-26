namespace SpecimenDotnetproject
{
    partial class PadFileOperationForm
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
            this.FileInfoListBox = new System.Windows.Forms.ListBox();
            this.ChooseFileOperationCombobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExecuteFileOperationButton = new System.Windows.Forms.Button();
            this.RenameLabel = new System.Windows.Forms.Label();
            this.RenameTextBox = new System.Windows.Forms.TextBox();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.FileOperationStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileInfoListBox
            // 
            this.FileInfoListBox.FormattingEnabled = true;
            this.FileInfoListBox.ItemHeight = 15;
            this.FileInfoListBox.Location = new System.Drawing.Point(11, 10);
            this.FileInfoListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FileInfoListBox.Name = "FileInfoListBox";
            this.FileInfoListBox.Size = new System.Drawing.Size(204, 109);
            this.FileInfoListBox.TabIndex = 0;
            // 
            // ChooseFileOperationCombobox
            // 
            this.ChooseFileOperationCombobox.FormattingEnabled = true;
            this.ChooseFileOperationCombobox.Items.AddRange(new object[] {
            "Read Pad Resources",
            "Copy file from pad",
            "Rename File on Pad",
            "Delete file from pad",
            "Upload File to Pad"});
            this.ChooseFileOperationCombobox.Location = new System.Drawing.Point(83, 126);
            this.ChooseFileOperationCombobox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChooseFileOperationCombobox.Name = "ChooseFileOperationCombobox";
            this.ChooseFileOperationCombobox.Size = new System.Drawing.Size(133, 23);
            this.ChooseFileOperationCombobox.TabIndex = 2;
            this.ChooseFileOperationCombobox.Text = "Read Pad Resources";
            this.ChooseFileOperationCombobox.SelectedIndexChanged += new System.EventHandler(this.ChooseFileOperationCombobox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select";
            // 
            // ExecuteFileOperationButton
            // 
            this.ExecuteFileOperationButton.Location = new System.Drawing.Point(15, 184);
            this.ExecuteFileOperationButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExecuteFileOperationButton.Name = "ExecuteFileOperationButton";
            this.ExecuteFileOperationButton.Size = new System.Drawing.Size(203, 28);
            this.ExecuteFileOperationButton.TabIndex = 4;
            this.ExecuteFileOperationButton.Text = "Execute";
            this.ExecuteFileOperationButton.UseVisualStyleBackColor = true;
            this.ExecuteFileOperationButton.Click += new System.EventHandler(this.ExecuteFileOperationButton_Click);
            // 
            // RenameLabel
            // 
            this.RenameLabel.AutoSize = true;
            this.RenameLabel.Location = new System.Drawing.Point(9, 156);
            this.RenameLabel.Name = "RenameLabel";
            this.RenameLabel.Size = new System.Drawing.Size(66, 15);
            this.RenameLabel.TabIndex = 5;
            this.RenameLabel.Text = "New Name";
            this.RenameLabel.Visible = false;
            // 
            // RenameTextBox
            // 
            this.RenameTextBox.Location = new System.Drawing.Point(83, 154);
            this.RenameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RenameTextBox.Name = "RenameTextBox";
            this.RenameTextBox.Size = new System.Drawing.Size(132, 23);
            this.RenameTextBox.TabIndex = 6;
            this.RenameTextBox.Visible = false;
            // 
            // StatusStrip
            // 
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileOperationStatusStrip});
            this.StatusStrip.Location = new System.Drawing.Point(0, 222);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.StatusStrip.Size = new System.Drawing.Size(225, 22);
            this.StatusStrip.TabIndex = 7;
            // 
            // FileOperationStatusStrip
            // 
            this.FileOperationStatusStrip.Name = "FileOperationStatusStrip";
            this.FileOperationStatusStrip.Size = new System.Drawing.Size(63, 17);
            this.FileOperationStatusStrip.Text = "Status Text";
            // 
            // PadFileOperationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 244);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.RenameTextBox);
            this.Controls.Add(this.RenameLabel);
            this.Controls.Add(this.ExecuteFileOperationButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChooseFileOperationCombobox);
            this.Controls.Add(this.FileInfoListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PadFileOperationForm";
            this.Text = "Pad File Operation";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FileInfoListBox;
        private System.Windows.Forms.ComboBox ChooseFileOperationCombobox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExecuteFileOperationButton;
        private System.Windows.Forms.Label RenameLabel;
        private System.Windows.Forms.TextBox RenameTextBox;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel FileOperationStatusStrip;
    }
}