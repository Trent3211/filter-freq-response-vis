using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Filter_Frequency_Response_Visualizer
{
    public class PlottingHandler
    {
        // Create the PlotData method that will plot the data from the listview to the chart
        public void PlotDataPhase(ListView ListView, Chart Chart, string SeriesName)
        {
            // Create a new series for the chart
            Series series = new Series(SeriesName);

            // Set the chart type
            series.ChartType = SeriesChartType.Spline;

            // Set the X and Y values
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                series.Points.AddXY(ListView.Items[i].SubItems[0].Text, ListView.Items[i].SubItems[1].Text);
            }

            // Add the series to the chart
            Chart.Series.Add(series);
        }
        public void PlotDataMag(ListView ListView, Chart Chart, string SeriesName)
        {
            // Create a new series for the chart
            Series series = new Series(SeriesName);

            // Set the chart type
            series.ChartType = SeriesChartType.Spline;

            // Set the X and Y values
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                series.Points.AddXY(ListView.Items[i].SubItems[0].Text, ListView.Items[i].SubItems[2].Text);
            }

            // Add the series to the chart
            Chart.Series.Add(series);
        }
    }
}
