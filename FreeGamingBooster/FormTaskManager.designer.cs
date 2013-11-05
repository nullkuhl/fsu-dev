using System;
using System.Collections.Generic;
using Microsoft.Win32.TaskScheduler;
using FreemiumUtilities.Infrastructure;

namespace FreeGamingBooster
{
	partial class FormTaskManager
	{
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskManager));
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.pnlWeek = new System.Windows.Forms.Panel();
            this.grbScheduleWeekly = new System.Windows.Forms.GroupBox();
            this.chkSat = new System.Windows.Forms.CheckBox();
            this.chkSun = new System.Windows.Forms.CheckBox();
            this.chkTue = new System.Windows.Forms.CheckBox();
            this.chkWed = new System.Windows.Forms.CheckBox();
            this.chkThu = new System.Windows.Forms.CheckBox();
            this.chkFri = new System.Windows.Forms.CheckBox();
            this.chkMon = new System.Windows.Forms.CheckBox();
            this.lblWeeks = new System.Windows.Forms.Label();
            this.nudWeeks = new System.Windows.Forms.NumericUpDown();
            this.lblEvery = new System.Windows.Forms.Label();
            this.pnlDay = new System.Windows.Forms.Panel();
            this.grbScheduleDaily = new System.Windows.Forms.GroupBox();
            this.lblDays = new System.Windows.Forms.Label();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.lblDailyEvery = new System.Windows.Forms.Label();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.cmbSelectSchedule = new System.Windows.Forms.ComboBox();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.pcbLogo = new System.Windows.Forms.PictureBox();
            this.pnlMonth = new System.Windows.Forms.Panel();
            this.grbScheduleMonthly = new System.Windows.Forms.GroupBox();
            this.lblOfTheMonth = new System.Windows.Forms.Label();
            this.cmbday = new System.Windows.Forms.ComboBox();
            this.cmbweek = new System.Windows.Forms.ComboBox();
            this.radMonth = new System.Windows.Forms.RadioButton();
            this.lblOfMonth = new System.Windows.Forms.Label();
            this.radDay = new System.Windows.Forms.RadioButton();
            this.nudDayMonth = new System.Windows.Forms.NumericUpDown();
            this.pnlOnce = new System.Windows.Forms.Panel();
            this.grbScheduleOnce = new System.Windows.Forms.GroupBox();
            this.timePickerOnce = new System.Windows.Forms.DateTimePicker();
            this.lblOn = new System.Windows.Forms.Label();
            this.pnlIdel = new System.Windows.Forms.Panel();
            this.grbScheduleIdle = new System.Windows.Forms.GroupBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblIdle = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbcMain.SuspendLayout();
            this.tabSchedule.SuspendLayout();
            this.pnlWeek.SuspendLayout();
            this.grbScheduleWeekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).BeginInit();
            this.pnlDay.SuspendLayout();
            this.grbScheduleDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).BeginInit();
            this.pnlMonth.SuspendLayout();
            this.grbScheduleMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDayMonth)).BeginInit();
            this.pnlOnce.SuspendLayout();
            this.grbScheduleOnce.SuspendLayout();
            this.pnlIdel.SuspendLayout();
            this.grbScheduleIdle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tabSchedule);
            this.tbcMain.Location = new System.Drawing.Point(6, 7);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(366, 328);
            this.tbcMain.TabIndex = 0;
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.pnlWeek);
            this.tabSchedule.Controls.Add(this.pnlDay);
            this.tabSchedule.Controls.Add(this.lblSeparator);
            this.tabSchedule.Controls.Add(this.timePicker);
            this.tabSchedule.Controls.Add(this.cmbSelectSchedule);
            this.tabSchedule.Controls.Add(this.lblTask);
            this.tabSchedule.Controls.Add(this.lblSchedule);
            this.tabSchedule.Controls.Add(this.pcbLogo);
            this.tabSchedule.Controls.Add(this.pnlMonth);
            this.tabSchedule.Controls.Add(this.pnlOnce);
            this.tabSchedule.Controls.Add(this.pnlIdel);
            this.tabSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedule.Size = new System.Drawing.Size(358, 302);
            this.tabSchedule.TabIndex = 2;
            this.tabSchedule.Text = "Schedule";
            this.tabSchedule.UseVisualStyleBackColor = true;
            // 
            // pnlWeek
            // 
            this.pnlWeek.Controls.Add(this.grbScheduleWeekly);
            this.pnlWeek.Location = new System.Drawing.Point(16, 130);
            this.pnlWeek.Name = "pnlWeek";
            this.pnlWeek.Size = new System.Drawing.Size(330, 150);
            this.pnlWeek.TabIndex = 7;
            // 
            // grbScheduleWeekly
            // 
            this.grbScheduleWeekly.Controls.Add(this.chkSat);
            this.grbScheduleWeekly.Controls.Add(this.chkSun);
            this.grbScheduleWeekly.Controls.Add(this.chkTue);
            this.grbScheduleWeekly.Controls.Add(this.chkWed);
            this.grbScheduleWeekly.Controls.Add(this.chkThu);
            this.grbScheduleWeekly.Controls.Add(this.chkFri);
            this.grbScheduleWeekly.Controls.Add(this.chkMon);
            this.grbScheduleWeekly.Controls.Add(this.lblWeeks);
            this.grbScheduleWeekly.Controls.Add(this.nudWeeks);
            this.grbScheduleWeekly.Controls.Add(this.lblEvery);
            this.grbScheduleWeekly.Location = new System.Drawing.Point(2, 2);
            this.grbScheduleWeekly.Name = "grbScheduleWeekly";
            this.grbScheduleWeekly.Size = new System.Drawing.Size(321, 145);
            this.grbScheduleWeekly.TabIndex = 1;
            this.grbScheduleWeekly.TabStop = false;
            this.grbScheduleWeekly.Text = "Schedule Task Weekly";
            // 
            // chkSat
            // 
            this.chkSat.AutoSize = true;
            this.chkSat.Location = new System.Drawing.Point(260, 23);
            this.chkSat.Name = "chkSat";
            this.chkSat.Size = new System.Drawing.Size(42, 17);
            this.chkSat.TabIndex = 9;
            this.chkSat.Text = "Sat";
            this.chkSat.UseVisualStyleBackColor = true;
            this.chkSat.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // chkSun
            // 
            this.chkSun.AutoSize = true;
            this.chkSun.Location = new System.Drawing.Point(260, 46);
            this.chkSun.Name = "chkSun";
            this.chkSun.Size = new System.Drawing.Size(45, 17);
            this.chkSun.TabIndex = 8;
            this.chkSun.Text = "Sun";
            this.chkSun.UseVisualStyleBackColor = true;
            this.chkSun.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // chkTue
            // 
            this.chkTue.AutoSize = true;
            this.chkTue.Location = new System.Drawing.Point(198, 46);
            this.chkTue.Name = "chkTue";
            this.chkTue.Size = new System.Drawing.Size(45, 17);
            this.chkTue.TabIndex = 7;
            this.chkTue.Text = "Tue";
            this.chkTue.UseVisualStyleBackColor = true;
            this.chkTue.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // chkWed
            // 
            this.chkWed.AutoSize = true;
            this.chkWed.Location = new System.Drawing.Point(198, 71);
            this.chkWed.Name = "chkWed";
            this.chkWed.Size = new System.Drawing.Size(49, 17);
            this.chkWed.TabIndex = 6;
            this.chkWed.Text = "Wed";
            this.chkWed.UseVisualStyleBackColor = true;
            this.chkWed.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // chkThu
            // 
            this.chkThu.AutoSize = true;
            this.chkThu.Location = new System.Drawing.Point(198, 95);
            this.chkThu.Name = "chkThu";
            this.chkThu.Size = new System.Drawing.Size(45, 17);
            this.chkThu.TabIndex = 5;
            this.chkThu.Text = "Thu";
            this.chkThu.UseVisualStyleBackColor = true;
            this.chkThu.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // chkFri
            // 
            this.chkFri.AutoSize = true;
            this.chkFri.Location = new System.Drawing.Point(198, 118);
            this.chkFri.Name = "chkFri";
            this.chkFri.Size = new System.Drawing.Size(37, 17);
            this.chkFri.TabIndex = 4;
            this.chkFri.Text = "Fri";
            this.chkFri.UseVisualStyleBackColor = true;
            this.chkFri.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // chkMon
            // 
            this.chkMon.AutoSize = true;
            this.chkMon.Location = new System.Drawing.Point(198, 22);
            this.chkMon.Name = "chkMon";
            this.chkMon.Size = new System.Drawing.Size(47, 17);
            this.chkMon.TabIndex = 3;
            this.chkMon.Text = "Mon";
            this.chkMon.UseVisualStyleBackColor = true;
            this.chkMon.CheckedChanged += new System.EventHandler(this.chkDay_CheckedChanged);
            // 
            // lblWeeks
            // 
            this.lblWeeks.AutoSize = true;
            this.lblWeeks.Location = new System.Drawing.Point(130, 23);
            this.lblWeeks.Name = "lblWeeks";
            this.lblWeeks.Size = new System.Drawing.Size(62, 13);
            this.lblWeeks.TabIndex = 2;
            this.lblWeeks.Text = "week(s) on:";
            // 
            // nudWeeks
            // 
            this.nudWeeks.Location = new System.Drawing.Point(51, 20);
            this.nudWeeks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeeks.Name = "nudWeeks";
            this.nudWeeks.Size = new System.Drawing.Size(67, 20);
            this.nudWeeks.TabIndex = 1;
            this.nudWeeks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeeks.ValueChanged += new System.EventHandler(this.nudWeeks_ValueChanged);
            // 
            // lblEvery
            // 
            this.lblEvery.AutoSize = true;
            this.lblEvery.Location = new System.Drawing.Point(6, 23);
            this.lblEvery.Name = "lblEvery";
            this.lblEvery.Size = new System.Drawing.Size(34, 13);
            this.lblEvery.TabIndex = 0;
            this.lblEvery.Text = "Every";
            // 
            // pnlDay
            // 
            this.pnlDay.Controls.Add(this.grbScheduleDaily);
            this.pnlDay.Location = new System.Drawing.Point(16, 130);
            this.pnlDay.Name = "pnlDay";
            this.pnlDay.Size = new System.Drawing.Size(330, 150);
            this.pnlDay.TabIndex = 6;
            // 
            // grbScheduleDaily
            // 
            this.grbScheduleDaily.Controls.Add(this.lblDays);
            this.grbScheduleDaily.Controls.Add(this.nudDays);
            this.grbScheduleDaily.Controls.Add(this.lblDailyEvery);
            this.grbScheduleDaily.Location = new System.Drawing.Point(2, 2);
            this.grbScheduleDaily.Name = "grbScheduleDaily";
            this.grbScheduleDaily.Size = new System.Drawing.Size(321, 68);
            this.grbScheduleDaily.TabIndex = 1;
            this.grbScheduleDaily.TabStop = false;
            this.grbScheduleDaily.Text = "Schedule Task Daily";
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.Location = new System.Drawing.Point(138, 31);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(35, 13);
            this.lblDays.TabIndex = 2;
            this.lblDays.Text = "day(s)";
            // 
            // nudDays
            // 
            this.nudDays.Location = new System.Drawing.Point(59, 28);
            this.nudDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(67, 20);
            this.nudDays.TabIndex = 1;
            this.nudDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.ValueChanged += new System.EventHandler(this.nudDays_ValueChanged);
            // 
            // lblDailyEvery
            // 
            this.lblDailyEvery.AutoSize = true;
            this.lblDailyEvery.Location = new System.Drawing.Point(14, 31);
            this.lblDailyEvery.Name = "lblDailyEvery";
            this.lblDailyEvery.Size = new System.Drawing.Size(34, 13);
            this.lblDailyEvery.TabIndex = 0;
            this.lblDailyEvery.Text = "Every";
            // 
            // lblSeparator
            // 
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Location = new System.Drawing.Point(16, 78);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(325, 2);
            this.lblSeparator.TabIndex = 5;
            // 
            // timePicker
            // 
            this.timePicker.CustomFormat = "HH:mm a";
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePicker.Location = new System.Drawing.Point(144, 104);
            this.timePicker.Name = "timePicker";
            this.timePicker.ShowUpDown = true;
            this.timePicker.Size = new System.Drawing.Size(101, 20);
            this.timePicker.TabIndex = 4;
            this.timePicker.ValueChanged += new System.EventHandler(this.timePicker_ValueChanged);
            // 
            // cmbSelectSchedule
            // 
            this.cmbSelectSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectSchedule.FormattingEnabled = true;
            this.cmbSelectSchedule.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly",
            "Once",
            "At System Startup",
            "At Logon",
            "When Idle"});
            this.cmbSelectSchedule.Location = new System.Drawing.Point(17, 103);
            this.cmbSelectSchedule.Name = "cmbSelectSchedule";
            this.cmbSelectSchedule.Size = new System.Drawing.Size(121, 21);
            this.cmbSelectSchedule.TabIndex = 3;
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Location = new System.Drawing.Point(14, 86);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(82, 13);
            this.lblTask.TabIndex = 2;
            this.lblTask.Text = "Schedule Task:";
            // 
            // lblSchedule
            // 
            this.lblSchedule.Location = new System.Drawing.Point(74, 13);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(272, 52);
            this.lblSchedule.TabIndex = 1;
            this.lblSchedule.Text = "Schedule";
            // 
            // pcbLogo
            // 
            this.pcbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pcbLogo.Image")));
            this.pcbLogo.Location = new System.Drawing.Point(15, 13);
            this.pcbLogo.Name = "pcbLogo";
            this.pcbLogo.Size = new System.Drawing.Size(56, 38);
            this.pcbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbLogo.TabIndex = 0;
            this.pcbLogo.TabStop = false;
            // 
            // pnlMonth
            // 
            this.pnlMonth.Controls.Add(this.grbScheduleMonthly);
            this.pnlMonth.Location = new System.Drawing.Point(16, 130);
            this.pnlMonth.Name = "pnlMonth";
            this.pnlMonth.Size = new System.Drawing.Size(330, 150);
            this.pnlMonth.TabIndex = 8;
            // 
            // grbScheduleMonthly
            // 
            this.grbScheduleMonthly.Controls.Add(this.lblOfTheMonth);
            this.grbScheduleMonthly.Controls.Add(this.cmbday);
            this.grbScheduleMonthly.Controls.Add(this.cmbweek);
            this.grbScheduleMonthly.Controls.Add(this.radMonth);
            this.grbScheduleMonthly.Controls.Add(this.lblOfMonth);
            this.grbScheduleMonthly.Controls.Add(this.radDay);
            this.grbScheduleMonthly.Controls.Add(this.nudDayMonth);
            this.grbScheduleMonthly.Location = new System.Drawing.Point(2, 2);
            this.grbScheduleMonthly.Name = "grbScheduleMonthly";
            this.grbScheduleMonthly.Size = new System.Drawing.Size(321, 91);
            this.grbScheduleMonthly.TabIndex = 1;
            this.grbScheduleMonthly.TabStop = false;
            this.grbScheduleMonthly.Text = "Schedule Task Monthly";
            // 
            // lblOfTheMonth
            // 
            this.lblOfTheMonth.AutoSize = true;
            this.lblOfTheMonth.Location = new System.Drawing.Point(233, 60);
            this.lblOfTheMonth.Name = "lblOfTheMonth";
            this.lblOfTheMonth.Size = new System.Drawing.Size(77, 13);
            this.lblOfTheMonth.TabIndex = 8;
            this.lblOfTheMonth.Text = "of the month(s)";
            // 
            // cmbday
            // 
            this.cmbday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbday.FormattingEnabled = true;
            this.cmbday.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.cmbday.Location = new System.Drawing.Point(144, 56);
            this.cmbday.Name = "cmbday";
            this.cmbday.Size = new System.Drawing.Size(83, 21);
            this.cmbday.TabIndex = 7;
            this.cmbday.SelectedIndexChanged += new System.EventHandler(this.cmbweek_SelectedIndexChanged);
            // 
            // cmbweek
            // 
            this.cmbweek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbweek.FormattingEnabled = true;
            this.cmbweek.Items.AddRange(new object[] {
            "First",
            "Second",
            "Third",
            "Fourth",
            "Last"});
            this.cmbweek.Location = new System.Drawing.Point(61, 56);
            this.cmbweek.Name = "cmbweek";
            this.cmbweek.Size = new System.Drawing.Size(80, 21);
            this.cmbweek.TabIndex = 6;
            this.cmbweek.SelectedIndexChanged += new System.EventHandler(this.cmbweek_SelectedIndexChanged);
            // 
            // radMonth
            // 
            this.radMonth.AutoSize = true;
            this.radMonth.Location = new System.Drawing.Point(9, 58);
            this.radMonth.Name = "radMonth";
            this.radMonth.Size = new System.Drawing.Size(55, 17);
            this.radMonth.TabIndex = 5;
            this.radMonth.TabStop = true;
            this.radMonth.Text = "Month";
            this.radMonth.UseVisualStyleBackColor = true;
            this.radMonth.CheckedChanged += new System.EventHandler(this.radDay_CheckedChanged);
            // 
            // lblOfMonth
            // 
            this.lblOfMonth.AutoSize = true;
            this.lblOfMonth.Location = new System.Drawing.Point(135, 23);
            this.lblOfMonth.Name = "lblOfMonth";
            this.lblOfMonth.Size = new System.Drawing.Size(77, 13);
            this.lblOfMonth.TabIndex = 4;
            this.lblOfMonth.Text = "of the month(s)";
            // 
            // radDay
            // 
            this.radDay.AutoSize = true;
            this.radDay.Location = new System.Drawing.Point(9, 21);
            this.radDay.Name = "radDay";
            this.radDay.Size = new System.Drawing.Size(44, 17);
            this.radDay.TabIndex = 3;
            this.radDay.TabStop = true;
            this.radDay.Text = "Day";
            this.radDay.UseVisualStyleBackColor = true;
            this.radDay.CheckedChanged += new System.EventHandler(this.radDay_CheckedChanged);
            // 
            // nudDayMonth
            // 
            this.nudDayMonth.Location = new System.Drawing.Point(58, 20);
            this.nudDayMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDayMonth.Name = "nudDayMonth";
            this.nudDayMonth.Size = new System.Drawing.Size(67, 20);
            this.nudDayMonth.TabIndex = 1;
            this.nudDayMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDayMonth.ValueChanged += new System.EventHandler(this.nudWeeks_ValueChanged);
            // 
            // pnlOnce
            // 
            this.pnlOnce.Controls.Add(this.grbScheduleOnce);
            this.pnlOnce.Location = new System.Drawing.Point(16, 130);
            this.pnlOnce.Name = "pnlOnce";
            this.pnlOnce.Size = new System.Drawing.Size(330, 150);
            this.pnlOnce.TabIndex = 9;
            // 
            // grbScheduleOnce
            // 
            this.grbScheduleOnce.Controls.Add(this.timePickerOnce);
            this.grbScheduleOnce.Controls.Add(this.lblOn);
            this.grbScheduleOnce.Location = new System.Drawing.Point(2, 2);
            this.grbScheduleOnce.Name = "grbScheduleOnce";
            this.grbScheduleOnce.Size = new System.Drawing.Size(321, 66);
            this.grbScheduleOnce.TabIndex = 1;
            this.grbScheduleOnce.TabStop = false;
            this.grbScheduleOnce.Text = "Schedule Task Once";
            // 
            // timePickerOnce
            // 
            this.timePickerOnce.Location = new System.Drawing.Point(90, 25);
            this.timePickerOnce.Name = "timePickerOnce";
            this.timePickerOnce.Size = new System.Drawing.Size(186, 20);
            this.timePickerOnce.TabIndex = 7;
            this.timePickerOnce.ValueChanged += new System.EventHandler(this.timPickerOnce_ValueChanged);
            // 
            // lblOn
            // 
            this.lblOn.AutoSize = true;
            this.lblOn.Location = new System.Drawing.Point(65, 28);
            this.lblOn.Name = "lblOn";
            this.lblOn.Size = new System.Drawing.Size(19, 13);
            this.lblOn.TabIndex = 1;
            this.lblOn.Text = "on";
            // 
            // pnlIdel
            // 
            this.pnlIdel.Controls.Add(this.grbScheduleIdle);
            this.pnlIdel.Location = new System.Drawing.Point(16, 130);
            this.pnlIdel.Name = "pnlIdel";
            this.pnlIdel.Size = new System.Drawing.Size(330, 150);
            this.pnlIdel.TabIndex = 10;
            // 
            // grbScheduleIdle
            // 
            this.grbScheduleIdle.Controls.Add(this.lblMinutes);
            this.grbScheduleIdle.Controls.Add(this.nudMinutes);
            this.grbScheduleIdle.Controls.Add(this.lblIdle);
            this.grbScheduleIdle.Location = new System.Drawing.Point(3, 2);
            this.grbScheduleIdle.Name = "grbScheduleIdle";
            this.grbScheduleIdle.Size = new System.Drawing.Size(321, 66);
            this.grbScheduleIdle.TabIndex = 1;
            this.grbScheduleIdle.TabStop = false;
            this.grbScheduleIdle.Text = "Schedule When Idle";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(256, 28);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(49, 13);
            this.lblMinutes.TabIndex = 3;
            this.lblMinutes.Text = "minute(s)";
            // 
            // nudMinutes
            // 
            this.nudMinutes.Location = new System.Drawing.Point(202, 24);
            this.nudMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            this.nudMinutes.Size = new System.Drawing.Size(48, 20);
            this.nudMinutes.TabIndex = 10;
            this.nudMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblIdle
            // 
            this.lblIdle.AutoSize = true;
            this.lblIdle.Location = new System.Drawing.Point(27, 28);
            this.lblIdle.Name = "lblIdle";
            this.lblIdle.Size = new System.Drawing.Size(173, 13);
            this.lblIdle.TabIndex = 1;
            this.lblIdle.Text = "When the system has been idle for:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(213, 339);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormTaskManager
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(377, 367);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTaskManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "1-Click Maintenance Schedule";
            this.Load += new System.EventHandler(this.FrmTaskManager_Load);
            this.tbcMain.ResumeLayout(false);
            this.tabSchedule.ResumeLayout(false);
            this.tabSchedule.PerformLayout();
            this.pnlWeek.ResumeLayout(false);
            this.grbScheduleWeekly.ResumeLayout(false);
            this.grbScheduleWeekly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).EndInit();
            this.pnlDay.ResumeLayout(false);
            this.grbScheduleDaily.ResumeLayout(false);
            this.grbScheduleDaily.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).EndInit();
            this.pnlMonth.ResumeLayout(false);
            this.grbScheduleMonthly.ResumeLayout(false);
            this.grbScheduleMonthly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDayMonth)).EndInit();
            this.pnlOnce.ResumeLayout(false);
            this.grbScheduleOnce.ResumeLayout(false);
            this.grbScheduleOnce.PerformLayout();
            this.pnlIdel.ResumeLayout(false);
            this.grbScheduleIdle.ResumeLayout(false);
            this.grbScheduleIdle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.TabControl tbcMain;
		System.Windows.Forms.TabPage tabSchedule;
		System.Windows.Forms.Label lblSeparator;
		System.Windows.Forms.DateTimePicker timePicker;
		System.Windows.Forms.ComboBox cmbSelectSchedule;
		System.Windows.Forms.Label lblTask;
		System.Windows.Forms.Label lblSchedule;
		System.Windows.Forms.PictureBox pcbLogo;
		System.Windows.Forms.Panel pnlDay;
		System.Windows.Forms.GroupBox grbScheduleDaily;
		System.Windows.Forms.Label lblDays;
		System.Windows.Forms.NumericUpDown nudDays;
		System.Windows.Forms.Label lblDailyEvery;
		System.Windows.Forms.Button btnOK;
		System.Windows.Forms.Button btnCancel;
		System.Windows.Forms.Panel pnlMonth;
		System.Windows.Forms.GroupBox grbScheduleMonthly;
		System.Windows.Forms.RadioButton radDay;
		System.Windows.Forms.NumericUpDown nudDayMonth;
		System.Windows.Forms.Label lblOfMonth;
		System.Windows.Forms.Label lblOfTheMonth;
		System.Windows.Forms.ComboBox cmbday;
		System.Windows.Forms.ComboBox cmbweek;
		System.Windows.Forms.RadioButton radMonth;
		System.Windows.Forms.Panel pnlOnce;
		System.Windows.Forms.GroupBox grbScheduleOnce;
		System.Windows.Forms.DateTimePicker timePickerOnce;
		System.Windows.Forms.Label lblOn;
		System.Windows.Forms.Panel pnlIdel;
		System.Windows.Forms.GroupBox grbScheduleIdle;
		System.Windows.Forms.Label lblMinutes;
		System.Windows.Forms.NumericUpDown nudMinutes;
		System.Windows.Forms.Label lblIdle;
		System.Windows.Forms.Panel pnlWeek;
		System.Windows.Forms.GroupBox grbScheduleWeekly;
		System.Windows.Forms.CheckBox chkSat;
		System.Windows.Forms.CheckBox chkSun;
		System.Windows.Forms.CheckBox chkTue;
		System.Windows.Forms.CheckBox chkWed;
		System.Windows.Forms.CheckBox chkThu;
		System.Windows.Forms.CheckBox chkFri;
		System.Windows.Forms.CheckBox chkMon;
		System.Windows.Forms.Label lblWeeks;
		System.Windows.Forms.NumericUpDown nudWeeks;
		System.Windows.Forms.Label lblEvery;

		/// <summary>
		/// Gets a weekly schedule status from the current UI state
		/// </summary>
		/// <returns></returns>
		string GetWeeklyStatus()
		{
			string strSelectedWeek = "";

			List<string> selectedWeek = new List<string>();

			if (chkMon.Checked)
				selectedWeek.Add(chkMon.Text);

			if (chkTue.Checked)
				selectedWeek.Add(chkTue.Text);

			if (chkWed.Checked)
				selectedWeek.Add(chkWed.Text);

			if (chkThu.Checked)
				selectedWeek.Add(chkThu.Text);

			if (chkFri.Checked)
				selectedWeek.Add(chkFri.Text);

			if (chkSat.Checked)
				selectedWeek.Add(chkSat.Text);

			if (chkSun.Checked)
				selectedWeek.Add(chkSun.Text);

			for (int i = 0; i < selectedWeek.Count; i++)
			{
				strSelectedWeek += selectedWeek[i].ToString();
				if (i != selectedWeek.Count - 1)
				{
					strSelectedWeek += ", ";
				}
			}

			return strSelectedWeek;
		}

		/// <summary>
		/// Analyzes the state of the UI elements and makes correspondent changes for the settings
		/// </summary>
		/// <param name="strStatus"></param>
		void ChangeSettings(string strStatus = "")
		{
			int selectedIndex = cmbSelectSchedule.SelectedIndex;
			string scheduleLabel = "";
			if (selectedIndex == (int)Schedule.Daily)
			{
				// Temporary disabled, need to be enabled in a future
				nudDays.Enabled = false;

				scheduleLabel = String.Format(
					WPFLocalizeExtensionHelpers.GetUIString("ScheduleDailyText"),
					timePicker.Value.ToString(WPFLocalizeExtensionHelpers.GetUIString("TimeFormat"), System.Globalization.DateTimeFormatInfo.InvariantInfo)
					);

				pnlDay.Visible = true;
				pnlWeek.Visible = false;
				pnlMonth.Visible = false;
				pnlOnce.Visible = false;
				pnlIdel.Visible = false;
			}
			else if (selectedIndex == (int)Schedule.Weekly)
			{
                if (!chkMon.Checked && !chkTue.Checked && !chkWed.Checked && !chkThu.Checked && !chkFri.Checked && !chkSat.Checked &&
                !chkSun.Checked)
                {
                    scheduleLabel = string.Empty;
                }
                else
                {
                    scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("RunAt") + " " +
                                    timePicker.Value.ToString(WPFLocalizeExtensionHelpers.GetUIString("TimeFormat"), System.Globalization.DateTimeFormatInfo.InvariantInfo) + GetWeeklyStatus() + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("RunEvery") + " " +
                                    (nudWeeks.Value > 1 ? nudWeeks.Value + " " + WPFLocalizeExtensionHelpers.GetUIString("RunEveryWeeks") : WPFLocalizeExtensionHelpers.GetUIString("RunEveryWeek"));

                    var o = new
                                {
                                    time = timePicker.Value.ToString(WPFLocalizeExtensionHelpers.GetUIString("TimeFormat"), System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                    day = GetWeeklyStatus(),
                                    week = (nudWeeks.Value > 1 ? nudWeeks.Value + " " + WPFLocalizeExtensionHelpers.GetUIString("RunEveryWeeks") : WPFLocalizeExtensionHelpers.GetUIString("RunEveryWeek"))
                                };
                    scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("ScheduleWeeklyText").Inject(o);
                }

				pnlDay.Visible = false;
				pnlWeek.Visible = true;
				pnlMonth.Visible = false;
				pnlOnce.Visible = false;
				pnlIdel.Visible = false;
			}
			else if (selectedIndex == (int)Schedule.Monthly)
			{
				var o = new
				        	{
				        		time = timePicker.Value.ToString(WPFLocalizeExtensionHelpers.GetUIString("TimeFormat"), System.Globalization.DateTimeFormatInfo.InvariantInfo),
				        		day = GetMonthlyStatus()
				        	};
				scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("ScheduleMonthlyText").Inject(o);

				pnlDay.Visible = false;
				pnlWeek.Visible = false;
				pnlMonth.Visible = true;
				pnlOnce.Visible = false;
				pnlIdel.Visible = false;
			}

			else if (selectedIndex == (int)Schedule.Once)
			{
				var o = new
				        	{
				        		time = timePicker.Value.ToString(WPFLocalizeExtensionHelpers.GetUIString("TimeFormat"), System.Globalization.DateTimeFormatInfo.InvariantInfo),
				        		date = timePickerOnce.Value.Date.ToShortDateString()
				        	};
				scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("ScheduleOnceText").Inject(o);

				pnlDay.Visible = false;
				pnlWeek.Visible = false;
				pnlMonth.Visible = false;
				pnlOnce.Visible = true;
				pnlIdel.Visible = false;
			}
			else if (selectedIndex == (int)Schedule.AtLogon)
			{
				scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("RunAtLogin");
				pnlDay.Visible = false;
				pnlWeek.Visible = false;
				pnlMonth.Visible = false;
				pnlOnce.Visible = false;
				pnlIdel.Visible = false;
			}
			else if (selectedIndex == (int)Schedule.AtSystemStartUp)
			{
				scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("RunAtStartup");
				pnlDay.Visible = false;
				pnlWeek.Visible = false;
				pnlMonth.Visible = false;
				pnlOnce.Visible = false;
				pnlIdel.Visible = false;
			}
			else if (selectedIndex == (int)Schedule.WhenIdel)
			{
				scheduleLabel = WPFLocalizeExtensionHelpers.GetUIString("RunWhenIdle");
				pnlDay.Visible = false;
				pnlWeek.Visible = false;
				pnlMonth.Visible = false;
				pnlOnce.Visible = false;
				pnlIdel.Visible = true;
			}
			lblSchedule.Text = scheduleLabel;
		}

		/// <summary>
		/// Gets a monthly schedule status from the current UI state
		/// </summary>
		/// <returns></returns>
		string GetMonthlyStatus()
		{
			string strStatus = "";
			if (radDay.Checked)
			{
				strStatus += String.Format(WPFLocalizeExtensionHelpers.GetUIString("ScheduleMonthlyRunEveryDay"), nudDayMonth.Value);
			}
			else if (radMonth.Checked)
			{
				strStatus += cmbweek.SelectedItem + " " + cmbday.SelectedItem;
			}
			return strStatus;
		}

		/// <summary>
		/// Fills Windows Task details according to the current UI state
		/// </summary>
		void FillTaskDetail()
		{
			try
			{
				TaskService service = new TaskService();
				Task task = null;

				task = service.FindTask("FreeGamingBooster1ClickMaint");

				if (task != null)
				{
					chkSat.Checked = chkSun.Checked = chkMon.Checked = chkTue.Checked = chkWed.Checked = chkThu.Checked = chkFri.Checked = false;
					foreach (Trigger trg in task.Definition.Triggers)
					{
						timePicker.Value = trg.StartBoundary;
						if (
							trg.TriggerType == TaskTriggerType.Daily ||
							(trg.TriggerType == TaskTriggerType.Weekly && (trg as WeeklyTrigger).DaysOfWeek == DaysOfTheWeek.AllDays)
							)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.Daily;
							nudDays.Value = (trg as DailyTrigger).DaysInterval;
						}
						else if (trg.TriggerType == TaskTriggerType.Weekly)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.Weekly;

							WeeklyTrigger wTrigger = (trg as WeeklyTrigger);
							nudWeeks.Value = wTrigger.WeeksInterval;

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Monday)
							{ chkMon.Checked = true; }

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Tuesday)
							{ chkTue.Checked = true; }

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Wednesday)
							{ chkWed.Checked = true; }

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Thursday)
							{ chkThu.Checked = true; }

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Friday)
							{ chkFri.Checked = true; }

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Saturday)
							{ chkSat.Checked = true; }

							if (wTrigger.DaysOfWeek == DaysOfTheWeek.Sunday)
							{ chkSun.Checked = true; }
						}
						else if (trg.TriggerType == TaskTriggerType.Monthly || trg.TriggerType == TaskTriggerType.MonthlyDOW)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.Monthly;

							if (trg.TriggerType == TaskTriggerType.Monthly)
							{
								radDay.Checked = true;
								nudDayMonth.Value = (trg as MonthlyTrigger).DaysOfMonth[0];
							}
							else
							{
								radMonth.Checked = true;
								MonthlyDOWTrigger mdTrigger = trg as MonthlyDOWTrigger;

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Monday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Monday; }

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Tuesday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Tuesday; }

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Wednesday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Wednesday; }

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Thursday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Thursday; }

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Friday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Friday; }

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Saturday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Saturday; }

								if (mdTrigger.DaysOfWeek == DaysOfTheWeek.Sunday)
								{ cmbday.SelectedIndex = (int)DayOfWeek.Sunday; }

								if (mdTrigger.WeeksOfMonth == WhichWeek.FirstWeek)
								{ cmbweek.SelectedIndex = 0; }
								if (mdTrigger.WeeksOfMonth == WhichWeek.SecondWeek)
								{ cmbweek.SelectedIndex = 1; }
								if (mdTrigger.WeeksOfMonth == WhichWeek.ThirdWeek)
								{ cmbweek.SelectedIndex = 2; }
								if (mdTrigger.WeeksOfMonth == WhichWeek.FourthWeek)
								{ cmbweek.SelectedIndex = 3; }
								if (mdTrigger.WeeksOfMonth == WhichWeek.LastWeek)
								{ cmbweek.SelectedIndex = 4; }

							}
						}
						else if (trg.TriggerType == TaskTriggerType.Time)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.Once;
							timePickerOnce.Value = trg.StartBoundary;
						}
						else if (trg.TriggerType == TaskTriggerType.Logon)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.AtLogon;
						}
						else if (trg.TriggerType == TaskTriggerType.Boot)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.AtSystemStartUp;
						}
						else if (trg.TriggerType == TaskTriggerType.Idle)
						{
							cmbSelectSchedule.SelectedIndex = (int)Schedule.WhenIdel;
							nudMinutes.Value = trg.StartBoundary.Minute;
						}
					}
					ChangeSettings();
				}
			}
			catch { }
		}
	}
}