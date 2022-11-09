using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public class ColResize
    {
        // Create a method that will auto resize column headers for a list view
        public void autoResizeColumns(ListView listView)
        {
            // Create a new column header auto resize style
            ColumnHeaderAutoResizeStyle headerAutoResize = ColumnHeaderAutoResizeStyle.ColumnContent;

            // Create a for loop that will auto resize the column headers for the list view
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                // Auto resize the column headers for the list view
                listView.AutoResizeColumn(i, headerAutoResize);
            }
        }
    }
}
