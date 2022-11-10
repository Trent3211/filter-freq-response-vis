using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public class ConnectionHandler
    {
        public void ComPortConnector(ComboBox cmbPort)
        {
            string[] ports = SerialPort.GetPortNames();
            cmbPort.Items.AddRange(ports);
        }

        public void OpenPort(SerialPort serialPort, ComboBox cmbPort, ComboBox cmbBaud)
        {
            serialPort = new SerialPort(cmbPort.Text, Convert.ToInt32(cmbBaud.Text));
            serialPort.ReadTimeout = 500;
            serialPort.Open();
        }

        public void ClosePort(SerialPort serialPort)
        {
            serialPort.Close();
        }
    }
}
