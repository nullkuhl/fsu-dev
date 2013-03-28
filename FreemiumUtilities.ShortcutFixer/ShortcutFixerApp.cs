using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using FreemiumUtilities.Infrastructure;
using IWshRuntimeLibrary;
using File = System.IO.File;

/// <summary>
/// The <see cref="FreemiumUtilities.ShortcutFixer"/> namespace defines a ShortcutFixer 1 Click-Maintenance application
/// </summary>
namespace FreemiumUtilities.ShortcutFixer
{
    /// <summary>
    /// ShortcutFixer 1 Click-Maintenance application <see cref="OneClickApp"/> implementation
    /// </summary>
    public class ShortcutFixerApp : OneClickApp
    {
        #region Instance Variables

        ProgressUpdate callback;
        CancelComplete cancelComplete;
        ScanComplete complete;
        bool fixAfterScan;

        #endregion

        #region Properties

        /// <summary>
        /// Broken shortcuts collection
        /// </summary>
        public List<Shortcut> BrokenShortcuts { get; set; }
        /// <summary>
        /// App execution terminating flag
        /// </summary>
        public bool ABORT { get; set; }
        /// <summary>
        /// Problems count
        /// </summary>
        public override int ProblemsCount { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// constructor for ShortcutFixerApp
        /// </summary>
        public ShortcutFixerApp()
        {
            BrokenShortcuts = new List<Shortcut>();
        }

        #endregion

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        /// <summary>
        /// desktop/start menu folders constants
        /// </summary>
        const int CSIDL_DESKTOP = 0x0000;
        const int CSIDL_DESKTOPDIRECTORY = 0x0010;
        const int CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019;
        const int CSIDL_STARTMENU = 0x000b;
        const int CSIDL_COMMON_STARTMENU = 0x0016;

        /// <summary>
        /// get special desktop/start menu folders path
        /// </summary>
        /// <param name="CSIDL"></param>
        /// <returns></returns>
        string GetSpecialFolderPath(int CSIDL)
        {
            try
            {
                StringBuilder path = new StringBuilder(260);
                SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL, false);
                return path.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #region Public Methods

        /// <summary>
        /// start scanning
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="complete"></param>
        /// <param name="cancelComplete"></param>
        /// <param name="fixAfterScan"></param>
        public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete, bool fixAfterScan)
        {
            ABORT = false;

            BrokenShortcuts.Clear();

            this.callback = callback;
            this.complete = complete;
            this.cancelComplete = cancelComplete;
            this.fixAfterScan = fixAfterScan;

            string root = Path.GetPathRoot(Environment.SystemDirectory);
            string user = Environment.UserName;

            HashSet<string> places = new HashSet<string>();

            if (OSIsXP())
            {
                places.Add(root + @"Documents and Settings\All Users\Desktop");
                places.Add(root + @"Documents and Settings\All Users\Start Menu");
                places.Add(root + @"Documents and Settings\Default User\Desktop");
                places.Add(root + @"Documents and Settings\Default User\Start Menu");
                places.Add(root + @"Documents and Settings\" + user + @"\Desktop");
                places.Add(root + @"Documents and Settings\" + user + @"\Start Menu");
            }
            places.Add(root + @"ProgramData\Desktop");
            places.Add(root + @"ProgramData\Microsoft\Windows\Start Menu");
            places.Add(root + @"ProgramData\Start Menu");
            places.Add(root + @"Users\Default\AppData\Roaming\Microsoft\Windows\Start Menu");
            places.Add(root + @"Users\Default\Desktop");
            places.Add(root + @"Users\Default\Start Menu");
            places.Add(root + @"Users\Public\Desktop");
            places.Add(root + @"Users\" + user + @"\AppData\Roaming\Microsoft\Windows\Start Menu");
            places.Add(root + @"Users\" + user + @"\Desktop");
            places.Add(root + @"Users\" + user + @"\Start Menu");
            places.Add(GetSpecialFolderPath(CSIDL_COMMON_DESKTOPDIRECTORY));
            places.Add(GetSpecialFolderPath(CSIDL_COMMON_STARTMENU));
            places.Add(GetSpecialFolderPath(CSIDL_DESKTOP));
            places.Add(GetSpecialFolderPath(CSIDL_DESKTOPDIRECTORY));
            places.Add(GetSpecialFolderPath(CSIDL_STARTMENU));

            List<FileInfo> shortcutList = new List<FileInfo>();
            foreach (string s in places)
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(s);
                    FileAttributes att = dir.Attributes;
                    if ((att & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                    {
                        shortcutList.AddRange(dir.GetFiles("*.lnk", SearchOption.AllDirectories));
                    }
                }
                catch { }
            }

            int shortcutsFound = shortcutList.Count;

            int shortcutsProcessed = 0;

            foreach (FileInfo shortcut in shortcutList)
            {
                if (ABORT)
                {
                    cancelComplete();
                    return;
                }

                try
                {
                    WshShellClass wshShellClass = new WshShellClass();
                    IWshShortcut shortcutInfo = (IWshShortcut)wshShellClass.CreateShortcut(shortcut.FullName);
                    string linkTarget = shortcutInfo.TargetPath;
                    string linkfile = linkTarget;
                    if (linkTarget.Contains("\\"))
                    {
                        linkfile = linkTarget.Substring(linkTarget.LastIndexOf("\\"));
                    }
                    if (string.IsNullOrEmpty(linkTarget))
                    {
                        continue;
                    }
                    var drive = new DriveInfo(linkTarget.Substring(0, 1));
                    if (drive.DriveType == DriveType.CDRom)
                    {
                        continue;
                    }
                    if (shortcutInfo.TargetPath.Contains("Program Files"))
                    {
                        IWshShortcut shortcutInfox86 = (IWshShortcut)wshShellClass.CreateShortcut(shortcut.FullName);
                        shortcutInfox86.TargetPath = shortcutInfox86.TargetPath.Replace(" (x86)", string.Empty);

                        if (!File.Exists(shortcutInfo.TargetPath) && !Directory.Exists(shortcutInfo.TargetPath)
                            && !File.Exists(shortcutInfox86.TargetPath.Replace(" (x86)", string.Empty)) && !Directory.Exists(shortcutInfox86.TargetPath.Replace(" (x86)", string.Empty)))
                        {
                            Shortcut brokenShortcut = new Shortcut
                                                    {
                                                        Name = shortcut.Name,
                                                        Target = shortcutInfox86.TargetPath,
                                                        Location = shortcut.DirectoryName,
                                                        Description = shortcutInfox86.Description
                                                    };

                            BrokenShortcuts.Add(brokenShortcut);
                            ProblemsCount++;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(shortcutInfo.TargetPath) && !File.Exists(shortcutInfo.TargetPath) && !Directory.Exists(shortcutInfo.TargetPath)
                            && !File.Exists(Environment.ExpandEnvironmentVariables(@"%systemroot%\Sysnative") + "\\" + linkfile)
                            && !File.Exists(Environment.SystemDirectory + "\\" + linkfile)
                            && !File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + linkfile))
                        {
                            var brokenShortcut = new Shortcut
                                                    {
                                                        Name = shortcut.Name,
                                                        Target = shortcutInfo.TargetPath,
                                                        Location = shortcut.DirectoryName,
                                                        Description = shortcutInfo.Description
                                                    };

                            BrokenShortcuts.Add(brokenShortcut);
                            ProblemsCount++;
                        }
                    }
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }

                shortcutsProcessed++;
                callback((int)((double)shortcutsProcessed / shortcutsFound * 100), shortcut.FullName);
            }
            complete(fixAfterScan);
        }

        /// <summary>
        /// check if the current windows version is xp
        /// </summary>
        /// <returns></returns>
        private bool OSIsXP()
        {
            try
            {
                OperatingSystem osInfo = Environment.OSVersion;
                switch (osInfo.Platform)
                {
                    case PlatformID.Win32NT:
                        switch (osInfo.Version.Major)
                        {
                            case 4:
                                return false;
                            case 5:
                                return true;
                        }
                        break;
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// cancel scanning
        /// </summary>
        public override void CancelScan()
        {
            ABORT = true;
        }

        /// <summary>
        /// start fixing
        /// </summary>
        /// <param name="callback"></param>
        public override void StartFix(ProgressUpdate callback)
        {
            ABORT = false;

            this.callback = callback;

            int shortcutsProcessed = 0;

            foreach (Shortcut brokenShortcut in BrokenShortcuts)
            {
                if (ABORT)
                {
                    cancelComplete();
                    return;
                }

                shortcutsProcessed++;

                try
                {
                    string shortcutFullname = brokenShortcut.Location + "\\" + brokenShortcut.Name;
                    if (File.Exists(shortcutFullname))
                    {
                        File.Delete(shortcutFullname);
                        callback((int)((double)shortcutsProcessed / BrokenShortcuts.Count * 100), shortcutFullname);
                    }
                }
                catch (Exception e)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }

            complete(fixAfterScan);
        }

        /// <summary>
        /// cacel fixing
        /// </summary>
        public override void CancelFix()
        {
            ABORT = true;
        }

        #endregion
    }
}