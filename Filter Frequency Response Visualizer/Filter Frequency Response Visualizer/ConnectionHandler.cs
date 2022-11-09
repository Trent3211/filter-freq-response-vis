using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace Filter_Frequency_Response_Visualizer
{
    public class ConnectionHandler
    {
        // Create a method that will start a separate thread to open the Serial Port
        public void openSerialPort(SerialPort nanoPort)
        {
            // Create a new thread to open the serial port
            Thread openPort = new Thread(() => nanoPort.Open());

            // Start the thread
            openPort.Start();
        }
    }
}
