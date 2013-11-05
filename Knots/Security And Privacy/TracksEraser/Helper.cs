using System.Linq;
using Microsoft.Win32;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace FreemiumUtilities.TracksEraser
{
    public class Helper
    {
        #region Checking if application is installed
        /// <summary>
        /// check if specific app is installed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subname"></param>
        /// <returns></returns>
        public static bool IsApplictionInstalled(string name, string subname = null)
        {
            try
            {
                // search in: CurrentUser
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (IsInRegistryKey(key, name, subname))
                    return true;

                // search in: LocalMachine_32
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (IsInRegistryKey(key, name, subname))
                    return true;

                // search in: LocalMachine_64
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                if (IsInRegistryKey(key, name, subname))
                    return true;

                if (IsInDir(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), name))
                    return true;

                if (IsInDir(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), name))
                    return true;

            }
            catch
            {
            }

            // NOT FOUND
            return false;
        }

        /// <summary>
        /// Determines if the registry key contains the searched item
        /// </summary>
        /// <param name="regKey">Registry Key where to search</param>
        /// <param name="name">name to be search</param>
        /// <param name="subname">subname to be searched</param>
        /// <returns>true - if found, false-otherwise</returns>
        static bool IsInRegistryKey(RegistryKey regKey, string name, string subname)
        {
            string displayName;
            bool result = false;
            if (regKey != null)
            {
                try
                {
                    foreach (string keyName in regKey.GetSubKeyNames())
                    {
                        RegistryKey subkey = regKey.OpenSubKey(keyName);
                        displayName = subkey != null && subkey.GetValue("DisplayName") != null
                                        ? subkey.GetValue("DisplayName").ToString()
                                        : string.Empty;
                        if (displayName.ToLower().Contains(name))
                        {
                            if (subname == null || (displayName.ToLower().Contains(subname)))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        /// <summary>
        /// Determines if the dicrectory contains the searched item
        /// </summary>
        /// <param name="path">path of directory to search</param>
        /// <param name="name">name to be searched</param>
        /// <returns>true - if found, false-otherwise</returns>
        static bool IsInDir(string path, string name)
        {
            bool result = false;
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (DirectoryInfo d in di.GetDirectories())
                {
                    if (d.Name.ToLower() == name)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// check if specific browser is installed
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static bool IsBrowserInstalled(string browser)
        {
            try
            {
                RegistryKey openSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                if (openSubKey != null)
                {
                    string[] s1 = openSubKey.GetSubKeyNames();
                    if (s1.Any(s => s.ToLower().Contains(browser)))
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            try
            {
                RegistryKey openSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Clients\StartMenuInternet");
                if (openSubKey != null)
                {
                    string[] s1 = openSubKey.GetSubKeyNames();
                    if (s1.Any(s => s.ToLower().Contains(browser)))
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// check if ms office is installed
        /// </summary>
        /// <returns></returns>
        public static bool IsMSOfficeInstalled()
        {
            string sPlugins = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            try
            {
                DirectoryInfo diInfoPlugins = new DirectoryInfo(sPlugins);
                FileSystemInfo[] fileSysInfoPlugins = diInfoPlugins.GetDirectories("*", SearchOption.TopDirectoryOnly);
                foreach (DirectoryInfo diNext in fileSysInfoPlugins)
                {
                    if (diNext.Name == "Microsoft")
                    {
                        try
                        {
                            string sPathMirosoft = diNext.FullName;
                            sPathMirosoft = sPathMirosoft + "\\Office\\Recent";

                            DirectoryInfo diInfoMicrosoft = new DirectoryInfo(sPathMirosoft);
                            FileSystemInfo[] fileSysInfoMicrosoft = diInfoMicrosoft.GetFiles();

                            return fileSysInfoMicrosoft.Count() > -1;
                        }
                        catch
                        {
                        }
                    }

                }
            }
            catch
            {
            }
            return false;
        }

        #endregion

        /// <summary>
        /// get list of files to be deleted
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static List<string> GetFilesAvailableForDrop(IEnumerable<string> files)
        {
            List<string> processedFiles = new List<string>();
            foreach (string file in files)
            {
                try
                {
                    if (file.Substring(1, 2) != @":\")
                    {
                        processedFiles.Add(file);
                        continue;
                    }

                    UserFileAccessRights rights = new UserFileAccessRights(file);
                    if (rights.CanDelete())
                    {
                        FileStream fileStream = GetStream(FileAccess.ReadWrite, file);
                        processedFiles.Add(file);
                        fileStream.Close();
                    }
                }
                catch
                {
                }
            }

            return processedFiles;
        }

        #region Checking file access

        const int NumberOfTries = 3;
        const int TimeIntervalBetweenTries = 100;
        /// <summary>
        /// check if specific file is locked
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        static bool IsFileLocked(Exception exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }

        /// <summary>
        /// try to access specific file
        /// </summary>
        /// <param name="fileAccess"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static FileStream GetStream(FileAccess fileAccess, string fileName)
        {
            int tries = 0;
            while (true)
            {
                try
                {
                    return File.Open(fileName, FileMode.Open, fileAccess, FileShare.None);
                }
                catch (IOException e)
                {
                    if (!IsFileLocked(e))
                        throw;
                    if (++tries > NumberOfTries)
                        throw new Exception("The file is locked too long: " + e.Message, e);
                    Thread.Sleep(TimeIntervalBetweenTries);
                }
            }
        }

        #endregion

        /// <summary>
        /// calculate size of files to be deleted
        /// </summary>
        /// <returns></returns>
        public static ulong CalcFilesToDelSize(List<string> filesToDelete)
        {
            ulong totalBytes = 0UL;

            foreach (string filename in filesToDelete)
            {
                try
                {
                    totalBytes += (ulong)(new FileInfo(filename).Length);
                }
                catch
                {
                }
            }

            return totalBytes;
        }

        /// <summary>
        /// formats size for display
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatSize(ulong bytes)
        {
            double size = bytes;
            string unit = " Bytes";
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " KB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " TB";
            }

            return size.ToString("0.##") + unit;
        }
    }
}
