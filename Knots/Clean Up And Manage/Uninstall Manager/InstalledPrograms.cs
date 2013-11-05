using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Uninstall_Manager
{
    /// <summary>
    /// Represents a program that is installed on a PC
    /// </summary>
    public class InstalledProgram : IComparable<InstalledProgram>, IEquatable<InstalledProgram>
    {
        /// <summary>
        /// Uninstall keys
        /// </summary>
        public static Dictionary<string, string> UninstallKeys;

        #region "Properties"

        string displayName = string.Empty;
        string parentDisplayName = string.Empty;
        string publisher = String.Empty;

        string version = string.Empty;

        /// <summary>
        /// The name that would be displayed in Add/Remove Programs
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        /// <summary>
        /// The version of the program
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        /// If this program is an update then this will contain the display name of the product that 
        /// this is an update to
        /// </summary>
        public string ParentDisplayName
        {
            get { return parentDisplayName; }
            set { parentDisplayName = value; }
        }

        /// <summary>
        /// Is this program classed as an update 
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// Publisher
        /// </summary>
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        /// <summary>
        /// Install date
        /// </summary>
        public string InstallDate { get; set; }

        /// <summary>
        /// Install size
        /// </summary>
        public string InstallSize { get; set; }

        /// <summary>
        /// Display icon path
        /// </summary>
        public string DisplayIconPath { get; set; }

        #endregion

        #region "Constructors"

        /// <summary>
        /// InstalledProgram constructor
        /// </summary>
        /// <param name="programDisplayName">Program display name</param>
        public InstalledProgram(string programDisplayName)
        {
            DisplayName = programDisplayName;
        }

        /// <summary>
        /// InstalledProgram constructor
        /// </summary>
        /// <param name="programDisplayName">Program display name</param>
        /// <param name="programParentDisplayName">Program parent display name</param>
        /// <param name="isProgramUpdate">Is program update</param>
        /// <param name="programVersion">Program version</param>
        public InstalledProgram(string programDisplayName, string programParentDisplayName, bool isProgramUpdate, string programVersion)
        {
            DisplayName = programDisplayName;
            ParentDisplayName = programParentDisplayName;
            IsUpdate = isProgramUpdate;
            Version = programVersion;
        }

        /// <summary>
        /// InstalledProgram constructor
        /// </summary>
        /// <param name="programDisplayName">Program display name</param>
        /// <param name="programParentDisplayName">Program parent display name</param>
        /// <param name="isProgramUpdate">Is program update</param>
        /// <param name="programVersion">Program version</param>
        /// <param name="programPublisher">Program publisher</param>
        /// <param name="programInstallDate">Install date</param>
        /// <param name="programInstallSize">Install size</param>
        /// <param name="programDisplayIconPath">Display icon path</param>
        public InstalledProgram(string programDisplayName, string programParentDisplayName, bool isProgramUpdate,
                                string programVersion, string programPublisher, string programInstallDate,
                                string programInstallSize, string programDisplayIconPath)
        {
            DisplayName = programDisplayName;
            ParentDisplayName = programParentDisplayName;
            IsUpdate = isProgramUpdate;
            Version = programVersion;
            Publisher = programPublisher;
            InstallDate = programInstallDate;
            InstallSize = programInstallSize;
            DisplayIconPath = programDisplayIconPath;
        }

        #endregion

        #region "Public Methods"

        #region IComparable<InstalledProgram> Members

        /// <summary>
        /// Compares two <c>InstalledProgram</c>s
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(InstalledProgram other)
        {
            return string.Compare(DisplayName, other.DisplayName);
        }

        #endregion

        #region IEquatable<InstalledProgram> Members

        /// <summary>
        /// Compares two <c>InstalledProgram</c>
        /// <param name="other">The <c>InstalledProgramm</c> compares to</param>
        /// <returns>true - if equal, otherwise - false</returns>
        bool IEquatable<InstalledProgram>.Equals(InstalledProgram other)
        {
            return string.Equals(DisplayName, other.DisplayName);
        }

        #endregion

        /// <summary>
        /// <c>ToString()</c> override
        /// </summary>
        /// <returns><c>InstalledProgram.DisplayName</c></returns>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Retrieves a list of all installed programs on the specified computer
        /// </summary>
        /// <param name="computerName">The name of the computer to get the list of installed programs from</param>
        /// <param name="includeUpdates">Determines whether or not updates for installed programs are included in the list</param>
        public static List<InstalledProgram> GetInstalledPrograms(string computerName, bool includeUpdates)
        {
            try
            {
                return InternalGetInstalledPrograms(includeUpdates,
                                                    RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName),
                                                    RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, computerName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // ToDo: send exception details via SmartAssembly bug reporting!
                return new List<InstalledProgram>();
            }
        }

        #endregion

        #region "Methods"

        static List<InstalledProgram> InternalGetInstalledPrograms(bool includeUpdates, RegistryKey hklmPath,
                                                                   RegistryKey hkuPath)
        {
            List<InstalledProgram> programList = new List<InstalledProgram>();

            try
            {
                RegistryKey classesKey = hklmPath.OpenSubKey("Software\\Classes\\Installer\\Products");

                //---Wow64 Uninstall key
                RegistryKey wow64UninstallKey =
                    hklmPath.OpenSubKey("Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                programList = GetUninstallKeyPrograms(wow64UninstallKey, classesKey, programList, includeUpdates);

                //---Standard Uninstall key
                RegistryKey stdUninstallKey = hklmPath.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                programList = GetUninstallKeyPrograms(stdUninstallKey, classesKey, programList, includeUpdates);

                foreach (string userSid in hkuPath.GetSubKeyNames())
                {
                    try
                    {
                        //---HKU Uninstall key
                        RegistryKey cuUnInstallKey =
                            hkuPath.OpenSubKey(userSid + "\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                        programList = GetUninstallKeyPrograms(cuUnInstallKey, classesKey, programList, includeUpdates);
                    }
                    catch
                    {
                    }
                    try
                    {
                        //---HKU Installer key
                        RegistryKey cuInstallerKey = hkuPath.OpenSubKey(userSid + "\\Software\\Microsoft\\Installer\\Products");
                        programList = GetUserInstallerKeyPrograms(cuInstallerKey, hklmPath, programList);
                    }
                    catch
                    {
                    }
                }                
            }
            catch
            {
            }
            finally
            {
                //Close the registry keys
                if (hklmPath != null)
                    hklmPath.Close();
                if (hkuPath != null)
                    hkuPath.Close();

            }
            //Sort the list alphabetically and return it to the caller
            programList.Sort();
            return programList;
        }

        static bool IsProgramInList(string programName, List<InstalledProgram> listToCheck)
        {
            return listToCheck.Contains(new InstalledProgram(programName));
        }

        static List<InstalledProgram> GetUserInstallerKeyPrograms(RegistryKey cuInstallerKey, RegistryKey hklmRootKey,
                                                                  List<InstalledProgram> existingProgramList)
        {
            if ((cuInstallerKey != null))
            {
                try
                {
                    foreach (string cuProductGuid in cuInstallerKey.GetSubKeyNames())
                    {
                        bool productFound = false;
                        RegistryKey openSubKey = hklmRootKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData");
                        if (openSubKey != null)
                            foreach (string userDataKeyName in openSubKey.GetSubKeyNames())
                            {
                                //Ignore the LocalSystem account
                                if (userDataKeyName != "S-1-5-18")
                                {
                                    RegistryKey productsKey =
                                        hklmRootKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\" + userDataKeyName +
                                                               "\\Products");
                                    if ((productsKey != null))
                                    {
                                        string[] lmProductGuids = productsKey.GetSubKeyNames();
                                        foreach (string lmProductGuid in lmProductGuids)
                                        {
                                            if (lmProductGuid != cuProductGuid) continue;
                                            RegistryKey userDataProgramKey =
                                                hklmRootKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\" + userDataKeyName +
                                                                       "\\Products\\" + lmProductGuid + "\\InstallProperties");
                                            if (userDataProgramKey != null && Convert.ToInt32(userDataProgramKey.GetValue("SystemComponent", 0)) != 1)
                                            {
                                                RegistryKey registryKey = cuInstallerKey.OpenSubKey(cuProductGuid);
                                                if (registryKey != null)
                                                {
                                                    string name = Convert.ToString(registryKey.GetValue("ProductName", string.Empty));
                                                    if (name != string.Empty && !IsProgramInList(name, existingProgramList))
                                                    {
                                                        existingProgramList.Add(new InstalledProgram(name));
                                                        productFound = true;
                                                    }
                                                }
                                            }
                                            break; // TODO: might not be correct. Was : Exit For
                                        }
                                        if (productFound)
                                        {
                                            break; // TODO: might not be correct. Was : Exit For
                                        }
                                        try
                                        {
                                            productsKey.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (hklmRootKey != null)
                        hklmRootKey.Close();
                }
            }
            return existingProgramList;
        }

        static List<InstalledProgram> GetUninstallKeyPrograms(RegistryKey uninstallKey, RegistryKey classesKey,
                                                              List<InstalledProgram> existingProgramList, bool includeUpdates)
        {
            //Make sure the key exists
            if ((uninstallKey != null))
            {
                //Loop through all subkeys (each one represents an installed program)
                foreach (string subKeyName in uninstallKey.GetSubKeyNames())
                {
                    try
                    {
                        RegistryKey currentSubKey = uninstallKey.OpenSubKey(subKeyName);
                        //Skip this program if the SystemComponent flag is set
                        int isSystemComponent = 0;
                        try
                        {
                            isSystemComponent = Convert.ToInt32(currentSubKey.GetValue("SystemComponent", 0));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(subKeyName + " - " + ex.Message);
                        }
                        if (isSystemComponent != 1)
                        {
                            //If the WindowsInstaller flag is set then add the key name to our list of Windows Installer GUIDs
                            if (Convert.ToInt32(currentSubKey.GetValue("WindowsInstaller", 0)) != 1)
                            {
                                Regex windowsUpdateRegEx = new Regex("KB[0-9]{6}$");
                                string programReleaseType = Convert.ToString(currentSubKey.GetValue("ReleaseType", string.Empty));

                                string progVersion = string.Empty;
                                try
                                {
                                    progVersion = Convert.ToString(currentSubKey.GetValue("DisplayVersion", string.Empty));
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(subKeyName + " - " + ex.Message);
                                }
                                //Check to see if this program is classed as an update
                                if (windowsUpdateRegEx.Match(subKeyName).Success ||
                                    Convert.ToString(currentSubKey.GetValue("ParentKeyName", string.Empty)) != string.Empty ||
                                    programReleaseType == "Security Update" || programReleaseType == "Update Rollup" ||
                                    programReleaseType == "Hotfix")
                                {
                                    if (includeUpdates)
                                    {
                                        //Add the program to our list if we are including updates in this search
                                        string name = Convert.ToString(currentSubKey.GetValue("DisplayName", string.Empty));
                                        if (name != string.Empty && !IsProgramInList(name, existingProgramList))
                                        {
                                            existingProgramList.Add(new InstalledProgram(name,
                                                                                         Convert.ToString(currentSubKey.GetValue("ParentDisplayName",
                                                                                                                                 string.Empty)), true,
                                                                                         progVersion));
                                        }
                                    }
                                    //If not classed as an update
                                }
                                else
                                {
                                    bool uninstallStringExists = false;
                                    foreach (string valuename in currentSubKey.GetValueNames())
                                    {
                                        if (string.Equals("UninstallString", valuename, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            uninstallStringExists = true;
                                            break; // TODO: might not be correct. Was : Exit For
                                        }
                                    }
                                    if (uninstallStringExists)
                                    {
                                        string name = Convert.ToString(currentSubKey.GetValue("DisplayName", string.Empty));

                                        string dInstallSize = string.Empty;
                                        string dtInstallDate = string.Empty;
                                        string sDisplayIconPath = string.Empty;
                                        string sUninstallPath = string.Empty;

                                        string sTemp = Convert.ToString(currentSubKey.GetValue("UninstallString", string.Empty));
                                        if (!string.IsNullOrEmpty(sTemp))
                                        {
                                            sUninstallPath = sTemp;
                                        }

                                        sTemp = Convert.ToString(currentSubKey.GetValue("EstimatedSize", string.Empty));
                                        if (!string.IsNullOrEmpty(sTemp))
                                        {
                                            dInstallSize = (double.Parse(sTemp) / 1024.0).ToString("0.00") + " MB";
                                        }
                                        string sPublisher = Convert.ToString(currentSubKey.GetValue("Publisher", string.Empty));
                                        sTemp = Convert.ToString(currentSubKey.GetValue("InstallDate", string.Empty));
                                        if (!string.IsNullOrEmpty(sTemp))
                                        {
                                            try
                                            {
                                                dtInstallDate =
                                                    DateTime.ParseExact(sTemp, "yyyyMMdd", null).ToShortDateString();
                                            }
                                            catch
                                            {
                                            }
                                            try
                                            {
                                                dtInstallDate = DateTime.ParseExact(sTemp, "MM/dd/yyyy", null).ToShortDateString();
                                            }
                                            catch
                                            {
                                            }
                                            try
                                            {
                                                dtInstallDate = DateTime.ParseExact(sTemp, "dd/MM/yyyy", null).ToShortDateString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        sTemp = Convert.ToString(currentSubKey.GetValue("DisplayIcon", string.Empty));
                                        if (!string.IsNullOrEmpty(sTemp))
                                        {
                                            try
                                            {
                                                sDisplayIconPath = sTemp;
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (!(name == string.Empty) && !IsProgramInList(name, existingProgramList))
                                        {
                                            if (UninstallKeys == null)
                                            {
                                                UninstallKeys = new Dictionary<string, string>();
                                            }
                                            if (!UninstallKeys.ContainsKey(name))
                                            {
                                                UninstallKeys.Add(name, sUninstallPath);
                                            }
                                            existingProgramList.Add(new InstalledProgram(name,
                                                                                         Convert.ToString(currentSubKey.GetValue("ParentDisplayName",
                                                                                                                                 string.Empty)), false,
                                                                                         progVersion, sPublisher, dtInstallDate, dInstallSize,
                                                                                         sDisplayIconPath));
                                        }
                                    }
                                }
                                //If WindowsInstaller
                            }
                            else
                            {
                                string progVersion = string.Empty;
                                string name = string.Empty;
                                try
                                {
                                    string msiKeyName = GetInstallerKeyNameFromGuid(subKeyName);
                                    RegistryKey crGuidKey = classesKey.OpenSubKey(msiKeyName);
                                    if ((crGuidKey != null))
                                    {
                                        name = Convert.ToString(crGuidKey.GetValue("ProductName", string.Empty));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(subKeyName + " - " + ex.Message);
                                }
                                try
                                {
                                    progVersion = Convert.ToString(currentSubKey.GetValue("DisplayVersion", string.Empty));
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                }

                                string dInstallSize = string.Empty;
                                string dtInstallDate = string.Empty;
                                string sDisplayIconPath = string.Empty;
                                string sUninstallPath = string.Empty;

                                string sTemp = Convert.ToString(currentSubKey.GetValue("UninstallString", string.Empty));
                                if (!string.IsNullOrEmpty(sTemp))
                                {
                                    sUninstallPath = sTemp;
                                }

                                sTemp = Convert.ToString(currentSubKey.GetValue("EstimatedSize", string.Empty));
                                if (!string.IsNullOrEmpty(sTemp))
                                {
                                    dInstallSize = (double.Parse(sTemp) / 1024.0).ToString("0.00") + " MB";
                                }
                                string sPublisher = Convert.ToString(currentSubKey.GetValue("Publisher", string.Empty));
                                sTemp = Convert.ToString(currentSubKey.GetValue("InstallDate", string.Empty));
                                if (!string.IsNullOrEmpty(sTemp))
                                {
                                    try
                                    {
                                        dtInstallDate = DateTime.ParseExact(sTemp, "yyyyMMdd", null).ToShortDateString();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        dtInstallDate = DateTime.ParseExact(sTemp, "mm/dd/yyyy", null).ToShortDateString();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        dtInstallDate = DateTime.ParseExact(sTemp, "dd/mm/yyyy", null).ToShortDateString();
                                    }
                                    catch
                                    {
                                    }
                                }
                                sTemp = Convert.ToString(currentSubKey.GetValue("DisplayIcon", string.Empty));
                                if (!string.IsNullOrEmpty(sTemp))
                                {
                                    try
                                    {
                                        sDisplayIconPath = sTemp;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (name != string.Empty && !IsProgramInList(name, existingProgramList))
                                {
                                    if (UninstallKeys == null)
                                    {
                                        UninstallKeys = new Dictionary<string, string>();
                                    }
                                    if (!UninstallKeys.ContainsKey(name))
                                    {
                                        UninstallKeys.Add(name, sUninstallPath);
                                    }
                                    existingProgramList.Add(new InstalledProgram(name,
                                                                                 Convert.ToString(currentSubKey.GetValue("ParentDisplayName",
                                                                                                                         string.Empty)), false,
                                                                                 progVersion, sPublisher, dtInstallDate, dInstallSize,
                                                                                 sDisplayIconPath));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(subKeyName + " - " + ex.Message);
                    }
                }
                //Close the registry key
                try
                {
                    uninstallKey.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return existingProgramList;
        }

        static string GetInstallerKeyNameFromGuid(string guidName)
        {
            string[] msiNameParts = guidName.Replace("{", string.Empty).Replace("}", string.Empty).Split('-');
            StringBuilder msiName = new StringBuilder();
            //Just reverse the first 3 parts
            for (var i = 0; i <= 2; i++)
            {
                msiName.Append(ReverseString(msiNameParts[i]));
            }
            //For the last 2 parts, reverse each character pair
            for (int j = 3; j <= 4; j++)
            {
                for (int i = 0; i <= msiNameParts[j].Length - 1; i++)
                {
                    msiName.Append(msiNameParts[j][i + 1]);
                    msiName.Append(msiNameParts[j][i]);
                    i += 1;
                }
            }
            return msiName.ToString();
        }

        static char[] ReverseString(string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return chars;
        }

        #endregion
    }
}