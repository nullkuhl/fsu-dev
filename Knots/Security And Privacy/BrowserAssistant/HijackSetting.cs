using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace BrowserAssistant
{
    /// <summary>
    /// HijackSetting model
    /// </summary>
    public interface IHijackSetting
    {
        string Name { get; }
        string Current { get; }
        string Default { get; }
        bool Restore { get; set; }

        void DoRestore();
    }

    /// <summary>
    /// IE HijackSetting
    /// </summary>
    public class IeHijackSetting : IHijackSetting
    {
        readonly string regKey;
        readonly string valueName;

        /// <summary>
        /// constructor for IeHijackSetting
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="key"></param>
        /// <param name="valName"></param>
        public IeHijackSetting(string name, string defVal, string key, string valName)
        {
            Name = name;
            Default = defVal;
            regKey = key;
            valueName = valName;
            try
            {
                Current = (string)Registry.GetValue(regKey, valueName, string.Empty);
            }
            catch
            {
            }
        }

        #region HijackSetting Members

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current value
        /// </summary>
        public string Current { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Restore
        /// </summary>
        public bool Restore { get; set; }

        /// <summary>
        /// restore default values
        /// </summary>
        public void DoRestore()
        {
            try
            {
                Registry.SetValue(regKey, valueName, Default, RegistryValueKind.String);
            }
            catch
            {
            }
        }

        #endregion
    }

    /// <summary>
    /// Firefox HijackSetting
    /// </summary>
    public class FfHijackSetting : IHijackSetting
    {
        static string[] prefLines;
        static string prefPath;
        static bool requireSave;

        readonly string[] curValues;
        readonly string[] defValues;
        readonly int[] prefIndexes;

        /// <summary>
        /// constructor for FfHijackSetting
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prefNames"></param>
        /// <param name="defaults"></param>
        public FfHijackSetting(string name, string[] prefNames, string[] defaults)
        {
            Name = name;
            defValues = defaults;

            if (prefLines == null)
            {
                string appDataPath = Environment.GetEnvironmentVariable("AppData");
                prefPath = File.ReadAllLines(appDataPath + @"\Mozilla\Firefox\profiles.ini")
                            .First(s => s.StartsWith("Path=")).Substring(5).Replace('/', '\\')
                            .Insert(0, appDataPath + @"\Mozilla\Firefox\") + "\\prefs.js";

                prefLines = File.ReadAllLines(prefPath);
            }

            curValues = new string[prefNames.Length];
            prefIndexes = new int[prefNames.Length];

            for (int i = 0; i < prefNames.Length; i++)
                for (int j = 0; j < prefLines.Length; j++)
                    if (prefLines[j].StartsWith("user_pref(\"" + prefNames[i] + "\""))
                    {
                        curValues[i] = prefLines[j].Split('\"')[3];
                        prefIndexes[i] = j;
                    }

            Current = curValues[0];
            Default = defValues[0];
        }

        #region HijackSetting Members


        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current value
        /// </summary>
        public string Current { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Restore
        /// </summary>
        public bool Restore { get; set; }

        /// <summary>
        /// Restore default values
        /// </summary>
        public void DoRestore()
        {
            for (int i = 0; i < prefIndexes.Length; i++)
            {
                if (curValues.Length > 0 && curValues[i] != null)
                {
                    prefLines[prefIndexes[i]] = prefLines[prefIndexes[i]].Replace(curValues[i], defValues[i]);
                }
            }
            requireSave = true;
        }

        #endregion

        /// <summary>
        /// save preferences
        /// </summary>
        public static void Save()
        {
            try
            {
                if (requireSave)
                {
                    File.WriteAllLines(prefPath, prefLines);
                }
                requireSave = false;
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// Chrome HijackSetting
    /// </summary>
    public class ChHijackSetting : IHijackSetting
    {
        static JObject prefObj;
        static string prefPath;
        static bool requireSave;

        readonly JProperty currProp;
        readonly JToken defToken;

        /// <summary>
        /// constructor for ChHijackSetting
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propName"></param>
        /// <param name="displayProp"></param>
        /// <param name="defValue"></param>
        public ChHijackSetting(string name, string propName, string displayProp, string defValue)
        {
            Name = name;
            defToken = JToken.Parse(defValue);

            if (prefObj == null)
            {
                prefPath = Environment.GetEnvironmentVariable("AppData") + @"\..\Local\Google\Chrome\User Data\Default\Preferences";
                if (!File.Exists(prefPath))
                    prefPath = Environment.GetEnvironmentVariable("USERPROFILE") +
                               @"\Local Settings\Application Data\Google\Chrome\User Data\Default\Preferences";
                prefObj = JObject.Parse(File.ReadAllText(prefPath));
            }

            currProp = prefObj.Property(propName);

            if (displayProp == null)
            {
                Current = currProp.Value.ToString();
                Default = defToken.ToString();
            }
            else
            {
                Current = currProp.Value[displayProp].ToString();
                Default = defToken[displayProp].ToString();
            }
        }

        #region HijackSetting Members

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current value
        /// </summary>
        public string Current { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Restore
        /// </summary>
        public bool Restore { get; set; }

        /// <summary>
        /// restore default values
        /// </summary>
        public void DoRestore()
        {
            currProp.Value = defToken;
            requireSave = true;
        }

        #endregion

        /// <summary>
        /// save preferences
        /// </summary>
        public static void Save()
        {
            try
            {
                if (requireSave)
                    File.WriteAllText(prefPath, prefObj.ToString());

                requireSave = false;
            }
            catch
            {
            }
        }
    }
}