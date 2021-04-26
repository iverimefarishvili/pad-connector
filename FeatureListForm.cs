using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SpecimenDotnetproject
{
    public partial class FeatureListForm : Form
    {
        public FeatureListForm()
        {
            InitializeComponent();
            ColorDeviceSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.ColorDeviceSupport.ToString(); // API to read Device Properties
            PortraitModeSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.PortraitModeSupport.ToString();
            EMPenSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.EmpenSupport.ToString();
            EMPenUpdateSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.EmpenUpdateSupport.ToString();
            ConfigPromoScreenDelaySupport_label.Text = Form1.driverInterface.DeviceProperties.Features.ConfigPromoscreenDelaySupport.ToString();
            SimpleDialogSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.SimpleDialogSupport.ToString();
            ContinuousScrollSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.ContinuousScrollingSupport.ToString();
            SOIFormatSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.SOIFormatSupport.ToString();
            VCOMSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.VCOMSupport.ToString();
            TouchConfigSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.TouchConfigSupport.ToString();
            EnhancedCryptoIDSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.EnhancedCryptoIdSupport.ToString();
            OpenStateDetectionSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.OpenStateDetectionSupport.ToString();
            BrightnessChangeSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.BrightnessChangeSupport.ToString();
            InternalClockSupport_label.Text = Form1.driverInterface.DeviceProperties.Features.InternalCLKSupport.ToString();
        }
    }
}
