using System.Windows.Forms;
using System;
using System.Threading;
using System.Resources;
using System.Globalization;

namespace ProcessManager
{
	partial class FormProcessManager
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		ResourceManager rm = new ResourceManager("ProcessManager.Resources", typeof(FormProcessManager).Assembly);

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcessManager));
            this.taskmgrnotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.lvcxtmnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mitEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.mitSetPriority = new System.Windows.Forms.ToolStripMenuItem();
            this.mitRealTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mitHigh = new System.Windows.Forms.ToolStripMenuItem();
            this.mitAboveNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mitNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mitBelowNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mitLow = new System.Windows.Forms.ToolStripMenuItem();
            this.stbMain = new System.Windows.Forms.StatusBar();
            this.processcount = new System.Windows.Forms.StatusBarPanel();
            this.cpuUsage = new System.Windows.Forms.StatusBarPanel();
            this.memoryUsage = new System.Windows.Forms.StatusBarPanel();
            this.threadcount = new System.Windows.Forms.StatusBarPanel();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsMain = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnEndProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnBlockProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnUBlck = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnShowDetails = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnGoogleIt = new System.Windows.Forms.ToolStripButton();
            this.lvprocesslist = new System.Windows.Forms.ListView();
            this.procname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPriorty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nonofthreads = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.proccputime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.memusage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.procFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.procuser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.procImageList = new System.Windows.Forms.ImageList(this.components);
            this.detailsImageList = new System.Windows.Forms.ImageList(this.components);
            this.ucTop = new ProcessManager.TopControl();
            this.ucBottom = new ProcessManager.BottomControl();
            this.lvcxtmnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processcount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryUsage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadcount)).BeginInit();
            this.tlsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskmgrnotify
            // 
            this.taskmgrnotify.Text = "Task Manager is in visible Mode";
            this.taskmgrnotify.Visible = true;
            this.taskmgrnotify.Click += new System.EventHandler(this.taskmgrnotify_Click);
            // 
            // lvcxtmnu
            // 
            this.lvcxtmnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitEnd,
            this.mitSetPriority});
            this.lvcxtmnu.Name = "lvcxtmnu";
            this.lvcxtmnu.Size = new System.Drawing.Size(138, 48);
            // 
            // mitEnd
            // 
            this.mitEnd.MergeIndex = 0;
            this.mitEnd.Name = "mitEnd";
            this.mitEnd.Size = new System.Drawing.Size(137, 22);
            this.mitEnd.Text = "End Process";
            this.mitEnd.Click += new System.EventHandler(this.mitEnd_Click);
            // 
            // mitSetPriority
            // 
            this.mitSetPriority.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitRealTime,
            this.mitHigh,
            this.mitAboveNormal,
            this.mitNormal,
            this.mitBelowNormal,
            this.mitLow});
            this.mitSetPriority.MergeIndex = 1;
            this.mitSetPriority.Name = "mitSetPriority";
            this.mitSetPriority.Size = new System.Drawing.Size(137, 22);
            this.mitSetPriority.Text = "Set Priority";
            this.mitSetPriority.DropDownOpening += new System.EventHandler(this.mitSetPriority_Popup);
            // 
            // mitRealTime
            // 
            this.mitRealTime.MergeIndex = 5;
            this.mitRealTime.Name = "mitRealTime";
            this.mitRealTime.Size = new System.Drawing.Size(151, 22);
            this.mitRealTime.Text = "Real Time";
            this.mitRealTime.Click += new System.EventHandler(this.mitRealTime_Click);
            // 
            // mitHigh
            // 
            this.mitHigh.MergeIndex = 0;
            this.mitHigh.Name = "mitHigh";
            this.mitHigh.Size = new System.Drawing.Size(151, 22);
            this.mitHigh.Text = "High";
            this.mitHigh.Click += new System.EventHandler(this.mitHigh_Click);
            // 
            // mitAboveNormal
            // 
            this.mitAboveNormal.MergeIndex = 1;
            this.mitAboveNormal.Name = "mitAboveNormal";
            this.mitAboveNormal.Size = new System.Drawing.Size(151, 22);
            this.mitAboveNormal.Text = "Above Normal";
            this.mitAboveNormal.Click += new System.EventHandler(this.mitAboveNormal_Click);
            // 
            // mitNormal
            // 
            this.mitNormal.MergeIndex = 3;
            this.mitNormal.Name = "mitNormal";
            this.mitNormal.Size = new System.Drawing.Size(151, 22);
            this.mitNormal.Text = "Normal";
            this.mitNormal.Click += new System.EventHandler(this.mitNormal_Click);
            // 
            // mitBelowNormal
            // 
            this.mitBelowNormal.MergeIndex = 2;
            this.mitBelowNormal.Name = "mitBelowNormal";
            this.mitBelowNormal.Size = new System.Drawing.Size(151, 22);
            this.mitBelowNormal.Text = "Below Normal";
            this.mitBelowNormal.Click += new System.EventHandler(this.mitBelowNormal_Click);
            // 
            // mitLow
            // 
            this.mitLow.MergeIndex = 4;
            this.mitLow.Name = "mitLow";
            this.mitLow.Size = new System.Drawing.Size(151, 22);
            this.mitLow.Text = "Low";
            this.mitLow.Click += new System.EventHandler(this.mitLow_Click);
            // 
            // stbMain
            // 
            this.stbMain.Location = new System.Drawing.Point(0, 480);
            this.stbMain.Name = "stbMain";
            this.stbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.processcount,
            this.cpuUsage,
            this.memoryUsage});
            this.stbMain.ShowPanels = true;
            this.stbMain.Size = new System.Drawing.Size(744, 24);
            this.stbMain.TabIndex = 1;
            // 
            // processcount
            // 
            this.processcount.Name = "processcount";
            this.processcount.Text = "Gathering Data...";
            this.processcount.Width = 135;
            // 
            // cpuUsage
            // 
            this.cpuUsage.Name = "cpuUsage";
            // 
            // memoryUsage
            // 
            this.memoryUsage.Name = "memoryUsage";
            // 
            // threadcount
            // 
            this.threadcount.Name = "threadcount";
            this.threadcount.Text = "threads";
            // 
            // tmrMain
            // 
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 1000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 25);
            // 
            // tss2
            // 
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(6, 25);
            // 
            // tlsMain
            // 
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnEndProcess,
            this.toolStripBtnBlockProcess,
            this.toolStripBtnUBlck,
            this.tss1,
            this.toolStripBtnShowDetails,
            this.toolStripBtnProperties,
            this.tss2,
            this.toolStripBtnGoogleIt});
            this.tlsMain.Location = new System.Drawing.Point(0, 64);
            this.tlsMain.MinimumSize = new System.Drawing.Size(700, 0);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(744, 25);
            this.tlsMain.TabIndex = 3;
            this.tlsMain.Text = "toolStrip1";
            // 
            // toolStripBtnEndProcess
            // 
            this.toolStripBtnEndProcess.Image = global::ProcessManager.Properties.Resources._1303096662_exit;
            this.toolStripBtnEndProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnEndProcess.Name = "toolStripBtnEndProcess";
            this.toolStripBtnEndProcess.Size = new System.Drawing.Size(90, 22);
            this.toolStripBtnEndProcess.Text = "End Process";
            this.toolStripBtnEndProcess.Click += new System.EventHandler(this.toolStripBtnEndProcess_Click);
            // 
            // toolStripBtnBlockProcess
            // 
            this.toolStripBtnBlockProcess.Image = global::ProcessManager.Properties.Resources.dialog_cancel;
            this.toolStripBtnBlockProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnBlockProcess.Name = "toolStripBtnBlockProcess";
            this.toolStripBtnBlockProcess.Size = new System.Drawing.Size(99, 22);
            this.toolStripBtnBlockProcess.Text = "Block Process";
            this.toolStripBtnBlockProcess.Click += new System.EventHandler(this.toolStripBtnBlockProcess_Click);
            // 
            // toolStripBtnUBlck
            // 
            this.toolStripBtnUBlck.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnUBlck.Image")));
            this.toolStripBtnUBlck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnUBlck.Name = "toolStripBtnUBlck";
            this.toolStripBtnUBlck.Size = new System.Drawing.Size(71, 22);
            this.toolStripBtnUBlck.Text = "Unblock";
            this.toolStripBtnUBlck.Click += new System.EventHandler(this.toolStripBtnUBlck_Click);
            // 
            // toolStripBtnShowDetails
            // 
            this.toolStripBtnShowDetails.Image = global::ProcessManager.Properties.Resources.stock_view_details;
            this.toolStripBtnShowDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnShowDetails.Name = "toolStripBtnShowDetails";
            this.toolStripBtnShowDetails.Size = new System.Drawing.Size(94, 22);
            this.toolStripBtnShowDetails.Text = "Show Details";
            this.toolStripBtnShowDetails.Click += new System.EventHandler(this.toolStripBtnShowDetails_Click);
            // 
            // toolStripBtnProperties
            // 
            this.toolStripBtnProperties.Image = global::ProcessManager.Properties.Resources._1303028428_Properties;
            this.toolStripBtnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnProperties.Name = "toolStripBtnProperties";
            this.toolStripBtnProperties.Size = new System.Drawing.Size(80, 22);
            this.toolStripBtnProperties.Text = "Properties";
            this.toolStripBtnProperties.Click += new System.EventHandler(this.toolStripBtnProperties_Click);
            // 
            // toolStripBtnGoogleIt
            // 
            this.toolStripBtnGoogleIt.Image = global::ProcessManager.Properties.Resources.google;
            this.toolStripBtnGoogleIt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnGoogleIt.Name = "toolStripBtnGoogleIt";
            this.toolStripBtnGoogleIt.Size = new System.Drawing.Size(134, 22);
            this.toolStripBtnGoogleIt.Text = "Search Over Internet";
            this.toolStripBtnGoogleIt.Click += new System.EventHandler(this.toolStripBtnGoogleIt_Click);
            // 
            // lvprocesslist
            // 
            this.lvprocesslist.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvprocesslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.procname,
            this.columnHeaderPID,
            this.columnHeaderPriorty,
            this.nonofthreads,
            this.proccputime,
            this.memusage,
            this.columnHeaderStartTime,
            this.procFilePath,
            this.procuser});
            this.lvprocesslist.ContextMenuStrip = this.lvcxtmnu;
            this.lvprocesslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvprocesslist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvprocesslist.FullRowSelect = true;
            this.lvprocesslist.Location = new System.Drawing.Point(0, 89);
            this.lvprocesslist.Name = "lvprocesslist";
            this.lvprocesslist.Size = new System.Drawing.Size(744, 391);
            this.lvprocesslist.SmallImageList = this.procImageList;
            this.lvprocesslist.TabIndex = 4;
            this.lvprocesslist.UseCompatibleStateImageBehavior = false;
            this.lvprocesslist.View = System.Windows.Forms.View.Details;
            this.lvprocesslist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvprocesslist_ColumnClick);
            this.lvprocesslist.SelectedIndexChanged += new System.EventHandler(this.lvprocesslist_SelectedIndexChanged);
            this.lvprocesslist.Leave += new System.EventHandler(this.lvprocesslist_Leave);
            // 
            // procname
            // 
            this.procname.Text = "Name";
            this.procname.Width = 123;
            // 
            // columnHeaderPID
            // 
            this.columnHeaderPID.Tag = "Numeric";
            this.columnHeaderPID.Text = "Process Id";
            // 
            // columnHeaderPriorty
            // 
            this.columnHeaderPriorty.Text = "Priority";
            // 
            // nonofthreads
            // 
            this.nonofthreads.Tag = "Numeric";
            this.nonofthreads.Text = "No.of Threads";
            this.nonofthreads.Width = 90;
            // 
            // proccputime
            // 
            this.proccputime.Text = "CPU Time";
            this.proccputime.Width = 80;
            // 
            // memusage
            // 
            this.memusage.Tag = "Memory";
            this.memusage.Text = "Memory";
            this.memusage.Width = 90;
            // 
            // columnHeaderStartTime
            // 
            this.columnHeaderStartTime.Text = "Start Time";
            // 
            // procFilePath
            // 
            this.procFilePath.Text = "File Path";
            this.procFilePath.Width = 107;
            // 
            // procuser
            // 
            this.procuser.Text = "User";
            // 
            // procImageList
            // 
            this.procImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.procImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.procImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // detailsImageList
            // 
            this.detailsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("detailsImageList.ImageStream")));
            this.detailsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.detailsImageList.Images.SetKeyName(0, "icon1.PNG");
            this.detailsImageList.Images.SetKeyName(1, "icon2.PNG");
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(744, 64);
            this.ucTop.TabIndex = 6;
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 504);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(744, 31);
            this.ucBottom.TabIndex = 7;
            // 
            // FormProcessManager
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 535);
            this.Controls.Add(this.lvprocesslist);
            this.Controls.Add(this.tlsMain);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.ucBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProcessManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Manager";
            this.Load += new System.EventHandler(this.FrmProcessManager_Load);
            this.lvcxtmnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.processcount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryUsage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadcount)).EndInit();
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}



		#endregion

		System.Windows.Forms.ToolStripMenuItem menuItem18;
		System.Windows.Forms.Timer tmrMain;
		System.Windows.Forms.StatusBarPanel cpuUsage;
		System.Windows.Forms.StatusBarPanel memoryUsage;
		ToolStripButton toolStripBtnEndProcess;
		ToolStripButton toolStripBtnBlockProcess;
		ToolStripSeparator tss1;
		ToolStripButton toolStripBtnShowDetails;
		ToolStripButton toolStripBtnProperties;
		ToolStripSeparator tss2;
		ToolStripButton toolStripBtnGoogleIt;
		ToolStrip tlsMain;
		ListView lvprocesslist;
		ColumnHeader procname;
		ColumnHeader columnHeaderPID;
		ColumnHeader columnHeaderPriorty;
		ColumnHeader nonofthreads;
		ColumnHeader proccputime;
		ColumnHeader memusage;
		ColumnHeader columnHeaderStartTime;
		ColumnHeader procFilePath;
		ImageList detailsImageList;
		ImageList procImageList;
		TopControl ucTop;
		BottomControl ucBottom;
		private ToolStripButton toolStripBtnUBlck;
        private ColumnHeader procuser;
	}

	public class Sorter : System.Collections.IComparer
	{
		public int Column = 0;
		public System.Windows.Forms.SortOrder Order = SortOrder.Ascending;
		public int Compare(object x, object y) // IComparer Member
		{
			try
			{
				if (!(x is ListViewItem))
					return (0);
				if (!(y is ListViewItem))
					return (0);

				ListViewItem l1 = (ListViewItem)x;
				ListViewItem l2 = (ListViewItem)y;

				if (l1.ListView.Columns[Column].Tag == null)
				{
					l1.ListView.Columns[Column].Tag = "Text";
				}

				if (l1.ListView.Columns[Column].Tag.ToString() == "Numeric")
				{
					int fl1 = int.Parse(l1.SubItems[Column].Text);
					int fl2 = int.Parse(l2.SubItems[Column].Text);

					if (Order == SortOrder.Ascending)
					{
						return fl1.CompareTo(fl2);
					}
					else
					{
						return fl2.CompareTo(fl1);
					}
				}
				else if (l1.ListView.Columns[Column].Tag.ToString() == "Memory")
				{
					int fl1 = int.Parse(l1.SubItems[Column].Text.Substring(0, l1.SubItems[Column].Text.Length - 2));
					int fl2 = int.Parse(l2.SubItems[Column].Text.Substring(0, l2.SubItems[Column].Text.Length - 2));

					if (Order == SortOrder.Ascending)
					{
						return fl1.CompareTo(fl2);
					}
					else
					{
						return fl2.CompareTo(fl1);
					}
				}
				else
				{
					string str1 = l1.SubItems[Column].Text;
					string str2 = l2.SubItems[Column].Text;

					if (Order == SortOrder.Ascending)
					{
						return str1.CompareTo(str2);
					}
					else
					{
						return str2.CompareTo(str1);
					}
				}
			}
			catch (Exception)
			{
				return 0;
			}
		}
	}
}

