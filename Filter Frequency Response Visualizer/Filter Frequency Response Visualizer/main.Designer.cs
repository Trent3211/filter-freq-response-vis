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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.cmbBaud = new System.Windows.Forms.ComboBox();
            this.labelBuffer = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.labelCOM = new System.Windows.Forms.Label();
            this.labelBaud = new System.Windows.Forms.Label();
            this.buttonRefreshCOM = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.arduinoPort = new System.IO.Ports.SerialPort(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPhase = new System.Windows.Forms.TabPage();
            this.tabMagnitude = new System.Windows.Forms.TabPage();
            this.connectionInfo = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolstripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionCOMInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataView = new System.Windows.Forms.ListView();
            this.timeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSample = new System.Windows.Forms.Button();
            this.buttonSaveChart = new System.Windows.Forms.Button();
            this.chartPhase = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMagnitude = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxStart.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPhase.SuspendLayout();
            this.tabMagnitude.SuspendLayout();
            this.connectionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMagnitude)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxStart
            // 
            this.groupBoxStart.Controls.Add(this.cmbBaud);
            this.groupBoxStart.Controls.Add(this.labelBuffer);
            this.groupBoxStart.Controls.Add(this.labelData);
            this.groupBoxStart.Controls.Add(this.labelCOM);
            this.groupBoxStart.Controls.Add(this.labelBaud);
            this.groupBoxStart.Controls.Add(this.buttonRefreshCOM);
            this.groupBoxStart.Controls.Add(this.saveButton);
            this.groupBoxStart.Controls.Add(this.btnConnect);
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
            // cmbBaud
            // 
            this.cmbBaud.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbBaud.FormattingEnabled = true;
            this.cmbBaud.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.cmbBaud.Location = new System.Drawing.Point(134, 17);
            this.cmbBaud.Name = "cmbBaud";
            this.cmbBaud.Size = new System.Drawing.Size(57, 26);
            this.cmbBaud.TabIndex = 16;
            this.cmbBaud.Text = "9600";
            this.cmbBaud.SelectedIndexChanged += new System.EventHandler(this.cmbBaud_SelectedIndexChanged);
            // 
            // labelBuffer
            // 
            this.labelBuffer.AutoSize = true;
            this.labelBuffer.Location = new System.Drawing.Point(13, 117);
            this.labelBuffer.Name = "labelBuffer";
            this.labelBuffer.Size = new System.Drawing.Size(59, 13);
            this.labelBuffer.TabIndex = 15;
            this.labelBuffer.Text = "Buffer size:";
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Location = new System.Drawing.Point(13, 96);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(52, 13);
            this.labelData.TabIndex = 14;
            this.labelData.Text = "Data bits:";
            // 
            // labelCOM
            // 
            this.labelCOM.AutoSize = true;
            this.labelCOM.Location = new System.Drawing.Point(13, 74);
            this.labelCOM.Name = "labelCOM";
            this.labelCOM.Size = new System.Drawing.Size(55, 13);
            this.labelCOM.TabIndex = 13;
            this.labelCOM.Text = "COM port:";
            // 
            // labelBaud
            // 
            this.labelBaud.AutoSize = true;
            this.labelBaud.Location = new System.Drawing.Point(12, 51);
            this.labelBaud.Name = "labelBaud";
            this.labelBaud.Size = new System.Drawing.Size(56, 13);
            this.labelBaud.TabIndex = 12;
            this.labelBaud.Text = "Baud rate:";
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
            // cmbPort
            // 
            this.cmbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(14, 17);
            this.cmbPort.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(115, 26);
            this.cmbPort.TabIndex = 2;
            this.cmbPort.Text = "Select a port...";
            // 
            // arduinoPort
            // 
            this.arduinoPort.PortName = "COM8";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPhase);
            this.tabControl.Controls.Add(this.tabMagnitude);
            this.tabControl.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.tabControl.ItemSize = new System.Drawing.Size(100, 30);
            this.tabControl.Location = new System.Drawing.Point(8, 197);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1029, 581);
            this.tabControl.TabIndex = 5;
            // 
            // tabPhase
            // 
            this.tabPhase.Controls.Add(this.chartPhase);
            this.tabPhase.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabPhase.Location = new System.Drawing.Point(4, 34);
            this.tabPhase.Margin = new System.Windows.Forms.Padding(2);
            this.tabPhase.Name = "tabPhase";
            this.tabPhase.Padding = new System.Windows.Forms.Padding(2);
            this.tabPhase.Size = new System.Drawing.Size(1021, 543);
            this.tabPhase.TabIndex = 0;
            this.tabPhase.Text = "Phase";
            this.tabPhase.UseVisualStyleBackColor = true;
            // 
            // tabMagnitude
            // 
            this.tabMagnitude.Controls.Add(this.chartMagnitude);
            this.tabMagnitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabMagnitude.Location = new System.Drawing.Point(4, 34);
            this.tabMagnitude.Margin = new System.Windows.Forms.Padding(2);
            this.tabMagnitude.Name = "tabMagnitude";
            this.tabMagnitude.Padding = new System.Windows.Forms.Padding(2);
            this.tabMagnitude.Size = new System.Drawing.Size(1021, 543);
            this.tabMagnitude.TabIndex = 1;
            this.tabMagnitude.Text = "Magnitude";
            this.tabMagnitude.UseVisualStyleBackColor = true;
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
            this.dataView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.dataView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeHeader,
            this.valueHeader});
            this.dataView.FullRowSelect = true;
            this.dataView.GridLines = true;
            this.dataView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dataView.HideSelection = false;
            this.dataView.LabelWrap = false;
            this.dataView.Location = new System.Drawing.Point(1042, 231);
            this.dataView.Name = "dataView";
            this.dataView.Size = new System.Drawing.Size(175, 547);
            this.dataView.TabIndex = 7;
            this.dataView.UseCompatibleStateImageBehavior = false;
            this.dataView.View = System.Windows.Forms.View.Details;
            // 
            // timeHeader
            // 
            this.timeHeader.Text = "Time";
            this.timeHeader.Width = 40;
            // 
            // valueHeader
            // 
            this.valueHeader.Text = "Value";
            this.valueHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valueHeader.Width = 114;
            // 
            // buttonSample
            // 
            this.buttonSample.Enabled = false;
            this.buttonSample.Location = new System.Drawing.Point(1042, 784);
            this.buttonSample.Name = "buttonSample";
            this.buttonSample.Size = new System.Drawing.Size(175, 35);
            this.buttonSample.TabIndex = 8;
            this.buttonSample.Text = "Plot Data";
            this.buttonSample.UseVisualStyleBackColor = true;
            this.buttonSample.Click += new System.EventHandler(this.buttonData_Click);
            // 
            // buttonSaveChart
            // 
            this.buttonSaveChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.buttonSaveChart.Location = new System.Drawing.Point(874, 784);
            this.buttonSaveChart.Name = "buttonSaveChart";
            this.buttonSaveChart.Size = new System.Drawing.Size(159, 35);
            this.buttonSaveChart.TabIndex = 9;
            this.buttonSaveChart.Text = "Save Current Chart";
            this.buttonSaveChart.UseVisualStyleBackColor = true;
            this.buttonSaveChart.Click += new System.EventHandler(this.buttonSaveChart_Click);
            // 
            // chartPhase
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPhase.ChartAreas.Add(chartArea1);
            legend1.Name = "Phase";
            this.chartPhase.Legends.Add(legend1);
            this.chartPhase.Location = new System.Drawing.Point(5, 5);
            this.chartPhase.Name = "chartPhase";
            this.chartPhase.Size = new System.Drawing.Size(1011, 533);
            this.chartPhase.TabIndex = 1;
            this.chartPhase.Text = "Phase Chart";
            title1.Name = "Title1";
            title1.Text = "Phase Chart";
            this.chartPhase.Titles.Add(title1);
            // 
            // chartMagnitude
            // 
            chartArea2.Name = "ChartArea1";
            this.chartMagnitude.ChartAreas.Add(chartArea2);
            legend2.Name = "Magnitude";
            this.chartMagnitude.Legends.Add(legend2);
            this.chartMagnitude.Location = new System.Drawing.Point(5, 5);
            this.chartMagnitude.Name = "chartMagnitude";
            this.chartMagnitude.Size = new System.Drawing.Size(1011, 533);
            this.chartMagnitude.TabIndex = 0;
            this.chartMagnitude.Text = "Magnitude Chart";
            title2.Name = "Title1";
            title2.Text = "Magnitude Chart";
            this.chartMagnitude.Titles.Add(title2);
            // 
            // homeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 854);
            this.Controls.Add(this.buttonSaveChart);
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
            this.tabPhase.ResumeLayout(false);
            this.tabMagnitude.ResumeLayout(false);
            this.connectionInfo.ResumeLayout(false);
            this.connectionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMagnitude)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxStart;
        private System.IO.Ports.SerialPort arduinoPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPhase;
        private System.Windows.Forms.TabPage tabMagnitude;
        private System.Windows.Forms.StatusStrip connectionInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolstripStatus;
        private System.Windows.Forms.ListView dataView;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button buttonSample;
        private System.Windows.Forms.ToolStripStatusLabel connectionCOMInfo;
        private System.Windows.Forms.Button buttonRefreshCOM;
        private System.Windows.Forms.Button buttonSaveChart;
        private System.Windows.Forms.ColumnHeader timeHeader;
        private System.Windows.Forms.ColumnHeader valueHeader;
        private System.Windows.Forms.Label labelBuffer;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label labelCOM;
        private System.Windows.Forms.Label labelBaud;
        private System.Windows.Forms.ComboBox cmbBaud;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhase;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMagnitude;
    }
}

