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
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.tabControlPlots = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.groupBoxIO = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.buttonIO = new System.Windows.Forms.Button();
            this.tabControlPlots.SuspendLayout();
            this.groupBoxStart.SuspendLayout();
            this.groupBoxIO.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPlots
            // 
            this.tabControlPlots.Controls.Add(this.tabPage1);
            this.tabControlPlots.Controls.Add(this.tabPage2);
            this.tabControlPlots.Location = new System.Drawing.Point(12, 269);
            this.tabControlPlots.Name = "tabControlPlots";
            this.tabControlPlots.SelectedIndex = 0;
            this.tabControlPlots.Size = new System.Drawing.Size(1145, 720);
            this.tabControlPlots.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1137, 687);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Phase";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1137, 687);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Magnitude";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBoxStart
            // 
            this.groupBoxStart.Controls.Add(this.buttonStop);
            this.groupBoxStart.Controls.Add(this.buttonStart);
            this.groupBoxStart.Location = new System.Drawing.Point(16, 12);
            this.groupBoxStart.Name = "groupBoxStart";
            this.groupBoxStart.Size = new System.Drawing.Size(507, 111);
            this.groupBoxStart.TabIndex = 1;
            this.groupBoxStart.TabStop = false;
            this.groupBoxStart.Text = "Start / Stop";
            // 
            // groupBoxIO
            // 
            this.groupBoxIO.Controls.Add(this.buttonIO);
            this.groupBoxIO.Controls.Add(this.maskedTextBox1);
            this.groupBoxIO.Location = new System.Drawing.Point(16, 129);
            this.groupBoxIO.Name = "groupBoxIO";
            this.groupBoxIO.Size = new System.Drawing.Size(507, 134);
            this.groupBoxIO.TabIndex = 2;
            this.groupBoxIO.TabStop = false;
            this.groupBoxIO.Text = "File Output";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(529, 55);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(624, 208);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data Output:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 993);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1166, 32);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(149, 25);
            this.toolStripStatusLabel1.Text = "statusConnection";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(21, 43);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(200, 45);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(286, 43);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(200, 45);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.maskedTextBox1.Location = new System.Drawing.Point(21, 57);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(390, 28);
            this.maskedTextBox1.TabIndex = 0;
            // 
            // buttonIO
            // 
            this.buttonIO.Location = new System.Drawing.Point(426, 56);
            this.buttonIO.Name = "buttonIO";
            this.buttonIO.Size = new System.Drawing.Size(60, 36);
            this.buttonIO.TabIndex = 6;
            this.buttonIO.Text = "...";
            this.buttonIO.UseVisualStyleBackColor = true;
            // 
            // homeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 1025);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBoxIO);
            this.Controls.Add(this.groupBoxStart);
            this.Controls.Add(this.tabControlPlots);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "homeForm";
            this.Text = "Filter Frequency Response Visualizer " + version;
            this.tabControlPlots.ResumeLayout(false);
            this.groupBoxStart.ResumeLayout(false);
            this.groupBoxIO.ResumeLayout(false);
            this.groupBoxIO.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPlots;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBoxIO;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonIO;
    }
}

