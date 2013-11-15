using System;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using FreeToolbarRemover.ViewModels;
using Application = System.Windows.Application;
using FreemiumUtilities.Infrastructure;

namespace FreeToolbarRemover
{
    /// <summary>
    /// Task manager settings form
    /// </summary>
    public partial class FormTaskManager : Form
    {
        /// <summary>
        /// constructor for FormTaskManager
        /// </summary>
        public FormTaskManager()
        {
            InitializeComponent();
            this.cmbSelectSchedule.SelectedIndexChanged -= new System.EventHandler(this.cmbSelectSchedule_SelectedIndexChanged);
            UpdateUILocalization();
            cmbSelectSchedule.SelectedIndex = 0;
            FillTaskDetail();
            this.cmbSelectSchedule.SelectedIndexChanged += new System.EventHandler(this.cmbSelectSchedule_SelectedIndexChanged);
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            Text = WPFLocalizeExtensionHelpers.GetUIString("OneClickSchedule");
            tabSchedule.Text = WPFLocalizeExtensionHelpers.GetUIString("TabScheduleText");
            lblSchedule.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelScheduleText");
            lblTask.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelScheduleTaskText");
            grbScheduleDaily.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleTaskDaily");
            grbScheduleWeekly.Text = WPFLocalizeExtensionHelpers.GetUIString("GroupBoxScheduleTaskWeeklyText");
            grbScheduleMonthly.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleTaskMonthly");
            grbScheduleOnce.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleTaskOnce");
            grbScheduleIdle.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleWhenIdle");
            lblDays.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleTaskDays");
            lblWeeks.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelWeeksOnText");
            lblEvery.Text = WPFLocalizeExtensionHelpers.GetUIString("Every");
            lblOfMonth.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleTaskMonth");
            lblOfTheMonth.Text = WPFLocalizeExtensionHelpers.GetUIString("ScheduleTaskMonth");
            lblOn.Text = WPFLocalizeExtensionHelpers.GetUIString("RunOn");
            lblIdle.Text = WPFLocalizeExtensionHelpers.GetUIString("WhenSystemIdle");
            lblMinutes.Text = WPFLocalizeExtensionHelpers.GetUIString("RunOnMinutes");
            lblDailyEvery.Text = WPFLocalizeExtensionHelpers.GetUIString("Every");
            chkMon.Text = WPFLocalizeExtensionHelpers.GetUIString("Monday");
            chkTue.Text = WPFLocalizeExtensionHelpers.GetUIString("Tuesday");
            chkWed.Text = WPFLocalizeExtensionHelpers.GetUIString("Wednesday");
            chkThu.Text = WPFLocalizeExtensionHelpers.GetUIString("Thursday");
            chkFri.Text = WPFLocalizeExtensionHelpers.GetUIString("Friday");
            chkSat.Text = WPFLocalizeExtensionHelpers.GetUIString("Saturday");
            chkSun.Text = WPFLocalizeExtensionHelpers.GetUIString("Sunday");
            btnOK.Text = WPFLocalizeExtensionHelpers.GetUIString("OK");
            btnCancel.Text = WPFLocalizeExtensionHelpers.GetUIString("Cancel");
            radDay.Text = WPFLocalizeExtensionHelpers.GetUIString("Day");
            radMonth.Text = WPFLocalizeExtensionHelpers.GetUIString("Month");

            cmbSelectSchedule.Items.Clear();
            cmbSelectSchedule.Items.AddRange(new object[]
			                                 	{
			                                 		WPFLocalizeExtensionHelpers.GetUIString("Daily"),
			                                 		WPFLocalizeExtensionHelpers.GetUIString("Weekly"),
			                                 		WPFLocalizeExtensionHelpers.GetUIString("Monthly"),
			                                 		WPFLocalizeExtensionHelpers.GetUIString("Once"),
			                                 		WPFLocalizeExtensionHelpers.GetUIString("AtSystemStartup"),
			                                 		WPFLocalizeExtensionHelpers.GetUIString("AtLogon"),
			                                 		WPFLocalizeExtensionHelpers.GetUIString("WhenIdle")
			                                 	});

            cmbday.Items.Clear();
            cmbday.Items.AddRange(new object[]
			                      	{
			                      		WPFLocalizeExtensionHelpers.GetUIString("MondayFull"),
			                      		WPFLocalizeExtensionHelpers.GetUIString("TuesdayFull"),
			                      		WPFLocalizeExtensionHelpers.GetUIString("WednesdayFull"),
			                      		WPFLocalizeExtensionHelpers.GetUIString("ThursdayFull"),
			                      		WPFLocalizeExtensionHelpers.GetUIString("FridayFull"),
			                      		WPFLocalizeExtensionHelpers.GetUIString("SaturdayFull"),
			                      		WPFLocalizeExtensionHelpers.GetUIString("SundayFull")
			                      	});

            cmbweek.Items.Clear();
            cmbweek.Items.AddRange(new object[]
			                       	{
			                       		WPFLocalizeExtensionHelpers.GetUIString("First"),
			                       		WPFLocalizeExtensionHelpers.GetUIString("Second"),
			                       		WPFLocalizeExtensionHelpers.GetUIString("Third"),
			                       		WPFLocalizeExtensionHelpers.GetUIString("Fourth"),
			                       		WPFLocalizeExtensionHelpers.GetUIString("Last")
			                       	});
        }

        /// <summary>
        /// Closes the task manager form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Schedule type combobox selected item changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbSelectSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectSchedule.SelectedIndex == (int)Schedule.Monthly)
            {
                radDay.Checked = true;
                cmbweek.SelectedIndex = 0;
                cmbday.SelectedIndex = 0;
            }
            else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.Weekly)
            {
                chkMon.Checked = true;
            }
            ChangeSettings();
        }

        /// <summary>
        /// Days numeric up down control value changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nudDays_ValueChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        /// <summary>
        /// Day radiobutton value changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void radDay_CheckedChanged(object sender, EventArgs e)
        {
            if (radDay.Checked)
            {
                nudDayMonth.Enabled = radDay.Checked;
                cmbweek.Enabled = radMonth.Checked;
                cmbday.Enabled = radMonth.Checked;
            }
            else if (radMonth.Checked)
            {
                nudDayMonth.Enabled = radDay.Checked;
                cmbweek.Enabled = radMonth.Checked;
                cmbday.Enabled = radMonth.Checked;
            }

            ChangeSettings();
        }

        /// <summary>
        /// Time picker value changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timePicker_ValueChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        /// <summary>
        /// Weeks numeric up down control value changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nudWeeks_ValueChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        /// <summary>
        /// Day checkbox checked event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkDay_CheckedChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        /// <summary>
        /// Week combobox selected item changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        /// <summary>
        /// Time picker value changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timPickerOnce_ValueChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        /// <summary>
        /// Saves all the settings setted in the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnOK_Click(object sender, EventArgs e)
        {
            bool isNewTask = true;
            try
            {
                TaskService service = new TaskService();
                TaskDefinition td = service.NewTask();
                TriggerCollection trgCollection;
                DateTime oldTriggerDate = new DateTime();

                Task task = service.FindTask("FreeToolbarRemover1ClickMaint");

                if (task != null)
                {
                    isNewTask = false;
                    oldTriggerDate = task.Definition.Triggers.Count > 0
                                        ? task.Definition.Triggers[0].StartBoundary.Date
                                        : DateTime.Today;
                    task.Definition.Triggers.Clear();
                    trgCollection = task.Definition.Triggers;
                }
                else
                {
                    td.RegistrationInfo.Description = "FreeToolbarRemover 1 ClickMaint";
                    td.Settings.Enabled = true;
                    td.Actions.Add(new ExecAction(Environment.CurrentDirectory + "\\1Click.exe"));
                    trgCollection = td.Triggers;
                }

                if (cmbSelectSchedule.SelectedIndex == (int)Schedule.Daily)
                {
                    DailyTrigger dTrigger = new DailyTrigger { DaysInterval = (short)nudDays.Value };

                    if (isNewTask)
                        dTrigger.StartBoundary = DateTime.Today.Date + timePicker.Value.TimeOfDay;
                    else
                        dTrigger.StartBoundary = oldTriggerDate + timePicker.Value.TimeOfDay;

                    trgCollection.Add(dTrigger);
                }
                else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.Weekly)
                {
                    WeeklyTrigger wTrigger = new WeeklyTrigger();

                    if (!chkMon.Checked && !chkTue.Checked && !chkWed.Checked && !chkThu.Checked && !chkFri.Checked && !chkSat.Checked &&
                        !chkSun.Checked)
                    {
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("select_day"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (chkMon.Checked && chkTue.Checked && chkWed.Checked && chkThu.Checked && chkFri.Checked && chkSat.Checked &&
                        chkSun.Checked)
                    {
                        wTrigger.DaysOfWeek = DaysOfTheWeek.AllDays;
                        trgCollection.Add(wTrigger);
                        cmbSelectSchedule.SelectedIndex = (int)Schedule.Daily;
                    }
                    else
                    {
                        if (chkMon.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Monday };
                            trgCollection.Add(wTrigger);
                        }
                        if (chkTue.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Tuesday };
                            trgCollection.Add(wTrigger);
                        }
                        if (chkWed.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Wednesday };
                            trgCollection.Add(wTrigger);
                        }
                        if (chkThu.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Thursday };
                            trgCollection.Add(wTrigger);
                        }
                        if (chkFri.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Friday };
                            trgCollection.Add(wTrigger);
                        }
                        if (chkSat.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Saturday };
                            trgCollection.Add(wTrigger);
                        }
                        if (chkSun.Checked)
                        {
                            wTrigger = new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Sunday };
                            trgCollection.Add(wTrigger);
                        }
                    }

                    foreach (WeeklyTrigger trg in trgCollection)
                    {
                        if (isNewTask)
                            trg.StartBoundary = DateTime.Today.Date + timePicker.Value.TimeOfDay;
                        else
                            trg.StartBoundary = oldTriggerDate + timePicker.Value.TimeOfDay;

                        trg.WeeksInterval = (short)nudWeeks.Value;
                    }
                }
                else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.Monthly)
                {
                    if (radDay.Checked)
                    {
                        MonthlyTrigger mTrigger = new MonthlyTrigger();

                        if (isNewTask)
                            mTrigger.StartBoundary = DateTime.Today.Date + timePicker.Value.TimeOfDay;
                        else
                            mTrigger.StartBoundary = oldTriggerDate + timePicker.Value.TimeOfDay;


                        mTrigger.MonthsOfYear = MonthsOfTheYear.AllMonths;
                        mTrigger.DaysOfMonth = new[] { (int)nudDayMonth.Value };

                        trgCollection.Add(mTrigger);
                    }
                    else if (radMonth.Checked)
                    {
                        MonthlyDOWTrigger mdTrigger = new MonthlyDOWTrigger();

                        if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("MondayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Monday;
                        }
                        else if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("TuesdayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Tuesday;
                        }
                        else if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("WednesdayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Wednesday;
                        }
                        else if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("ThursdayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Thursday;
                        }
                        else if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("FridayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Friday;
                        }
                        else if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("SaturdayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Saturday;
                        }
                        else if (cmbday.Text == WPFLocalizeExtensionHelpers.GetUIString("SundayFull"))
                        {
                            mdTrigger.DaysOfWeek = DaysOfTheWeek.Sunday;
                        }

                        if (cmbweek.SelectedIndex == 0)
                        {
                            mdTrigger.WeeksOfMonth = WhichWeek.FirstWeek;
                        }
                        else if (cmbweek.SelectedIndex == 1)
                        {
                            mdTrigger.WeeksOfMonth = WhichWeek.SecondWeek;
                        }
                        else if (cmbweek.SelectedIndex == 2)
                        {
                            mdTrigger.WeeksOfMonth = WhichWeek.ThirdWeek;
                        }
                        else if (cmbweek.SelectedIndex == 3)
                        {
                            mdTrigger.WeeksOfMonth = WhichWeek.FourthWeek;
                        }
                        else if (cmbweek.SelectedIndex == 4)
                        {
                            mdTrigger.WeeksOfMonth = WhichWeek.LastWeek;
                        }

                        mdTrigger.MonthsOfYear = MonthsOfTheYear.AllMonths;
                        mdTrigger.StartBoundary = DateTime.Today.Date + timePicker.Value.TimeOfDay;

                        trgCollection.Add(mdTrigger);
                    }
                }
                else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.Once)
                {
                    TimeTrigger tTrigger = new TimeTrigger { StartBoundary = timePickerOnce.Value.Date + timePicker.Value.TimeOfDay };
                    trgCollection.Add(tTrigger);
                }
                else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.AtSystemStartUp)
                {
                    BootTrigger bTrigger = new BootTrigger { Delay = TimeSpan.FromMinutes(2) };
                    trgCollection.Add(bTrigger);
                }
                else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.AtLogon)
                {
                    LogonTrigger lTrigger = new LogonTrigger { Delay = TimeSpan.FromSeconds(1) };
                    trgCollection.Add(lTrigger);
                }
                else if (cmbSelectSchedule.SelectedIndex == (int)Schedule.WhenIdel)
                {
                    IdleTrigger iTrigger = new IdleTrigger();
                    if (isNewTask)
                        iTrigger.StartBoundary = DateTime.Today.Date + TimeSpan.FromMinutes((double)nudMinutes.Value);
                    else
                        iTrigger.StartBoundary = oldTriggerDate + TimeSpan.FromMinutes((double)nudMinutes.Value);

                    trgCollection.Add(iTrigger);
                }

                // Register the task in the root folder
                if (isNewTask)
                    service.RootFolder.RegisterTaskDefinition(@"FreeToolbarRemover1ClickMaint", td);
                else
                    task.RegisterChanges();

                ((OneClickAppsViewModel)Application.Current.MainWindow.DataContext).SchedulerText = lblSchedule.Text;
                
                Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Task Manager form load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmTaskManager_Load(object sender, EventArgs e)
        {
            FillTaskDetail();
        }
    }

    #region Enums

    /// <summary>
    /// Schedule types collection
    /// </summary>
    public enum Schedule
    {
        Daily = 0,
        Weekly = 1,
        Monthly = 2,
        Once = 3,
        AtSystemStartUp = 4,
        AtLogon = 5,
        WhenIdel = 6
    }

    /// <summary>
    /// Days of the week collection
    /// </summary>
    public enum DayOfWeek
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }

    #endregion
}