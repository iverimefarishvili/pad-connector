using Sig.DeviceAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SpecimenDotnetproject
{
    public partial class PromoScreenForm : Form
    {
        public PromoScreenForm()
        {
            InitializeComponent();
            PromoScreenNO_numericUpDown.Minimum = 1;
            PromoScreenNO_numericUpDown.Maximum = 10;
            PromoScreenDuration_numericUpDown.Minimum = 5;
            PromoScreenDuration_numericUpDown.Maximum = 20 * 60 * 1000;

            PromoScreen_comboBox.Items.Clear();
            PromoScreen_comboBox.Items.Add(DisplayOrientation.DEG_0);
            PromoScreen_comboBox.Items.Add(DisplayOrientation.DEG_180);
            PromoScreen_comboBox.Items.Add(DisplayOrientation.DEG_90);
            PromoScreen_comboBox.Items.Add(DisplayOrientation.DEG_270);
            ReadPromoscreenList();


        }

        private void DeletePromoscreen_button_Click(object sender, EventArgs e)
        {
            Error r = Form1.driverInterface.RemovePromoscreen((int)PromoScreenNO_numericUpDown.Value, (DisplayOrientation)Enum.Parse(typeof(DisplayOrientation), PromoScreen_comboBox.Text)); // API to Delete the promoscreen
            if (r == Error.SUCCESS)
                ReadPromoscreenList();
            else
            {
                PromoScreentoolStripStatusLabel.BackColor = Color.OrangeRed;
                PromoScreentoolStripStatusLabel.Text = r.ToString();
            }
            
        }

        private void ReadPromoscreenList()
        {
            PromoScreen_listBox.Items.Clear();
            Error r = Form1.driverInterface.GetFileInfo(out List<PadFileInfo> filelist);  // API to GetFileInfo

            if (r == Error.SUCCESS)
            {
                PromoScreentoolStripStatusLabel.BackColor = Color.LightGreen;
                foreach (PadFileInfo file in filelist)
                    if (file.Name.Contains("PromoScreen"))
                        PromoScreen_listBox.Items.Add(file.Name);
            }
            else
                PromoScreentoolStripStatusLabel.BackColor = Color.OrangeRed;

            PromoScreentoolStripStatusLabel.Text = r.ToString();

        }
        private void UploadPromoScreen_button_Click(object sender, EventArgs e)
        {
            if (((Form1)Owner).openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Error r = Form1.driverInterface.UploadPromoScreen(((Form1)Owner).openFileDialog1.FileName, (int)PromoScreenNO_numericUpDown.Value, (DisplayOrientation)Enum.Parse(typeof(DisplayOrientation), PromoScreen_comboBox.Text),  (int)PromoScreenDuration_numericUpDown.Value); // API for Uploading PromoScreen
                if (r == Error.SUCCESS)
                {
                    ReadPromoscreenList();

                }
                else
                {
                    PromoScreentoolStripStatusLabel.BackColor = Color.OrangeRed;
                    PromoScreentoolStripStatusLabel.Text = r.ToString();
                }
            }

        }
    }
}
