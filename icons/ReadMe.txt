This file gives a brief Explanantion of usage of Sig.DeviceAPI

The Sig.DeviceAPI has some APIs , Events and some Deviceproperties.
There are some Initialization API's Which needs to be executed before using other API.

Note1 : If Deviceproperties are accessed without establishing connection then it will return -1 other wise all property value is >= 0.
Note2 : Step1 and Step2 is sequential and Essential, All others steps are not sequential although APIs used within that step may be sequntial which is be documented there.
Note3 : If other steps are executed without Step1 and step2 the Devicenotconnected Error is returned.
Note4 : If during Execution of other steps Connection is lost then Connectionlost is returned and user may have to execute step2 again to re-establish connection.


Step1 : Instantiate the Driver.
		The driver has an Interface class called IDriver and Implementation class called Driver. An example of Instantiating a Driver is as below :-

		//------------------ API Implementation Example --------------------------------
		public static  IDriver driverInterface = new Driver();  // Driver Instantiation
		//------------------------------------------------------------------------------
		

Step2 : Search Devices and Establishing connection.
		There are Two APIs to search the connected Device
		
		public Error DeviceSearch(out string[] padStrings, int guiOption, FilterDeviceKind DeviceFilter = FilterDeviceKind.dkStepOver)
		public Error EnumerateDevices(out string[] padStrings, out int count, FilterDeviceKind DeviceFilter = FilterDeviceKind.dkStepOver)
		
		The DeviceSearch API Gives an Additional guioption as Against the EnumerateDevices API. The guioption = 1 is Enabled, guioption = 0 is Disabled.
		If Enabled the DeviceSearch API pops up a Listbox UI, so the User can select which Device to Establish connection.
		The padStrings variable is an output of the API which contains a string list of devices found.
		The DeviceFilter variable is optional variable, if no value is given the DeviceFilter searches for StepoverDevices. Please Check the DeviceFilter options in the API
		
		Note1 : The guioption is only available if the Driver is implemented only on  Windows Platform Applications based on NETFRAMEWORK else this option is ignored.
		Note2 : The Specimen Application is implemented on netcoreapp3.1 so this option is Irrelevant and henc uses its own Listbox and uses the EnumeratDevices API
		
		The Implementation in speciment is as below
		
		The devfilter variable is of type FilterDeviceKind Declared in DeviceListForm.cs.
		User selection of the variable is assigned in function "private void SetDeviceMenuItem_Click(object sender, EventArgs e)"
		
		The devfilter is Selected and DeviceListForm is Instantiated.
		
		
		The API Implementation of device search is in the constructor of  DeviceListForm.cs		
		//------------------ API Implementation Example --------------------------------
		Error r = Form1.driverInterface.EnumerateDevices(out string[] pads, out int count, devfilter);
		//------------------------------------------------------------------------------
		
		This seraches for connected Devices based on devfilter selection and populates the listbox
		
		Communication With a selected device is established using the API "public Error SetDevice(string padString)"
		
		here the variable padstring could be replaced with pads[0], ie the first device in the list 
		
		In the Application Doubleclicking on a device on the listbox, SetsDevice or Establishes communication with it. This Implementation can be seen in the function "private void SetDeviceMenuItem_Click(object sender, EventArgs e)"
		
		//------------------ API Implementation Example --------------------------------
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
		//------------------------------------------------------------------------------
		
		If the above operation is successful then communication is established with the device and It is now ready for operation.
		
		
		


Step3 : Events
		The following are the list of events available with Sig.DeviceAPI
		 - ButtonEvent -> used in Document view mode, Document sign mode, Signature confirm mode
		 - SignStarted -> used in all signature modes
		 - SignFinished -> used in all signature modes
		 - SignImgChanged -> used in all signature modes
		 - PenEvent -> used in all Simpledialog mode only
		 - ScrollEvent -> used in all Continous Scroll mode only
		 
		The user can choose to use the events which are necessary and is not mandatory to implement all events. For example if connected to BW pads PenEvent and Scroll Event is not necessary
		The Following is an example for associating an event with user defined function
		
		driverInterface.ButtonEvent += (object sender, ButtonEventArgs args) => HandleButtonEvents(args);
		driverInterface.SignStarted += (object sender, EventArgs args) => HandleSignStartedEvents(args);
		driverInterface.SignFinished += (object sender, EventArgs args) => HandleSignFinishedEvents(args);
		driverInterface.SignImgChanged += (object sender, EventArgs args) => HandleSignImgChangedEvents(args);
		driverInterface.PenEvent += (object sender, PenEventArgs args) => HandlePenEventEvents(args);
		driverInterface.ScrollEvent += (object sender, Sig.DeviceAPI.ScrollEventArgs e) => HandleScrollEventEvents(args);
		
		In the Application the evnts are implemented a little diffrently in the function "private void SubscribeToEvents()"
		
		The Explanation of the Events will be done in the steps wher it is relevant.
		
		
Step4 : Start Signature With Background. The Events relavant here is ButtonEvent, SignStarted, SignFinished, SignImgChanged.

		Signature with background requires three API as listed below
		 - public Error SetSignBackground(Bitmap bitmap)
		 - public Error SetSignatureRectangle(int XStart, int YStart, int width, int height)
										or 
		 - public Error SetSignatureRectangle(int XStart, int YStart, int width, int height, int posX, int posY, SignRectOption option)  // for Extended mode. Please read API Explanations for further info 
		 - public Error StartSignatureMode(SignMode mode)
		 
		 The SetSignBackground() function and SetSignatureRectangle() functions have to be called before StartSignatureMode() is called.
		
		
		Note1 : SetSignatureRectangle is ignored for BW pads
		
		The API Implementation of this can be seen in the functions "private void button_StartSignatureBg_Click(object sender, EventArgs e)" and "private void SetsignatureRectangleOk_button_Click(object sender, EventArgs e)"
		
		As can be seen by Association of APi to events
		Button events are handled by 
		
		driverInterface.ButtonEvent += (object sender, ButtonEventArgs args) =>
            {
                toolStripStatusButtonorsignEvent.Text = "Button event:" + args.ButtonKind; 
                Handlebuttonevent(args);        // User defined function
            };
			
			
			
		Signstarted event only updates the status strip on the UI
		driverInterface.SignStarted += (object sender, EventArgs args) =>
            {
                toolStripStatusButtonorsignEvent.Text = "Signature started event triggered"; 
            };
			
		
		SignFinished event does hadle two Application specific functions and one API
		driverInterface.SignFinished += (object sender, EventArgs args) =>
            {
                toolStripStatusButtonorsignEvent.Text = "Signature finished event triggered";        
                button_StartSignature.Invoke((MethodInvoker)(() => button_StartSignature.Enabled = true));
                driverInterface.StopSignatureCapture();  // API Function (if required by  the user can disable further pen events)

            };
			
			the Stopcapture disables all the listener threads inside the driver and also disables the touch on the pad. The user may choose to emply this or may choose to do something else.
			this is just for example
			
		SignImgChanged event can be used to update the image on the Application as is done here
		driverInterface.SignImgChanged += (object sender, EventArgs args) =>
            {
                imgSignature.Invoke((MethodInvoker)(() => UpdateSignatureImage()));
            };
		
		
		
Step5 : Start Signature Without Background. The Events relavant here is ButtonEvent, SignStarted, SignFinished, SignImgChanged.

		Signature without background requires two API as listed below
		 - public Error SetSignatureRectangle(int XStart, int YStart, int width, int height)
		 - public Error StartSignatureMode(SignMode mode)
		 
		
		The SetSignatureRectangle() function is optional here, If not used earlier then the signature rectangle size is by default the size of the Screen.
		Note : if set previuously used ,perhaps while using Start Signature With Background then the signature rectangle is stored in the Device untill the device is restarted or
		       it can be reset by sending 0 like below
			   SetSignatureRectangle(0, 0, 0, 0);
		
		The API Implementation of this can be seen in the function "private void button_StartSignature_Click(object sender, EventArgs e)"
		
		The Events explanation is similar to Step4
		
		
Step6 : Documentview

		Description : This Displays one single page at a time and can be scrolled u to end of the page, if zoomed in then it can be scrolled left to write as well.
		if next page is desired to be viewed then next page button can be pressed and the Application should send next image while also updating the current page no.
		

		The Documentview mode is available only in color pads. It has only one API "public Error SetDocumentViewing(Bitmap bitmap, int CurpageNo, int MaxpageNo)"
		
		The Api implementation in the specimen application can be seen in the function "private void OpenDocview()"
		//------------------ API Implementation Example --------------------------------
		Error r = driverInterface.SetDocumentViewing(openFileDialog1.FileName, 1, 3); // API for starting Doc view mode
                if (r != Error.SUCCESS)
                    MessageBox.Show(r.ToString(), "Warning");
		//------------------------------------------------------------------------------
		
Step7 : Continous Scroll Mode. The relevant events here is ScrollEvent
		Description : In Document view mode only one page can be scrolled and next page is to be requested by clicking on next page button, In continous scroll mode
		No button needs to be pressed as the user scrolls up the next tile is automatically requested.
		here pages are divided in to tiles, each tile size is 992x702. In the Example used in the Specimen Application each A4 page size is taken as an image of size 992x1404
		and is divided in to two tiles. The Mode has to be initialized with max no of tiles, max no of Pages, Start Tile now from which it has to be dispalyed, 
		the offset for the tile ie form which point with in tile the start of displayis intended, The string to be dispalyed on the button used to start signature.
		
		This API is "public Error SetContinuousScrollMode(int numberOfPages, int numberOfTiles, int startTileNumber, int startTileOffset,string buttonText, int tileWidth, int tileHeight = 702)"
		
		The Implementation in the specimen application is as below
		//------------------ API Implementation Example --------------------------------
		 if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(folderBrowserDialog1.SelectedPath);
                int count = dir.GetFiles().Length;  // gives no of files in the directory

                Error r = driverInterface.SetContinuousScrollMode(count/2, count, 1, 0, "Start Signature", 992, 702); // API to Initiate ContinuousScrollMode
                if (r != Error.SUCCESS)
                    toolStripStatusDeviceName.BackColor = Color.Red;
                else
                    toolStripStatusDeviceName.BackColor = Color.LightGreen;

                toolStripStatusDeviceName.Text = r.ToString();
            }
		//------------------------------------------------------------------------------
			

		The Event Implementation is below
		
		//------------------ API Implementation Example --------------------------------
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
		//------------------------------------------------------------------------------
		
		This implements two APIs, if the req is page no it sets appropriate Tile no.
		if the request is Tile no then sends the tile no from the previously chosen directory. This can be implemented diffrently by the user if required.
		

Step8 : Simpledialog Mode, The relevant events here is PenEvent, SignStarted, SignFinished, SignImgChanged. 

		Description : The SimpleDialog Mode can be used by the user to create a cutome dialog mode. Here is an example where when initailize
		Creates a 4 menu button on the mainpage, when each button is clicked it loads appropriate pages such as 
			- tile-fill page - used fill a page using a keyboard
			- tile-send page - can used by application to send a page its destination, here this is not implemented
			- tile-sign page - loads a signature page if clicked on ok goes back to main page
			- tile-view page - loads a Document view  page if clicked on ok goes back to main page

			The API used for Initialization is "SetSimpleDialogMode();"
			The API used for Loading the Images  is "public Error SetSimpleDialogResourceImage(Bitmap bitmap, int column, int row, SelectBuf drawbuf)"
			The API used for Updating the display "public Error UpdateSimpleDialogDisplay(CopyBufOption bufOption)"
			The API Implementation can be seen below.

			//------------------ API Implementation Example --------------------------------
		 Error r = driverInterface.SetSimpleDialogMode();
            if (r != Error.SUCCESS)
            {
                toolStripStatusDeviceName.BackColor = Color.Red;
                toolStripStatusDeviceName.Text = r.ToString();
            }
                
            else
            {
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
				else if ( (r == Error.DeviceNotConnected) && (r == Error.ConnectionLost))
				{
					toolStripStatusDeviceName.Text = r.ToString();
					toolStripStatusDeviceName.BackColor = Color.Red;
				}

            }
		//------------------------------------------------------------------------------
		
		The Rest of it is handled through the Peneventhandler.
		Everytime a penevent occurs the pen values in x,y,z are availabe throught the penevents which calls "private void UpdateSimpleDialogPenEvent(PenEventArgs args)"
		This is the userdefined handler associated with penevent.

		in this function there are four cases. By defult the case0 is executed. This checks if both pendown and penupevent occurs with in any of the four button location and size.
		if true, it flags this as a valid press event and calls the appropriate loading of the page and updates the caseno.
		for example if tile-sign button is pressed then sign page with sign rectangle and ok button is loaded using the function "private void loadTileSignpage()"
		and switches the caseno for UpdateSimpleDialogPenEvent() function, it also starts the signature mode for simpledialog. In this mode if pen event occurs with in the 
		signature rectangle defined then this is not reported via pen event but via  SignStarted, SignFinished, SignImgChanged events.
		However if pen events occure outside of signature rectangle then Pen event is generated. If ok button is clicked then it returns to main page with 4 buttons and updates the case no to 0
		Now the process is repeated again.


		
		

Step9  : Pad Orientation, This is a Device property which can be read or set. It has no Events
		In the Application on starting up, it reads the current Device Orientation. otherwise it set it
		//------------------ API Implementation Example --------------------------------

		driverInterface.DeviceProperties.Status.DisplayOrientation = DisplayOrientation.DEG_90; // gets the value of the device

		DisplayOrientation Rot = driverInterface.DeviceProperties.Status.DisplayOrientation; // sets the value of device

		Note: In both cases it returns -1 if device is not connected
		//------------------------------------------------------------------------------

Step10 : Set Sign Rectangle, This is used to choose where in the Document the signature is to be drawn and its size in Document sign mode and in standard signature mode
	     it chooses the size and location on the screen. The Extended mode has addition parameter which can choose where the signrectangle is displayed on the screen.
		 The Extende mode is supported by generation 12 and generation 13 pads for others if used is ignored by pad.
         In order to support the features it has two APIs as below 
		 public Error SetSignatureRectangle(int XStart, int YStart, int width, int height)
		 public Error SetSignatureRectangle(int XStart, int YStart, int width, int height, int posX, int posY, SignRectOption option)
		 
		 In the Application it is used in SetSignatureRectangleForm.cs. in "private void SetsignatureRectangleOk_button_Click(object sender, EventArgs e)" function
		 

		//------------------ API Implementation Example --------------------------------

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
		//------------------------------------------------------------------------------


Step11 : Button Config, This is implemented using two APIs
		  - public Error GetButtonConfig(out ButtonBar buttonBar)
		  - public Error SetButtonConfig(ButtonBar buttonBar, bool persist = false)

		  In the application this is implemented in "private void SetConfig_button_Click(object sender, EventArgs e)" and "private void GetConfig_button_Click(object sender, EventArgs e)"
		  This has Checkbox to choose enabled and visible and also buttons with similar icons to change the background color
		  //------------------ API Implementation Example --------------------------------

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
		//------------------------------------------------------------------------------

Step12 : Calibrate sensor  Implements an API public Error CalibrateSensor() which intiates the calibartion of the Touchsensor.
		 The user has to touch and hold on the markers displayed on the pad until it shifts position to all four corners the calibration is complete


Step13 : Pad File Operation, It can do the following operations
		 - Read Pad Resources
		 - Copy file from pad
		 - Rename File on Pad
		 - Delete file from pad
		 - Upload File to Pad

		 It uses the following APIs to accomplish this operation
		 - public Error GetFileInfo(out List<PadFileInfo> fileList)
		 - public Error CopyFileFromDevice(string filename, string localSavePath)
		 - public Error RenameFileOnPad(string existingFileName, string newFileName)
		 - public Error DeleteFileFromPad(string fileName)
		 - public Error CopyFileToDevice(string filename, string SaveAsFileName)

		 Note : The Operation on some of the files on the pad is restricted and may return "Pad is Locked" error if used.

		 The API Implemetation is in PadFileOperationForm.cs.

Step14 : Promoscreen, It has two APIs
		 - public Error UploadPromoScreen(string fileName, int number, DisplayOrientation rotation, int screenTime)
		 - public Error RemovePromoscreen(int Number, DisplayOrientation Rotation)
		 Description - In the Color pads it is possible upload up to 10 Prmoscreens to be displayed one after the other with varying time on the screen
		 In order to do this an image has to be uploaded with complete path in filename,
		 The number of the promoscreen image between 1 - 10,
		 Note if you use the the number already available on the pad, it will be replaced.
		 orientation of the promoscreen is to be provided it has to be displayed in landscpe or portrait mode this is only for g13 pads
		 screen time is the amount of time that promoscreen has to be displayed, the value is between 5 and 12,00,000 seconds

		 To delete the promoscreen all you have to do is to provide the no of the promoscreen to be developed
		 This is implemented in PromoScreenForm.cs

Step15 : FeatureList, this lists all the features available in the pad. This available only in the latest version of firmware
		 please see the API explanation for details


Step16 : CryptoIds  - It is deviceproperty which gives an xml string of all avaibale cryptoids
		please see the API explanation for details


		
		

