using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FreemiumUtilities.StartupManager
{
    public class Helper
    {
        #region Constants and Variables
        const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        const string WowRunKey = @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";
        // Shortcut type (location) constants.
        const string HKCU = "Registry: Current User";
        const string WHKCU = "Registry: x86 Current User";
        const string HKLM = "Registry: All Users";
        const string WHKLM = "Registry: x86 All Users";
        const string StartupCurrentUser = "Startup Folder: Current User";
        const string StartupAllUsers = "Startup Folder: All Users";
        #endregion
        /// <summary>
        /// Deletes item from registry
        /// </summary>
        /// <param name="regDir">registry direco=tory</param>
        /// <param name="subKeyName">name of subkey to be deleted</param>
        /// <returns>true - if success, false - otherwise</returns>
        public static bool DeleteItemFromReg(string regDir, string subKeyName)
        {
            bool result = false;
            RegistryKey regKey = null;
            try
            {
                if (regDir == "HKCU")
                    // Open "Run" key in HKEY_CURRENT_USER.
                    regKey = Registry.CurrentUser.OpenSubKey(RunKey, true);
                else if (regDir == "WHKCU")
                    // Open "Run" key in HKEY_CURRENT_USER\Wow6432Node.
                    regKey = Registry.CurrentUser.OpenSubKey(WowRunKey, true);
                else if (regDir == "HKLM")
                    // Open "Run" key in HKEY_LOCAL_MACHINE.
                    regKey = Registry.LocalMachine.OpenSubKey(RunKey, true);
                else if (regDir == "WHKLM")
                    // Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node.
                    regKey = Registry.LocalMachine.OpenSubKey(WowRunKey, true);

                if (regKey != null)
                    regKey.DeleteValue(subKeyName);

                result = true;
            }
            catch
            {
            }
            finally
            {
                if (regKey != null)
                    regKey.Close();
            }
            return result;
        }

        /// <summary>
        /// Delete item for user
        /// </summary>
        /// <param name="fileName">The name of the file to be deleted</param>
        /// <returns>true - if success, false - otherwise</returns>
        public static bool DeleteItemForUser(string fileName)
        {
            bool result = false;
            if (File.Exists(fileName))
            {
                // Remove attributes.
                File.SetAttributes(fileName, FileAttributes.Normal);

                // Delete file.
                File.Delete(fileName);
                result = true;
                MessageBox.Show("The file was not found.", Application.ProductName,
                                         MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return result;
        }

        #region Return FilePath Method

        public static string ReturnFilePath(string value)
        {
            try
            {
                int p;

                // Check for quotes, and if present, remove them.
                if (value.Contains("\"")) // quote character 34, 22H
                {
                    value = value.Replace("\"", string.Empty);
                }

                // Check for brackets, and if present, remove them.
                if (value.Contains("["))
                {
                    value = value.Replace("[", string.Empty);
                }

                if (value.Contains("]"))
                {
                    value = value.Replace("]", string.Empty);
                }

                // Check for hyphens, and if present, return the part before first one.
                if (value.Contains(" -"))
                {
                    p = value.IndexOf(" -");
                    value = value.Substring(0, p);
                }

                // Check for forward slashes, and if present, return the part before first one.
                if (value.Contains("/"))
                {
                    p = value.IndexOf("/");
                    value = value.Substring(0, p - 1);
                }

                // Check for a space followed by a percent sign, and if present, return the part before the first one.
                if (value.Contains(" %"))
                {
                    p = value.IndexOf(" %");
                    value = value.Substring(0, p);
                }

                // Check for "rundll32" or "rundll32.exe"
                if (value.ToLower().Contains("rundll32"))
                {
                    value = "rundll32.exe";
                }

                // If path does not contain ":\", then it is not complete: check PATH environment variable.
                if (!value.Contains(@":\"))
                {
                    if (!string.IsNullOrEmpty(FindPathFromEnvironment(value)))
                    {
                        value = FindPathFromEnvironment(value);
                        return value;
                    }
                    return string.Empty;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    return Path.GetFullPath(value);
                }
                return string.Empty;
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("The folder was not found." + "\r\n" +
                                "Description: " + e.Message + "\r\n" + "Command: " + value,
                                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The file was not found." + "\r\n" +
                                "Description: " + ex.Message + "\r\n" + "Command: " + value,
                                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }


        /// <summary>
        /// Used when only the file name is given (no path). This means either that the file name is
        /// on a path defined by the PATH environment variable, or that the entry is in error.
        /// </summary>
        /// <param name="partialPath">File name without path.</param>
        /// <returns>Complete path to file or blank if it cannot be resolved.</returns>
        static string FindPathFromEnvironment(string partialPath)
        {
            // Get all defined paths for all users.
            string definedPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);

            // Combine with all defined paths for current user.
            definedPath = definedPath + Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);

            try
            {
                // Remove any quote characters.
                definedPath = definedPath.Replace("\"", string.Empty);

                // Get all of the defined paths.
                string[] paths = definedPath.Split(new[] { ';' });

                // Check all of the paths. Return complete path if found, otherwise a blank.
                foreach (string path in paths)
                {
                    if (File.Exists(Path.Combine(path, partialPath)))
                    {
                        definedPath = Path.Combine(path, partialPath);
                        break;
                    }
                    definedPath = string.Empty;
                }
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }

            return definedPath;
        }

        #endregion
    }
}
