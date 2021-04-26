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
    public partial class ButtonConfigForm : Form
    {
        Color button_90BackColor = Color.Blue;
        Color button_180BackColor = Color.Blue;
        Color button_nextBackColor = Color.Blue;
        Color button_prevBackColor = Color.Blue;
        Color button_OKBackColor = Color.Blue;
        Color button_cancelBackColor = Color.Blue;
        Color button_ZoomInBackColor = Color.Blue;
        Color button_ZoomOutBackColor = Color.Blue;
        Color button_RepeatBackColor = Color.Blue;
        Color button_StartBackColor = Color.Blue;
        Color button_UpBackColor = Color.Blue;
        Color button_DownBackColor = Color.Blue;
        public ButtonConfigForm()
        {
            InitializeComponent();
        }

        private void ButtonConfigForm_Load_1(object sender, EventArgs e)
        {

        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Enable_Down.Enabled = false;
            checkBox_Enable_Up.Enabled = false;
            checkBox_Enable_Start.Enabled = false;
            checkBox_Enable_Repeat.Enabled = false;
            checkBox_Enable_ZoomOut.Enabled = false;
            checkBox_Enable_ZoomIn.Enabled = false;
            checkBox_Enable_Cancel.Enabled = false;
            checkBox_Enable_OK.Enabled = false;
            checkBox_Enable_Prev.Enabled = false;
            checkBox_Enable_Next.Enabled = false;
            checkBox_Enable_Rotate180.Enabled = false;
            checkBox_Enable_Rotate90.Enabled = false;

            checkBox_Visible_Down.Enabled = false;
            checkBox_Visible_Up.Enabled = false;
            checkBox_Visible_Start.Enabled = false;
            checkBox_Visible_Repeat.Enabled = false;
            checkBox_Visible_ZoomOut.Enabled = false;
            checkBox_Visible_ZoomIn.Enabled = false;
            checkBox_Visible_Cancel.Enabled = false;
            checkBox_Visible_OK.Enabled = false;
            checkBox_Visible_Prev.Enabled = false;
            checkBox_Visible_Next.Enabled = false;
            checkBox_Visible_Rotate180.Enabled = false;
            checkBox_Visible_Rotate90.Enabled = false;


            


            button_90.Enabled = false;
            button_180.Enabled = false;
            button_next.Enabled = false;
            button_prev.Enabled = false;
            button_OK.Enabled = false;
            button_cancel.Enabled = false;
            button_ZoomIn.Enabled = false;
            button_ZoomOut.Enabled = false;
            button_Repeat.Enabled = false;
            button_Start.Enabled = false;
            button_Up.Enabled = false;
            button_Down.Enabled = false;

            button_90.BackColor = Color.Gray;
            button_180.BackColor = Color.Gray;
            button_next.BackColor = Color.Gray;
            button_prev.BackColor = Color.Gray;
            button_OK.BackColor = Color.Gray;
            button_cancel.BackColor = Color.Gray;
            button_ZoomIn.BackColor = Color.Gray;
            button_ZoomOut.BackColor = Color.Gray;
            button_Repeat.BackColor = Color.Gray;
            button_Start.BackColor = Color.Gray;
            button_Up.BackColor = Color.Gray;
            button_Down.BackColor = Color.Gray;

            if (radioButton_DocView.Checked)
            {
                checkBox_Visible_Rotate180.Enabled = true;
                checkBox_Visible_Rotate90.Enabled = true;
                checkBox_Visible_ZoomIn.Enabled = true;
                checkBox_Visible_ZoomOut.Enabled = true;                
                checkBox_Visible_Up.Enabled = true;
                checkBox_Visible_Down.Enabled = true;
                checkBox_Visible_Start.Enabled = true;

                checkBox_Enable_Rotate180.Enabled = true;
                checkBox_Enable_Rotate90.Enabled = true;
                checkBox_Enable_ZoomIn.Enabled = true;
                checkBox_Enable_ZoomOut.Enabled = true;
                checkBox_Enable_Up.Enabled = true;
                checkBox_Enable_Down.Enabled = true;
                checkBox_Enable_Start.Enabled = true;

                button_90.Enabled = true;
                button_180.Enabled = true;
                button_ZoomIn.Enabled = true;
                button_ZoomOut.Enabled = true;
                button_Start.Enabled = true;
                button_Up.Enabled = true;
                button_Down.Enabled = true;

                button_90.BackColor = button_90BackColor;
                button_180.BackColor = button_180BackColor;
                button_ZoomIn.BackColor = button_ZoomInBackColor;
                button_ZoomOut.BackColor = button_ZoomOutBackColor;
                button_Start.BackColor = button_StartBackColor;
                button_Up.BackColor = button_UpBackColor;
                button_Down.BackColor = button_DownBackColor;

            }
            else if( (radioButton_DocSign.Checked) || (radioButton_CustomSign.Checked) )
            {
                checkBox_Visible_Rotate180.Enabled = true;
                checkBox_Visible_Rotate90.Enabled = true;
                checkBox_Visible_Next.Enabled = true;
                checkBox_Visible_Prev.Enabled = true;
                checkBox_Visible_OK.Enabled = true;
                checkBox_Visible_Repeat.Enabled = true;
                checkBox_Visible_Cancel.Enabled = true;


                checkBox_Enable_Rotate180.Enabled = true;
                checkBox_Enable_Rotate90.Enabled = true;
                checkBox_Enable_Next.Enabled = true;
                checkBox_Enable_Prev.Enabled = true;
                checkBox_Enable_OK.Enabled = true;
                checkBox_Enable_Repeat.Enabled = true;
                checkBox_Enable_Cancel.Enabled = true;

                button_90.Enabled = true;
                button_180.Enabled = true;
                button_next.Enabled = true;
                button_prev.Enabled = true;
                button_OK.Enabled = true;
                button_cancel.Enabled = true;
                button_Repeat.Enabled = true;

                button_90.BackColor = button_90BackColor;
                button_180.BackColor = button_180BackColor;
                button_next.BackColor = button_nextBackColor;
                button_prev.BackColor = button_prevBackColor;
                button_OK.BackColor = button_OKBackColor;
                button_cancel.BackColor = button_cancelBackColor;
                button_Repeat.BackColor = button_RepeatBackColor;

            }
            else
            {
                checkBox_Enable_Down.Enabled = true;
                checkBox_Enable_Up.Enabled = true;
                checkBox_Enable_Start.Enabled = true;
                checkBox_Enable_Repeat.Enabled = true;
                checkBox_Enable_ZoomOut.Enabled = true;
                checkBox_Enable_ZoomIn.Enabled = true;
                checkBox_Enable_Cancel.Enabled = true;
                checkBox_Enable_OK.Enabled = true;
                checkBox_Enable_Prev.Enabled = true;
                checkBox_Enable_Next.Enabled = true;
                checkBox_Enable_Rotate180.Enabled = true;
                checkBox_Enable_Rotate90.Enabled = true;

                checkBox_Visible_Down.Enabled = true;
                checkBox_Visible_Up.Enabled = true;
                checkBox_Visible_Start.Enabled = true;
                checkBox_Visible_Repeat.Enabled = true;
                checkBox_Visible_ZoomOut.Enabled = true;
                checkBox_Visible_ZoomIn.Enabled = true;
                checkBox_Visible_Cancel.Enabled = true;
                checkBox_Visible_OK.Enabled = true;
                checkBox_Visible_Prev.Enabled = true;
                checkBox_Visible_Next.Enabled = true;
                checkBox_Visible_Rotate180.Enabled = true;
                checkBox_Visible_Rotate90.Enabled = true;

                button_90.Enabled = true;
                button_180.Enabled = true;
                button_next.Enabled = true;
                button_prev.Enabled = true;
                button_OK.Enabled = true;
                button_cancel.Enabled = true;
                button_ZoomIn.Enabled = true;
                button_ZoomOut.Enabled = true;
                button_Repeat.Enabled = true;
                button_Start.Enabled = true;
                button_Up.Enabled = true;
                button_Down.Enabled = true;

                button_90.BackColor = button_90BackColor;
                button_180.BackColor = button_180BackColor;
                button_next.BackColor = button_nextBackColor;
                button_prev.BackColor = button_prevBackColor;
                button_OK.BackColor = button_OKBackColor;
                button_cancel.BackColor = button_cancelBackColor;
                button_ZoomIn.BackColor = button_ZoomInBackColor;
                button_ZoomOut.BackColor = button_ZoomOutBackColor;
                button_Repeat.BackColor = button_RepeatBackColor;
                button_Start.BackColor = button_StartBackColor;
                button_Up.BackColor = button_UpBackColor;
                button_Down.BackColor = button_DownBackColor;

            }

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                btn.BackColor = colorDialog1.Color;

            button_90BackColor = button_90.BackColor;
            button_180BackColor = button_180.BackColor;
            button_nextBackColor = button_next.BackColor;
            button_prevBackColor = button_prev.BackColor;
            button_OKBackColor = button_OK.BackColor;
            button_cancelBackColor = button_cancel.BackColor;
            button_ZoomInBackColor = button_ZoomIn.BackColor;
            button_ZoomOutBackColor = button_ZoomOut.BackColor;
            button_RepeatBackColor = button_Repeat.BackColor;
            button_StartBackColor = button_Start.BackColor;
            button_UpBackColor = button_Up.BackColor;
            button_DownBackColor = button_Down.BackColor;

        }

        private void GetConfig_button_Click(object sender, EventArgs e)
        {
            Error error = Form1.driverInterface.GetButtonConfig(out ButtonBar buttonBar); // API 

            foreach (ButtonConfig buttonConfig in buttonBar.Configs.Values)
            {
                switch (buttonConfig.Kind)
                {
                    case ButtonKind.Rotate:
                        checkBox_Enable_Rotate180.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Rotate180.Checked = buttonConfig.Visible;
                        button_180.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.Rotate90:
                        checkBox_Enable_Rotate90.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Rotate90.Checked = buttonConfig.Visible;
                        button_90.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.Next:
                        checkBox_Enable_Next.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Next.Checked = buttonConfig.Visible;
                        button_next .BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.Prev:
                        checkBox_Enable_Prev.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Prev.Checked = buttonConfig.Visible;
                        button_prev.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.Ok:
                        checkBox_Enable_OK.Checked = buttonConfig.Enabled;
                        checkBox_Visible_OK.Checked = buttonConfig.Visible;
                        button_OK.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.Cancel:
                        checkBox_Enable_Cancel.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Cancel.Checked = buttonConfig.Visible;
                        button_cancel.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.ZoomIn:
                        checkBox_Enable_ZoomIn.Checked = buttonConfig.Enabled;
                        checkBox_Visible_ZoomIn.Checked = buttonConfig.Visible;
                        button_ZoomIn.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.ZoomOut:
                        checkBox_Enable_ZoomOut.Checked = buttonConfig.Enabled;
                        checkBox_Visible_ZoomOut.Checked = buttonConfig.Visible;
                        button_ZoomOut.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.Repeat:
                        checkBox_Enable_Repeat.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Repeat.Checked = buttonConfig.Visible;
                        button_Repeat.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                    case ButtonKind.StartSignature:
                        checkBox_Enable_Start.Checked = buttonConfig.Enabled;
                        checkBox_Visible_Start.Checked = buttonConfig.Visible;
                        button_Start.BackColor = Color.FromArgb(buttonConfig.R, buttonConfig.G, buttonConfig.B);
                        break;
                }
            }


            button_90BackColor = button_90.BackColor;
            button_180BackColor = button_180.BackColor;
            button_nextBackColor = button_next.BackColor;
            button_prevBackColor = button_prev.BackColor;
            button_OKBackColor = button_OK.BackColor;
            button_cancelBackColor = button_cancel.BackColor;
            button_ZoomInBackColor = button_ZoomIn.BackColor;
            button_ZoomOutBackColor = button_ZoomOut.BackColor;
            button_RepeatBackColor = button_Repeat.BackColor;
            button_StartBackColor = button_Start.BackColor;
            button_UpBackColor = button_Up.BackColor;
            button_DownBackColor = button_Down.BackColor;

        }

        private void SetConfig_button_Click(object sender, EventArgs e)
        {
            Error error = Form1.driverInterface.GetButtonConfig(out ButtonBar buttonBar);   // API
            foreach (ButtonConfig buttonConfig in buttonBar.Configs.Values)
            {
                Color color;
                switch (buttonConfig.Kind)
                {
                    case ButtonKind.Rotate:
                        buttonConfig.Enabled = checkBox_Enable_Rotate180.Checked;
                        buttonConfig.Visible = checkBox_Visible_Rotate180.Checked;
                        color = button_180.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.Rotate90:
                        buttonConfig.Enabled = checkBox_Enable_Rotate90.Checked;
                        buttonConfig.Visible = checkBox_Visible_Rotate90.Checked;
                        color = button_90.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.Next:
                        buttonConfig.Enabled = checkBox_Enable_Next.Checked;
                        buttonConfig.Visible = checkBox_Visible_Next.Checked;
                        color = button_next.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.Prev:
                        buttonConfig.Enabled = checkBox_Enable_Prev.Checked;
                        buttonConfig.Visible = checkBox_Visible_Prev.Checked;
                        color = button_prev.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.Ok:
                        buttonConfig.Enabled = checkBox_Enable_OK.Checked;
                        buttonConfig.Visible = checkBox_Visible_OK.Checked;
                        color = button_OK.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.Cancel:
                        buttonConfig.Enabled = checkBox_Enable_Cancel.Checked;
                        buttonConfig.Visible = checkBox_Visible_Cancel.Checked;
                        color = button_cancel.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.ZoomIn:
                        buttonConfig.Enabled = checkBox_Enable_ZoomIn.Checked;
                        buttonConfig.Visible = checkBox_Visible_ZoomIn.Checked;
                        color = button_ZoomIn.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.ZoomOut:
                        buttonConfig.Enabled = checkBox_Enable_ZoomOut.Checked;
                        buttonConfig.Visible = checkBox_Visible_ZoomOut.Checked;
                        color = button_ZoomOut.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.Repeat:
                        buttonConfig.Enabled = checkBox_Enable_Repeat.Checked;
                        buttonConfig.Visible = checkBox_Visible_Repeat.Checked;
                        color = button_Repeat.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                    case ButtonKind.StartSignature:
                        buttonConfig.Enabled = checkBox_Enable_Start.Checked;
                        buttonConfig.Visible = checkBox_Visible_Start.Checked;
                        color = button_Start.BackColor;
                        buttonConfig.FromArgb(color.A, color.R, color.G, color.B);
                        break;
                }
                error = Form1.driverInterface.SetButtonConfig(buttonBar);// API 
            }

        }
    }
}
