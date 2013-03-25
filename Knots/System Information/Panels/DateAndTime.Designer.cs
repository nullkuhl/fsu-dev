using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace SystemInformation
{
	/// <summary>
	/// Designer for DateAndTime.
	/// </summary>
    public partial class DateAndTime : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public DateAndTime()
        {

            //This call is required by the Windows Form Designer.
            InitializeComponent();

        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.labelSelectedWeekOfYear = new System.Windows.Forms.Label();
            this.labelSelectedWeeksLeft = new System.Windows.Forms.Label();
            this.labelSelectedWeekOfYearDesc = new System.Windows.Forms.Label();
            this.labelSelectedWeeksLeftDesc = new System.Windows.Forms.Label();
            this.labelCurrentWeekOfYear = new System.Windows.Forms.Label();
            this.labelCurrentWeeksLeft = new System.Windows.Forms.Label();
            this.labelCurrentWeekOfYearDesc = new System.Windows.Forms.Label();
            this.labelCurrentWeeksLeftDesc = new System.Windows.Forms.Label();
            this.labelSelectedDate = new System.Windows.Forms.Label();
            this.labelSelectedDayOfYear = new System.Windows.Forms.Label();
            this.labelSelectedDaysLeft = new System.Windows.Forms.Label();
            this.labelOffsetFromToday = new System.Windows.Forms.Label();
            this.labelSelectedHoliday = new System.Windows.Forms.Label();
            this.labelSelectedDayOfYearDesc = new System.Windows.Forms.Label();
            this.labelSelectedDaysLeftDesc = new System.Windows.Forms.Label();
            this.labelOffsetFromTodayDesc = new System.Windows.Forms.Label();
            this.labelSelectedDateDesc = new System.Windows.Forms.Label();
            this.labelTCurrentDateDesc = new System.Windows.Forms.Label();
            this.labelCurrentDayOfYear = new System.Windows.Forms.Label();
            this.labelCurrentDaysLeft = new System.Windows.Forms.Label();
            this.labelCurrentHoliday = new System.Windows.Forms.Label();
            this.labelCurrentDayOfYearDesc = new System.Windows.Forms.Label();
            this.labelCurrentDaysLeftDesc = new System.Windows.Forms.Label();
            this.labelCurrentDate = new System.Windows.Forms.Label();
            this.calendarDateTime = new System.Windows.Forms.MonthCalendar();
            this.labelTimeZone = new System.Windows.Forms.Label();
            this.labelDaylightEndDesc = new System.Windows.Forms.Label();
            this.labelDaylightStartDesc = new System.Windows.Forms.Label();
            this.labelOffset = new System.Windows.Forms.Label();
            this.labelDaylightEnd = new System.Windows.Forms.Label();
            this.labelDaylightStart = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.labelLocal = new System.Windows.Forms.Label();
            this.labelUniversalDateTime = new System.Windows.Forms.Label();
            this.labelUniversal = new System.Windows.Forms.Label();
            this.labelLocalDateTime = new System.Windows.Forms.Label();
            this.labelOffsetFromUTC = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // labelSelectedWeekOfYear
            // 
            this.labelSelectedWeekOfYear.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedWeekOfYear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedWeekOfYear.Location = new System.Drawing.Point(488, 334);
            this.labelSelectedWeekOfYear.Name = "labelSelectedWeekOfYear";
            this.labelSelectedWeekOfYear.Size = new System.Drawing.Size(24, 15);
            this.labelSelectedWeekOfYear.TabIndex = 84;
            this.labelSelectedWeekOfYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSelectedWeeksLeft
            // 
            this.labelSelectedWeeksLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedWeeksLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedWeeksLeft.Location = new System.Drawing.Point(488, 354);
            this.labelSelectedWeeksLeft.Name = "labelSelectedWeeksLeft";
            this.labelSelectedWeeksLeft.Size = new System.Drawing.Size(24, 15);
            this.labelSelectedWeeksLeft.TabIndex = 83;
            this.labelSelectedWeeksLeft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSelectedWeekOfYearDesc
            // 
            this.labelSelectedWeekOfYearDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedWeekOfYearDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedWeekOfYearDesc.Location = new System.Drawing.Point(365, 334);
            this.labelSelectedWeekOfYearDesc.Name = "labelSelectedWeekOfYearDesc";
            this.labelSelectedWeekOfYearDesc.Size = new System.Drawing.Size(111, 15);
            this.labelSelectedWeekOfYearDesc.TabIndex = 82;
            this.labelSelectedWeekOfYearDesc.Text = "datetime_weekofyear:";
            // 
            // labelSelectedWeeksLeftDesc
            // 
            this.labelSelectedWeeksLeftDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedWeeksLeftDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedWeeksLeftDesc.Location = new System.Drawing.Point(365, 354);
            this.labelSelectedWeeksLeftDesc.Name = "labelSelectedWeeksLeftDesc";
            this.labelSelectedWeeksLeftDesc.Size = new System.Drawing.Size(111, 15);
            this.labelSelectedWeeksLeftDesc.TabIndex = 81;
            this.labelSelectedWeeksLeftDesc.Text = "datetime_weeksleft:";
            // 
            // labelCurrentWeekOfYear
            // 
            this.labelCurrentWeekOfYear.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentWeekOfYear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentWeekOfYear.Location = new System.Drawing.Point(486, 244);
            this.labelCurrentWeekOfYear.Name = "labelCurrentWeekOfYear";
            this.labelCurrentWeekOfYear.Size = new System.Drawing.Size(24, 15);
            this.labelCurrentWeekOfYear.TabIndex = 80;
            this.labelCurrentWeekOfYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCurrentWeeksLeft
            // 
            this.labelCurrentWeeksLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentWeeksLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentWeeksLeft.Location = new System.Drawing.Point(486, 264);
            this.labelCurrentWeeksLeft.Name = "labelCurrentWeeksLeft";
            this.labelCurrentWeeksLeft.Size = new System.Drawing.Size(24, 15);
            this.labelCurrentWeeksLeft.TabIndex = 79;
            this.labelCurrentWeeksLeft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCurrentWeekOfYearDesc
            // 
            this.labelCurrentWeekOfYearDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentWeekOfYearDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentWeekOfYearDesc.Location = new System.Drawing.Point(364, 244);
            this.labelCurrentWeekOfYearDesc.Name = "labelCurrentWeekOfYearDesc";
            this.labelCurrentWeekOfYearDesc.Size = new System.Drawing.Size(112, 15);
            this.labelCurrentWeekOfYearDesc.TabIndex = 78;
            this.labelCurrentWeekOfYearDesc.Text = "datetime_weekofyear:";
            // 
            // labelCurrentWeeksLeftDesc
            // 
            this.labelCurrentWeeksLeftDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentWeeksLeftDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentWeeksLeftDesc.Location = new System.Drawing.Point(364, 264);
            this.labelCurrentWeeksLeftDesc.Name = "labelCurrentWeeksLeftDesc";
            this.labelCurrentWeeksLeftDesc.Size = new System.Drawing.Size(112, 15);
            this.labelCurrentWeeksLeftDesc.TabIndex = 77;
            this.labelCurrentWeeksLeftDesc.Text = "datetime_weeksleft:";
            // 
            // labelSelectedDate
            // 
            this.labelSelectedDate.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedDate.Location = new System.Drawing.Point(364, 314);
            this.labelSelectedDate.Name = "labelSelectedDate";
            this.labelSelectedDate.Size = new System.Drawing.Size(142, 15);
            this.labelSelectedDate.TabIndex = 76;
            // 
            // labelSelectedDayOfYear
            // 
            this.labelSelectedDayOfYear.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedDayOfYear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedDayOfYear.Location = new System.Drawing.Point(324, 334);
            this.labelSelectedDayOfYear.Name = "labelSelectedDayOfYear";
            this.labelSelectedDayOfYear.Size = new System.Drawing.Size(28, 15);
            this.labelSelectedDayOfYear.TabIndex = 75;
            this.labelSelectedDayOfYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSelectedDaysLeft
            // 
            this.labelSelectedDaysLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedDaysLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedDaysLeft.Location = new System.Drawing.Point(324, 354);
            this.labelSelectedDaysLeft.Name = "labelSelectedDaysLeft";
            this.labelSelectedDaysLeft.Size = new System.Drawing.Size(28, 15);
            this.labelSelectedDaysLeft.TabIndex = 74;
            this.labelSelectedDaysLeft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelOffsetFromToday
            // 
            this.labelOffsetFromToday.BackColor = System.Drawing.Color.Transparent;
            this.labelOffsetFromToday.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelOffsetFromToday.Location = new System.Drawing.Point(426, 374);
            this.labelOffsetFromToday.Name = "labelOffsetFromToday";
            this.labelOffsetFromToday.Size = new System.Drawing.Size(86, 15);
            this.labelOffsetFromToday.TabIndex = 73;
            this.labelOffsetFromToday.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSelectedHoliday
            // 
            this.labelSelectedHoliday.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedHoliday.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelSelectedHoliday.Location = new System.Drawing.Point(307, 394);
            this.labelSelectedHoliday.Name = "labelSelectedHoliday";
            this.labelSelectedHoliday.Size = new System.Drawing.Size(220, 15);
            this.labelSelectedHoliday.TabIndex = 72;
            // 
            // labelSelectedDayOfYearDesc
            // 
            this.labelSelectedDayOfYearDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedDayOfYearDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedDayOfYearDesc.Location = new System.Drawing.Point(212, 334);
            this.labelSelectedDayOfYearDesc.Name = "labelSelectedDayOfYearDesc";
            this.labelSelectedDayOfYearDesc.Size = new System.Drawing.Size(104, 15);
            this.labelSelectedDayOfYearDesc.TabIndex = 70;
            this.labelSelectedDayOfYearDesc.Text = "datetime_dayofyear:";
            // 
            // labelSelectedDaysLeftDesc
            // 
            this.labelSelectedDaysLeftDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedDaysLeftDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedDaysLeftDesc.Location = new System.Drawing.Point(212, 354);
            this.labelSelectedDaysLeftDesc.Name = "labelSelectedDaysLeftDesc";
            this.labelSelectedDaysLeftDesc.Size = new System.Drawing.Size(104, 15);
            this.labelSelectedDaysLeftDesc.TabIndex = 69;
            this.labelSelectedDaysLeftDesc.Text = "datetime_daysleft:";
            // 
            // labelOffsetFromTodayDesc
            // 
            this.labelOffsetFromTodayDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelOffsetFromTodayDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelOffsetFromTodayDesc.Location = new System.Drawing.Point(212, 374);
            this.labelOffsetFromTodayDesc.Name = "labelOffsetFromTodayDesc";
            this.labelOffsetFromTodayDesc.Size = new System.Drawing.Size(208, 15);
            this.labelOffsetFromTodayDesc.TabIndex = 68;
            this.labelOffsetFromTodayDesc.Text = "datetime_offset_from_today:";
            // 
            // labelSelectedDateDesc
            // 
            this.labelSelectedDateDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectedDateDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSelectedDateDesc.Location = new System.Drawing.Point(212, 314);
            this.labelSelectedDateDesc.Name = "labelSelectedDateDesc";
            this.labelSelectedDateDesc.Size = new System.Drawing.Size(108, 15);
            this.labelSelectedDateDesc.TabIndex = 67;
            this.labelSelectedDateDesc.Text = "datetime_selected_date:";
            // 
            // labelTCurrentDateDesc
            // 
            this.labelTCurrentDateDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelTCurrentDateDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTCurrentDateDesc.Location = new System.Drawing.Point(212, 224);
            this.labelTCurrentDateDesc.Name = "labelTCurrentDateDesc";
            this.labelTCurrentDateDesc.Size = new System.Drawing.Size(108, 15);
            this.labelTCurrentDateDesc.TabIndex = 66;
            this.labelTCurrentDateDesc.Text = "today:";
            // 
            // labelCurrentDayOfYear
            // 
            this.labelCurrentDayOfYear.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentDayOfYear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentDayOfYear.Location = new System.Drawing.Point(324, 244);
            this.labelCurrentDayOfYear.Name = "labelCurrentDayOfYear";
            this.labelCurrentDayOfYear.Size = new System.Drawing.Size(28, 15);
            this.labelCurrentDayOfYear.TabIndex = 65;
            this.labelCurrentDayOfYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCurrentDaysLeft
            // 
            this.labelCurrentDaysLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentDaysLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentDaysLeft.Location = new System.Drawing.Point(324, 264);
            this.labelCurrentDaysLeft.Name = "labelCurrentDaysLeft";
            this.labelCurrentDaysLeft.Size = new System.Drawing.Size(28, 15);
            this.labelCurrentDaysLeft.TabIndex = 64;
            this.labelCurrentDaysLeft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCurrentHoliday
            // 
            this.labelCurrentHoliday.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentHoliday.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelCurrentHoliday.Location = new System.Drawing.Point(286, 284);
            this.labelCurrentHoliday.Name = "labelCurrentHoliday";
            this.labelCurrentHoliday.Size = new System.Drawing.Size(220, 15);
            this.labelCurrentHoliday.TabIndex = 63;
            // 
            // labelCurrentDayOfYearDesc
            // 
            this.labelCurrentDayOfYearDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentDayOfYearDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentDayOfYearDesc.Location = new System.Drawing.Point(212, 244);
            this.labelCurrentDayOfYearDesc.Name = "labelCurrentDayOfYearDesc";
            this.labelCurrentDayOfYearDesc.Size = new System.Drawing.Size(104, 15);
            this.labelCurrentDayOfYearDesc.TabIndex = 61;
            this.labelCurrentDayOfYearDesc.Text = "datetime_dayofyear:";
            // 
            // labelCurrentDaysLeftDesc
            // 
            this.labelCurrentDaysLeftDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentDaysLeftDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentDaysLeftDesc.Location = new System.Drawing.Point(212, 264);
            this.labelCurrentDaysLeftDesc.Name = "labelCurrentDaysLeftDesc";
            this.labelCurrentDaysLeftDesc.Size = new System.Drawing.Size(104, 15);
            this.labelCurrentDaysLeftDesc.TabIndex = 60;
            this.labelCurrentDaysLeftDesc.Text = "datetime_daysleft:";
            // 
            // labelCurrentDate
            // 
            this.labelCurrentDate.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentDate.Location = new System.Drawing.Point(364, 224);
            this.labelCurrentDate.Name = "labelCurrentDate";
            this.labelCurrentDate.Size = new System.Drawing.Size(142, 15);
            this.labelCurrentDate.TabIndex = 59;
            // 
            // calendarDateTime
            // 
            this.calendarDateTime.Location = new System.Drawing.Point(15, 224);
            this.calendarDateTime.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.calendarDateTime.MinDate = new System.DateTime(1776, 7, 1, 0, 0, 0, 0);
            this.calendarDateTime.Name = "calendarDateTime";
            this.calendarDateTime.ShowToday = false;
            this.calendarDateTime.TabIndex = 48;
            this.calendarDateTime.TitleBackColor = System.Drawing.Color.DodgerBlue;
            this.calendarDateTime.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendarDateTime_DateChanged);
            // 
            // labelTimeZone
            // 
            this.labelTimeZone.BackColor = System.Drawing.Color.Transparent;
            this.labelTimeZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelTimeZone.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelTimeZone.Location = new System.Drawing.Point(15, 72);
            this.labelTimeZone.Name = "labelTimeZone";
            this.labelTimeZone.Size = new System.Drawing.Size(487, 17);
            this.labelTimeZone.TabIndex = 40;
            // 
            // labelDaylightEndDesc
            // 
            this.labelDaylightEndDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDaylightEndDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDaylightEndDesc.Location = new System.Drawing.Point(15, 184);
            this.labelDaylightEndDesc.Name = "labelDaylightEndDesc";
            this.labelDaylightEndDesc.Size = new System.Drawing.Size(156, 15);
            this.labelDaylightEndDesc.TabIndex = 38;
            this.labelDaylightEndDesc.Text = "datetime_save_end:";
            // 
            // labelDaylightStartDesc
            // 
            this.labelDaylightStartDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDaylightStartDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDaylightStartDesc.Location = new System.Drawing.Point(15, 164);
            this.labelDaylightStartDesc.Name = "labelDaylightStartDesc";
            this.labelDaylightStartDesc.Size = new System.Drawing.Size(156, 15);
            this.labelDaylightStartDesc.TabIndex = 36;
            this.labelDaylightStartDesc.Text = "datetime_save_start:";
            // 
            // labelOffset
            // 
            this.labelOffset.BackColor = System.Drawing.Color.Transparent;
            this.labelOffset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelOffset.Location = new System.Drawing.Point(15, 104);
            this.labelOffset.Name = "labelOffset";
            this.labelOffset.Size = new System.Drawing.Size(156, 15);
            this.labelOffset.TabIndex = 34;
            this.labelOffset.Text = "datetime_offset:";
            // 
            // labelDaylightEnd
            // 
            this.labelDaylightEnd.BackColor = System.Drawing.Color.Transparent;
            this.labelDaylightEnd.Location = new System.Drawing.Point(175, 184);
            this.labelDaylightEnd.Name = "labelDaylightEnd";
            this.labelDaylightEnd.Size = new System.Drawing.Size(327, 15);
            this.labelDaylightEnd.TabIndex = 29;
            // 
            // labelDaylightStart
            // 
            this.labelDaylightStart.BackColor = System.Drawing.Color.Transparent;
            this.labelDaylightStart.Location = new System.Drawing.Point(175, 164);
            this.labelDaylightStart.Name = "labelDaylightStart";
            this.labelDaylightStart.Size = new System.Drawing.Size(327, 15);
            this.labelDaylightStart.TabIndex = 26;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Black;
            this.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Label6.Location = new System.Drawing.Point(15, 214);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(487, 3);
            this.Label6.TabIndex = 24;
            // 
            // labelLocal
            // 
            this.labelLocal.BackColor = System.Drawing.Color.Transparent;
            this.labelLocal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelLocal.Location = new System.Drawing.Point(15, 124);
            this.labelLocal.Name = "labelLocal";
            this.labelLocal.Size = new System.Drawing.Size(156, 15);
            this.labelLocal.TabIndex = 23;
            this.labelLocal.Text = "datetime_local:";
            // 
            // labelUniversalDateTime
            // 
            this.labelUniversalDateTime.BackColor = System.Drawing.Color.Transparent;
            this.labelUniversalDateTime.Location = new System.Drawing.Point(175, 144);
            this.labelUniversalDateTime.Name = "labelUniversalDateTime";
            this.labelUniversalDateTime.Size = new System.Drawing.Size(327, 15);
            this.labelUniversalDateTime.TabIndex = 21;
            // 
            // labelUniversal
            // 
            this.labelUniversal.BackColor = System.Drawing.Color.Transparent;
            this.labelUniversal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelUniversal.Location = new System.Drawing.Point(15, 144);
            this.labelUniversal.Name = "labelUniversal";
            this.labelUniversal.Size = new System.Drawing.Size(156, 15);
            this.labelUniversal.TabIndex = 20;
            this.labelUniversal.Text = "datetime_univ:";
            // 
            // labelLocalDateTime
            // 
            this.labelLocalDateTime.BackColor = System.Drawing.Color.Transparent;
            this.labelLocalDateTime.Location = new System.Drawing.Point(175, 124);
            this.labelLocalDateTime.Name = "labelLocalDateTime";
            this.labelLocalDateTime.Size = new System.Drawing.Size(327, 15);
            this.labelLocalDateTime.TabIndex = 19;
            // 
            // labelOffsetFromUTC
            // 
            this.labelOffsetFromUTC.BackColor = System.Drawing.Color.Transparent;
            this.labelOffsetFromUTC.Location = new System.Drawing.Point(175, 104);
            this.labelOffsetFromUTC.Name = "labelOffsetFromUTC";
            this.labelOffsetFromUTC.Size = new System.Drawing.Size(327, 15);
            this.labelOffsetFromUTC.TabIndex = 18;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(15, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(487, 3);
            this.labelSeparator.TabIndex = 15;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(74, 28);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(148, 24);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "node_datetime";
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Date_Time_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 2;
            this.picturePanel.TabStop = false;
            // 
            // DateAndTime
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelSelectedWeekOfYear);
            this.Controls.Add(this.labelSelectedWeeksLeft);
            this.Controls.Add(this.labelSelectedWeekOfYearDesc);
            this.Controls.Add(this.labelSelectedWeeksLeftDesc);
            this.Controls.Add(this.labelCurrentWeekOfYear);
            this.Controls.Add(this.labelCurrentWeeksLeft);
            this.Controls.Add(this.labelCurrentWeekOfYearDesc);
            this.Controls.Add(this.labelCurrentWeeksLeftDesc);
            this.Controls.Add(this.labelSelectedDate);
            this.Controls.Add(this.labelSelectedDayOfYear);
            this.Controls.Add(this.labelSelectedDaysLeft);
            this.Controls.Add(this.labelOffsetFromToday);
            this.Controls.Add(this.labelSelectedHoliday);
            this.Controls.Add(this.labelSelectedDayOfYearDesc);
            this.Controls.Add(this.labelSelectedDaysLeftDesc);
            this.Controls.Add(this.labelOffsetFromTodayDesc);
            this.Controls.Add(this.labelSelectedDateDesc);
            this.Controls.Add(this.labelTCurrentDateDesc);
            this.Controls.Add(this.labelCurrentDayOfYear);
            this.Controls.Add(this.labelCurrentDaysLeft);
            this.Controls.Add(this.labelCurrentHoliday);
            this.Controls.Add(this.labelCurrentDayOfYearDesc);
            this.Controls.Add(this.labelCurrentDaysLeftDesc);
            this.Controls.Add(this.labelCurrentDate);
            this.Controls.Add(this.calendarDateTime);
            this.Controls.Add(this.labelTimeZone);
            this.Controls.Add(this.labelDaylightEndDesc);
            this.Controls.Add(this.labelDaylightStartDesc);
            this.Controls.Add(this.labelOffset);
            this.Controls.Add(this.labelDaylightEnd);
            this.Controls.Add(this.labelDaylightStart);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.labelLocal);
            this.Controls.Add(this.labelUniversalDateTime);
            this.Controls.Add(this.labelUniversal);
            this.Controls.Add(this.labelLocalDateTime);
            this.Controls.Add(this.labelOffsetFromUTC);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "DateAndTime";
            this.Size = new System.Drawing.Size(558, 425);
            this.Load += new System.EventHandler(this.DateAndTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelLocalDateTime;
        System.Windows.Forms.Label labelOffsetFromUTC;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelUniversal;
        System.Windows.Forms.Label labelUniversalDateTime;
        System.Windows.Forms.Label labelLocal;
        System.Windows.Forms.Label labelDaylightEnd;
        System.Windows.Forms.Label labelDaylightStart;
        System.Windows.Forms.Label Label6;
        System.Windows.Forms.Timer timerDateTime;
        System.Windows.Forms.Label labelOffset;
        System.Windows.Forms.Label labelDaylightStartDesc;
        System.Windows.Forms.Label labelDaylightEndDesc;
        Label labelTimeZone;
        MonthCalendar calendarDateTime;
        Label labelSelectedDate;
        Label labelSelectedDayOfYear;
        Label labelSelectedDaysLeft;
        Label labelOffsetFromToday;
        Label labelSelectedHoliday;
        Label labelSelectedDayOfYearDesc;
        Label labelSelectedDaysLeftDesc;
        Label labelOffsetFromTodayDesc;
        Label labelSelectedDateDesc;
        Label labelTCurrentDateDesc;
        Label labelCurrentDayOfYear;
        Label labelCurrentDaysLeft;
        Label labelCurrentHoliday;
        Label labelCurrentDayOfYearDesc;
        Label labelCurrentDaysLeftDesc;
        Label labelCurrentDate;
        Label labelCurrentWeekOfYear;
        Label labelCurrentWeeksLeft;
        Label labelCurrentWeekOfYearDesc;
        Label labelCurrentWeeksLeftDesc;
        Label labelSelectedWeekOfYear;
        Label labelSelectedWeeksLeft;
        Label labelSelectedWeekOfYearDesc;
        Label labelSelectedWeeksLeftDesc;
		
	}
	
}
