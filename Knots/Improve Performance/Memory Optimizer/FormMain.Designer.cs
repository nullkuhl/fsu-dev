using System.Resources;
using System.Globalization;
using System.Threading;

namespace MemoryOptimizer
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("MemoryOptimizer.Resources",
			System.Reflection.Assembly.GetExecutingAssembly());

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
		void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClearClipboard = new System.Windows.Forms.Button();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.lblMemoryUsage = new System.Windows.Forms.Label();
            this.lblCPUUsage = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpOptimization = new System.Windows.Forms.TabPage();
            this.grbMemory = new System.Windows.Forms.GroupBox();
            this.prbMemory = new System.Windows.Forms.ProgressBar();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.trackBarMemoryAmount = new System.Windows.Forms.TrackBar();
            this.lblMemory = new System.Windows.Forms.Label();
            this.lblSelect = new System.Windows.Forms.Label();
            this.grbResources = new System.Windows.Forms.GroupBox();
            this.lblSlash = new System.Windows.Forms.Label();
            this.lblMemoryTotal = new System.Windows.Forms.Label();
            this.lblTotalMemoryUsage = new System.Windows.Forms.Label();
            this.chtMemory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbl0 = new System.Windows.Forms.Label();
            this.lbl100 = new System.Windows.Forms.Label();
            this.prbCPU = new MemoryOptimizer.VerticalProgressBar();
            this.grbClipboard = new System.Windows.Forms.GroupBox();
            this.lblClearClipboard = new System.Windows.Forms.Label();
            this.tbpOptions = new System.Windows.Forms.TabPage();
            this.chbLoadOnStartup = new System.Windows.Forms.CheckBox();
            this.grbSettings = new System.Windows.Forms.GroupBox();
            this.nudOptimizeIfCPU = new System.Windows.Forms.NumericUpDown();
            this.nudIncreaseMemory = new System.Windows.Forms.NumericUpDown();
            this.nudOptimizeIfMemory = new System.Windows.Forms.NumericUpDown();
            this.lblOptimizeIf = new System.Windows.Forms.Label();
            this.lblIncreaseMemory = new System.Windows.Forms.Label();
            this.lblOptimize = new System.Windows.Forms.Label();
            this.btnRecommended = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwMemory = new System.ComponentModel.BackgroundWorker();
            this.bgwCounter = new System.ComponentModel.BackgroundWorker();
            this.prsMain = new System.Diagnostics.Process();
            this.ucBottom = new MemoryOptimizer.BottomControl();
            this.ucTop = new MemoryOptimizer.TopControl();
            this.tcMain.SuspendLayout();
            this.tbpOptimization.SuspendLayout();
            this.grbMemory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMemoryAmount)).BeginInit();
            this.grbResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtMemory)).BeginInit();
            this.grbClipboard.SuspendLayout();
            this.tbpOptions.SuspendLayout();
            this.grbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOptimizeIfCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIncreaseMemory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOptimizeIfMemory)).BeginInit();
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(111, 79);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 0;
            // 
            // btnClearClipboard
            // 
            this.btnClearClipboard.Location = new System.Drawing.Point(256, 14);
            this.btnClearClipboard.Name = "btnClearClipboard";
            this.btnClearClipboard.Size = new System.Drawing.Size(167, 30);
            this.btnClearClipboard.TabIndex = 1;
            this.btnClearClipboard.Text = "Clear clipboard";
            this.btnClearClipboard.UseVisualStyleBackColor = true;
            this.btnClearClipboard.Click += new System.EventHandler(this.btnClearClipboard_Click);
            // 
            // tmrMain
            // 
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 1000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // lblMemoryUsage
            // 
            this.lblMemoryUsage.AutoSize = true;
            this.lblMemoryUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblMemoryUsage.Location = new System.Drawing.Point(267, 23);
            this.lblMemoryUsage.Name = "lblMemoryUsage";
            this.lblMemoryUsage.Size = new System.Drawing.Size(50, 13);
            this.lblMemoryUsage.TabIndex = 3;
            this.lblMemoryUsage.Text = "0000 MB";
            // 
            // lblCPUUsage
            // 
            this.lblCPUUsage.Location = new System.Drawing.Point(13, 21);
            this.lblCPUUsage.Name = "lblCPUUsage";
            this.lblCPUUsage.Size = new System.Drawing.Size(142, 16);
            this.lblCPUUsage.TabIndex = 4;
            this.lblCPUUsage.Text = "CPU Usage";
            this.lblCPUUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpOptimization);
            this.tcMain.Controls.Add(this.tbpOptions);
            this.tcMain.Location = new System.Drawing.Point(1, 68);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(449, 386);
            this.tcMain.TabIndex = 6;
            // 
            // tbpOptimization
            // 
            this.tbpOptimization.Controls.Add(this.grbMemory);
            this.tbpOptimization.Controls.Add(this.grbResources);
            this.tbpOptimization.Controls.Add(this.grbClipboard);
            this.tbpOptimization.Location = new System.Drawing.Point(4, 22);
            this.tbpOptimization.Name = "tbpOptimization";
            this.tbpOptimization.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOptimization.Size = new System.Drawing.Size(441, 360);
            this.tbpOptimization.TabIndex = 0;
            this.tbpOptimization.Text = "Optimization";
            this.tbpOptimization.UseVisualStyleBackColor = true;
            // 
            // grbMemory
            // 
            this.grbMemory.Controls.Add(this.prbMemory);
            this.grbMemory.Controls.Add(this.btnOptimize);
            this.grbMemory.Controls.Add(this.trackBarMemoryAmount);
            this.grbMemory.Controls.Add(this.lblMemory);
            this.grbMemory.Controls.Add(this.lblSelect);
            this.grbMemory.Location = new System.Drawing.Point(6, 159);
            this.grbMemory.Name = "grbMemory";
            this.grbMemory.Size = new System.Drawing.Size(429, 136);
            this.grbMemory.TabIndex = 8;
            this.grbMemory.TabStop = false;
            this.grbMemory.Text = "Memory Optimizer";
            // 
            // prbMemory
            // 
            this.prbMemory.Location = new System.Drawing.Point(10, 100);
            this.prbMemory.Name = "prbMemory";
            this.prbMemory.Size = new System.Drawing.Size(317, 24);
            this.prbMemory.TabIndex = 4;
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(333, 100);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(90, 24);
            this.btnOptimize.TabIndex = 3;
            this.btnOptimize.Text = "Optimize";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.btnOptimize_Click);
            // 
            // trackBarMemoryAmount
            // 
            this.trackBarMemoryAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(238)))));
            this.trackBarMemoryAmount.Location = new System.Drawing.Point(10, 73);
            this.trackBarMemoryAmount.Maximum = 100;
            this.trackBarMemoryAmount.Minimum = 100;
            this.trackBarMemoryAmount.MinimumSize = new System.Drawing.Size(0, 55);
            this.trackBarMemoryAmount.Name = "trackBarMemoryAmount";
            this.trackBarMemoryAmount.Size = new System.Drawing.Size(413, 45);
            this.trackBarMemoryAmount.TabIndex = 2;
            this.trackBarMemoryAmount.TickFrequency = 0;
            this.trackBarMemoryAmount.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMemoryAmount.Value = 100;
            this.trackBarMemoryAmount.ValueChanged += new System.EventHandler(this.trackBarMemoryAmount_ValueChanged);
            // 
            // lblMemory
            // 
            this.lblMemory.AutoSize = true;
            this.lblMemory.Location = new System.Drawing.Point(7, 57);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(89, 13);
            this.lblMemory.TabIndex = 1;
            this.lblMemory.Text = "Memory Amount :";
            // 
            // lblSelect
            // 
            this.lblSelect.Location = new System.Drawing.Point(7, 16);
            this.lblSelect.MinimumSize = new System.Drawing.Size(280, 0);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(416, 41);
            this.lblSelect.TabIndex = 0;
            this.lblSelect.Text = "Select the amount of memory you want to free up then click Optimize.";
            this.lblSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grbResources
            // 
            this.grbResources.BackColor = System.Drawing.Color.Transparent;
            this.grbResources.Controls.Add(this.lblSlash);
            this.grbResources.Controls.Add(this.lblMemoryTotal);
            this.grbResources.Controls.Add(this.lblTotalMemoryUsage);
            this.grbResources.Controls.Add(this.chtMemory);
            this.grbResources.Controls.Add(this.lbl0);
            this.grbResources.Controls.Add(this.lbl100);
            this.grbResources.Controls.Add(this.prbCPU);
            this.grbResources.Controls.Add(this.lblMemoryUsage);
            this.grbResources.Controls.Add(this.lblCPUUsage);
            this.grbResources.Controls.Add(this.lblStatus);
            this.grbResources.Location = new System.Drawing.Point(6, 6);
            this.grbResources.Name = "grbResources";
            this.grbResources.Size = new System.Drawing.Size(429, 147);
            this.grbResources.TabIndex = 7;
            this.grbResources.TabStop = false;
            this.grbResources.Text = "Resources Usage";
            // 
            // lblSlash
            // 
            this.lblSlash.AutoSize = true;
            this.lblSlash.Location = new System.Drawing.Point(313, 23);
            this.lblSlash.Name = "lblSlash";
            this.lblSlash.Size = new System.Drawing.Size(12, 13);
            this.lblSlash.TabIndex = 11;
            this.lblSlash.Text = "/";
            // 
            // lblMemoryTotal
            // 
            this.lblMemoryTotal.AutoSize = true;
            this.lblMemoryTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblMemoryTotal.Location = new System.Drawing.Point(323, 23);
            this.lblMemoryTotal.Name = "lblMemoryTotal";
            this.lblMemoryTotal.Size = new System.Drawing.Size(50, 13);
            this.lblMemoryTotal.TabIndex = 8;
            this.lblMemoryTotal.Text = "0000 MB";
            // 
            // lblTotalMemoryUsage
            // 
            this.lblTotalMemoryUsage.AutoSize = true;
            this.lblTotalMemoryUsage.Location = new System.Drawing.Point(161, 23);
            this.lblTotalMemoryUsage.Name = "lblTotalMemoryUsage";
            this.lblTotalMemoryUsage.Size = new System.Drawing.Size(108, 13);
            this.lblTotalMemoryUsage.TabIndex = 10;
            this.lblTotalMemoryUsage.Text = "Total Memory Usage:";
            // 
            // chtMemory
            // 
            this.chtMemory.BackColor = System.Drawing.SystemColors.Window;
            this.chtMemory.BorderlineColor = System.Drawing.SystemColors.WindowText;
            this.chtMemory.BorderlineWidth = 0;
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.Area3DStyle.Inclination = 50;
            chartArea1.Area3DStyle.Rotation = 40;
            chartArea1.Area3DStyle.WallWidth = 20;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.ForestGreen;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX.MajorGrid.Interval = 2D;
            chartArea1.AxisX.MajorGrid.IntervalOffset = 0D;
            chartArea1.AxisX.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(0)))));
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(0)))));
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisX2.LabelStyle.Enabled = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGreen;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(0)))));
            chartArea1.AxisY2.LabelStyle.Enabled = false;
            chartArea1.AxisY2.LineWidth = 0;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.BorderWidth = 0;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 100F;
            chartArea1.InnerPlotPosition.Width = 100F;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.chtMemory.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Enabled = false;
            legend1.ForeColor = System.Drawing.Color.White;
            legend1.HeaderSeparatorColor = System.Drawing.Color.White;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.White;
            legend1.ItemColumnSpacing = 0;
            legend1.Name = "Legend1";
            legend1.TitleForeColor = System.Drawing.Color.White;
            legend1.TitleSeparatorColor = System.Drawing.Color.White;
            this.chtMemory.Legends.Add(legend1);
            this.chtMemory.Location = new System.Drawing.Point(164, 46);
            this.chtMemory.Margin = new System.Windows.Forms.Padding(0);
            this.chtMemory.Name = "chtMemory";
            this.chtMemory.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.CustomProperties = "LineTension=0.2";
            series1.IsVisibleInLegend = false;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.MarkerBorderWidth = 0;
            series1.MarkerSize = 1;
            series1.Name = "Series1";
            this.chtMemory.Series.Add(series1);
            this.chtMemory.Size = new System.Drawing.Size(259, 87);
            this.chtMemory.TabIndex = 0;
            this.chtMemory.Text = "chart1";
            // 
            // lbl0
            // 
            this.lbl0.AutoSize = true;
            this.lbl0.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl0.Location = new System.Drawing.Point(20, 123);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(29, 12);
            this.lbl0.TabIndex = 7;
            this.lbl0.Text = "0 %   -";
            // 
            // lbl100
            // 
            this.lbl100.AutoSize = true;
            this.lbl100.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl100.Location = new System.Drawing.Point(17, 41);
            this.lbl100.Name = "lbl100";
            this.lbl100.Size = new System.Drawing.Size(35, 12);
            this.lbl100.TabIndex = 6;
            this.lbl100.Text = "100 % -";
            // 
            // prbCPU
            // 
            this.prbCPU.Location = new System.Drawing.Point(58, 46);
            this.prbCPU.Name = "prbCPU";
            this.prbCPU.Size = new System.Drawing.Size(47, 87);
            this.prbCPU.TabIndex = 0;
            // 
            // grbClipboard
            // 
            this.grbClipboard.Controls.Add(this.lblClearClipboard);
            this.grbClipboard.Controls.Add(this.btnClearClipboard);
            this.grbClipboard.Location = new System.Drawing.Point(6, 301);
            this.grbClipboard.Name = "grbClipboard";
            this.grbClipboard.Size = new System.Drawing.Size(429, 54);
            this.grbClipboard.TabIndex = 6;
            this.grbClipboard.TabStop = false;
            this.grbClipboard.Text = "Clear Clipboard";
            // 
            // lblClearClipboard
            // 
            this.lblClearClipboard.Location = new System.Drawing.Point(10, 14);
            this.lblClearClipboard.Name = "lblClearClipboard";
            this.lblClearClipboard.Size = new System.Drawing.Size(240, 35);
            this.lblClearClipboard.TabIndex = 2;
            this.lblClearClipboard.Text = "Clear Clipboard so that the Copied things will be cleared.";
            this.lblClearClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbpOptions
            // 
            this.tbpOptions.Controls.Add(this.chbLoadOnStartup);
            this.tbpOptions.Controls.Add(this.grbSettings);
            this.tbpOptions.Location = new System.Drawing.Point(4, 22);
            this.tbpOptions.Name = "tbpOptions";
            this.tbpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOptions.Size = new System.Drawing.Size(441, 360);
            this.tbpOptions.TabIndex = 1;
            this.tbpOptions.Text = "Options";
            this.tbpOptions.UseVisualStyleBackColor = true;
            // 
            // chbLoadOnStartup
            // 
            this.chbLoadOnStartup.AutoSize = true;
            this.chbLoadOnStartup.Location = new System.Drawing.Point(12, 200);
            this.chbLoadOnStartup.Name = "chbLoadOnStartup";
            this.chbLoadOnStartup.Size = new System.Drawing.Size(149, 17);
            this.chbLoadOnStartup.TabIndex = 1;
            this.chbLoadOnStartup.Text = "Load on Windows Startup";
            this.chbLoadOnStartup.UseVisualStyleBackColor = true;
            this.chbLoadOnStartup.CheckedChanged += new System.EventHandler(this.chbLoadOnStartup_CheckedChanged);
            // 
            // grbSettings
            // 
            this.grbSettings.Controls.Add(this.nudOptimizeIfCPU);
            this.grbSettings.Controls.Add(this.nudIncreaseMemory);
            this.grbSettings.Controls.Add(this.nudOptimizeIfMemory);
            this.grbSettings.Controls.Add(this.lblOptimizeIf);
            this.grbSettings.Controls.Add(this.lblIncreaseMemory);
            this.grbSettings.Controls.Add(this.lblOptimize);
            this.grbSettings.Controls.Add(this.btnRecommended);
            this.grbSettings.Location = new System.Drawing.Point(6, 6);
            this.grbSettings.Name = "grbSettings";
            this.grbSettings.Size = new System.Drawing.Size(428, 188);
            this.grbSettings.TabIndex = 0;
            this.grbSettings.TabStop = false;
            this.grbSettings.Text = "Auto Optimization Settings";
            // 
            // nudOptimizeIfCPU
            // 
            this.nudOptimizeIfCPU.Location = new System.Drawing.Point(335, 106);
            this.nudOptimizeIfCPU.Name = "nudOptimizeIfCPU";
            this.nudOptimizeIfCPU.Size = new System.Drawing.Size(75, 20);
            this.nudOptimizeIfCPU.TabIndex = 8;
            // 
            // nudIncreaseMemory
            // 
            this.nudIncreaseMemory.Location = new System.Drawing.Point(335, 67);
            this.nudIncreaseMemory.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudIncreaseMemory.Name = "nudIncreaseMemory";
            this.nudIncreaseMemory.Size = new System.Drawing.Size(75, 20);
            this.nudIncreaseMemory.TabIndex = 7;
            // 
            // nudOptimizeIfMemory
            // 
            this.nudOptimizeIfMemory.Location = new System.Drawing.Point(335, 29);
            this.nudOptimizeIfMemory.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOptimizeIfMemory.Name = "nudOptimizeIfMemory";
            this.nudOptimizeIfMemory.Size = new System.Drawing.Size(75, 20);
            this.nudOptimizeIfMemory.TabIndex = 6;
            // 
            // lblOptimizeIf
            // 
            this.lblOptimizeIf.Location = new System.Drawing.Point(9, 98);
            this.lblOptimizeIf.MaximumSize = new System.Drawing.Size(320, 34);
            this.lblOptimizeIf.MinimumSize = new System.Drawing.Size(180, 0);
            this.lblOptimizeIf.Name = "lblOptimizeIf";
            this.lblOptimizeIf.Size = new System.Drawing.Size(320, 34);
            this.lblOptimizeIf.TabIndex = 5;
            this.lblOptimizeIf.Text = "Only Optimize if CPU-Usage is below: (%)";
            this.lblOptimizeIf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIncreaseMemory
            // 
            this.lblIncreaseMemory.Location = new System.Drawing.Point(21, 66);
            this.lblIncreaseMemory.Name = "lblIncreaseMemory";
            this.lblIncreaseMemory.Size = new System.Drawing.Size(308, 18);
            this.lblIncreaseMemory.TabIndex = 4;
            this.lblIncreaseMemory.Text = "Increase free memory to: (MB)";
            this.lblIncreaseMemory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOptimize
            // 
            this.lblOptimize.Location = new System.Drawing.Point(9, 29);
            this.lblOptimize.MinimumSize = new System.Drawing.Size(190, 0);
            this.lblOptimize.Name = "lblOptimize";
            this.lblOptimize.Size = new System.Drawing.Size(320, 20);
            this.lblOptimize.TabIndex = 3;
            this.lblOptimize.Text = "Optimize automatically when free memory at: (MB)";
            this.lblOptimize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRecommended
            // 
            this.btnRecommended.Location = new System.Drawing.Point(273, 151);
            this.btnRecommended.Name = "btnRecommended";
            this.btnRecommended.Size = new System.Drawing.Size(137, 23);
            this.btnRecommended.TabIndex = 2;
            this.btnRecommended.Text = "Recommended Settings";
            this.btnRecommended.UseVisualStyleBackColor = true;
            this.btnRecommended.Click += new System.EventHandler(this.btnRecommended_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(290, 460);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(371, 460);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // niMain
            // 
            this.niMain.BalloonTipText = "Memory optimizer is still running";
            this.niMain.ContextMenuStrip = this.cmsMain;
            this.niMain.Icon = ((System.Drawing.Icon)(resources.GetObject("niMain.Icon")));
            this.niMain.Visible = true;
            this.niMain.DoubleClick += new System.EventHandler(this.niMain_DoubleClick);
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainWindowToolStripMenuItem,
            this.clearClipboardToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.cmsMain.Name = "contextMenuStrip1";
            this.cmsMain.Size = new System.Drawing.Size(181, 70);
            // 
            // showMainWindowToolStripMenuItem
            // 
            this.showMainWindowToolStripMenuItem.Name = "showMainWindowToolStripMenuItem";
            this.showMainWindowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showMainWindowToolStripMenuItem.Text = "Show Main Window";
            this.showMainWindowToolStripMenuItem.Click += new System.EventHandler(this.showMainWindowToolStripMenuItem_Click);
            // 
            // clearClipboardToolStripMenuItem
            // 
            this.clearClipboardToolStripMenuItem.Name = "clearClipboardToolStripMenuItem";
            this.clearClipboardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearClipboardToolStripMenuItem.Text = "Clear Clipboard";
            this.clearClipboardToolStripMenuItem.Click += new System.EventHandler(this.clearClipboardToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // prsMain
            // 
            this.prsMain.StartInfo.Domain = "";
            this.prsMain.StartInfo.LoadUserProfile = false;
            this.prsMain.StartInfo.Password = null;
            this.prsMain.StartInfo.StandardErrorEncoding = null;
            this.prsMain.StartInfo.StandardOutputEncoding = null;
            this.prsMain.StartInfo.UserName = "";
            this.prsMain.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            this.prsMain.SynchronizingObject = this;
            this.prsMain.Exited += new System.EventHandler(this.prsMain_Exited);
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 488);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(450, 31);
            this.ucBottom.TabIndex = 10;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(450, 64);
            this.ucTop.TabIndex = 9;
            this.ucTop.Load += new System.EventHandler(this.ucTop_Load);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(450, 519);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Optimizer";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tcMain.ResumeLayout(false);
            this.tbpOptimization.ResumeLayout(false);
            this.grbMemory.ResumeLayout(false);
            this.grbMemory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMemoryAmount)).EndInit();
            this.grbResources.ResumeLayout(false);
            this.grbResources.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtMemory)).EndInit();
            this.grbClipboard.ResumeLayout(false);
            this.tbpOptions.ResumeLayout(false);
            this.tbpOptions.PerformLayout();
            this.grbSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudOptimizeIfCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIncreaseMemory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOptimizeIfMemory)).EndInit();
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.Label lblStatus;
		System.Windows.Forms.Button btnClearClipboard;
		MemoryOptimizer.VerticalProgressBar prbCPU;
		System.Windows.Forms.Timer tmrMain;
		System.Windows.Forms.Label lblMemoryUsage;
		System.Windows.Forms.Label lblCPUUsage;
		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage tbpOptimization;
		System.Windows.Forms.GroupBox grbClipboard;
		System.Windows.Forms.TabPage tbpOptions;
		System.Windows.Forms.GroupBox grbResources;
		System.Windows.Forms.Label lblClearClipboard;
		System.Windows.Forms.Label lbl0;
		System.Windows.Forms.Label lbl100;
		System.Windows.Forms.GroupBox grbMemory;
		System.Windows.Forms.Label lblSelect;
		System.Windows.Forms.TrackBar trackBarMemoryAmount;
		System.Windows.Forms.Label lblMemory;
		System.Windows.Forms.Button btnOptimize;
		System.Windows.Forms.Label lblMemoryTotal;
		System.Windows.Forms.Button btnOK;
		System.Windows.Forms.Button btnClose;
		System.Windows.Forms.GroupBox grbSettings;
		System.Windows.Forms.CheckBox chbLoadOnStartup;
		System.Windows.Forms.Label lblOptimizeIf;
		System.Windows.Forms.Label lblIncreaseMemory;
		System.Windows.Forms.Label lblOptimize;
		System.Windows.Forms.Button btnRecommended;
		System.Windows.Forms.NumericUpDown nudOptimizeIfCPU;
		System.Windows.Forms.NumericUpDown nudIncreaseMemory;
		System.Windows.Forms.NumericUpDown nudOptimizeIfMemory;
		System.Windows.Forms.NotifyIcon niMain;
		System.Windows.Forms.ContextMenuStrip cmsMain;
		System.Windows.Forms.ToolStripMenuItem showMainWindowToolStripMenuItem;
		System.Windows.Forms.ToolStripMenuItem clearClipboardToolStripMenuItem;
		System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		System.Windows.Forms.ProgressBar prbMemory;
		System.ComponentModel.BackgroundWorker bgwMemory;
		System.ComponentModel.BackgroundWorker bgwCounter;
		System.Diagnostics.Process prsMain;
		System.Windows.Forms.DataVisualization.Charting.Chart chtMemory;
		System.Windows.Forms.Label lblTotalMemoryUsage;
		System.Windows.Forms.Label lblSlash;
		BottomControl ucBottom;
		TopControl ucTop;


	}
}

