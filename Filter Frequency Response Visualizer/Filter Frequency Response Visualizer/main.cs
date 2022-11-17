using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public partial class homeForm : Form
    {

        #region Documentation

        // Functionality:
        // Read Button -> ReadArduino() -> Print Button -> ParseCSV() -> Process Button -> ProcessData() -> Plot Button -> PlotData()
        // This is just a basic outline of how this may work.

        #endregion

        #region Global Values
        public bool endOfData = false;
        public List<string> inputStringList = new List<string>();

        private BackgroundWorker Worker;
        Thread testing, data;
        SaveFileHandler SaveCSV = new SaveFileHandler();
        PlottingHandler Plotter = new PlottingHandler();
        ConnectionHandler ComPorts = new ConnectionHandler();
        Logger Log = new Logger();



        #endregion

        #region Form Initializer
        public homeForm()
        {
            InitializeComponent();
            Worker = new BackgroundWorker();
        }
        #endregion

        #region Form Load
        private void homeForm_Load(object sender, EventArgs e)
        {
            // Write a conditional if a com port is available
            if (SerialPort.GetPortNames().Length > 0)
            {
                ComPorts.ComPortConnector(cmbPort);
                cmbPort.SelectedIndex = 0;
                cmbBaud.SelectedIndex = 6;
            }
            else
            {
                MessageBox.Show("No COM ports available. Please connect a device and restart the program.");
            }
        }

        #endregion

        #region Methods
        // Create a random thread job method
        public void RandomThreadJob()
        {
            // Create a random number generator
            Random rand = new Random();

            // Create a random number between 1 and 100
            int randNum = rand.Next(1, 100);

            // Get the random number and store it for the maximum of the progress bar
            barThread.Maximum = randNum;

            // Create a for loop to iterate through the random number
            for (int i = 0; i < randNum; i++)
            {
                // Increment the progress bar
                barThread.Increment(1);
                // Increment lblTime
                lblTime.Text = i.ToString();
                // Sleep the thread for 100 milliseconds
                Thread.Sleep(100);
            }
        }

        // Create the method called AcquireArduinoData that will be called by the background worker
        // This method will use the WriteLine method to send the command "B" to the Arduino to acquire the data
        // The method should have a try catch block to catch any exceptions
        // The last item should be "END" to indicate the end of the data and should not be included in the data.
        // The data will be store in a list of strings called inputStringList
        // Then the inputStringList will be written to the listview called dataView
        // A comma will be used as the delimiter.
        private void AcquireArduinoData()
        {
            try
            {
                // Clear the items from the listview
                dataView.Items.Clear();
                arduinoPort.WriteLine("B");

                // The while loop will check for endOfData to be false
                // Then, in the loop, Readline will be used to read each line of data.
                // Each line of data will consist of x y with a comma in between. 
                // Write x to column 0 and y to column 1
                // When "END" is read, endOfData will be set to true and exit the loop and stop reading data
                while (!endOfData)
                {
                    string inputString = arduinoPort.ReadLine();
                    if (inputString == "END")
                    {
                        endOfData = true;
                    }
                    else
                    {
                        inputStringList.Add(inputString);
                        string[] inputStringArray = inputString.Split(',');
                        ListViewItem item = new ListViewItem(inputStringArray[0]);
                        item.SubItems.Add(inputStringArray[1]);
                        item.SubItems.Add(inputStringArray[2]);
                        dataView.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // Create the error message
/*                string errorMessage = "Error: " + ex.Message;
                MessageBox.Show("Error", errorMessage);*/

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
            cmbPort.SelectedIndex = 0;
        }
        private void buttonData_Click(object sender, EventArgs e)
        {
            // Clear chartPhase

        }
        private void btnConnect_Click(object sender, EventArgs e)
        {

            // This method will connect to the Arduino and simply enable/disable buttons and change connectionInfo status strip
            try
            {  // This method will also create a new thread to read the data from the Arduino
                if (btnConnect.Text == "Connect")
                {
                    // Open Handler

                    arduinoPort = new SerialPort(cmbPort.Text, Convert.ToInt32(cmbBaud.Text));
                    arduinoPort.ReadTimeout = 500;
                    arduinoPort.Open();



                    btnConnect.Text = "Disconnect";
                    // Change the connectionInfo status strip
                    connectionCOMInfo.Text = "Connected to " + cmbPort.Text;
                    connectionCOMInfo.ForeColor = Color.Green;
                    // Disable the COM port select combo box and refresh button
                    cmbPort.Enabled = false;
                    cmbBaud.Enabled = false;
                    buttonRefreshCOM.Enabled = false;

                    // Display if serialReader thread is running on labelThread
                    //labelThread.Text = "Thread Running: " + thread.IsAlive.ToString();

                    // Display the properties of the open serial port to the following labels:
                    labelBaud.Text = "Baud rate: " + arduinoPort.BaudRate;
                    labelCOM.Text = "COM port: " + arduinoPort.PortName;
                    labelData.Text = "Data bits: " + arduinoPort.DataBits;
                    labelBuffer.Text = "Buffer size: " + arduinoPort.ReadBufferSize;
                }
                else
                {
                    // Close Handler
                    ComPorts.ClosePort(arduinoPort, cmbPort, cmbBaud);
                    // Change the button text to "Connect"
                    btnConnect.Text = "Connect";
                    // Change the connectionInfo status strip
                    connectionCOMInfo.Text = "Disconnected";
                    connectionCOMInfo.ForeColor = Color.Red;
                    // Enable the COM port select combo box and refresh button
                    cmbPort.Enabled = true;
                    cmbBaud.Enabled = true;
                    buttonRefreshCOM.Enabled = true;
                    // Clear all four labels
                    labelBaud.Text = "Baud rate: ";
                    labelCOM.Text = "COM port: ";
                    labelData.Text = "Data bits: ";
                    labelBuffer.Text = "Buffer size: ";
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
            if (tabControl.SelectedTab == tabPhase)
            {
                SavePlot.savePlotImage(chartPhase, "Phase");
            }
            else if (tabControl.SelectedTab == tabMagnitude)
            {
                SavePlot.savePlotImage(chartMagnitude, "Magnitude");
            }
            else
            {
                MessageBox.Show("Please select a tab to save the chart from.");

            }
        }
        private void buttonThread_Click(object sender, EventArgs e)
        {
            // This button will handle the start of the testing thread.
            // However, if the testing thread is already running, it will stop the thread and reset the button text to "Start"
            if (buttonThread.Text == "Start")
            {
                // Create a new thread
                testing = new Thread(RandomThreadJob);
                // Start the thread
                testing.Start();
                // Change the button text to "Stop"
                buttonThread.Text = "Stop";

                // Change threadInfo
                threadInfo.Text = "Thread Running: " + testing.IsAlive.ToString();

                // Showm multiple thread information in the lblThreadName
                lblThreadName.Text = "Thread ID: " + testing.ManagedThreadId + "\nThread Priority: " + testing.Priority + "\nThread State: " + testing.ThreadState;
                // Log the data to the rtbLog
                Log.LogData(rtbLog, "Thread " + testing.ManagedThreadId + " Started");
            }
            else
            {
                // Abort the thread
                testing.Abort();
                // Clear the progress bar
                barThread.Value = 0;
                lblTime.Text = "0";
                lblThreadName.Text = "";
                // Change threadInfo
                threadInfo.Text = "Thread Running: " + testing.IsAlive.ToString();
                Log.LogData(rtbLog, "Thread " + testing.ManagedThreadId + " Stopped");
                // Change the button text to "Start"
                buttonThread.Text = "Start";
            }
        }

        private void buttonAcquire_Click(object sender, EventArgs e)
        {
            if (arduinoPort.IsOpen)
            {

                chartPhase.Series.Clear();
                chartMagnitude.Series.Clear();
                AcquireArduinoData();
                Plotter.PlotDataPhase(dataView, chartPhase, "Value 1");
                Plotter.PlotDataMag(dataView, chartMagnitude, "Value 2");
                Log.LogData(rtbLog, "Value 1 Data Plotted.");
                Log.LogData(rtbLog, "Value 2 Data Plotted.");
                endOfData = false;
            }
            else
            {
                MessageBox.Show("Please check your connection before attempting to acquire data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
