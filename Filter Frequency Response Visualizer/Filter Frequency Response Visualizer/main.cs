using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using LiveCharts;

namespace Filter_Frequency_Response_Visualizer
{
    public partial class homeForm : Form
    {
        #region Global Values
        public bool endOfData = false;
        public List<string> inputStringList = new List<string>();

        public SerialPort nanoPort;

        //Thread serialReader;
        ListViewDataRand Populate = new ListViewDataRand();
        SeriesCollection series = new SeriesCollection();
        SaveFileHandler SaveCSV = new SaveFileHandler();
        PlottingHandler Plotter = new PlottingHandler();


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
            Populate.populateListView(dataView);
            //phaseChart.LegendLocation = LegendLocation.Bottom;
            //magnitudeChart.LegendLocation = LegendLocation.Bottom;
            loadComPorts();
        }

        #endregion

        #region DOCS

        #endregion

        #region TODO

        #endregion

        #region Methods
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
            SaveCSV.saveCSV(dataView);

        }

        private void buttonRefreshCOM_Click(object sender, EventArgs e)
        {
            cmbPort.Items.Clear();
            loadComPorts();
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            Plotter.PlotData(dataView, chartPhase, "Phase");
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectionHandler ComPort = new ConnectionHandler();

            // This method will connect to the Arduino and simply enable/disable buttons and change connectionInfo status strip
            try
            {  // This method will also create a new thread to read the data from the Arduino
                if (btnConnect.Text == "Connect")
                {
                    // Start a separate thread to read the data from the Arduino
                    nanoPort = new SerialPort(cmbPort.Text, Convert.ToInt32(cmbBaud.Text));
                    nanoPort.ReadTimeout = 500;
                    nanoPort.Open();
                    btnConnect.Text = "Disconnect";
                    // Change the connectionInfo status strip
                    connectionCOMInfo.Text = "Connected to " + cmbPort.Text;
                    connectionCOMInfo.ForeColor = Color.Green;
                    // Disable the COM port select combo box and refresh button
                    cmbPort.Enabled = false;
                    cmbBaud.Enabled = false;
                    buttonRefreshCOM.Enabled = false;
                    buttonSample.Enabled = true;

                    // Display the properties of the open serial port to the following labels:
                    labelBaud.Text = "Baud rate: " + nanoPort.BaudRate;
                    labelCOM.Text = "COM port: " + nanoPort.PortName;
                    labelData.Text = "Data bits: " + nanoPort.DataBits;
                    labelBuffer.Text = "Buffer size: " + nanoPort.ReadBufferSize;
                }
                else
                {
                    // Close the serial port
                    nanoPort.Close();
                    // Change the button text to "Connect"
                    btnConnect.Text = "Connect";
                    // Change the connectionInfo status strip
                    connectionCOMInfo.Text = "Disconnected";
                    connectionCOMInfo.ForeColor = Color.Red;
                    // Enable the COM port select combo box and refresh button
                    cmbPort.Enabled = true;
                    cmbBaud.Enabled = true;
                    buttonRefreshCOM.Enabled = true;
                    buttonSample.Enabled = false;
                    // Stop the thread
                    //serialReader.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveChart_Click(object sender, EventArgs e)
        {
            SaveFileHandler SavePlot = new SaveFileHandler();

            // if tabPhase is selected, save phase chart
            // if tabMagnitude is selected, save magnitude chart
            if(tabControl.SelectedTab == tabPhase)
            {
                SavePlot.savePlotImage(chartPhase, "Phase");
            } else if (tabControl.SelectedTab == tabMagnitude)
            {
                SavePlot.savePlotImage(chartMagnitude, "Magnitude");
            } else
            {
                MessageBox.Show("Error");

            }
        }

        #endregion

        private void cmbBaud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
