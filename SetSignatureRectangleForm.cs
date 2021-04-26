using Sig.DeviceAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpecimenDotnetproject
{
    public partial class SetSignatureRectangleForm : Form
    {
        public SetSignatureRectangleForm()
        {
            InitializeComponent();
            SetSignatureOption_combobx.Items.Add(SignRectOption.ScrollXY);
            SetSignatureOption_combobx.Items.Add(SignRectOption.ScrollXCenterY);
            SetSignatureOption_combobx.Items.Add(SignRectOption.ScrollYCenterX);
            SetSignatureOption_combobx.Items.Add(SignRectOption.CenterXY);
            SetSignatureOption_combobx.Text = SetSignatureOption_combobx.Items[0].ToString();

            SetSignature_PosX_numericUpDown.Maximum = 10000;
            SetSignature_PosY_numericUpDown.Maximum = 10000;
            SetSignature_Width_numericUpDown.Maximum = 10000;
            SetSignature_Height_numericUpDown4.Maximum = 10000;
            SetSignature_Width_numericUpDown.Value = 200;
            SetSignature_Height_numericUpDown4.Value = 300;

            SetSignatureExtended_PosX_numericUpDown.Maximum = 10000;
            SetSignatureExtended_PosY_numericUpDown.Maximum = 10000;
            SetSignatureExtended_PosY_numericUpDown.Value = 600;
            string[] lines = File.ReadAllLines("Settings.ini");
            int i = 0;
            for (; i< lines.Length; i++)
            {
                if (lines[i].Contains("SetSignRect"))
                    break;
            }
            if(i< lines.Length)
            {
                string[] SetSignRect = lines[i].Split('=');
                string[] values = SetSignRect[1].Split(',');
                
                SetSignature_PosX_numericUpDown.Value = int.Parse(values[0], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                SetSignature_PosX_numericUpDown.Value = int.Parse(values[1], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                SetSignature_Width_numericUpDown.Value = int.Parse(values[2], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                SetSignature_Height_numericUpDown4.Value = int.Parse(values[3], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                SetSignatureExtended_Enable_checkBox.Checked = Convert.ToBoolean(values[4]);
                SetSignatureExtended_PosX_numericUpDown.Value = int.Parse(values[5], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                SetSignatureExtended_PosY_numericUpDown.Value = int.Parse(values[6], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                 
                SetSignatureOption_combobx.Text = values[7];


            }
            else
            {
                string SetSignRect = "SetSignRect =";
                SetSignRect += SetSignature_PosX_numericUpDown.Value.ToString() + ',';
                SetSignRect += SetSignature_PosY_numericUpDown.Value.ToString() + ',';
                SetSignRect += SetSignature_Width_numericUpDown.Value.ToString() + ',';
                SetSignRect += SetSignature_Height_numericUpDown4.Value.ToString() + ',';
                SetSignRect += SetSignatureExtended_Enable_checkBox.Checked.ToString() + ',';
                SetSignRect += SetSignatureExtended_PosX_numericUpDown.Value.ToString() + ',';
                SetSignRect += SetSignatureExtended_PosY_numericUpDown.Value.ToString() + ',';
                SetSignRect += SetSignatureOption_combobx.Text;

                using (FileStream fs = File.Create("Settings.ini"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(string.Join("\n", lines) + "\n" + SetSignRect + "\n");
                    fs.Write(info, 0, info.Length);
                }
            }


        }

        private void SetSignatureRectangleForm_Load(object sender, EventArgs e)
        {

        }

        private void SetSignatureExtended_Enable_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SetSignatureExtended_Enable_checkBox.Checked)
            {
                SetSignatureExtended_PosX_numericUpDown.Enabled = true;
                SetSignatureExtended_PosY_numericUpDown.Enabled = true;
                SetSignatureOption_combobx.Enabled = true;
            }
            else
            {
                SetSignatureExtended_PosX_numericUpDown.Enabled = false;
                SetSignatureExtended_PosY_numericUpDown.Enabled = false;
                SetSignatureOption_combobx.Enabled = false;
            }
        }

        private void SetSignatureCancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetsignatureRectangleOk_button_Click(object sender, EventArgs e)
        {
            Error r = Error.SUCCESS;
            if (SetSignatureExtended_Enable_checkBox.Checked)
            {
                r = Form1.driverInterface.SetSignatureRectangle((int)SetSignature_PosX_numericUpDown.Value, (int)SetSignature_PosX_numericUpDown.Value, (int)SetSignature_Width_numericUpDown.Value, (int)SetSignature_Height_numericUpDown4.Value, (int)SetSignatureExtended_PosX_numericUpDown.Value, (int)SetSignatureExtended_PosY_numericUpDown.Value, (SignRectOption)SetSignatureOption_combobx.SelectedItem); // API for Extended mode Set Sign Rectangle

            }
            else
            {
                r = Form1.driverInterface.SetSignatureRectangle((int)SetSignature_PosX_numericUpDown.Value, (int)SetSignature_PosX_numericUpDown.Value, (int)SetSignature_Width_numericUpDown.Value, (int)SetSignature_Height_numericUpDown4.Value); // API for Normal Sign Rectangle
            }
            if (r != Error.SUCCESS)
                MessageBox.Show(r.ToString(), "Warning");
            else
                Form1.SignrectSet = true;

            string SetSignRect = "SetSignRect =";
            SetSignRect += SetSignature_PosX_numericUpDown.Value.ToString() + ',';
            SetSignRect += SetSignature_PosY_numericUpDown.Value.ToString() + ',';
            SetSignRect += SetSignature_Width_numericUpDown.Value.ToString() + ',';
            SetSignRect += SetSignature_Height_numericUpDown4.Value.ToString() + ',';
            SetSignRect += SetSignatureExtended_Enable_checkBox.Checked.ToString() + ',';
            SetSignRect += SetSignatureExtended_PosX_numericUpDown.Value.ToString() + ',';
            SetSignRect += SetSignatureExtended_PosY_numericUpDown.Value.ToString() + ',';
            SetSignRect += SetSignatureOption_combobx.Text;
            string[] lines = File.ReadAllLines("Settings.ini");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("SetSignRect"))
                    lines[i] = SetSignRect;
            }
            using (FileStream fs = File.Create("Settings.ini"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(string.Join("\n", lines) + "\n");
                fs.Write(info, 0, info.Length);
            }


            this.Close();
            
        }
    }
}
