using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter_Frequency_Response_Visualizer
{
    public partial class homeForm : Form
    {
        #region Global Values
        public bool eof = false;

        public SerialPort namePort;

        //public List<string> inputStringList = new List<string>;
        //public InputDataProcessor processor;

        #endregion

        #region Main Form Initializer
        public homeForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Main Form Load
        private void homeForm_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region DOCS

        #endregion

        #region TODO

        #endregion

        #region Methods

        #endregion

        #region Event Handlers
        private void btnConnect_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void buttonIO_Click(object sender, EventArgs e)
        {
            // Opens a folder dialogue for the user to select the folder location to add to maskedIOBox
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.SelectedPath = maskedIOBox.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                maskedIOBox.Text = folderBrowserDialog1.SelectedPath;
            }
            



        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Write the data from the 



        }
    }
}
