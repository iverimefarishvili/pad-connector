using Sig.DeviceAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpecimenDotnetproject
{
    public partial class PadFileOperationForm : Form
    {
        private static string FilePath = null;
        public PadFileOperationForm()
        {
            InitializeComponent();
            ReadFilelist();
        }

        private void SelectFilePathBtn_Click(object sender, EventArgs e)
        {
            if (((Form1)Owner).openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath = ((Form1)Owner).openFileDialog1.FileName;

            }

        }

        private void ChooseFileOperationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

            RenameLabel.Visible = false;
            RenameTextBox.Visible = false;
            FileInfoListBox.SelectionMode = SelectionMode.One;

            switch (ChooseFileOperationCombobox.SelectedItem.ToString())
            {
                case "Copy file from pad"   :   FileInfoListBox.SelectionMode = SelectionMode.MultiExtended; break;
                case "Rename File on Pad"   :
                                                RenameLabel.Visible = true;
                                                RenameTextBox.Visible = true; 
                                                break;
                case "Delete file from pad" :   FileInfoListBox.SelectionMode = SelectionMode.MultiExtended; break;
                case "Upload File to Pad"   :
                                                RenameLabel.Visible = true;
                                                RenameTextBox.Visible = true; 
                                                break;
            }



        }

        private void ReadFilelist()
        {

            FileInfoListBox.Items.Clear();
            Error r = Form1.driverInterface.GetFileInfo(out List<PadFileInfo> filelist);

            if (r == Error.SUCCESS)
            {
                FileOperationStatusStrip.BackColor = Color.LightGreen;
                foreach (PadFileInfo file in filelist)
                    FileInfoListBox.Items.Add(file.Name);
            }
            else
                FileOperationStatusStrip.BackColor = Color.OrangeRed;

            FileOperationStatusStrip.Text = r.ToString();

        }

        private void ExecuteFileOperationButton_Click(object sender, EventArgs e)
        {
            Error r = Error.GENERAL_FAILURE;
            FileOperationStatusStrip.BackColor = Color.Gray;
            FileOperationStatusStrip.Text = "";
            switch (ChooseFileOperationCombobox.SelectedItem.ToString())
            {
                case "Read Pad Resources"   :   ReadFilelist(); break;

                case "Copy file from pad"   :
                                                if (FileInfoListBox.SelectedItem == null)
                                                {
                                                    MessageBox.Show("Please Select File To Be copied", " Warning");
                                                    break;
                                                }
                                                //if (((Form1)Owner).openFileDialog1.ShowDialog() == DialogResult.OK)
                                                if(((Form1)Owner).folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                                                {
                                                    //FilePath = ((Form1)Owner).openFileDialog1.FileName;
                                                    foreach (var list in FileInfoListBox.SelectedItems)
                                                    {
                                                        r = Form1.driverInterface.CopyFileFromDevice(list.ToString(), ((Form1)Owner).folderBrowserDialog1.SelectedPath + @"\");
                                                        if (r != Error.SUCCESS)
                                                            FileOperationStatusStrip.BackColor = Color.OrangeRed;
                                                        else
                                                            FileOperationStatusStrip.BackColor = Color.LightGreen;

                                                        FileOperationStatusStrip.Text = r.ToString();
                                                    }                     
                                                }
                                                break;

                case "Rename File on Pad"   :
                                                if (RenameTextBox.Text == "")
                                                {
                                                    MessageBox.Show("Please enter New Name", " Warning");
                                                    break;
                                                }
                                                r = Form1.driverInterface.RenameFileOnPad(FileInfoListBox.SelectedItem.ToString(), RenameTextBox.Text);
                                                if (r == Error.SUCCESS)
                                                    ReadFilelist();
                                                else
                                                {
                                                    FileOperationStatusStrip.BackColor = Color.OrangeRed;
                                                    FileOperationStatusStrip.Text = r.ToString();
                                                }
                                                break;
                case "Delete file from pad" :
                                                if (FileInfoListBox.SelectedItem == null)
                                                {
                                                    MessageBox.Show("Please Select File To Be Deleted", " Warning");
                                                    break;
                                                }
                                                r = Form1.driverInterface.DeleteFileFromPad(FileInfoListBox.SelectedItem.ToString());
                                                if (r == Error.SUCCESS)
                                                    ReadFilelist();
                                                else
                                                {
                                                    FileOperationStatusStrip.BackColor = Color.OrangeRed;
                                                    FileOperationStatusStrip.Text = r.ToString();
                                                }
                                                break;

                case "Upload File to Pad"   :
                                                if (RenameTextBox.Text == "")
                                                {
                                                    MessageBox.Show("Please enter Name to be saved on the pad", " Warning");
                                                    break;
                                                }
                                                if (((Form1)Owner).openFileDialog1.ShowDialog() == DialogResult.OK) 
                                                {
                                                    
                                                    r = Form1.driverInterface.CopyFileToDevice(((Form1)Owner).openFileDialog1.FileName, RenameTextBox.Text);
                                                    if (r == Error.SUCCESS)
                                                        ReadFilelist();
                                                    else
                                                    {
                                                        FileOperationStatusStrip.BackColor = Color.OrangeRed;
                                                        FileOperationStatusStrip.Text = r.ToString();
                                                    }
                                                }
                                                break;
            }

           
        }
    }
}
