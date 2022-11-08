using System.Reflection;

namespace Filter_Frequency_Response_Visualizer
{
    partial class homeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.buttonRefreshCOM = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonIO = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.maskedIOBox = new System.Windows.Forms.MaskedTextBox();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.arduinoPort = new System.IO.Ports.SerialPort(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.phaseChart = new LiveCharts.WinForms.CartesianChart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.connectionInfo = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolstripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionCOMInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataView = new System.Windows.Forms.ListView();
            this.buttonSample = new System.Windows.Forms.Button();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.groupBoxStart.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.connectionInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStart
            // 
            this.groupBoxStart.Controls.Add(this.buttonRefreshCOM);
            this.groupBoxStart.Controls.Add(this.saveButton);
            this.groupBoxStart.Controls.Add(this.fileName);
            this.groupBoxStart.Controls.Add(this.label2);
            this.groupBoxStart.Controls.Add(this.buttonIO);
            this.groupBoxStart.Controls.Add(this.btnConnect);
            this.groupBoxStart.Controls.Add(this.maskedIOBox);
            this.groupBoxStart.Controls.Add(this.cmbPort);
            this.groupBoxStart.Location = new System.Drawing.Point(15, 11);
            this.groupBoxStart.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxStart.Name = "groupBoxStart";
            this.groupBoxStart.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxStart.Size = new System.Drawing.Size(1201, 172);
            this.groupBoxStart.TabIndex = 1;
            this.groupBoxStart.TabStop = false;
            this.groupBoxStart.Text = "Start / Stop";
            // 
            // buttonRefreshCOM
            // 
            this.buttonRefreshCOM.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.buttonRefreshCOM.Location = new System.Drawing.Point(293, 17);
            this.buttonRefreshCOM.Name = "buttonRefreshCOM";
            this.buttonRefreshCOM.Size = new System.Drawing.Size(24, 26);
            this.buttonRefreshCOM.TabIndex = 11;
            this.buttonRefreshCOM.Text = "⟳";
            this.buttonRefreshCOM.UseVisualStyleBackColor = true;
            this.buttonRefreshCOM.Click += new System.EventHandler(this.buttonRefreshCOM_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.saveButton.Location = new System.Drawing.Point(14, 135);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(303, 32);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save Data";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // fileName
            // 
            this.fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.fileName.Location = new System.Drawing.Point(14, 72);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(100, 24);
            this.fileName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "File name:";
            // 
            // buttonIO
            // 
            this.buttonIO.Location = new System.Drawing.Point(271, 105);
            this.buttonIO.Margin = new System.Windows.Forms.Padding(2);
            this.buttonIO.Name = "buttonIO";
            this.buttonIO.Size = new System.Drawing.Size(46, 25);
            this.buttonIO.TabIndex = 6;
            this.buttonIO.Text = "...";
            this.buttonIO.UseVisualStyleBackColor = true;
            this.buttonIO.Click += new System.EventHandler(this.buttonIO_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnConnect.Location = new System.Drawing.Point(196, 17);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(97, 26);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // maskedIOBox
            // 
            this.maskedIOBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.maskedIOBox.Location = new System.Drawing.Point(14, 106);
            this.maskedIOBox.Margin = new System.Windows.Forms.Padding(2);
            this.maskedIOBox.Name = "maskedIOBox";
            this.maskedIOBox.Size = new System.Drawing.Size(253, 24);
            this.maskedIOBox.TabIndex = 0;
            // 
            // cmbPort
            // 
            this.cmbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(14, 17);
            this.cmbPort.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(179, 26);
            this.cmbPort.TabIndex = 2;
            this.cmbPort.Text = "Select a port...";
            // 
            // arduinoPort
            // 
            this.arduinoPort.PortName = "COM8";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.tabControl.ItemSize = new System.Drawing.Size(100, 30);
            this.tabControl.Location = new System.Drawing.Point(8, 197);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(925, 581);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.phaseChart);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(917, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Phase";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // phaseChart
            // 
            this.phaseChart.BackColor = System.Drawing.Color.Black;
            this.phaseChart.Location = new System.Drawing.Point(5, 5);
            this.phaseChart.Name = "phaseChart";
            this.phaseChart.Padding = new System.Windows.Forms.Padding(5);
            this.phaseChart.Size = new System.Drawing.Size(907, 533);
            this.phaseChart.TabIndex = 0;
            this.phaseChart.Text = "cartesianChart1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cartesianChart2);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(917, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Magnitude";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // connectionInfo
            // 
            this.connectionInfo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.connectionInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolstripStatus,
            this.connectionCOMInfo});
            this.connectionInfo.Location = new System.Drawing.Point(0, 832);
            this.connectionInfo.Name = "connectionInfo";
            this.connectionInfo.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.connectionInfo.Size = new System.Drawing.Size(1227, 22);
            this.connectionInfo.TabIndex = 6;
            this.connectionInfo.Text = "connectionInfo";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolstripStatus
            // 
            this.toolstripStatus.Name = "toolstripStatus";
            this.toolstripStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // connectionCOMInfo
            // 
            this.connectionCOMInfo.Name = "connectionCOMInfo";
            this.connectionCOMInfo.Size = new System.Drawing.Size(109, 17);
            this.connectionCOMInfo.Text = "Connect to a port...";
            // 
            // dataView
            // 
            this.dataView.HideSelection = false;
            this.dataView.Location = new System.Drawing.Point(934, 231);
            this.dataView.Name = "dataView";
            this.dataView.Size = new System.Drawing.Size(283, 547);
            this.dataView.TabIndex = 7;
            this.dataView.UseCompatibleStateImageBehavior = false;
            // 
            // buttonSample
            // 
            this.buttonSample.Enabled = false;
            this.buttonSample.Location = new System.Drawing.Point(934, 784);
            this.buttonSample.Name = "buttonSample";
            this.buttonSample.Size = new System.Drawing.Size(283, 35);
            this.buttonSample.TabIndex = 8;
            this.buttonSample.Text = "Sample Data";
            this.buttonSample.UseVisualStyleBackColor = true;
            this.buttonSample.Click += new System.EventHandler(this.buttonSample_Click);
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.BackColor = System.Drawing.Color.Black;
            this.cartesianChart2.Location = new System.Drawing.Point(5, 5);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Padding = new System.Windows.Forms.Padding(5);
            this.cartesianChart2.Size = new System.Drawing.Size(907, 533);
            this.cartesianChart2.TabIndex = 0;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // homeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 854);
            this.Controls.Add(this.buttonSample);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.connectionInfo);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBoxStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "homeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter Frequency Response Visualizer";
            this.Load += new System.EventHandler(this.homeForm_Load);
            this.groupBoxStart.ResumeLayout(false);
            this.groupBoxStart.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.connectionInfo.ResumeLayout(false);
            this.connectionInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxStart;
        private System.Windows.Forms.Button buttonIO;
        private System.IO.Ports.SerialPort arduinoPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip connectionInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolstripStatus;
        private LiveCharts.WinForms.CartesianChart phaseChart;
        private System.Windows.Forms.ListView dataView;
        private System.Windows.Forms.MaskedTextBox maskedIOBox;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button buttonSample;
        private System.Windows.Forms.ToolStripStatusLabel connectionCOMInfo;
        private System.Windows.Forms.Button buttonRefreshCOM;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
    }
}

