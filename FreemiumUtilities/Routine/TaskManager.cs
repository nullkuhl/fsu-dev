using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32.TaskScheduler;

namespace FreemiumUtilities.Routine
{
	/// <summary>
	/// The <see cref="FreemiumUtilities.Routine"/> namespace contains a set of routine classes
	/// of the <see cref="FreemiumUtilities"/> project
	/// </summary>
	[CompilerGenerated]
	internal class NamespaceDoc
	{
	}

	/// <summary>
	/// Provides a methods to operate with a Windows Task Manager
	/// </summary>
	public class TaskManager
	{
		const string ScanAndFixCommandLineArg = "ScanFix";

		#region Public Methods

		public static Task GetTaskByName(string taskName)
		{
			Task task = null;
			try
			{
				var service = new TaskService();
				task = service.FindTask(taskName, true);
			}
			catch
			{
				return null;
			}

			return task;
		}

		public static bool IsTaskScheduled(string taskName)
		{
			Task task = GetTaskByName(taskName);
			if (task != null)
				return true;
			else
				return false;
		}

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

		public static void DeleteTask(string taskName)
		{
			Task task = null;
			try
			{
				var service = new TaskService();
				task = service.FindTask(taskName, true);

				if (task != null)
					service.RootFolder.DeleteTask(taskName);
			}
			catch
			{
			}
		}

		public static void UpdateTaskStatus(string taskName, bool isEnabled)
		{
			try
			{
				var service = new TaskService();
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

		public static void UpdateTaskActionType(string taskName, bool scanOnly)
		{
			try
			{
				var service = new TaskService();
				Task task = service.FindTask(taskName, true);

				if (task != null)
				{
					if (scanOnly)
					{
						task.Definition.Actions[0] = new ExecAction(Environment.CurrentDirectory + "\\1Click.exe", null, null);
					}
					else
					{
						task.Definition.Actions[0] = new ExecAction(Environment.CurrentDirectory + "\\1Click.exe",
						                                            ScanAndFixCommandLineArg, null);
					}
					task.RegisterChanges();
				}
			}
			catch
			{
			}
		}

		public static void CreateDefaultTask(string taskName, bool isEnabled)
		{
			try
			{
				DeleteTask(taskName);

				var service = new TaskService();
				TaskDefinition td = service.NewTask();

				td.Settings.Enabled = isEnabled;
				td.RegistrationInfo.Description = "Freemium 1 ClickMaint";

				// Create an action that will launch Notepad whenever the trigger fires
				td.Actions.Add(new ExecAction(Environment.CurrentDirectory + "\\1Click.exe", null, null));

				var mTrigger = new WeeklyTrigger();
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