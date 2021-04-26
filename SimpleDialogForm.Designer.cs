namespace SpecimenDotnetproject
{
#if SDFRAMEWORK_WAS_REMOVED
    partial class SimpleDialogForm
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
            this.Initialize_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Initialize_button
            // 
            this.Initialize_button.Location = new System.Drawing.Point(24, 19);
            this.Initialize_button.Name = "Initialize_button";
            this.Initialize_button.Size = new System.Drawing.Size(143, 116);
            this.Initialize_button.TabIndex = 0;
            this.Initialize_button.Text = "Initialize";
            this.Initialize_button.UseVisualStyleBackColor = true;
            this.Initialize_button.Click += new System.EventHandler(this.Initialize_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(197, 17);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(143, 116);
            this.Close_button.TabIndex = 0;
            this.Close_button.Text = "Close";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // SimpleDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 154);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Initialize_button);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimpleDialogForm";
            this.Text = "SimpleDialogForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleDialogForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Initialize_button;
        private System.Windows.Forms.Button Close_button;
    }
#endif
}