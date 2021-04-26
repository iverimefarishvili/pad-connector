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
    public partial class DeviceListForm : Form
    {
        public static FilterDeviceKind devfilter = default;
        public DeviceListForm()
        {
            InitializeComponent();

            Error r = Form1.driverInterface.EnumerateDevices(out string[] pads, out int count, devfilter);
            foreach (string pad in pads)
            {
                listBox1.Items.Add(pad);
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
