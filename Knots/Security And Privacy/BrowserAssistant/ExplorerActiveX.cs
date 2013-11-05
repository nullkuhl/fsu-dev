using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Win32;

namespace BrowserAssistant
{
	/// <summary>
	/// IE ActiveX control model
	/// </summary>
	public class ExplorerActiveX
	{
		readonly int flagsOld;
		int flags;

		/// <summary>
		/// constructor for ExplorerActiveX
		/// </summary>
		/// <param name="CLSID"></param>
		/// <param name="name"></param>
		/// <param name="path"></param>
		/// <param name="dword"></param>
		ExplorerActiveX(string CLSID, string name, string path, int dword)
		{
			Id = CLSID;
			Name = name;
			Path = path;
			flags = flagsOld = dword;
		}

		/// <summary>
		/// Id
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Path
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Flags
		/// </summary>
		public string Flags
		{
			get { return "0x" + flags.ToString("x"); }
			set
			{
				if (value.StartsWith("0x"))
					try
					{
						flags = int.Parse(value.Substring(2), NumberStyles.HexNumber);
					}
					catch
					{
					}
				else
					try
					{
						flags = int.Parse(value);
					}
					catch
					{
					}
			}
		}

		/// <summary>
		/// Is enabled
		/// </summary>
		public bool IsEnabled
		{
			get { return (flags & 0x400) == 0; }
			set
			{
				if (value)
					flags = flags & (-0x400 - 1);
				else
					flags = flags | 0x400;
			}
		}

		/// <summary>
		/// Has changed
		/// </summary>
		public bool HasChanged
		{
			get { return flags != flagsOld; }
		}

		/// <summary>
		/// get a list of internet explorer activex
		/// </summary>
		/// <returns></returns>
		public static List<ExplorerActiveX> List()
		{
			var registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE");
			if (registryKey != null)
			{
				var subKey = registryKey.OpenSubKey("Microsoft");
				if (subKey != null)
				{
					var key = subKey.OpenSubKey("Internet Explorer");
					if (key != null)
					{
						var openSubKey1 = key.OpenSubKey("ActiveX Compatibility");
						if (openSubKey1 != null)
						{
							var CLSIDs = openSubKey1.GetSubKeyNames().Where(s => s.StartsWith("{"));
							var result = new List<ExplorerActiveX>();

							foreach (string clsid in CLSIDs)
							{
								try
								{
									var openSubKey = Registry.ClassesRoot.OpenSubKey("CLSID");
									if (openSubKey != null)
									{
										RegistryKey classKey = openSubKey.OpenSubKey(clsid);
										if (classKey == null) continue;
									}

									string name = Helper.GetNameFromClsid(clsid);
									if (string.IsNullOrEmpty(name)) continue;

									string path = Helper.GetPathFromClsid(clsid);

									var registryKey1 = openSubKey1.OpenSubKey(clsid);
									if (registryKey1 != null)
									{
										object flags = registryKey1.GetValue("Compatibility Flags");

										if (flags != null)
											result.Add(new ExplorerActiveX(clsid, name, path, (int)flags));
									}
								}
								catch
								{
								}
							}

							return result;
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// save changes to internet explorer activex
		/// </summary>
		/// <param name="activex"></param>
		public static void SaveChanges(IEnumerable<ExplorerActiveX> activex)
		{
			foreach (var a in activex)
			{
				var openSubKey = Registry.LocalMachine.OpenSubKey("SOFTWARE");
				if (openSubKey == null) continue;
				var registryKey = openSubKey.OpenSubKey("Microsoft");
				if (registryKey == null) continue;
				var subKey = registryKey.OpenSubKey("Internet Explorer");
				if (subKey == null) continue;
				var key = subKey
					.OpenSubKey("ActiveX Compatibility");
				if (key == null) continue;
				var openSubKey1 = key.OpenSubKey(a.Id, true);
				if (openSubKey1 == null) continue;
				openSubKey1.SetValue("Compatibility Flags", a.flags, RegistryValueKind.DWord);
			}
		}
	}
}