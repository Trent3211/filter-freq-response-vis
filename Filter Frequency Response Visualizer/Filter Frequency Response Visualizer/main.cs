using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<string> inputStringList = new List<string>();

        Thread testing;
        RandDataHandler Populate = new RandDataHandler();
        //SeriesCollection series = new SeriesCollection();
        SaveFileHandler SaveCSV = new SaveFileHandler();
        PlottingHandler Plotter = new PlottingHandler();
        ConnectionHandler ComPorts = new ConnectionHandler();


        #endregion

        #region Form Initializer
        public homeForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load
        private void homeForm_Load(object sender, EventArgs e)
        {
            Populate.populateListView(dataView);
            ComPorts.ComPortConnector(cmbPort);
            
        }

        #endregion

        #region Methods
        // Create a random thread job method
        public void RandomThreadJob()
        {
            // Increment lblTime
            lblTime.Text = "0";
            int time = 0;
            while (endOfData == false)
            {
                time++;
                lblTime.Text = time.ToString();
                Thread.Sleep(100);
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
            ComPorts.ComPortConnector(cmbPort);
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            Plotter.PlotData(dataView, chartPhase, "Phase");
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {

            // This method will connect to the Arduino and simply enable/disable buttons and change connectionInfo status strip
            try
            {  // This method will also create a new thread to read the data from the Arduino
                if (btnConnect.Text == "Connect")
                {
                    // Open Handler
                    ComPorts.OpenPort(arduinoPort, cmbPort, cmbBaud);


                    btnConnect.Text = "Disconnect";
                    // Change the connectionInfo status strip
                    connectionCOMInfo.Text = "Connected to " + cmbPort.Text;
                    connectionCOMInfo.ForeColor = Color.Green;
                    // Disable the COM port select combo box and refresh button
                    cmbPort.Enabled = false;
                    cmbBaud.Enabled = false;
                    buttonRefreshCOM.Enabled = false;
                    buttonSample.Enabled = true;

                    // Display if serialReader thread is running on labelThread
                    //labelThread.Text = "Thread Running: " + thread.IsAlive.ToString();

                    // Display the properties of the open serial port to the following labels:
                    labelBaud.Text = "Baud rate: " + arduinoPort.BaudRate;
                    labelCOM.Text = "COM port: " + arduinoPort.PortName;
                    labelData.Text = "Data bits: " + arduinoPort.DataBits;
                    labelBuffer.Text = "Buffer size: " +    arduinoPort.ReadBufferSize;
                }
                else
                {
                    // Close Handler
                    ComPorts.ClosePort(arduinoPort);
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
                    //thread.Abort();
                    
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
                MessageBox.Show("Please select a tab to save the chart from.");

            }
        }

        #endregion

        private void cmbBaud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void buttonThread_Click(object sender, EventArgs e)
        {
            testing = new Thread(RandomThreadJob);
            // If testing thread is running, abort it before starting another
            if (testing.IsAlive)
            {

                testing.Abort();
                threadInfo.Text = "Thread Running: " + testing.IsAlive.ToString();
            }
            else
            {
                // Start the testing thread
                testing.Start();
                // Update how many threads are running in the program at threadInfo
                threadInfo.Text = "Thread Running: " + testing.IsAlive.ToString();
                //threadInfo.Text = "Threads Running: " + Process.GetCurrentProcess().Threads.Count.ToString();
            }
        }

        public void buttonStopThread_Click(object sender, EventArgs e)
        {
            testing.Abort();
            // Update how many threads are running in the program at threadInfo
            threadInfo.Text = "Thread Running: " + testing.IsAlive.ToString();
            //threadInfo.Text = "Threads Running: " + Process.GetCurrentProcess().Threads.Count.ToString();
        }
    }
}
