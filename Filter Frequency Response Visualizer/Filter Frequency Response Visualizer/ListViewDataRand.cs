using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    internal class ListViewDataRand
    {
        // Create a method that will generate 10000 random numbers per column (2 columns) and add them to the list view
        // Have a parameter that will accept the list view to be populated
        public void populateListView(ListView listView)
        {
            // Create a new random number generator
            Random rand = new Random();

            // Create a new list view item
            ListViewItem newItem;

            // Create a new string array
            string[] itemArray = new string[2];

            // Create a for loop that will generate 10000 random numbers per column (2 columns) and add them to the list view
            for (int i = 0; i < 100; i++)
            {
                // Generate a random number
                itemArray[1] = rand.Next(0, 10000).ToString();

                // This will be the iterator
                itemArray[0] = i.ToString();

                // Create a new list view item and add the string array to it
                newItem = new ListViewItem(itemArray);

                // Add the list view item to the list view
                listView.Items.Add(newItem);
            }
        }
        
    }
}
