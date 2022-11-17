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
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(homeForm));
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonThread = new System.Windows.Forms.Button();
            this.lblThreadName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.cmbBaud = new System.Windows.Forms.ComboBox();
            this.labelBuffer = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.labelCOM = new System.Windows.Forms.Label();
            this.labelBaud = new System.Windows.Forms.Label();
            this.buttonRefreshCOM = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPhase = new System.Windows.Forms.TabPage();
            this.chartPhase = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabMagnitude = new System.Windows.Forms.TabPage();
            this.chartMagnitude = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.connectionInfo = new System.Windows.Forms.StatusStrip();
            this.connectionCOMInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.threadInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.barThread = new System.Windows.Forms.ToolStripProgressBar();
            this.dataView = new System.Windows.Forms.ListView();
            this.timeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valuePhase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueMagnitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSaveChart = new System.Windows.Forms.Button();
            this.buttonAcquire = new System.Windows.Forms.Button();
            this.arduinoPort = new System.IO.Ports.SerialPort(this.components);
            this.groupBoxStart.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPhase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhase)).BeginInit();
            this.tabMagnitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMagnitude)).BeginInit();
            this.connectionInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStart
            // 
            this.groupBoxStart.Controls.Add(this.groupBox2);
            this.groupBoxStart.Controls.Add(this.groupBox1);
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
            this.groupBoxStart.Text = "Connection Handling";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbLog);
            this.groupBox2.Location = new System.Drawing.Point(740, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 150);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logging";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(6, 16);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(444, 128);
            this.rtbLog.TabIndex = 25;
            this.rtbLog.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonThread);
            this.groupBox1.Controls.Add(this.lblThreadName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Location = new System.Drawing.Point(346, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 150);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threading Debugging";
            // 
            // buttonThread
            // 
            this.buttonThread.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.buttonThread.Location = new System.Drawing.Point(6, 110);
            this.buttonThread.Name = "buttonThread";
            this.buttonThread.Size = new System.Drawing.Size(376, 34);
            this.buttonThread.TabIndex = 18;
            this.buttonThread.Text = "Start";
            this.buttonThread.UseVisualStyleBackColor = true;
            this.buttonThread.Click += new System.EventHandler(this.buttonThread_Click);
            // 
            // lblThreadName
            // 
            this.lblThreadName.AutoSize = true;
            this.lblThreadName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblThreadName.Location = new System.Drawing.Point(154, 16);
            this.lblThreadName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblThreadName.Name = "lblThreadName";
            this.lblThreadName.Size = new System.Drawing.Size(16, 18);
            this.lblThreadName.TabIndex = 24;
            this.lblThreadName.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Job timer:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblTime.Location = new System.Drawing.Point(78, 16);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(16, 18);
            this.lblTime.TabIndex = 20;
            this.lblTime.Text = "0";
            // 
            // cmbBaud
            // 
            this.cmbBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbBaud.Size = new System.Drawing.Size(77, 26);
            this.cmbBaud.TabIndex = 16;
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
            this.buttonRefreshCOM.Location = new System.Drawing.Point(316, 17);
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
            this.saveButton.Size = new System.Drawing.Size(326, 32);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save Data";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnConnect.Location = new System.Drawing.Point(214, 17);
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
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbPort.Location = new System.Drawing.Point(14, 17);
            this.cmbPort.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(115, 26);
            this.cmbPort.Sorted = true;
            this.cmbPort.TabIndex = 2;
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
            this.tabControl.Size = new System.Drawing.Size(955, 581);
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
            this.tabPhase.Size = new System.Drawing.Size(947, 543);
            this.tabPhase.TabIndex = 0;
            this.tabPhase.Text = "Phase";
            this.tabPhase.UseVisualStyleBackColor = true;
            // 
            // chartPhase
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPhase.ChartAreas.Add(chartArea1);
            legend1.Name = "Voltage / Time";
            this.chartPhase.Legends.Add(legend1);
            this.chartPhase.Location = new System.Drawing.Point(5, 5);
            this.chartPhase.Name = "chartPhase";
            this.chartPhase.Size = new System.Drawing.Size(937, 533);
            this.chartPhase.TabIndex = 1;
            this.chartPhase.Text = "Phase Chart";
            title1.Name = "Title1";
            title1.Text = "Voltage Test Chart";
            title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title2.Name = "Title2";
            title2.Text = "Time";
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title3.Name = "Title3";
            title3.Text = "Voltage";
            this.chartPhase.Titles.Add(title1);
            this.chartPhase.Titles.Add(title2);
            this.chartPhase.Titles.Add(title3);
            // 
            // tabMagnitude
            // 
            this.tabMagnitude.Controls.Add(this.chartMagnitude);
            this.tabMagnitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabMagnitude.Location = new System.Drawing.Point(4, 34);
            this.tabMagnitude.Margin = new System.Windows.Forms.Padding(2);
            this.tabMagnitude.Name = "tabMagnitude";
            this.tabMagnitude.Padding = new System.Windows.Forms.Padding(2);
            this.tabMagnitude.Size = new System.Drawing.Size(947, 543);
            this.tabMagnitude.TabIndex = 1;
            this.tabMagnitude.Text = "Magnitude";
            this.tabMagnitude.UseVisualStyleBackColor = true;
            // 
            // chartMagnitude
            // 
            chartArea2.Name = "ChartArea1";
            this.chartMagnitude.ChartAreas.Add(chartArea2);
            legend2.Name = "Voltage / Time";
            this.chartMagnitude.Legends.Add(legend2);
            this.chartMagnitude.Location = new System.Drawing.Point(5, 5);
            this.chartMagnitude.Name = "chartMagnitude";
            this.chartMagnitude.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            this.chartMagnitude.Size = new System.Drawing.Size(937, 533);
            this.chartMagnitude.TabIndex = 0;
            this.chartMagnitude.Text = "Magnitude Chart";
            title4.Name = "Title1";
            title4.Text = "Magnitude Chart";
            title5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title5.Name = "Title2";
            title5.Text = "Time";
            title6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title6.Name = "Title3";
            title6.Text = "Voltage";
            this.chartMagnitude.Titles.Add(title4);
            this.chartMagnitude.Titles.Add(title5);
            this.chartMagnitude.Titles.Add(title6);
            // 
            // connectionInfo
            // 
            this.connectionInfo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.connectionInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionCOMInfo,
            this.threadInfo,
            this.barThread});
            this.connectionInfo.Location = new System.Drawing.Point(0, 832);
            this.connectionInfo.Name = "connectionInfo";
            this.connectionInfo.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.connectionInfo.Size = new System.Drawing.Size(1227, 22);
            this.connectionInfo.TabIndex = 6;
            this.connectionInfo.Text = "connectionInfo";
            // 
            // connectionCOMInfo
            // 
            this.connectionCOMInfo.Name = "connectionCOMInfo";
            this.connectionCOMInfo.Size = new System.Drawing.Size(109, 17);
            this.connectionCOMInfo.Text = "Connect to a port...";
            // 
            // threadInfo
            // 
            this.threadInfo.Name = "threadInfo";
            this.threadInfo.Size = new System.Drawing.Size(123, 17);
            this.threadInfo.Text = "Thread Running: False";
            // 
            // barThread
            // 
            this.barThread.Name = "barThread";
            this.barThread.Size = new System.Drawing.Size(100, 16);
            this.barThread.Step = 1;
            this.barThread.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // dataView
            // 
            this.dataView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.dataView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeHeader,
            this.valuePhase,
            this.valueMagnitude});
            this.dataView.FullRowSelect = true;
            this.dataView.GridLines = true;
            this.dataView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dataView.HideSelection = false;
            this.dataView.LabelWrap = false;
            this.dataView.Location = new System.Drawing.Point(968, 231);
            this.dataView.Name = "dataView";
            this.dataView.Size = new System.Drawing.Size(249, 547);
            this.dataView.TabIndex = 7;
            this.dataView.UseCompatibleStateImageBehavior = false;
            this.dataView.View = System.Windows.Forms.View.Details;
            // 
            // timeHeader
            // 
            this.timeHeader.Text = "Time (uS)";
            this.timeHeader.Width = 65;
            // 
            // valuePhase
            // 
            this.valuePhase.Text = "Value 1";
            this.valuePhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valuePhase.Width = 90;
            // 
            // valueMagnitude
            // 
            this.valueMagnitude.Text = "Value 2";
            this.valueMagnitude.Width = 90;
            // 
            // buttonSaveChart
            // 
            this.buttonSaveChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.buttonSaveChart.Location = new System.Drawing.Point(800, 784);
            this.buttonSaveChart.Name = "buttonSaveChart";
            this.buttonSaveChart.Size = new System.Drawing.Size(159, 35);
            this.buttonSaveChart.TabIndex = 9;
            this.buttonSaveChart.Text = "Save Current Chart";
            this.buttonSaveChart.UseVisualStyleBackColor = true;
            this.buttonSaveChart.Click += new System.EventHandler(this.buttonSaveChart_Click);
            // 
            // buttonAcquire
            // 
            this.buttonAcquire.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.buttonAcquire.Location = new System.Drawing.Point(968, 784);
            this.buttonAcquire.Name = "buttonAcquire";
            this.buttonAcquire.Size = new System.Drawing.Size(249, 35);
            this.buttonAcquire.TabIndex = 10;
            this.buttonAcquire.Text = "Acquire";
            this.buttonAcquire.UseVisualStyleBackColor = true;
            this.buttonAcquire.Click += new System.EventHandler(this.buttonAcquire_Click);
            // 
            // arduinoPort
            // 
            this.arduinoPort.PortName = "COM7";
            this.arduinoPort.ReadBufferSize = 4095;
            this.arduinoPort.WriteBufferSize = 4096;
            // 
            // homeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1227, 854);
            this.Controls.Add(this.buttonAcquire);
            this.Controls.Add(this.buttonSaveChart);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.connectionInfo);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBoxStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "homeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter Frequency Response Visualizer";
            this.Load += new System.EventHandler(this.homeForm_Load);
            this.groupBoxStart.ResumeLayout(false);
            this.groupBoxStart.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPhase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPhase)).EndInit();
            this.tabMagnitude.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMagnitude)).EndInit();
            this.connectionInfo.ResumeLayout(false);
            this.connectionInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxStart;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPhase;
        private System.Windows.Forms.TabPage tabMagnitude;
        private System.Windows.Forms.StatusStrip connectionInfo;
        private System.Windows.Forms.ListView dataView;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ToolStripStatusLabel connectionCOMInfo;
        private System.Windows.Forms.Button buttonRefreshCOM;
        private System.Windows.Forms.Button buttonSaveChart;
        private System.Windows.Forms.ColumnHeader timeHeader;
        private System.Windows.Forms.ColumnHeader valuePhase;
        private System.Windows.Forms.Label labelBuffer;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label labelCOM;
        private System.Windows.Forms.Label labelBaud;
        private System.Windows.Forms.ComboBox cmbBaud;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhase;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMagnitude;
        private System.Windows.Forms.Button buttonThread;
        private System.Windows.Forms.ToolStripStatusLabel threadInfo;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblThreadName;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAcquire;
        private System.IO.Ports.SerialPort arduinoPort;
        private System.Windows.Forms.ToolStripProgressBar barThread;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader valueMagnitude;
    }
}

