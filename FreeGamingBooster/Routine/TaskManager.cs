using System;
using Microsoft.Win32.TaskScheduler;

/// <summary>
/// The <see cref="FreeGamingBooster.Routine"/> namespace contains a set of routine classes
/// of the <see cref="FreeGamingBooster"/> project
/// </summary>
namespace FreeGamingBooster.Routine
{
    /// <summary>
    /// Provides a methods to operate with a Windows Task Manager
    /// </summary>
    public class TaskManager
    {
        const string ScanAndFixCommandLineArg = "ScanFix";

        #region Public Methods

        /// <summary>
        /// Gets task by its name
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <returns></returns>
        public static Task GetTaskByName(string taskName)
        {
            Task task = null;
            try
            {
                TaskService service = new TaskService();
                task = service.FindTask(taskName, true);
            }
            catch
            {
                return null;
            }

            return task;
        }

        /// <summary>
        /// Check if the task in schedule
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <returns>true - if the task scheduled, false - otherwise</returns>
        public static bool IsTaskScheduled(string taskName)
        {
            Task task = GetTaskByName(taskName);
            if (task != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets task description
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <returns></returns>
        public static string GetTaskDescription(string taskName)
        {
            string taskDescription = "";
            Task task = GetTaskByName(taskName);
            if (task != null)
            {
                foreach (Trigger trg in task.Definition.Triggers)
                {
                    taskDescription += trg + ". ";
                }
            }
            if (taskDescription.IndexOf(", starting") > 0)
                return taskDescription.Substring(0, (taskDescription.Length - taskDescription.IndexOf(", starting")) + 1);
            else
                return taskDescription;
        }

        /// <summary>
        /// Deletes task
        /// </summary>
        /// <param name="taskName">task name</param>
        public static void DeleteTask(string taskName)
        {
            Task task = null;
            try
            {
                TaskService service = new TaskService();
                task = service.FindTask(taskName, true);

                if (task != null)
                    service.RootFolder.DeleteTask(taskName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Updates task status
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <param name="isEnabled">status of task - enabled/disabled</param>
        public static void UpdateTaskStatus(string taskName, bool isEnabled)
        {
            try
            {
                TaskService service = new TaskService();
                Task task = service.FindTask(taskName, true);

                if (task != null)
                {
                    task.Enabled = isEnabled;
                    task.RegisterChanges();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Updates action type of task - "scan only" or "scan and fix"
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <param name="scanOnly">true - if we need to scan only, false if we need to scan and fix</param>
        public static void UpdateTaskActionType(string taskName, bool scanOnly)
        {
            try
            {
                TaskService service = new TaskService();
                Task task = service.FindTask(taskName, true);

                if (task != null)
                {
                    if (scanOnly)
                    {
                        task.Definition.Actions[0] = new ExecAction(Environment.CurrentDirectory + "\\1Click.exe", null, Environment.CurrentDirectory);
                    }
                    else
                    {
                        task.Definition.Actions[0] = new ExecAction(Environment.CurrentDirectory + "\\1Click.exe",
                                                                    ScanAndFixCommandLineArg, Environment.CurrentDirectory);
                    }
                    task.RegisterChanges();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Create default task in the Task Scheduler service
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <param name="isEnabled">true - if enabled, false - otherwise</param>
        public static void CreateDefaultTask(string taskName, bool isEnabled)
        {
            try
            {
                DeleteTask(taskName);

                TaskService service = new TaskService();
                TaskDefinition td = service.NewTask();

                td.Settings.Enabled = isEnabled;
                td.RegistrationInfo.Description = "FreeGamingBooster 1 ClickMaint";
                td.Principal.RunLevel = TaskRunLevel.Highest;
                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction(Environment.CurrentDirectory + "\\1Click.exe", null, Environment.CurrentDirectory));                
                WeeklyTrigger mTrigger = new WeeklyTrigger();
                mTrigger.DaysOfWeek = DaysOfTheWeek.Friday;
                mTrigger.StartBoundary = DateTime.Today.AddHours(12);

                mTrigger.Repetition.StopAtDurationEnd = false;
                td.Triggers.Add(mTrigger);
                // Register the task in the root folder
                service.RootFolder.RegisterTaskDefinition(taskName, td);
            }
            catch
            {
            }
        }

        #endregion
    }
}