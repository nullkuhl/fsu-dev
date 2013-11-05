using System;
using System.Collections;
using System.Runtime.InteropServices;
using mScriptable;

namespace FreeGamingBooster
{
	/// <summary>
	/// Contains a <c>mScript</c> script texts to operate with the system restore points
	/// </summary>
	public class Script
	{
		#region Members

		string m_createRestorePoint = "strComputer = \".\" " + Environment.NewLine +
		                              "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\root\\default\") " +
		                              Environment.NewLine +
		                              "Set objItem = objWMIService.Get(\"SystemRestore\")" + Environment.NewLine +
                                      "CSRP = objItem.createrestorepoint (\"Free Gaming Booster\", 0, 100)" +
		                              Environment.NewLine +
		                              "return 1,1";

		string m_getRestorePoint = "Set CDatum = CreateObject(\"WbemScripting.SWbemDateTime\")" + Environment.NewLine +
		                           "strComputer = \".\"" + Environment.NewLine +
		                           "Set oWm = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\root\\default\") " +
		                           Environment.NewLine +
		                           "Set cli = oWm.ExecQuery(\"Select * from SystemRestore\") " + Environment.NewLine +
		                           "For Each objItem in cli " + Environment.NewLine +
		                           "   CDatum.Value = objItem.CreationTime " + Environment.NewLine +
		                           "   dtmCreationTime = CDatum.GetVarDate " + Environment.NewLine +
		                           "   return objItem.SequenceNumber, dtmCreationTime & \"|\" & objItem.Description" +
		                           Environment.NewLine +
		                           "Next ";

		string m_restoreSystem = "If a = \"\" Then  " + Environment.NewLine +
		                         "Set a = Nothing  " + Environment.NewLine +
		                         "WScript.Quit " + Environment.NewLine +
		                         "End If  " + Environment.NewLine +
		                         "strComputer = \".\" " + Environment.NewLine +
		                         "Set objWMIService = GetObject(\"winmgmts:\" & \"{impersonationLevel=impersonate}!\\\\\" & strComputer & \"\\root\\default\")" +
		                         Environment.NewLine +
		                         "Set objItem = objWMIService.Get(\"SystemRestore\")" + Environment.NewLine +
		                         "errResults = objItem.Restore(a) " + Environment.NewLine +
		                         "Set OpSysSet = GetObject(\"winmgmts:{(Shutdown)}//./root/cimv2\").ExecQuery(\"select * from Win32_OperatingSystem where Primary=true\") " +
		                         Environment.NewLine +
		                         "For Each OpSys In OpSysSet " + Environment.NewLine +
		                         "OpSys.Reboot()" + Environment.NewLine +
		                         "Next ";

		#endregion

		#region Public Properties

		/// <summary>
		/// Represents a script for getting the system restore points list
		/// </summary>
		public string GetRestorePoints
		{
			get { return m_getRestorePoint; }
			set { m_getRestorePoint = value; }
		}

		/// <summary>
		/// Represents a script for restoring the system
		/// </summary>
		public string RestoreSystem
		{
			get { return m_restoreSystem; }
			set { m_restoreSystem = value; }
		}

		/// <summary>
		/// Represents a script for creating the system restore point
		/// </summary>
		public string CreateRestorePoint
		{
			get { return m_createRestorePoint; }
			set { m_createRestorePoint = value; }
		}

		#endregion
	}

	/// <summary>
	/// Provides a methods to operate with a system restore point
	/// </summary>
	public class SystemRestore
	{
		readonly Script myScript = new Script();
		readonly mScript scriptManager = new mScript();

		[DllImport("Srclient.dll")]
		public static extern int SRRemoveRestorePoint(int index);

		/// <summary>
		/// Runs a <c>mScript</c> script to get the system restore points
		/// </summary>
		/// <returns>System restore points composed in a <c>Hashtable</c></returns>
		public Hashtable GetRestorePoints()
		{
            Hashtable result = new Hashtable();
			try
			{
				scriptManager.setScript(myScript.GetRestorePoints);
				result = scriptManager.runScript();
			}
			catch
			{
			}
			return result;
		}

		/// <summary>
		/// Creates a new system restore point
		/// </summary>
		public void CreateRestorePoint()
		{
			try
			{
				scriptManager.setScript(myScript.CreateRestorePoint);
				Hashtable results = scriptManager.runScript();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Restores a system state for a specified sequence no
		/// </summary>
		/// <param name="sequenceNo">Sequence no for restoring</param>
		public void RestoreSystem(int sequenceNo)
		{
			try
			{
				scriptManager.setScript("a=" + sequenceNo + ":" + myScript.RestoreSystem);
				Hashtable results = scriptManager.runScript();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Deletes a system restore point for a specified sequence no
		/// </summary>
		/// <param name="sequenceNo">>Sequence no for deleting</param>
		public void DeleteRestorePoint(int sequenceNo)
		{
			try
			{
				SRRemoveRestorePoint(sequenceNo);
			}
			catch
			{
			}
		}
	}
}