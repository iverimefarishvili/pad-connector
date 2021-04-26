using Sig.DeviceAPI;
using Sig.DeviceDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

#if SDFRAMEWORK_WAS_REMOVED

namespace SpecimenDotnetproject
{
    public partial class SimpleDialogForm : Form
    {

        static string ChosenTextField = null;

        public struct ActiveSignatureBox
        {
            internal bool SignCaptureInitializedFlag;
            internal bool SignFinishedFlag;
            internal string SceneName;
            internal string ComponentName;
            internal Size ComponentSize;
            internal SDFWComponentType ComponentType;
        }

        ActiveSignatureBox ActivTextBox = new ActiveSignatureBox();
        public SimpleDialogForm()
        {
            InitializeComponent();
        }

        
        public static Bitmap ResizeImage(Image thisImage, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(thisImage.HorizontalResolution, thisImage.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(thisImage, destRect, 0, 0, thisImage.Width, thisImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public void SimpleSignFinished(object sender, EventArgs args)
        {

            Form1.driverInterface.ReadSignatureImage(ActivTextBox.ComponentSize.Width, ActivTextBox.ComponentSize.Height, out Bitmap bmp);
            Form1.driverInterface.StopSignatureCapture();
            bmp = ResizeImage(bmp, ActivTextBox.ComponentSize.Width, ActivTextBox.ComponentSize.Height);
            bmp.MakeTransparent(Color.White);
            bmp.Save("textfiled.png", ImageFormat.Png);
            Form1.driverInterface.SetBitmapBoxSDialogFrameWork(ActivTextBox.SceneName, ActivTextBox.ComponentName, bmp);
            ActivTextBox.SignFinishedFlag = true;

        }


        public void HandleSDialogFrameWorkEvents(object obj, SDFWargs sdfwargs)
        {

            if (sdfwargs.Component == SDFWComponentType.KeyBoard)
                HandleKeyboard(obj, sdfwargs);

            if (sdfwargs.SceneName == "MainScene")
                HandleMainScene(obj, sdfwargs);
            else if (sdfwargs.SceneName == "DocumentViewScene")
                HandleDocumentViewScene(obj, sdfwargs);
            else if (sdfwargs.SceneName == "DocumentfillScene")
                HandleDocumentfillScene(obj, sdfwargs);
            else if (sdfwargs.SceneName == "DocumentsendScene")
                HandleDocumentsendScene(obj, sdfwargs);

            if (sdfwargs.Component == SDFWComponentType.NoComponent)
            {
                Form1.driverInterface.GetCurrentActiveKeyBoardSDialogFrameWork(out SimpleDialogFWKeyBoard Keyborad);
                if (Keyborad != null)
                {
                    Form1.driverInterface.GetCurrentActiveSceneSDialogFrameWork(out SimpleDialogFWScene Scene);
                    Form1.driverInterface.DisplaySceneSDialogFrameWork(Scene.Name);

                }
            }

            if ((ActivTextBox.SignFinishedFlag) && (ActivTextBox.SignCaptureInitializedFlag))
            {
                ActivTextBox.SignFinishedFlag = false;
                ActivTextBox.SignCaptureInitializedFlag = false;
                Form1.driverInterface.GetCurrentActiveSceneSDialogFrameWork(out SimpleDialogFWScene Scene);
                Form1.driverInterface.DisplaySceneSDialogFrameWork(Scene.Name);
            }


        }

        private void Initialize_button_Click(object sender, EventArgs e)
        {
            Form1.driverInterface.InitializeSDialogFrameWork();
            Form1.driverInterface.AddKeyBoardSDialogFrameWork("SmallAlphabetsKeyBoard", new Size(1000, 200), " *10=100| *9=111| *9=111| *2=100, 600, *2=100",
                "q,w,e,r,t,y,u,i,o,p|a,s,d,f,g,h,j,k,l|ABC,z,x,c,v,b,n,m,BS|?123, , space, , Enter", Color.White, Color.Black, Color.Gray, "Arial", 24, FontStyle.Regular, Color.White);

            Form1.driverInterface.AddKeyBoardSDialogFrameWork("BigAlphabetsKeyBoard", new Size(1000, 200), " *10=100| *9=111| *9=111| *2=100, 600, *2=100",
                "Q,W,E,R,T,Y,U,I,O,P|A,S,D,F,G,H,J,K,L|abc,Z,X,C,V,B,N,M,BS|?123, , space, , Enter", Color.White, Color.Black, Color.Gray, "Arial", 24, FontStyle.Regular, Color.White);

            Form1.driverInterface.AddKeyBoardSDialogFrameWork("NumericNSymbolsKeyBoard", new Size(1000, 200), " *10=100| *9=111| *9=111| *2=100, 600, *2=100",
                @"1,2,3,4,5,6,7,8,9,0|-,/,:,;,(,),€,&,\|abc,.,?,!,',$,+,-,BS|?123,@, space,=, Enter", Color.White, Color.Black, Color.Gray, "Arial", 24, FontStyle.Regular, Color.White);


            //----------------------- Main Scene with four buttons ------------------------------------------------------------------------------

            Form1.driverInterface.AddSceneSDialogFrameWork("MainScene", null, new Size(1024, 600), Color.White);


            Form1.driverInterface.AddButtonSDialogFrameWork("MainScene", "tile-fillButton",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-fill.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-fill-Pressed.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-fill-Disabled.png",
                                       true, new Point(41, 40));
            Form1.driverInterface.AddButtonSDialogFrameWork("MainScene", "tile-sendButton",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-send.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-send-Pressed.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-send-Disabled.png",
                                       true, new Point(532, 40));
            Form1.driverInterface.AddButtonSDialogFrameWork("MainScene", "tile-signButton",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-sign.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-sign-Pressed.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-sign-Disabled.png",
                                       true, new Point(41, 320));
            Form1.driverInterface.AddButtonSDialogFrameWork("MainScene", "tile-viewButton",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-view.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-view-Pressed.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\tile-view-Disabled.png",
                                       false, new Point(532, 320));

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------


            //----------------------- Document View Scene ------------------------------------------------------------------------------------------------------------------

            Form1.driverInterface.AddSceneSDialogFrameWork("DocumentViewScene", @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\png\doc_p1.png", new Size(1024, 600), Color.White);
            Form1.driverInterface.AddButtonSDialogFrameWork("DocumentViewScene", "AcceptButton",
                                      @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept.png",
                                      @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept-hover.png",
                                      @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept-inactive.png",
                                      true, new Point(940, 500));

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------

            //----------------------- Document fill Scene ------------------------------------------------------------------------------------------------------------------

            Form1.driverInterface.AddSceneSDialogFrameWork("DocumentfillScene", @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\png\background.png", new Size(1024, 600), Color.White);
            Form1.driverInterface.AddTextFieldSDialogFrameWork("DocumentfillScene", "TeleFonNummerTextField", false, new Point(290, 130), 10, Color.Green, Color.LightPink, "Arial", 20);
            Form1.driverInterface.AddTextFieldSDialogFrameWork("DocumentfillScene", "VonTextField", false, new Point(700, 130), 5, Color.Green, Color.LightPink, "Arial", 20);
            Form1.driverInterface.AddTextFieldSDialogFrameWork("DocumentfillScene", "BisTextField", false, new Point(850, 130), 5, Color.Green, Color.LightPink, "Arial", 20);
            Form1.driverInterface.AddBoxSDialogFrameWork("DocumentfillScene", "FirmaTextBox", true, new Point(365, 210), new Size(600, 30));
            Form1.driverInterface.AddBoxSDialogFrameWork("DocumentfillScene", "StrasseTextBox", false, new Point(180, 245), new Size(340, 30));
            Form1.driverInterface.AddBoxSDialogFrameWork("DocumentfillScene", "OrtTextBox", false, new Point(620, 245), new Size(340, 30));
            Form1.driverInterface.AddCheckBoxSDialogFrameWork("DocumentfillScene", "EnableCheckbox",
                                                @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\checkbox.png",
                                                @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\checkbox-active.png",
                                                @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\checkbox-Inactive.png",
                                                 true, new Point(110, 450), false, "Enable Controls");
            Form1.driverInterface.AddButtonSDialogFrameWork("DocumentfillScene", "AcceptButton",
                                      @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept.png",
                                      @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept-hover.png",
                                      @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept-inactive.png",
                                      false, new Point(940, 500));

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------

            //----------------------- Document send Scene ------------------------------------------------------------------------------------------------------------------

            Form1.driverInterface.AddSceneSDialogFrameWork("DocumentsendScene", @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\png\simple-dialog-payment-overlay.png", new Size(1024, 600), Color.White);
            Form1.driverInterface.AddRadioButtonSDialogFrameWork("DocumentsendScene", "JährRadioButton", 1,
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio-active.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio-inactive.png",
                                       false, new Point(120, 200), false);
            Form1.driverInterface.AddRadioButtonSDialogFrameWork("DocumentsendScene", "MonatRadioButton", 1,
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio-active.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio-inactive.png",
                                       true, new Point(120, 245), false);
            Form1.driverInterface.AddRadioButtonSDialogFrameWork("DocumentsendScene", "WöchenRadioButton", 1,
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio-active.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\radio-inactive.png",
                                       true, new Point(120, 290), false);

            Form1.driverInterface.AddButtonSDialogFrameWork("DocumentsendScene", "AcceptButton",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept-hover.png",
                                       @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\button-accept-inactive.png",
                                       false, new Point(940, 500));

            Form1.driverInterface.AddCheckBoxSDialogFrameWork("DocumentsendScene", "AcceptCheckBx",
                                                @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\checkbox.png",
                                                @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\checkbox-active.png",
                                                @"C:\Sanjay\Work\C#\GIT\gitolite\dotnetdriver\src\UnitTestProject1\TestResource\ceBitTester\g13\SDFWTest\checkbox-Inactive.png",
                                                true, new Point(110, 450), false);

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------
            Form1.driverInterface.DisplaySceneSDialogFrameWork("MainScene");

        }




        public void HandleMainScene(object obj, SDFWargs sdfwargs)
        {

            switch (sdfwargs.Component)
            {
                case SDFWComponentType.Button:

                    if (sdfwargs.ComponentName == "tile-viewButton")
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("DocumentViewScene");

                    if (sdfwargs.ComponentName == "tile-fillButton")
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("DocumentfillScene");

                    if (sdfwargs.ComponentName == "tile-signButton")
                    {
                        Form1.driverInterface.AddBoxSDialogFrameWork("DocumentfillScene", "SignatureBox", true, new Point(300, 400), new Size(400, 150));
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("DocumentfillScene");

                    }

                    if (sdfwargs.ComponentName == "tile-sendButton")
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("DocumentsendScene");



                    break;

                default: break;
            }

        }



        public void HandleDocumentViewScene(object obj, SDFWargs sdfwargs)
        {

            switch (sdfwargs.Component)
            {
                case SDFWComponentType.Button:

                    if (sdfwargs.ComponentName == "AcceptButton")
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("MainScene");
                    break;

                default: break;
            }

        }



        public void HandleDocumentfillScene(object obj, SDFWargs sdfwargs)
        {
            switch (sdfwargs.Component)
            {
                case SDFWComponentType.Button:
                    if (sdfwargs.ComponentName == "AcceptButton")
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("MainScene"); break;
                case SDFWComponentType.CheckBox:
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "TeleFonNummerTextField", SDFWComponentType.TextField, true);
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "VonTextField", SDFWComponentType.TextField, true);
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "BisTextField", SDFWComponentType.TextField, true);
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "FirmaTextBox", SDFWComponentType.Box, true);
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "StrasseTextBox", SDFWComponentType.Box, true);
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "OrtTextBox", SDFWComponentType.Box, true);
                    Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "AcceptButton", SDFWComponentType.Button, true);
                    Form1.driverInterface.DisplaySceneSDialogFrameWork(sdfwargs.SceneName);
                    break;
                case SDFWComponentType.RadioButton: break;

                case SDFWComponentType.TextField:
                    Form1.driverInterface.DisplayKeyboardSDialogFrameWork("SmallAlphabetsKeyBoard", new Point(12, 400));
                    ChosenTextField = sdfwargs.ComponentName;
                    break;
                case SDFWComponentType.Box:
                    if (!ActivTextBox.SignCaptureInitializedFlag)
                    {
                        SimpleDialogFWBox Box = (SimpleDialogFWBox)obj;
                        ActivTextBox.SignCaptureInitializedFlag = true;
                        Form1.driverInterface.SetSignatureRectangle(Box.Location.X, Box.Location.Y, Box.size.Width, Box.size.Height);
                        Form1.driverInterface.StartSignatureMode(SignMode.SimpleDialogSign);
                        ActivTextBox.SceneName = sdfwargs.SceneName;
                        ActivTextBox.ComponentName = Box.Name;
                        ActivTextBox.ComponentSize = Box.size;
                        ActivTextBox.ComponentType = SDFWComponentType.Box;

                    }
                    break;
                default: break;
            }

        }


        public void HandleDocumentsendScene(object obj, SDFWargs sdfwargs)
        {

            switch (sdfwargs.Component)
            {
                case SDFWComponentType.Button:
                    if (sdfwargs.ComponentName == "AcceptButton")
                        Form1.driverInterface.DisplaySceneSDialogFrameWork("MainScene"); break;
                case SDFWComponentType.CheckBox:
                    if (sdfwargs.ComponentName == "AcceptCheckBx")
                    {
                        Form1.driverInterface.EnableComponentSDialogFrameWork(sdfwargs.SceneName, "AcceptButton", SDFWComponentType.Button, true);
                    }
                    break;
                case SDFWComponentType.RadioButton: break;
                case SDFWComponentType.KeyBoard: break;
                case SDFWComponentType.TextField: break;
                case SDFWComponentType.Box:

                    break;
                default: break;
            }

        }


        public void HandleDocumentSignScene(object obj, SDFWargs sdfwargs)
        {

            switch (sdfwargs.Component)
            {
                case SDFWComponentType.Button: break;

                default: break;
            }

        }


        public void HandleKeyboard(object obj, SDFWargs sdfwargs)
        {
            Form1.driverInterface.GetCurrentActiveSceneSDialogFrameWork(out SimpleDialogFWScene Scene);

            if (sdfwargs.KeyText.ToUpper() == "ABC")
            {
                Form1.driverInterface.GetCurrentActiveKeyBoardSDialogFrameWork(out SimpleDialogFWKeyBoard Keyborad1);
                //SimpleDialogFWKeyBoard kybrd = SimpleDialogFrameWork.GetCurrentActiveKeyBoard();
                if (Keyborad1.Name == "SmallAlphabetsKeyBoard")
                    Form1.driverInterface.DisplayKeyboardSDialogFrameWork("BigAlphabetsKeyBoard", new Point(12, 400));
                else
                    Form1.driverInterface.DisplayKeyboardSDialogFrameWork("SmallAlphabetsKeyBoard", new Point(12, 400));
            }
            else if (sdfwargs.KeyText == "BS")
            {
                Form1.driverInterface.DeleteTextFieldcharSDialogFrameWork(Scene.Name, ChosenTextField);
            }
            else if (sdfwargs.KeyText == "?123")
            {
                Form1.driverInterface.DisplayKeyboardSDialogFrameWork("NumericNSymbolsKeyBoard", new Point(12, 400));
            }
            else
            {
                Form1.driverInterface.AppendTextFieldcharSDialogFrameWork(Scene.Name, ChosenTextField, sdfwargs.KeyText);
            }



        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SimpleDialogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.driverInterface.DeInitializeSDialogFrameWork();
        }
    }
}

#endif