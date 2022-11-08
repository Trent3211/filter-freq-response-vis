using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public partial class homeForm : Form
    {
        #region Global Values
        public bool endOfData = false;

        public SerialPort nanoPort;

        Thread serialReader;

        public List<string> inputStringList = new List<string>();

        #endregion

        #region Main Form Initializer
        public homeForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Main Form Load
        private void homeForm_Load(object sender, EventArgs e)
        {
            loadComPorts();

        }

        #endregion

        #region DOCS

        #endregion

        #region TODO

        #endregion

        #region Methods

        // Create a GetManufacturer mt
        private void loadComPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            cmbPort.Items.AddRange(ports);
        }

        private void ReadArduino()
        {
            while (!endOfData)
            {
                try
                {
                    string inputString = nanoPort.ReadLine();

                    if (inputString.TrimEnd('\r', '\n') == "END")
                    {
                        endOfData = true;
                    }
                    else
                    {
                        inputStringList.Add(inputString);
                        dataView.Invoke(new MethodInvoker(delegate { dataView.Text = inputString; }));
                    }
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Timeout Exception");
                }
            }
        }
        #endregion

        #region Event Handlers
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Write the data from the 



        }

        private void buttonRefreshCOM_Click(object sender, EventArgs e)
        {
            cmbPort.Items.Clear();
            loadComPorts();
        }

        private void buttonSample_Click(object sender, EventArgs e)
        {
            
        }
        private void buttonIO_Click(object sender, EventArgs e)
        {
            // Opens a folder dialogue for the user to select the folder location to add to maskedIOBox
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.SelectedPath = maskedIOBox.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                maskedIOBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // This method will connect to the Arduino and simply enable/disable buttons and change connectionInfo status strip
            try
            {  // This method will also create a new thread to read the data from the Arduino
                if (btnConnect.Text == "Connect")
                {
                    // Start a separate thread to read the data from the Arduino
                    nanoPort = new SerialPort(cmbPort.Text, 9600);
                    nanoPort.ReadTimeout = 500;
                    nanoPort.Open();
                    // Create a new thread to read the data from the Arduino
                    serialReader = new Thread(new ThreadStart(ReadArduino));
                    serialReader.Start();
                    // Change the button text to "Disconnect"
                    btnConnect.Text = "Disconnect";
                    // Change the connectionInfo status strip
                    connectionInfo.Text = "Connected to " + cmbPort.Text;
                    // Disable the COM port select combo box and refresh button
                    cmbPort.Enabled = false;
                    buttonRefreshCOM.Enabled = false;
                }
                else
                {
                    // Close the serial port
                    nanoPort.Close();
                    // Change the button text to "Connect"
                    btnConnect.Text = "Connect";
                    // Change the connectionInfo status strip
                    connectionInfo.Text = "Not connected";
                    // Enable the COM port select combo box and refresh button
                    cmbPort.Enabled = true;
                    buttonRefreshCOM.Enabled = true;
                    // Stop the thread
                    serialReader.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

    }
}
