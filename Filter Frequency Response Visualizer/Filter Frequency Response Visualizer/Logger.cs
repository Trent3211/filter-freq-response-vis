using System;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public class Logger
    {
        // Create a class that will log threading data to a rich text box
        public void LogData(RichTextBox rtbLog, string data)
        {
            // Check if the rich text box is null
            if (rtbLog != null)
            {
                // Check if the rich text box is invoked
                if (rtbLog.InvokeRequired)
                {
                    // Invoke the rich text box
                    rtbLog.Invoke(new Action<RichTextBox, string>(LogData), new object[] { rtbLog, data });
                    return;
                }

                // Append the data to the rich text box with a timestamp before the data
                rtbLog.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + " - " + data + Environment.NewLine);
            }
        }
    }
}
