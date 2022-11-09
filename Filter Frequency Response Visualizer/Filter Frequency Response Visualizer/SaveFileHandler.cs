using LiveCharts.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Filter_Frequency_Response_Visualizer
{
    public class SaveFileHandler
    {
        SaveFileDialog saveFile = new SaveFileDialog();
        // this save file method will have two parameters, the chart and tab
        public void savePlotImage(Chart chart, String Name)
        {
            
            // Set the default file name
            saveFile.FileName = Name;

            // Set the default file extension
            saveFile.DefaultExt = ".png";

            // Set the file type
            saveFile.Filter = "PNG Files (*.png)|*.png";

            // Show the save file dialog
            DialogResult result = saveFile.ShowDialog();

            // Use the bitmap method to save the chart as a png file
            Bitmap bmp = new Bitmap((int)chart.Width, (int)chart.Height);
            chart.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            bmp.Save(saveFile.FileName);
        }
            // Create a method that will take the two columns of data from the list view and save them to a csv file.
        public void saveCSV(ListView ListView)
        {
            // Create a new save file dialog

            // Set the default file name
            saveFile.FileName = "Data";

            // Set the default file extension
            saveFile.DefaultExt = ".csv";

            // Set the file type
            saveFile.Filter = "CSV Files (*.csv)|*.csv";

            // Show the save file dialog
            DialogResult result = saveFile.ShowDialog();

            // Create a new string builder
            StringBuilder sb = new StringBuilder();

            // Create a new string array to hold the column headers
            string[] columnHeaders = new string[ListView.Columns.Count];

            // Loop through the columns and add the headers to the string array
            for (int i = 0; i < ListView.Columns.Count; i++)
            {
                columnHeaders[i] = ListView.Columns[i].Text;
            }

            // Add the column headers to the string builder
            sb.AppendLine(string.Join(",", columnHeaders));

            // Loop through the rows and add the data to the string builder
            foreach (ListViewItem item in ListView.Items)
            {
                string[] row = new string[item.SubItems.Count];
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    row[i] = item.SubItems[i].Text;
                }
                sb.AppendLine(string.Join(",", row));
            }

            // Write the string builder to the file
            System.IO.File.WriteAllText(saveFile.FileName, sb.ToString());
        }
    }
}