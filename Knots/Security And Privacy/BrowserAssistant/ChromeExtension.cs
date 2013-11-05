using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BrowserAssistant
{
	/// <summary>
	/// Chrome extension model
	/// </summary>
	public class ChromeExtension : IBrowserExtension
	{
		/// <summary>
		/// Is old version
		/// </summary>
		public static bool IsOldVer;

		/// <summary>
		/// Is x86
		/// </summary>
		public static bool IsX86;

		static JObject jsonObj;
		static bool isWinXP;

		/// <summary>
		/// Path to the Chrome preferences directory
		/// </summary>
		public static string ChromePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
										  @"\Google\Chrome\User Data\Default\Preferences";

		readonly JProperty jsonProp;
		readonly int state;
		bool hasChanged;
		bool isEnabled;

		/// <summary>
		/// constructor for ChromeExtension
		/// </summary>
		/// <param name="j"></param>
		public ChromeExtension(JProperty j)
		{
			jsonProp = j;
			Id = j.Name;
			Name = j.Value["manifest"]["name"].ToString();
			Version = j.Value["manifest"]["version"].ToString();
			state = j.Value["state"].Value<int>();
			IsEnabled = state == 1;
		}

		#region IBrowserExtension Members

		/// <summary>
		/// Id
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Version
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Is enabled
		/// </summary>
		public bool IsEnabled
		{
			get { return isEnabled; }
			set
			{
				hasChanged = value && state == 0 || !value && state == 1;
				isEnabled = value;
			}
		}

		#endregion

		/// <summary>
		/// check if chrome installed
		/// </summary>
		/// <returns></returns>
		public static bool IsBrowserInstalled()
		{
			//var appDataPath = Environment.GetEnvironmentVariable("AppData");
			//var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");

			if (File.Exists(ChromePath))
			{
				return true;
			}

			/*
			string strFilepath = userProfilePath + @"\Local Settings\Application Data\Google\Chrome\User Data\Default\Preferences";
			if (File.Exists(strFilepath))
			{
				return true;
			}
			else
			{
				strFilepath = appDataPath + @"\..\Local\Google\Chrome\User Data\Default\Preferences";
				if (File.Exists(strFilepath))
				{
					return true;
				}
			}*/

			return false;
		}

		/// <summary>
		/// get a list of chrome extensions
		/// </summary>
		/// <returns></returns>
		public static ChromeExtension[] List()
		{
			/*var appDataPath = Environment.GetEnvironmentVariable("AppData");
			var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
			if (appDataPath.EndsWith("Roaming"))
			{
				appDataPath = appDataPath.Replace("\\Roaming", "");
			}*/

			try
			{
				jsonObj = JObject.Parse(File.ReadAllText(ChromePath));
			}
			catch (DirectoryNotFoundException)
			{
				isWinXP = true;
				jsonObj = JObject.Parse(File.ReadAllText(ChromePath));
			}

			if (jsonObj["extensions"]["settings"] == null)
			{
				return null;
			}
			return
				jsonObj["extensions"]["settings"].Where(j => j.First.Children<JProperty>().Count(jp => jp.Name == "manifest") > 0)
					.Select(j => new ChromeExtension((JProperty)j)).ToArray();
		}

		/// <summary>
		/// save changes to chrome extensions
		/// </summary>
		/// <param name="extensions"></param>
		public static void SaveChanges(IEnumerable<ChromeExtension> extensions)
		{
			foreach (ChromeExtension e in extensions.Where(ex => ex.hasChanged))
				((JValue)e.jsonProp.Value["state"]).Value = e.IsEnabled ? 1 : 0;

			File.WriteAllText(ChromePath, jsonObj.ToString());
		}
	}
}