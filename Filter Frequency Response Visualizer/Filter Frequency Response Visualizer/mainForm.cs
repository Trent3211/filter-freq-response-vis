using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public partial class homeForm : Form
    {
        public homeForm()
        {
            InitializeComponent();
            setFormVersion();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            // Make a messagebox that will output about information
            MessageBox.Show("Filter Frequency Response Visualizer\nVersion: " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n\nCreated by: Michael J. P. Morse\n\nThis program is free software: you can redistribute it and/or modify\nit under the terms of the GNU General Public License as published by\nthe Free Software Foundation, either version 3 of the License, or\n(at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\nGNU General Public License for more details.\n\nYou should have received a copy of the GNU General Public License\nalong with this program.  If not, see <http://www.gnu.org/licenses/>.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setFormVersion()
        {
            // Set the form version to the version of the assembly
            this.Text = "Filter Frequency Response Visualizer " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
