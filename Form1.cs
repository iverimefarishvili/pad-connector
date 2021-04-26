using Sig.DeviceAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using System.Resources;
using System.Globalization;

using SuperWebSocket;
using System.Drawing.Imaging;

namespace SpecimenDotnetproject
{
    public partial class Form1 : Form
    {
        string DocPath = null;
        string LicPath = null;
        public static bool SignrectSet = false;
        int Page = 0;
        int tilefillX, tilefillY;
        int tileWidth, tileHeight;
        int tilesendX, tilesendY;
        int tilesignX, tilesignY;
        int tileviewX, tileviewY;
        bool tilefillPenDown = false;
        bool tilesendPenDown = false;
        bool tilesignPenDown = false;
        bool tileviewPenDown = false;
        bool HandlesignPagePenDown = false;
        bool HandleViewPagePenDown = false;

        private static WebSocketServer wsServer;
        private static WebSocketSession clientSession;



        public static IDriver driverInterface = new Driver();  // Driver Instantiation
#if SDFRAMEWORK_WAS_REMOVED
        public SimpleDialogForm Sdfwform = new SimpleDialogForm();
#endif

        private static void WsServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Debug.WriteLine("SessionClosed");
        }

        private static void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            Debug.WriteLine("NewDataReceived");
        }

        private static void WsServer_NewMessageReceived(WebSocketSession session, string value)
        {
            Debug.WriteLine("NewMessageReceived: " + value);
            clientSession = session;
            if (value == "Hello server")
            {
                session.Send("Hello client");
            }
        }

        private static void WsServer_NewSessionConnected(WebSocketSession session)
        {
            Debug.WriteLine("NewSessionConnected");
        }

        public Form1()
        {

            wsServer = new WebSocketServer();
            int port = 8181;
            wsServer.Setup(port);
            /*wsServer.NewSessionConnected += WsServer_NewSessionConnected;*/
            wsServer.NewMessageReceived += WsServer_NewMessageReceived;
            wsServer.NewDataReceived += WsServer_NewDataReceived;
            wsServer.SessionClosed += WsServer_SessionClosed;
            wsServer.Start();
            Debug.WriteLine("Server is running on port " + port + ". Press ENTER to exit....");

            InitializeComponent();


            //---------------- Application sepcific  to store user setting can be ignored ----------------------------------
            if (!File.Exists("Settings.ini"))
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create("Settings.ini"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is a Settings.ini file for Specimen Application");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

            }
            //---------------------------------------------------------------------------------------------------------------
            SubscribeToEvents(); // Subscribing to events such as ButtonEvent,SignStarted, SignFinished,SignImgChanged, PenEvent, ScrollEvent
        }

        private void SubscribeToEvents()
        {
            /*
             * This is ButtonEvent event, it is triggered when a user clicks on a button on the pad in any of the following modes mentioned below
             *  Document view, Document sign, Standard Sign, Signature Confirm.
             *  
             *  In order to Handle this event a user defined function/method can be associated to this event. In this case a user defined function
             *  private void Handlebuttonevent(ButtonEventArgs args) is associated with this event.
             *  In its simplest form it can be called as below.
             *  driverInterface.ButtonEvent += (object sender, ButtonEventArgs args) => Handlebuttonevent(args);
             *  
             *  However here it is updating an Application specific status bar ta the bottom before the user dfeined application is called
             */
            driverInterface.ButtonEvent += (object sender, ButtonEventArgs args) =>
            {
                toolStripStatusButtonorsignEvent.Text = "Button event:" + args.ButtonKind;
                Handlebuttonevent(args);        // User defined function
            };

            /*
             * This event is triggered when a pendown event occurs with in the signature rectangle on the device which is Signature mode or Document signature mode
             * Here it is just updating the Application specific status bar below
             */
            driverInterface.SignStarted += (object sender, EventArgs args) =>
            {
                toolStripStatusButtonorsignEvent.Text = "Signature started event triggered";
            };

            /*
             * This event is triggered after a user defined timeout after a last penup has occured at the end of signing process by the user when the device which is Signature mode or Document signature mode
             * Here it is just updating the Application specific status bar below and enabling a button and then calling StopSignatureCapture() function, which then disables all pen events on pad
             */
            driverInterface.SignFinished += (object sender, EventArgs args) =>
            {
#if SDFRAMEWORK_WAS_REMOVED
                if (driverInterface.DeviceProperties.Status.Mode == DeviceMode.SimpleDialogSign)
                    Sdfwform.SimpleSignFinished(sender, args);
                        else
                        {
                            toolStripStatusButtonorsignEvent.Text = "Signature finished event triggered";
                            button_StartSignature.Invoke((MethodInvoker)(() => button_StartSignature.Enabled = true));
                            driverInterface.StopSignatureCapture();  // API Function (if required by  the user can disable further pen events)
                        }
#endif
            };

            /*
             *  This event is triggered as and when there is movement of a pen or anydrawing on the defined sign rectangle after SignStarted event.
             *  Here it is associated with a user defined function/method void UpdateSignatureImage(), this updates the signature image on the application which has a picturebox
             */
            driverInterface.SignImgChanged += (object sender, EventArgs args) =>
            {
                imgSignature.Invoke((MethodInvoker)(() => UpdateSignatureImage()));
            };


            /*
             * This is an event specifically meant for simpledialog mode. when the pad is Simpledialog mode and 
             * when penup and pendown event has occured on the screen this event is triggered, it gives the X, Y location along with a penup or pendown event
             * Note : this does not get triggered when the pad is in simple dialog sign mode and the pen is with in Defined sign rectangle
             * Here is associated with user defined function  called UpdateSimpleDialogPenEvent(sender, args)
             */
            driverInterface.PenEvent += (object sender, PenEventArgs args) =>
            {
                UpdateSimpleDialogPenEvent(args);
            };


            /*
             * This event is specifically meant for continuous scroll event. It is triggered when ever there is new tile request or new page no request
             * if page no is requested the the corresponding tile no is ent via SetContinuousScrollTileNumber() function
             * if Tile no is requested appropriate tile no is requested via UploadContinuousScrollTile() function
             */
            driverInterface.ScrollEvent += (object sender, Sig.DeviceAPI.ScrollEventArgs e) =>
            {
                int pageNumber, tileNumber;
                if (e.IsPageNumber)
                {
                    pageNumber = e.RequestedNumber;
                    tileNumber = 2 * pageNumber;
                    driverInterface.SetContinuousScrollTileNumber(tileNumber, 132, 0);
                    return;
                }
                else
                {
                    tileNumber = e.RequestedNumber;
                    pageNumber = (tileNumber + tileNumber % 2) / 2;
                }

                Bitmap bitmap = new Bitmap(folderBrowserDialog1.SelectedPath + @"\" + tileNumber.ToString() + ".bmp");
                driverInterface.UploadContinuousScrollTile(bitmap, tileNumber, pageNumber, 132);
            };

            /*
             * The next sequence of events is searching for device and setting the device. Please look at the function "SetDeviceMenuItem_Click(object sender, EventArgs e)"
             */


#if SDFRAMEWORK_WAS_REMOVED
            driverInterface.SimpleDialogFrameworkEvent += Sdfwform.HandleSDialogFrameWorkEvents;
#endif

        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DocPath = openFileDialog1.FileName;
            }
        }

        private void PadFileOperationMenuItem_Click(object sender, EventArgs e)
        {
            PadFileOperationForm PadFileOpForm = new PadFileOperationForm();
            //PadFileOpForm.ParentForm => this;
            PadFileOpForm.StartPosition = FormStartPosition.Manual;
            PadFileOpForm.Left = this.Left;
            PadFileOpForm.Top = this.Top;
            PadFileOpForm.ShowDialog(this);
        }

        private void SetDeviceMenuItem_Click(object sender, EventArgs e)
        {
            string devname = null;
            //--------------------------------------------------------------------------------------------------------------------------------
            /*
             * This application specific code to get the user selected device filter, must be Selected from one of FilterDeviceKind
             */
            ToolStripMenuItem DevFilterMenustrip;
            for (int i = 0; i < DeviceSearchFilterMenuItem.DropDownItems.Count; i++)
            {
                DevFilterMenustrip = (ToolStripMenuItem)DeviceSearchFilterMenuItem.DropDownItems[i];
                if (DevFilterMenustrip.Checked)
                    devname = DeviceSearchFilterMenuItem.DropDownItems[i].Text;

            }
            DeviceListForm.devfilter = (FilterDeviceKind)Enum.Parse(typeof(FilterDeviceKind), devname);

            // DeviceListForm.devfilter is user selected device type to search
            //------------------------------------------------------------------------------------------------------------------------------------


            //--------------------------------------------------------------------------------------------------------------------------------------
            /*
             * This is part of the code is used for searching devices and establishing connection using another form with list box
             * 
             * 
             * PLEASE CHECK THE CONSTRUCTOR of  DeviceListForm.cs  where the device serach function is called using the function below
             *  Error r = Form1.driverInterface.EnumerateDevices(out string[] pads, out int count, devfilter);
             *  
             *  This will populate a listbox on the form of all the devices found and when the user doubleclicks on the specific device then that form closes 
             *  and selected index is sent
             *   The connection is established with user selected device using the function 
             *   Error r = driverInterface.SetDevice(ListDevices.listBox1.SelectedItem.ToString());
             *   and the status is updated accordingly
             */
            DeviceListForm ListDevices = new DeviceListForm();
            ListDevices.SuspendLayout();
            ListDevices.Text = "Device List";
            ListDevices.StartPosition = FormStartPosition.Manual;
            ListDevices.Left = this.Left;
            ListDevices.Top = this.Top;
            ListDevices.ResumeLayout();
            ListDevices.ShowDialog();

            if (ListDevices.listBox1.SelectedItem != null)
            {
                Error r = driverInterface.SetDevice(ListDevices.listBox1.SelectedItem.ToString()); // API Function - This is how the connection is established with the pad
                if (r == Error.SUCCESS)
                {
                    toolStripStatusDeviceName.Text = ListDevices.listBox1.SelectedItem.ToString();
                    toolStripStatusDeviceName.BackColor = Color.LightGreen;
                }
                else
                {
                    toolStripStatusDeviceName.Text = r.ToString();
                    toolStripStatusDeviceName.BackColor = Color.Red;
                }

                for (int i = 0; i < PadOrientationMenuItem.DropDownItems.Count; i++)
                {
                    DevFilterMenustrip = (ToolStripMenuItem)PadOrientationMenuItem.DropDownItems[i];
                    DevFilterMenustrip.Checked = false;

                }
                //---------------------------------------------------------------------------------------------------------------------------


                //-----------------------------------------------------------------------------------------------------------------------------
                /*
                 * The below code is for reading all avalible cryptoid on the pad and populating a combo box
                 * This is a property of the device and can be read as below, it returns an xml string.
                 * string cryptoidstring = driverInterface.DeviceProperties.CryptoIDInfo;
                 */

                DevFilterMenustrip = (ToolStripMenuItem)PadOrientationMenuItem.DropDownItems[(int)driverInterface.DeviceProperties.Status.DisplayOrientation]; // Application specific
                DevFilterMenustrip.Checked = true;

                CryptoIdComboBox.Items.Clear();
                XmlDocument xmltest = new XmlDocument();
                string cryptoIdString = driverInterface.DeviceProperties.CryptoIDInfo; // API Function

                if (string.IsNullOrEmpty(cryptoIdString))
                {
                    return;
                }
                else
                {
                    xmltest.LoadXml(cryptoIdString);
                    XmlNodeList elemlist = xmltest.GetElementsByTagName("description");
                    foreach (XmlNode element in elemlist)
                        CryptoIdComboBox.Items.Add(element.InnerXml);

                    CryptoIdComboBox.SelectedIndex = 0;
                    //-----------------------------------------------------------------------------------------------------------------
                }

            }
            else
            {
                toolStripStatusDeviceName.Text = "Not Connected";
                toolStripStatusDeviceName.BackColor = Color.Red;
                CryptoIdComboBox.Items.Clear();
            }






        }

        private void SetBtnConfigMenuItem_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * This Sets or gets the button configuration. 
             * PLEASE CHECK ButtonConfigForm.cs  where are the button confiurations are accessed as Device properties
             */
            ButtonConfigForm ButtonConfigform = new ButtonConfigForm();
            ButtonConfigform.SuspendLayout();
            //ButtonConfigform.Text = "Device List";
            ButtonConfigform.StartPosition = FormStartPosition.Manual;
            ButtonConfigform.Left = this.Left;
            ButtonConfigform.Top = this.Top;
            ButtonConfigform.ResumeLayout();
            ButtonConfigform.ShowDialog();
            //-----------------------------------------------------------------------------------------------------------------------------
        }


        void UpdateSignatureImage()
        {
            Error error = driverInterface.ReadSignatureImage(imgSignature.Width, imgSignature.Height, out Bitmap bitmap); // API which gets the Signature image

            // ----------------- -------------------- Application specific code to adjust size of the signature image to the picture box  can be ignored --------------------------------------
            bool source_is_wider = (float)bitmap.Width / bitmap.Height > (float)imgSignature.Width / imgSignature.Height;
            var resized = new Bitmap(imgSignature.Width, imgSignature.Height);
            var g = Graphics.FromImage(resized);
            var dest_rect = new Rectangle(0, 0, imgSignature.Width, imgSignature.Height);
            Rectangle src_rect;

            if (source_is_wider)
            {
                float size_ratio = (float)imgSignature.Height / bitmap.Height;
                int sample_width = (int)(imgSignature.Width / size_ratio);
                src_rect = new Rectangle((bitmap.Width - sample_width) / 2, 0, sample_width, bitmap.Height);
            }
            else
            {
                float size_ratio = (float)imgSignature.Width / bitmap.Width;
                int sample_height = (int)(imgSignature.Height / size_ratio);
                src_rect = new Rectangle(0, (bitmap.Height - sample_height) / 2, bitmap.Width, sample_height);
            }
            g.DrawImage(bitmap, dest_rect, src_rect, GraphicsUnit.Pixel);
            g.Dispose();

            
            imgSignature.Image = resized;
            
            Bitmap bImage = (Bitmap)imgSignature.Image;  // Your Bitmap Image
            System.IO.MemoryStream ms = new MemoryStream();
            bImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);
            clientSession.Send(System.Convert.FromBase64String(SigBase64));
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //imgSignature.Image = bitmap;
        }

        private void button_StartSignature_Click(object sender, EventArgs e)
        {
            Error r = driverInterface.StartSignatureMode(SignMode.StandardSign); // API To start standard Signature
            if (r != Error.SUCCESS)
                MessageBox.Show(r.ToString(), "Warning");
            //else
            //button_StartSignature.Enabled = false;
        }

        private void SetSignRectangleMenuItem_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * The below code is for Setting the Sign rectangle
             * PLEASE CHECK SetSignatureRectangleForm.cs for API USAGE
             */
            SetSignatureRectangleForm setSignatureRectangleform = new SetSignatureRectangleForm();
            setSignatureRectangleform.SuspendLayout();
            //ButtonConfigform.Text = "Device List";
            setSignatureRectangleform.StartPosition = FormStartPosition.Manual;
            setSignatureRectangleform.Left = this.Left;
            setSignatureRectangleform.Top = this.Top;
            setSignatureRectangleform.ResumeLayout();
            setSignatureRectangleform.ShowDialog();
            //-----------------------------------------------------------------------------------------------------------------------------
        }

        private void LoadLicenceMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LicPath = openFileDialog1.FileName;
            }

        }

        private void button_StartSignatureBg_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * Start signaturee with background i.e Document view mode
             * PLEASE CHECK SetSignatureRectangleForm.cs for API USAGE
             */
            if (DocPath == null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DocPath = openFileDialog1.FileName;
                }
            }
            if (!SignrectSet)
            {
                MessageBox.Show("Signature Rectangle is not set", "Error");
                return;
            }

            Error r = driverInterface.SetSignBackground(DocPath);  // API for loading the background image
            if (r != Error.SUCCESS)
            {
                // do nothing
            }
            else
                r = driverInterface.StartSignatureMode(SignMode.DocumentSign); // API  starting the mode

            if (r != Error.SUCCESS)
                MessageBox.Show(r.ToString(), "Warning");

        }

        private void DocumentViewMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocview();

        }


        private void DeviceSearchFilterMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // This is APP specific and can be ignored 
            ToolStripMenuItem DevFilterMenustrip;
            for (int i = 0; i < DeviceSearchFilterMenuItem.DropDownItems.Count; i++)
            {
                DevFilterMenustrip = (ToolStripMenuItem)DeviceSearchFilterMenuItem.DropDownItems[i];
                DevFilterMenustrip.Checked = false;

            }

            DevFilterMenustrip = (ToolStripMenuItem)e.ClickedItem;
            DevFilterMenustrip.Checked = !DevFilterMenustrip.Checked;

        }


        private void button_FinalizeSignature_Click(object sender, EventArgs e)
        {

            Random random = new Random();
            byte[] hash = new byte[32];

            Error r = driverInterface.SetPreliminaryDocumentHash(hash); // API for   signing 
            if (r != Error.SUCCESS)
            {
                // do nothing
            }
            else
                r = driverInterface.SetFinalDocumentHash(hash, false); // API for  signing 

            if (r != Error.SUCCESS)
                MessageBox.Show(r.ToString(), "Warning");


        }

        private void Handlebuttonevent(ButtonEventArgs args)
        {

            MessageBox.Show("Button Event = " + args.ButtonKind.ToString());
            /* switch (args.ButtonKind)
             {
                 case ButtonKind.Rotate:

                     break;
                 case ButtonKind.Rotate90:

                     break;
                 case ButtonKind.Next:
                     OpenDocview();

                     break;
                 case ButtonKind.Prev:

                     break;
                 case ButtonKind.Ok:

                     break;
                 case ButtonKind.Cancel:

                     break;
                 case ButtonKind.ZoomIn:

                     break;
                 case ButtonKind.ZoomOut:

                     break;
                 case ButtonKind.Repeat:

                     break;
                 case ButtonKind.StartSignature:

                     break;
             }*/
        }


        private void OpenDocview()
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                /*try
                {
                    DisplayOrientation deg = driverInterface.DeviceProperties.Status.DisplayOrientation;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning");
                }*/
                Error r = driverInterface.SetDocumentViewing(openFileDialog1.FileName, 1, 3); // API for starting Doc view mode
                if (r != Error.SUCCESS)
                    MessageBox.Show(r.ToString(), "Warning");
            }

        }

        private void PadOrientationMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // This is APP specific and can be ignored 
            ToolStripMenuItem DevFilterMenustrip;
            for (int i = 0; i < PadOrientationMenuItem.DropDownItems.Count; i++)
            {
                DevFilterMenustrip = (ToolStripMenuItem)PadOrientationMenuItem.DropDownItems[i];
                DevFilterMenustrip.Checked = false;

            }

            DevFilterMenustrip = (ToolStripMenuItem)e.ClickedItem;
            DevFilterMenustrip.Checked = true;


            driverInterface.DeviceProperties.Status.DisplayOrientation = (DisplayOrientation)Enum.Parse(typeof(DisplayOrientation), "DEG_" + DevFilterMenustrip.Text);

        }

        private void PromoScreenMenuItem_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * Start signaturee with background i.e Document view mode
             * PLEASE CHECK PromoScreenForm.cs for API USAGE
             */
            PromoScreenForm PromoScreenform = new PromoScreenForm();
            PromoScreenform.StartPosition = FormStartPosition.Manual;
            PromoScreenform.Left = this.Left;
            PromoScreenform.Top = this.Top;
            PromoScreenform.ShowDialog(this);
            //-----------------------------------------------------------------------------------------------------------------------------

        }

        private void FeatureListMenuItem_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * Start signaturee with background i.e Document view mode
             * PLEASE CHECK FeatureListForm.cs for API USAGE
             */
            FeatureListForm FeatureListform = new FeatureListForm();
            FeatureListform.StartPosition = FormStartPosition.Manual;
            FeatureListform.Left = this.Left;
            FeatureListform.Top = this.Top;
            FeatureListform.ShowDialog(this);
            //-----------------------------------------------------------------------------------------------------------------------------

        }

        private void CalibrateTouchMenuItem_Click(object sender, EventArgs e)
        {
            Error r = driverInterface.CalibrateSensor();   // API to Calibratesensor
            if (r != Error.SUCCESS)
                toolStripStatusDeviceName.BackColor = Color.Red;
            else
                toolStripStatusDeviceName.BackColor = Color.LightGreen;

            toolStripStatusDeviceName.Text = r.ToString();


        }

        private void ContinuousScrollDocumentMenuItem_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * Continuous scroll API
             * Please check the ScrollEvent for other APIs in continuous scroll mode
             */
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(folderBrowserDialog1.SelectedPath);
                int count = dir.GetFiles().Length;

                Error r = driverInterface.SetContinuousScrollMode(count / 2, count, 1, 0, "Start Signature", 992, 702); // API to Initiate ContinuousScrollMode
                if (r != Error.SUCCESS)
                    toolStripStatusDeviceName.BackColor = Color.Red;
                else
                    toolStripStatusDeviceName.BackColor = Color.LightGreen;

                toolStripStatusDeviceName.Text = r.ToString();
            }
        }

        private void SimpleDialogMenuItem_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            /*
             * Continuous Set Simple Dialog API
             * Please check the PenEvent for other APIs in Simple Dialog mode
             */
            Error r = driverInterface.SetSimpleDialogMode();
            if (r != Error.SUCCESS)
            {
                toolStripStatusDeviceName.BackColor = Color.Red;
                toolStripStatusDeviceName.Text = r.ToString();
            }

            else
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(folderBrowserDialog1.SelectedPath);
                    int count = dir.GetFiles().Length;
                    tilefillX = 31;
                    tilefillY = 30;
                    tileWidth = 450;
                    tileHeight = 240;
                    tilesendX = 512;
                    tilesendY = 30;
                    tilesignX = 31;
                    tilesignY = 300;
                    tileviewX = 512;
                    tileviewY = 300;
                    r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-fill.bmp", tilefillX, tilefillY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
                    r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-send.bmp", tilesendX, tilesendY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
                    r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-sign.bmp", tilesignX, tilesignY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
                    r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-view.bmp", tileviewX, tileviewY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
                    if (r == Error.SUCCESS)
                        r = driverInterface.UpdateSimpleDialogDisplay(CopyBufOption.DoNotCopyCurrentToShadowBeforeSwitching);
                    else if ((r == Error.DeviceNotConnected) && (r == Error.ConnectionLost))
                    {
                        toolStripStatusDeviceName.Text = r.ToString();
                        toolStripStatusDeviceName.BackColor = Color.Red;
                    }
                    else
                        MessageBox.Show(" SimpleDialog Initialization Failed => " + r.ToString(), "Error");

                }

            }

        }

        private void UpdateSimpleDialogPenEvent(PenEventArgs args)
        {
            switch (Page)
            {
                case 0: ReadMenuButtonsMainPage(args); break;
                case 1:
                    if ((args.PenEventType == PenEventType.PenDown) && PenEventwithinRange(0, 440, 1024, 160, args))
                    {
                        MessageBox.Show(" Keyboard Reading Not implemented, Will Return to Main Menu", "Warning");
                        LoadMainPage();
                    }
                    break;
                case 2:
                    if (args.PenEventType == PenEventType.PenDown)
                    {
                        if (PenEventwithinRange(900, 400, 75, 75, args))
                            HandlesignPagePenDown = true;
                    }
                    else
                    {

                        if (HandlesignPagePenDown && PenEventwithinRange(900, 400, 75, 75, args))
                        {
                            //MessageBox.Show("User Clicked Ok, Will Return to Main Menu", " Warning");
                            driverInterface.StopSignatureCapture(); // This is done incase ok button is pressed before signfinished event is triggered
                            LoadMainPage();
                        }
                        else
                            HandlesignPagePenDown = false;

                    }
                    break;
                case 3:
                    if (args.PenEventType == PenEventType.PenDown)
                    {
                        if (PenEventwithinRange(940, 500, 75, 75, args))
                            HandleViewPagePenDown = true;
                    }
                    else
                    {

                        if (HandleViewPagePenDown && PenEventwithinRange(940, 500, 75, 75, args))
                        {
                            //MessageBox.Show("User Clicked Ok, Will Return to Main Menu", " Warning");
                            LoadMainPage();
                        }
                        else
                            HandleViewPagePenDown = false;

                    }
                    break;
            }
        }

        private bool PenEventwithinRange(int x, int y, int width, int height, PenEventArgs args)
        {
            if ((args.X > x) && (args.X < (x + width)))
            {
                if ((args.Y > y) && (args.Y < (y + height)))
                {
                    return true;
                }

            }
            return false;
        }

        private void ReadMenuButtonsMainPage(PenEventArgs args)
        {

            if (args.PenEventType == PenEventType.PenDown)
            {
                if (PenEventwithinRange(tilefillX, tilefillY, tileWidth, tileHeight, args))
                    tilefillPenDown = true;

                if (PenEventwithinRange(tilesendX, tilesendY, tileWidth, tileHeight, args))
                    tilesendPenDown = true;

                if (PenEventwithinRange(tilesignX, tilesignY, tileWidth, tileHeight, args))
                    tilesignPenDown = true;

                if (PenEventwithinRange(tileviewX, tileviewY, tileWidth, tileHeight, args))
                    tileviewPenDown = true;
            }
            else
            {
                if ((tilefillPenDown) && PenEventwithinRange(tilefillX, tilefillY, tileWidth, tileHeight, args))
                    loadTileFillpage();
                else
                    tilefillPenDown = false;


                if (tilesendPenDown && PenEventwithinRange(tilesendX, tilesendY, tileWidth, tileHeight, args))
                    loadTileSendpage();
                else
                    tilesendPenDown = false;


                if (tilesignPenDown && PenEventwithinRange(tilesignX, tilesignY, tileWidth, tileHeight, args))
                    loadTileSignpage();
                else
                    tilesignPenDown = false;


                if (tileviewPenDown && PenEventwithinRange(tileviewX, tileviewY, tileWidth, tileHeight, args))
                    loadTileViewpage();
                else
                    tileviewPenDown = false;

            }
        }

        private void HelpSpecimenMenuItem_Click(object sender, EventArgs e)
        {
            String foo = ExampleApp.Properties.Resources.ReadMe;
            showNotepad.NotepadHelper.ShowMessage(foo, "ReadMe.txt");
        }

        private void HelpAPIMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                Process.Start("cmd", "/C start" + " " + "https://www.stepoverinfo.net/confluence/display/NETDEVAPI/Public+-+.NET+DeviceAPI+Home");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void ConvertToSoiMenuItem_Click(object sender, EventArgs e)
        {
            string openFileDialogFilter = openFileDialog1.Filter;
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                {
                    string savefilename = Path.ChangeExtension(file, ".soi");

                    Error r = driverInterface.ConvertImageToSoi(file, savefilename);

                }


            }

            openFileDialog1.Filter = openFileDialogFilter;
            openFileDialog1.Multiselect = false;
        }

        private void ConvertSoiToImg(string SaveFileType)
        {

            string openFileDialogFilter = openFileDialog1.Filter;
            openFileDialog1.Filter = "Soi Files|*.soi;";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog1.FileNames)
                    {
                        DialogResult res = DialogResult.OK;
                        string fname = folderBrowserDialog1.SelectedPath + "\\" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + SaveFileType;
                        if (File.Exists(fname))
                        {
                            res = MessageBox.Show("File " + folderBrowserDialog1.SelectedPath + SaveFileType + " Already Exists. Do you want to Replace it", "Warning", MessageBoxButtons.OKCancel);
                        }

                        Error r = Error.SUCCESS;
                        if (res == DialogResult.OK)
                            r = driverInterface.ConvertSoiToImage(openFileDialog1.FileName, fname);

                    }

                }

            }

            openFileDialog1.Filter = openFileDialogFilter;
            openFileDialog1.Multiselect = false;

        }

        private void BMPMenuItem_Click(object sender, EventArgs e)
        {
            ConvertSoiToImg(".bmp");
        }

        private void GIFMenuItem_Click(object sender, EventArgs e)
        {
            ConvertSoiToImg(".gif");
        }

        private void JPGMenuItem_Click(object sender, EventArgs e)
        {
            ConvertSoiToImg(".jpg");
        }

        private void PNGMenuItem_Click(object sender, EventArgs e)
        {
            ConvertSoiToImg(".png");
        }

        private void SimpleDialogFrameWorkMenuItem_Click(object sender, EventArgs e)
        {
#if SDFRAMEWORK_WAS_REMOVED
            Sdfwform.ShowDialog();
#endif
        }

        private void LoadMainPage()
        {
            Error r = driverInterface.SetSimpleDialogClearDisplay(SelectBufClearOption.BothBuffer);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-fill.bmp", tilefillX, tilefillY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-send.bmp", tilesendX, tilesendY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-sign.bmp", tilesignX, tilesignY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\tile-view.bmp", tileviewX, tileviewY, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            if (r != Error.SUCCESS)
                toolStripStatusDeviceName.BackColor = Color.Red;
            else
            {
                toolStripStatusDeviceName.BackColor = Color.LightGreen;
                r = driverInterface.UpdateSimpleDialogDisplay(CopyBufOption.DoNotCopyCurrentToShadowBeforeSwitching);
            }

            toolStripStatusDeviceName.Text = r.ToString();
            Page = 0;
        }

        private void loadTileFillpage()
        {
            tilefillPenDown = false;

            Error r = driverInterface.SetSimpleDialogClearDisplay(SelectBufClearOption.BothBuffer);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\background.png", 0, 0, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\AOK_signArea0_activ.png", 362, 174, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\keyboard_empty.png", 0, 440, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);

            r = driverInterface.SetSimpleDialogResourceFont(@"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\resources\arial10.sof", SimpleDialogFileLoadOptions.SelectExistingFileOnPad);
            if (Error.SUCCESS == r)
            {
                driverInterface.CreateSimpleDialogText("1", 45, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("2", 140, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("3", 235, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("4", 330, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("5", 415, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("6", 505, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("7", 600, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("8", 690, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("9", 785, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("0", 870, 450, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("DEL", 960, 450, Color.Red, Color.Red);

                driverInterface.CreateSimpleDialogText("q", 45, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("w", 140, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("e", 235, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("r", 330, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("t", 415, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("y", 505, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("u", 600, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("i", 690, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("o", 785, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("p", 870, 490, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("u", 960, 490, Color.Red, Color.Red);

                driverInterface.CreateSimpleDialogText("a", 45, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("s", 140, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("d", 235, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("f", 330, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("g", 415, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("h", 505, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("j", 600, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("k", 690, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("l", 785, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText(";", 870, 530, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("'", 960, 530, Color.Red, Color.Red);

                driverInterface.CreateSimpleDialogText("Shift", 45, 570, Color.Red, Color.Red);
                //driverInterface.CreateSimpleDialogText("s", 140, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("z", 235, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("x", 330, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("c", 415, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("v", 505, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("b", 600, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("n", 690, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("m", 785, 570, Color.Red, Color.Red);
                driverInterface.CreateSimpleDialogText("Space", 870, 570, Color.Red, Color.Red);
                //driverInterface.CreateSimpleDialogText("'", 960, 570, Color.Red, Color.Red);

            }

            r = driverInterface.UpdateSimpleDialogDisplay(CopyBufOption.DoNotCopyCurrentToShadowBeforeSwitching);
            Page = 1;
        }

        private void loadTileSendpage()
        {
            tilesendPenDown = false;
            MessageBox.Show("Send Page Not Implemented", " Warning");
        }

        private void loadTileSignpage()
        {
            tilesignPenDown = false;
            Error r = driverInterface.SetSimpleDialogClearDisplay(SelectBufClearOption.BothBuffer);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\background.png", 0, 0, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\AOK_signArea0_activ.png", 50, 400, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\button-accept.png", 900, 400, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSignatureRectangle(50, 400, 400, 65);
            r = driverInterface.StartSignatureMode(SignMode.SimpleDialogSign);
            r = driverInterface.UpdateSimpleDialogDisplay(CopyBufOption.DoNotCopyCurrentToShadowBeforeSwitching);
            Page = 2;
        }

        private void loadTileViewpage()
        {
            tileviewPenDown = false;
            Error r = driverInterface.SetSimpleDialogClearDisplay(SelectBufClearOption.BothBuffer);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\doc_p1.png", 0, 0, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.SetSimpleDialogResourceImage(folderBrowserDialog1.SelectedPath + @"\button-accept.png", 940, 500, SelectBuf.ShadowBuffer, SimpleDialogFileLoadOptions.LoadNewFileToPad);
            r = driverInterface.UpdateSimpleDialogDisplay(CopyBufOption.DoNotCopyCurrentToShadowBeforeSwitching);
            Page = 3;
        }
    }
}
