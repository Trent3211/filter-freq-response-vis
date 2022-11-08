using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using LiveCharts.Wpf;
using System.Windows.Media.Converters;

namespace Filter_Frequency_Response_Visualizer
{
    public partial class homeForm : Form
    {
        #region Initializing
        // Global Variables
        public bool endOfData = false;
        Thread serialReader;
        
        public List<string> inputStringList = new List<string>();
        public InputDataProcessor processor;


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
            // Load the COM ports into the cmbPorts
            string[] ports = SerialPort.GetPortNames();
            cmbPort.Items.AddRange(ports);
            cmbPort.SelectedIndex = 0;
        
            

        }

        #endregion

        #region DOCS

        #endregion

        #region TODO

        #endregion

        #region Methods
        // Create the method ReadArduino()
        private void ReadArduino()
        {
            int stringsReadCount = 1;

            while (!endOfData)
            {
                try
                {
                    string inputString = arduinoPort.ReadLine();
                    if (inputString.TrimEnd('\r', '\n') == "END")
                    {
                        endOfData = true;
                    } else
                    {
                        inputStringList.Add(inputString);

                        dataView.Invoke(new MethodInvoker(
                            delegate
                            {
                                dataView.Text = "String read = " + stringsReadCount.ToString() + "\r\n" + inputString;
                            }));
                    }
                }
                catch (TimeoutException)
                {
                    // Do nothing
                }
                if (!endOfData)
                {
                    stringsReadCount++;
                }
            }
        }

        public void ParseCSVList()
        {
            processor = new InputDataProcessor(inputStringList.Count);
            processor.inputIntArray = new int[inputStringList.Count, 2];
            int i = 0;

            foreach(string s in inputStringList)
            {
                string trimmed = s.TrimEnd('\r', '\n');
                string[] subs = trimmed.Split(',');

                if (int.TryParse(subs[0], out processor.inputIntArray[i, 0]))
                {
                }
                else
                {
                    processor.inputIntArray[i, 0] = 0;
                }

                if (int.TryParse(subs[1], out processor.inputIntArray[i, 1]))
                {
                }
                else
                {
                    processor.inputIntArray[i, 1] = 0;
                }
            }
        }

        private void PlotData(int numSamples)
        {
            for (int i = 0; i < numSamples; i++)
            {
                double time = processor.inputDoubleArray[i, 0];
                double value = processor.inputDoubleArray[i, 1];

                // Plot the data on phaseChart

            }
        }
        private void ProcessData()
        {
            processor.ZeroTimes();
            processor.ScaleValues(1023, 5.0);
        }
        #endregion

        #region Event Handlers
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // This button is used to open the COM port presented in cmbPort
            if (arduinoPort.IsOpen)
            {
                arduinoPort.Close();
                btnConnect.Text = "Connect";
                // Change the connectionStatus statuslabel to "Disconnected" in red
                connectionStatus.Text = "Disconnected";
                connectionStatus.ForeColor = Color.Red;
            }
            else
            {
                arduinoPort.PortName = cmbPort.Text;
                arduinoPort.Open();
                btnConnect.Text = "Disconnect";
                // Change the connectionStatus statuslabel to "Connected" in green
                connectionStatus.Text = "Connected to " + cmbPort.Text;
                connectionStatus.ForeColor = Color.Green;
                // Enable Load Data button
                btnLoadData.Enabled = true;
            }
        }
        #endregion

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Write the data from the 



        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            
            try
            {
                arduinoPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            

            serialReader = new Thread(ReadArduino);
            serialReader.IsBackground = true;

            arduinoPort.Write("B");
            
        }
    }
}
