using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using FreemiumUtil;

#region Notes
/// "Control Scan"
/// "User Scan"
/// "System Software"
/// "System Fonts"
/// "System Help Files"
/// "Shared Libraries"
/// "Startup Entries"
/// "Installation Strings"
/// "Virtual Devices"
/// "History and Start Menu"
/// "Deep System Scan"

/// CONTEXT ID STRINGS:
/// --ControlScan--
/// AppIDPaths              -1
/// ProcServerPaths         -2
/// TypeLibPaths            -3
/// InterfacePaths          -4 type             -5 proxy
/// TypePaths               -6 help             -7 win32
/// ClassSubPaths           -8 ext              -9 open             -10 edit
/// --UserScan--
/// userscan                -11 usr
/// --FullScan--
/// fullscan                -12 class name      -13 clsid           -14 icon
/// --FontScan--
/// fontscan                -15 paths
/// --HelpScan--
/// help                    -16 html/chm
/// --Shared Libraries--
/// LibShared               -17 lib
/// --Startup--
/// Startup Enries          -18 path
/// --Install Scan--
/// UnInstall Strings       -19 path
/// --Virtual Devices--
/// VDF Check               -20 repair
/// --Link Scan--
/// History and Start       -21 expl            -22 start               -23 linkhist               -24 linkstart
/// --Deep Scan--
/// DeepScan                -25 ms              -26 sft

/// Phase 1 -CLSID
/// 1) valid registration  - HKEY_CLASSES_ROOT\CLSID\..\InProcSvr32(+)
/// 2) typelib paths       - HKEY_CLASSES_ROOT\..
/// 3) appid paths         - HKEY_CLASSES_ROOT\CLSID\... Val-AppID <-> HKEY_CLASSES_ROOT\AppID
/// Phase 2 -Interface
/// 4) type lib paths      - HKEY_CLASSES_ROOT\Interface\TypeLib <-> CLSID\TypeLib
/// 5) interface paths     - HKEY_CLASSES_ROOT\Interface\...\ProxyStubClsid32 <-> CLSID
/// Phase 3 -TypeLib
/// 6) empty keys          - HKEY_CLASSES_ROOT\TypeLib\..\HELPDIR
/// 7) typelib paths       - HKEY_CLASSES_ROOT\TypeLib\..\..\win32
/// Phase 4 -File Extensions
/// 8) empty ext keys      - HKEY_CLASSES_ROOT\...

/// -File Types -6 step-
/// 1) test ext names      - HKEY_CLASSES_ROOT\
/// 2) test class names    - HKEY_CLASSES_ROOT\.xxx DefVal <-> HKEY_CLASSES_ROOT\..
/// 3) test clsid          - HKEY_CLASSES_ROOT\..\CLSID DefVal <-> HKEY_CLASSES_ROOT\CLSID
/// 4) object menu path    - HKEY_CLASSES_ROOT\..\shell\edit\command
/// 5) object open path    - HKEY_CLASSES_ROOT\..\shell\open\command
/// 5) object icon path    - HKEY_CLASSES_ROOT\..\DefaultIcon
/// 6) empty ext key       - HKEY_CLASSES_ROOT\...

/// -Help Files -2 step-
/// 1) path test           - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Help
/// 2) path test           - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\HTML Help

/// -Font Scan-
/// - path test            - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts

/// -History Lists-
/// - path test            - HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\MenuOrder\Start Menu\Programs

/// -ShortCuts -3 step-
/// 1) path link test      - HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSaveMRU - file list
/// 2) binary link test    - HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts - explorer
/// 3) binary link test    - HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist\...\Count - start menu

/// -Shared Dlls-
/// - file paths           - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs

/// -Deep Scan-
/// - test path values     - HKEY_LOCAL_MACHINE\SOFTWARE

/// -Current User-
/// - test path values     - HKEY_CURRENT_USER\Software\Microsoft\VBExpress\8.0\FileMRUList

/// -Startup Entries -3 step-
/// 1) test paths          - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
/// 2) test paths          - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce
/// 3) test paths          - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnceEx

/// -Uninstall Strings-
/// -test paths            - HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall

/// Virtual Devices
/// -value test            - HKEY_LOCAL_MACHINE\System\ControlSet\Control\VirtualDeviceDrivers

/// Integrity Test
/// -compare keys          - HKEY_USERS\... <-> HKEY_CURRENT_USER
#endregion

/// <summary>
/// The <see cref="RegistryCleanerCore"/> namespace defines a Registry Cleaner app and knot core
/// </summary>
namespace RegistryCleanerCore
{
    #region ScanData
    /// <summary>
    /// Scan data
    /// </summary>
    public class ScanData : INotifyPropertyChanged
    {
        string k = "";
        string v = "";
        string d = "";
        string c = "";
        string n = "";
        int i = 0;
        int s = 0;
        bool b = false;
        cLightning.ROOT_KEY r = cLightning.ROOT_KEY.HKEY_CURRENT_USER;

        /// <summary>
        /// Scan data constructor
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <param name="img"></param>
        /// <param name="name"></param>
        /// <param name="scope"></param>
        /// <param name="id"></param>
        public ScanData(cLightning.ROOT_KEY root, string key, string value, string data, string img, string name, int scope, int id)
        {
            r = root;
            k = key;
            v = value;
            d = data;
            c = img;
            n = name;
            i = id;
            s = scope;
        }
        /// <summary>
        /// Root key
        /// </summary>
        public cLightning.ROOT_KEY Root
        {
            get { return r; }
            set { r = value; OnPropertyChanged("Root"); }
        }
        /// <summary>
        /// Abbreviated string of Root
        /// </summary>
        public string AbbreviatedRoot
        {
            get
            {
                switch (r)
                {
                    case cLightning.ROOT_KEY.HKEY_CLASSES_ROOT:
                        return "HKCR";
                    case cLightning.ROOT_KEY.HKEY_CURRENT_CONFIG:
                        return "HKCC";
                    case cLightning.ROOT_KEY.HKEY_CURRENT_USER:
                        return "HKCU";
                    case cLightning.ROOT_KEY.HKEY_DYN_DATA:
                        return "HKDD";
                    case cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE:
                        return "HKLM";
                    case cLightning.ROOT_KEY.HKEY_PERFORMANCE_DATA:
                        return "HKPD";
                    case cLightning.ROOT_KEY.HKEY_USERS:
                        return "HKU";
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// Key
        /// </summary>
        public string Key
        {
            get { return k; }
            set { k = value; OnPropertyChanged("Key"); }
        }
        /// <summary>
        /// Value
        /// </summary>
        public string Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        /// <summary>
        /// Data
        /// </summary>
        public string Data
        {
            get { return d; }
            set { d = value; OnPropertyChanged("Data"); }
        }
        /// <summary>
        /// Check
        /// </summary>
        public bool Check
        {
            get { return b; }
            set { b = value; OnPropertyChanged("Check"); }
        }
        /// <summary>
        /// Image path
        /// </summary>
        public string ImagePath
        {
            get { return c; }
            set { c = value; OnPropertyChanged("ImagePath"); }
        }
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return n; }
            set { n = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return i; }
            set { i = value; OnPropertyChanged("Id"); }
        }

        /// <summary>
        /// Scope
        /// </summary>
        public int Scope
        {
            get { return s; }
            set { s = value; OnPropertyChanged("Scope"); }
        }

        /// <summary>
        /// PropertyChanged event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
    #endregion

    #region Public Enums

    /// <summary>
    /// Result types
    /// </summary>
    public enum RESULT_TYPE
    {                           //del strat:
        ControlAppID = 1,       //1     value
        ControlProcServer,      //2     value
        ControlTypeLib,         //3     value
        ControlInterfaceType,   //4     value
        ControlInterfaceProxy,  //5     value
        ControlTypeHelp,        //6     key
        ControlTypeWin32,       //7     value
        ControlClassSubExt,     //8     key
        ControlClassSubOpen,    //9     value
        ControlClassSubEdit,    //10    value
        User,                   //11    value
        FullClassName,          //12    value
        FullClsid,              //13    value
        FullIcon,               //14    value
        Font,                   //15    value
        Help,                   //16    value
        Shared,                 //17    value
        Startup,                //18    value
        Uninstall,              //19    value
        Vdf,                    //20    value
        HistoryExplorer,        //21    value
        HistoryStart,           //22    value
        HistoryLink,            //23    value
        HistoryMenu,            //24    value
        DeepMs,                 //25    value
        DeepSft,                //26    value
        Mru                     //27    value
    }
    #endregion

    /// <summary>
    /// Reg scan utility
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class cRegScan
    {
        #region Constants
        const int INVALID_HANDLE_VALUE = -1;
        const string CHR_PERIOD = ".";
        const string CHR_BSLASH = @"\";
        const string CHR_FSLASH = "/";
        const string CHR_PERC = "%";
        const string CHR_DASH = "-";
        const string CHR_COMMA = ",";
        const string CHR_COLAN = ":";
        const string CHR_ASTERISK = "*";
        const string CHR_SPACE = " ";
        const string CHR_TILDE = "~";
        const string STR_KILO = "KB";
        const string STR_PACK = "SERVICE PACK";
        const string STR_UIST = "UninstallString";
        const string STR_MS = "MICROSOFT";
        const string STR_RC = "RECYCLE";
        const string STR_POL = "POLICIES";
        const string STR_MSPATH = @"SOFTWARE\Microsoft\";
        const string STR_INSPRP = "InstallProperties";

        const string STR_DLL = "DLL";
        const string STR_DBLZERO = "00";
        const string STR_CLASS = "CLSID";
        const string STR_CLASSB = @"CLSID\";
        const string STR_TYPE = "TypeLib";
        const string STR_TYPEB = @"TypeLib\";
        const string STR_INTERFACE = "Interface";
        const string STR_INTERFACEB = @"Interface\";
        const string STR_DEFICON = @"\DefaultIcon";
        const string STR_DEFAULT = "Default";
        const string STR_EMPTY = "Value Not Set";
        const string STR_NONAME = "Value Name Empty";
        const string STR_APPID = "AppID";
        const string STR_WIN32 = "WIN32";
        const string STR_PROXY = "ProxyStubClsid32";
        const string STR_OWLIST = "OpenWithList";
        const string STR_OWLISTB = @"\OpenWithList";
        const string STR_OWPROG = "OpenWithProgids";
        const string STR_OWPROGB = @"\OpenWithProgids";
        const string STR_SHELLOPEN = @"\shell\open\command";
        const string STR_SHELLEDIT = @"\shell\edit\command";

        const string HIST_START = @"Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist";
        const string HIST_EXPLORER = @"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts";
        const string HIST_EXPLORERB = @"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\";
        const string HIST_FILE = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSaveMRU";
        const string HIST_FILEB = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSaveMRU\";
        const string HIST_FILEPID = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU";
        const string HIST_FILEPIDB = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU\";
        const string LINK_START = @"Software\Microsoft\Windows\CurrentVersion\Explorer\MenuOrder\Start Menu\Programs";
        const string LINK_STARTB = @"Software\Microsoft\Windows\CurrentVersion\Explorer\MenuOrder\Start Menu\Programs\";

        const string MRU_DOCUMENTS = @"Software\Microsoft\Windows\CurrentVersion\Explorer\RecentDocs";
        const string MRU_TYPEDURLS = @"Software\Microsoft\Internet Explorer\TypedURLs";
        const string MRU_RUNMRU = @"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU";
        const string MRU_FILESEARCH = @"Software\Microsoft\Search Assistant\ACMru\5603";
        const string MRU_INTERNET = @"Software\Microsoft\Search Assistant\ACMru\5001";
        const string MRU_PPL = @"Software\Microsoft\Search Assistant\ACMru\5647";
        const string MRU_WORDPAD = @"Software\Microsoft\Windows\CurrentVersion\Applets\Wordpad\Recent File List";
        const string MRU_REGFAV = @"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit\Favorites";
        const string MRU_REGEDIT = @"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";
        const string MRU_PAINT = @"Software\Microsoft\Windows\CurrentVersion\Applets\Paint\Recent File List";
        const string MRU_COMDLG = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedMRU";
        const string MRU_EXPLORER = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpaper\MRU";
        const string MRU_MEDREC = @"Software\Microsoft\MediaPlayer\Player\RecentFileList";
        const string MRU_MEDURL = @"Software\Microsoft\MediaPlayer\Player\RecentURLList";

        const string FLT_COM = ".COM";
        const string STR_PATH = @":\";
        const string STR_VDD = "VDD";
        const string STR_HELP = "HELPDIR";
        const string STR_PROC32 = "InProcServer32";
        const string STR_PROC32B = @"\INPROCSERVER32";
        const string STR_PROC = "InProcServer";
        const string STR_PROCB = @"\INPROCSERVER";
        const string STR_LOCAL32 = "LocalServer32";
        const string STR_LOCAL32B = @"\LOCALSERVER32";
        const string STR_LOCAL = "LocalServer";
        const string STR_LOCALB = @"\LOCALSERVER";
        const string STR_EMPTYKEY = "Empty Key";
        const string STR_EMPTYVALUE = "Empty Value";

        const string APP_REG = "REGSVR32.EXE";
        const string APP_RUN = "RUNDLL32.EXE";
        const string APP_MSIE = "MSIEXEC.EXE";
        const string APP_START = "START";
        const string REG_HKCU = @"HKEY_CURRENT_USER\";
        const string REG_HKLM = @"HKEY_LOCAL_MACHINE\";
        const string REG_HKCR = @"HKEY_CLASSES_ROOT\";
        const string REG_HKCUB = @"HKEY_CURRENT_USER";
        const string REG_HKLMB = @"HKEY_LOCAL_MACHINE";
        const string REG_HKCRB = @"HKEY_CLASSES_ROOT";
        const string REG_HKLMCLSID = @"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\";
        const string REG_HKCRCLSID = @"HKEY_CLASSES_ROOT\CLSID\";
        const string REG_HKLMFONTS = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts";
        const string REG_HKLMHELP = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Help";
        const string REG_HKLMSHARE = @"SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs";
        const string REG_HKLMUISL = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";
        const string REG_HKLMVDEV = @"SYSTEM\ControlSet\Control\VirtualDeviceDrivers";
        const string MEDIATYPES = @"Applications\wmplayer.exe\SupportedTypes";

        const int MAXDWORD = 0xFFFF;
        const int FILE_ATTRIBUTE_READONLY = 0x1;
        const int FILE_ATTRIBUTE_HIDDEN = 0x2;
        const int FILE_ATTRIBUTE_SYSTEM = 0x4;
        const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        const int FILE_ATTRIBUTE_ARCHIVE = 0x20;
        const int FILE_ATTRIBUTE_NORMAL = 0x80;
        const int FILE_ATTRIBUTE_TEMPORARY = 0x100;
        #endregion

        #region API
        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SHGetPathFromIDListW(IntPtr pidl, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszPath);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetFileAttributes(string lpFileName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetInputState();

        [DllImport("comdlg32.dll")]
        static extern int GetFileTitle(string lpFileName, StringBuilder lpszTitle, int cbBuf);

        [DllImport("shlwapi.dll")]
        static extern int PathFileExists(string pszPath);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.U4)]
        static extern int GetLongPathName([MarshalAs(UnmanagedType.LPTStr)]string lpszShortPath,
            [MarshalAs(UnmanagedType.LPTStr)]StringBuilder lpszLongPath, [MarshalAs(UnmanagedType.U4)]int cchBuffer);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule,
            [MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);

        #endregion

        #region Properties
        /// <summary>
        /// Scan control
        /// </summary>
        public bool ScanControl { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public bool ScanUser { get; set; }
        /// <summary>
        /// File
        /// </summary>
        public bool ScanFile { get; set; }
        /// <summary>
        /// Font
        /// </summary>
        public bool ScanFont { get; set; }
        /// <summary>
        /// Help
        /// </summary>
        public bool ScanHelp { get; set; }
        /// <summary>
        /// Shared dll
        /// </summary>
        public bool ScanSharedDll { get; set; }
        /// <summary>
        /// Startup entries
        /// </summary>
        public bool ScanStartupEntries { get; set; }
        /// <summary>
        /// Uninstall strings
        /// </summary>
        public bool ScanUninstallStrings { get; set; }
        /// <summary>
        /// VDM
        /// </summary>
        public bool ScanVDM { get; set; }
        /// <summary>
        /// History
        /// </summary>
        public bool ScanHistory { get; set; }
        /// <summary>
        /// Deep
        /// </summary>
        public bool ScanDeep { get; set; }
        /// <summary>
        /// MRU
        /// </summary>
        public bool ScanMru { get; set; }
        /// <summary>
        /// Scan data collection
        /// </summary>
        public List<ScanData> Data { get; set; }
        //List<ScanData> myList = new List<ScanData>();
        ResourceManager rm { get; set; }

        /// <summary>
        /// Culture info
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                if (culture == null)
                    return new CultureInfo(CfgFile.Get("Lang"));
                else
                    return culture;
            }
            set
            {
                culture = value;
            }
        }
        CultureInfo culture;
        Thread bgWorkerThread;
        #endregion

        #region Delegates
        /// <summary>
        /// Label change
        /// </summary>
        /// <param name="phase"></param>
        /// <param name="label"></param>
        public delegate void LabelChangeDelegate(string phase, string label);
        /// <summary>
        /// Current path
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="path"></param>
        public delegate void CurrentPathDelegate(string hive, string path);
        /// <summary>
        /// Key count
        /// </summary>
        public delegate void KeyCountDelegate();
        /// <summary>
        /// Match item
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <param name="id"></param>
        public delegate void MatchItemDelegate(cLightning.ROOT_KEY root, string subkey, string value, string data, RESULT_TYPE id);
        /// <summary>
        /// Status change
        /// </summary>
        /// <param name="label"></param>
        public delegate void StatusChangeDelegate(string label);
        /// <summary>
        /// Process change
        /// </summary>
        public delegate void ProcessChangeDelegate();
        /// <summary>
        /// Scan count
        /// </summary>
        /// <param name="count"></param>
        public delegate void ScanCountDelegate(int count);
        /// <summary>
        /// Scan complete
        /// </summary>
        public delegate void ScanCompleteDelegate();
        /// <summary>
        /// Subscan complete
        /// </summary>
        /// <param name="id"></param>
        public delegate void SubScanCompleteDelegate(string id);
        #endregion

        #region Events
        /// <summary>
        /// Status update
        /// </summary>
        [Description("Status update")]
        public event LabelChangeDelegate LabelChange;

        /// <summary>
        /// Current processing path
        /// </summary>
        [Description("Current processing path")]
        public event CurrentPathDelegate CurrentPath;

        /// <summary>
        /// Key processed count
        /// </summary>
        [Description("Key processed count")]
        public event KeyCountDelegate KeyCount;

        /// <summary>
        /// Match item was found
        /// </summary>
        [Description("Match item was found")]
        public event MatchItemDelegate MatchItem;

        /// <summary>
        /// Processing status has changed
        /// </summary>
        [Description("Processing status has changed")]
        public event StatusChangeDelegate StatusChange;

        /// <summary>
        /// Processing shifted to new task
        /// </summary>
        [Description("Processing shifted to new task")]
        public event ProcessChangeDelegate ProcessChange;

        /// <summary>
        /// Task counter
        /// </summary>
        [Description("Task counter")]
        public event ScanCountDelegate ScanCount;

        /// <summary>
        /// Scan Completed
        /// </summary>
        [Description("Scan Completed")]
        public event ScanCompleteDelegate ScanComplete;

        /// <summary>
        /// Subscan Completed
        /// </summary>
        [Description("Scan Completed")]
        public event SubScanCompleteDelegate SubScanComplete;
        #endregion

        #region Fields
        static Regex _regPath = new Regex(
            @"([?a-z A-Z]:.*\\)([?\w.]+)", RegexOptions.IgnoreCase |
                RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Compiled
            );

        static Regex _regName = new Regex(
            @"(\w+[.]\w+$+)", RegexOptions.IgnoreCase |
                RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Compiled
            );

        cLightning _cLightning = new cLightning();
        ArrayList _aExtensions = new ArrayList();
        string[] _aDriveRoot;
        string _sWindowsDirectory;
        string _sFontsDirectory;
        string _sSystem32Directory;
        string _sProgramsDirectory;
        string _sUserProgramsDirectory;
        #endregion

        #region Constructor

        /// <summary>
        /// <c>cRegScan</c> constructor
        /// </summary>
        public cRegScan()
        {
            rm = new ResourceManager("RegistryCleanerCore.Properties.Resources", typeof(cRegScan).Assembly);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CfgFile.Get("Lang"));
            _aExtensions = GetFileExtensions();
            StoreLogicalDrives();
            StoreCommonPaths();
            Data = new List<ScanData>();
        }
        #endregion

        #region Methods
        #region Asynchronous Processing
        BackgroundWorker _oProcessAsyncBackgroundWorker;

        /// <summary>
        /// ProcessCompleted event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ProcessCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);

        /// <summary>
        /// ProcessCompleted event
        /// </summary>
        public event ProcessCompletedEventHandler ProcessCompleted;

        /// <summary>
        /// Async scan
        /// </summary>
        public void AsyncScan()
        {
            if ((_oProcessAsyncBackgroundWorker != null && !_oProcessAsyncBackgroundWorker.IsBusy) || _oProcessAsyncBackgroundWorker == null)
            {
                if (_oProcessAsyncBackgroundWorker != null)
                {
                    ResetAsync();
                }

                _oProcessAsyncBackgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
                _oProcessAsyncBackgroundWorker.DoWork += ProcessAsyncBackgroundWorker_DoWork;
                _oProcessAsyncBackgroundWorker.RunWorkerCompleted += ProcessAsyncBackgroundWorker_RunWorkerCompleted;
                _oProcessAsyncBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Async cancel process
        /// </summary>
        public void CancelProcessAsync()
        {
            if (_oProcessAsyncBackgroundWorker != null)
            {
                _oProcessAsyncBackgroundWorker.CancelAsync();
                //ResetAsync();
            }
        }

        void ResetAsync()
        {
            if (_oProcessAsyncBackgroundWorker != null)
            {
                _oProcessAsyncBackgroundWorker.Dispose();
            }
        }

        void ProcessAsyncBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = Culture;
            bgWorkerThread = Thread.CurrentThread;

            if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            if (!StartScan())
            {
                CancelProcessAsync();
            }
        }

        void ProcessAsyncBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ProcessCompleted != null)
            {
                ProcessCompleted(this, e);
            }
            ResetAsync();
        }

        #endregion

        #region Control

        /// <summary>
        /// Starts registry scanning
        /// </summary>
        /// <returns></returns>
        public bool StartScan()
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = Culture;

                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return false;

                // saves a lot of needless null checks..
                if (LabelChange == null || CurrentPath == null || KeyCount == null || MatchItem == null ||
                    StatusChange == null || ProcessChange == null || ScanCount == null || ScanComplete == null || SubScanComplete == null)
                {
                    return false;
                }

                // signal number of pending tasks
                ScanCounter();

                // control registration scan
                if (ScanControl)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("SystemControlClasses")));
                    ControlScan();
                    ProcessChange();
                    SubScanComplete("CONTROL");
                }
                // user software scan
                if (ScanUser)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("UserSoftware")));
                    UserScan();
                    ProcessChange();
                    SubScanComplete("USER");
                }
                // system software scan
                if (ScanFile)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("SoftwareRegistrations")));
                    FileScan();
                    ProcessChange();
                    SubScanComplete("SOFTWARE");
                }
                // font scan
                if (ScanFont)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("SystemFonts")));
                    FontScan(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
                    ProcessChange();
                    SubScanComplete("FONT");
                }
                // help strings
                if (ScanHelp)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("RegisteredHelpFiles")));
                    HelpScan(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\Help");
                    ProcessChange();
                    HelpScan(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\HTML Help");
                    ProcessChange();
                    SubScanComplete("HELP");
                }
                // shared dlls
                if (ScanSharedDll)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("SharedLibraries")));
                    SharedDllScan(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs");
                    ProcessChange();
                    SubScanComplete("SHAREDDLL");
                }
                // startup entries
                if (ScanStartupEntries)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("StartupOptions")));
                    StartupEntries(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    ProcessChange();
                    StartupEntries(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce");
                    ProcessChange();
                    StartupEntries(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnceEx");
                    ProcessChange();
                    SubScanComplete("STARTUP");
                }
                // installed software
                if (ScanUninstallStrings)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("InstalledSoftware")));
                    UninstallStringsScan(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    ProcessChange();
                    SubScanComplete("UNINSTALL");
                }
                // virtual devices
                if (ScanVDM)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("VirtualDevices")));
                    VDMScan(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, @"System\ControlSet\Control\VirtualDeviceDrivers");
                    ProcessChange();
                    SubScanComplete("VDM");
                }
                // shortcuts
                if (ScanHistory)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("ApplicationHistory")));
                    HistoryScan();
                    ProcessChange();
                    SubScanComplete("HISTORY");
                }
                // deep scan
                if (ScanDeep)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("DeepSystemScan")));
                    DeepScan();
                    ProcessChange();
                    SubScanComplete("DEEP");
                }
                // mru scan
                if (ScanMru)
                {
                    StatusChange(String.Format(rm.GetString("StatusChangeFormat"), rm.GetString("MostRecentlyUsedLists")));
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_DOCUMENTS);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_TYPEDURLS);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_RUNMRU);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_FILESEARCH);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_INTERNET);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_PPL);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_WORDPAD);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_REGFAV);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_REGEDIT);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_PAINT);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_COMDLG);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_EXPLORER);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_MEDREC);
                    MruScan(cLightning.ROOT_KEY.HKEY_CURRENT_USER, MRU_MEDURL);
                    ProcessChange();
                    SubScanComplete("MRU");
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            ScanComplete();
            return true;
        }

        void ScanCounter()
        {
            int ct = 0;
            if (ScanControl)
            {
                ct += 1;
            }
            if (ScanUser)
            {
                ct += 1;
            }
            if (ScanFile)
            {
                ct += 1;
            }
            if (ScanFont)
            {
                ct += 1;
            }
            if (ScanHelp)
            {
                ct += 2;
            }
            if (ScanSharedDll)
            {
                ct += 1;
            }
            if (ScanStartupEntries)
            {
                ct += 3;
            }
            if (ScanUninstallStrings)
            {
                ct += 1;
            }
            if (ScanVDM)
            {
                ct += 1;
            }
            if (ScanHistory)
            {
                ct += 1;
            }
            if (ScanDeep)
            {
                ct += 1;
            }
            if (ScanMru)
            {
                ct += 1;
            }
            if (ScanCount != null)
            {
                ScanCount(ct);
            }
        }
        //store: root, subkey, value, path, id
        //scandata: key root, string key, string value, string path, string img, string name, int scope, int id
        void StoreResults(cLightning.ROOT_KEY root, string subkey, string value, string data, RESULT_TYPE id)
        {
            // ****************************************************
            // Trying to check registry key permissions
            // ****************************************************
            try
            {
                var permission = new RegistryPermission(RegistryPermissionAccess.Write, root.ToString());
                permission.Demand();
            }
            catch (System.Security.SecurityException ex)
            {
                return;
            }
            // ****************************************************
            // Trying to check registry key permissions
            // ****************************************************

            if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
            {
                return;
            }

            int i = (int)id;
            if (value.Length == 0)
            {
                value = STR_DEFAULT;
            }
            Data.Add(new ScanData(root, subkey, value, data, "", IdConverter(id), IdToScope(i), i));
            // notify
            MatchItem(root, subkey, value, data, id);
        }
        #endregion

        /// <summary>
        /// Test for valid software class registration and command paths (CLSID)
        /// 
        /// Scan 1                               - CLSID
        /// 1) valid registration                - HKEY_CLASSES_ROOT\CLSID\..\InProcSvr32(+)
        /// 2) typelib paths                     - HKEY_CLASSES_ROOT\..
        /// 3) appid paths                       - HKEY_CLASSES_ROOT\CLSID\... Val-AppID <-> HKEY_CLASSES_ROOT\AppID
        /// Scan 2                               - Interface
        /// 4) type lib paths                    - HKEY_CLASSES_ROOT\Interface\TypeLib <-> CLSID\TypeLib
        /// 5) interface paths                   - HKEY_CLASSES_ROOT\Interface\...\ProxyStubClsid32 <-> CLSID
        /// Scan 3                               - TypeLib
        /// 6) empty keys                        - HKEY_CLASSES_ROOT\TypeLib\..\HELPDIR
        /// 7) typelib paths                     - HKEY_CLASSES_ROOT\TypeLib\..\..\win32
        /// Scan 4                               - File Extensions
        /// 8) empty ext keys                    - HKEY_CLASSES_ROOT\...
        /// </summary>
        public void ControlScan()
        {
            // scan hkcr keys
            LabelChange(rm.GetString("ApplicationIDPaths"), rm.GetString("ComparingInternalID"));

            ArrayList al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_CLASS);

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                AppIDPaths(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_CLASSB + s);
                ProcServerPaths(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_CLASSB + s);
                TypeLibPaths(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_CLASSB + s);
                CurrentPath(REG_HKCRB, s);
                KeyCount();
            }

            // test interface keys;
            LabelChange(rm.GetString("InterfaceTypes"), rm.GetString("TestingClassInterfaces"));

            al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_INTERFACE);

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                InterfacePaths(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_INTERFACEB + s);
                CurrentPath(REG_HKCRB, STR_INTERFACEB + s);
                KeyCount();
            }

            // test typelib keys
            LabelChange(rm.GetString("TypeLibraries"), rm.GetString("TestingTypeLibraryReferences"));

            al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_TYPE);

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                TypePaths(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_TYPEB + s);
                CurrentPath(REG_HKCRB, STR_TYPEB + s);
                KeyCount();
            }

            // file extensions scan
            LabelChange(rm.GetString("ClassKeySubPaths"), rm.GetString("TestingClasskeyPathReferences"));

            al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, "");

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                ClassSubPaths(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s);
                CurrentPath(REG_HKCRB, s);
                KeyCount();
            }
        }

        void AppIDPaths(cLightning.ROOT_KEY Key, string SubKey)
        {
            // test for valid app registration ids
            string id;
            // CLSID pointer matches registered Application ->HKCR\CLSID\{value} <-> HKCR\AppId\{value}
            if (_cLightning.ValueExists(Key, SubKey, STR_APPID))
            {
                id = _cLightning.ReadString(Key, SubKey, STR_APPID);
                if (!_cLightning.KeyExists(Key, STR_APPID + CHR_BSLASH + id))
                {
                    StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey, STR_APPID, id, RESULT_TYPE.ControlAppID);
                }
            }
        }

        void ClassSubPaths(cLightning.ROOT_KEY Key, string SubKey)
        {
            // test class key subpaths
            string sp = "";

            if (SubKey.Contains(STR_CLASS) || SubKey.Contains(STR_TYPE) || SubKey.Contains(STR_INTERFACE))
            {
                return;
            }
            // default application ->HKCR\extension\default->path
            if (SubKey.StartsWith(CHR_PERIOD))
            {
                if (_cLightning.KeyIsEmpty(Key, SubKey))
                {
                    StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey, STR_DEFAULT, STR_EMPTY, RESULT_TYPE.ControlClassSubExt);
                }
            }
            else
            {
                // default shell ->HKCR\name\shell\open\command\default->path
                if (_cLightning.KeyExists(Key, SubKey + STR_SHELLOPEN))
                {
                    sp = _cLightning.ReadString(Key, SubKey + STR_SHELLOPEN, "");
                    if (sp.Length > 4)
                    {
                        if (IsValidPath(sp))
                        {
                            sp = CleanPath(sp);
                            if (!FileExists(sp))
                            {
                                StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + STR_SHELLOPEN, STR_DEFAULT, sp, RESULT_TYPE.ControlClassSubOpen);
                            }
                        }
                    }
                }
                // default editing tool ->HKCR\name\shell\edit\command\default->path
                if (_cLightning.KeyExists(Key, SubKey + STR_SHELLEDIT))
                {
                    sp = _cLightning.ReadString(Key, SubKey + STR_SHELLEDIT, "");
                    if (sp.Length > 4)
                    {
                        if (IsValidPath(sp))
                        {
                            sp = CleanPath(sp);
                            if (!FileExists(sp) && IsFileCandidate(sp))
                            {
                                StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + STR_SHELLEDIT, STR_DEFAULT, sp, RESULT_TYPE.ControlClassSubEdit);
                            }
                        }
                    }
                }
            }
        }

        void InterfacePaths(cLightning.ROOT_KEY Key, string SubKey)
        {
            // test paths from \proxystub -> CLSID
            // test paths from \typelib -> TypeLib
            // remove value
            string sp = "";
            ArrayList al = KeyCollector(Key, SubKey);
            bool findSTR_TYPE = false, findSTR_PROXY = false;

            // test pointers to valid type libraries HKCR\Interface\*name*\TypeLib <-> HKCR\TypeLib\{value}
            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (s.Contains(STR_TYPE))
                {
                    findSTR_TYPE = true;
                    sp = _cLightning.ReadString(Key, s, "");
                    if (!_cLightning.KeyExists(Key, STR_TYPEB + sp))
                    {
                        StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s, STR_DEFAULT, sp, RESULT_TYPE.ControlInterfaceType);
                    }
                }

                if (s.Contains(STR_PROXY) && (!Is64BitOperatingSystem())) //invalid in 64bit OS
                {
                    findSTR_PROXY = true;
                    sp = _cLightning.ReadString(Key, s, "");
                    if (!_cLightning.KeyExists(Key, STR_CLASSB + sp))
                    {
                        StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s, STR_DEFAULT, sp, RESULT_TYPE.ControlInterfaceProxy);
                    }
                }

                if (findSTR_TYPE && findSTR_PROXY)
                    break;
            }
        }

        void ProcServerPaths(cLightning.ROOT_KEY Key, string SubKey)
        {
            // process server subkeys
            string sp;
            // test pointers to valid paths HKCR\CLSID\*Proc* <-> library path
            // test for proc subkey existence
            if (_cLightning.KeyExists(Key, SubKey + STR_PROC32B))
            {
                ///* get the path
                sp = _cLightning.ReadString(Key, SubKey + STR_PROC32B, "");
                ///* test path length and type
                if (sp.Length > 0)
                {
                    if (IsValidPath(sp))
                    {
                        // format path and test
                        if (!FileExists(CleanPath(sp)) && IsFileCandidate(sp))
                        {
                            // add hklm path
                            StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + CHR_BSLASH + STR_PROC32, STR_DEFAULT, sp, RESULT_TYPE.ControlProcServer);
                        }
                    }
                }
            }
            if (_cLightning.KeyExists(Key, SubKey + STR_LOCAL32B))
            {
                sp = _cLightning.ReadString(Key, SubKey + STR_LOCAL32B, "");
                if (sp.Length > 0)
                {
                    if (IsValidPath(sp))
                    {
                        if (!FileExists(CleanPath(sp)) && IsFileCandidate(sp))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + CHR_BSLASH + STR_LOCAL32, STR_DEFAULT, sp, RESULT_TYPE.ControlProcServer);
                        }
                    }
                }
            }
            if (_cLightning.KeyExists(Key, SubKey + STR_PROCB))
            {
                sp = _cLightning.ReadString(Key, SubKey + STR_PROCB, "");
                if (sp.Length > 0)
                {
                    if (IsValidPath(sp))
                    {
                        if (!FileExists(CleanPath(sp)) && IsFileCandidate(sp))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + CHR_BSLASH + STR_PROC, STR_DEFAULT, sp, RESULT_TYPE.ControlProcServer);
                        }
                    }
                }
            }
            if (_cLightning.KeyExists(Key, SubKey + STR_LOCALB))
            {
                sp = _cLightning.ReadString(Key, SubKey + STR_LOCALB, "");
                if (sp.Length > 0)
                {
                    if (IsValidPath(sp))
                    {
                        if (!FileExists(CleanPath(sp)) && IsFileCandidate(sp))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + CHR_BSLASH + STR_LOCAL, STR_DEFAULT, STR_EMPTYVALUE, RESULT_TYPE.ControlProcServer);
                        }
                    }
                }
            }
        }

        void TypeLibPaths(cLightning.ROOT_KEY Key, string SubKey)
        {
            // test typelib registration id
            string sr;

            // test pointers to valid type library registration HKCR\CLSID\*name*\TypeLib {value} <-> HKCR\TypeLib\{value}
            // test for typelib subkey
            if (_cLightning.KeyExists(Key, SubKey + CHR_BSLASH + STR_TYPE))
            {
                // get the clsid
                sr = _cLightning.ReadString(Key, SubKey + CHR_BSLASH + STR_TYPE, "");
                // test id length
                if (sr.Length > 0)
                {
                    // tlb is not registered
                    if (!_cLightning.KeyExists(Key, STR_TYPEB + sr))
                    {
                        StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, SubKey + CHR_BSLASH + STR_TYPE, STR_DEFAULT, sr, RESULT_TYPE.ControlTypeLib);
                    }
                }
            }
        }

        void TypePaths(cLightning.ROOT_KEY Key, string SubKey)
        {
            // test for empty help keys
            // 6- delete key
            // 7- delete values
            string u = "";
            string sp = "";
            ArrayList al = KeyCollector(Key, SubKey);

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                u = s.ToUpper();
                // test pointers to valid help file registration HKCR\\TypeLib\*name*\helpdir->path
                if (u.Contains(STR_HELP))
                {
                    if (_cLightning.KeyIsEmpty(Key, SubKey))
                    {
                        StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s, STR_DEFAULT, STR_EMPTYVALUE, RESULT_TYPE.ControlTypeHelp);
                    }
                }
                // test pointers to valid win32 library registration HKCR\\TypeLib\*name*\win32->path
                else if (u.Contains(STR_WIN32))
                {
                    sp = _cLightning.ReadString(Key, s, "");
                    if (sp.Length > 0)
                    {
                        if (IsFileCandidate(sp))
                        {
                            sp = CleanPath(sp);
                            if (!FileExists(sp))
                            {
                                StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s, STR_DEFAULT, sp, RESULT_TYPE.ControlTypeWin32);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Phase 2 : User Software

        /// <summary>
        /// Test for valid software paths
        /// 
        /// Scan 1                           -Software
        /// Location                         HKEY_CURRENT_USER\Software
        /// Collect all software keys from \Software branch, and scan for valid path entries
        /// </summary>
        public void UserScan()
        {
            LabelChange(rm.GetString("UserSoftwarePaths"), rm.GetString("TestingUserSoftwarePaths"));

            string sp = "";
            ArrayList al = KeyCollector(cLightning.ROOT_KEY.HKEY_CURRENT_USER, "Software");
            ArrayList cv = new ArrayList();

            foreach (string k in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (k.StartsWith(@"Software\Microsoft\Windows Live"))
                    continue;

                cv = _cLightning.EnumValues(cLightning.ROOT_KEY.HKEY_CURRENT_USER, k);
                foreach (string v in cv)
                {
                    if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                        return;

                    if (v.Length > 4)
                    {
                        if (IsValidRoot(v))
                        {
                            sp = CleanPath(v);
                            if (IsFileCandidate(sp))
                            {
                                if (!FileExists(sp))
                                {
                                    string d = _cLightning.ReadString(cLightning.ROOT_KEY.HKEY_CURRENT_USER, k, v);
                                    StoreResults(cLightning.ROOT_KEY.HKEY_CURRENT_USER, k, v, v, RESULT_TYPE.User);
                                }
                            }
                            else if (!DirectoryExists(sp))
                            {
                                StoreResults(cLightning.ROOT_KEY.HKEY_CURRENT_USER, k, v, STR_EMPTY, RESULT_TYPE.User);
                            }
                        }
                    }
                    CurrentPath(REG_HKCUB, k);
                    KeyCount();
                }
            }
        }
        #endregion

        #region Phase 3 : System Software
        ///Purpose: Scan 1-5 -System Software  Test for valid id registrations
        ///1) test class names        - HKEY_CLASSES_ROOT\.xxx DefVal <-> HKEY_CLASSES_ROOT\..
        ///2) test clsid              - HKEY_CLASSES_ROOT\..\CLSID DefVal <-> HKEY_CLASSES_ROOT\CLSID
        ///3) object menu path        - HKEY_CLASSES_ROOT\..\shell\edit\command
        ///4) object open path        - HKEY_CLASSES_ROOT\..\shell\open\command
        ///5) object icon path        - HKEY_CLASSES_ROOT\..\DefaultIcon
        void FileScan()
        {
            LabelChange(rm.GetString("ClassNameRegistrations"), rm.GetString("CheckingClassNameRegistrations"));

            ArrayList al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, "");
            string sr = "";

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                {
                    return;
                }

                if (s.StartsWith(CHR_PERIOD))
                {
                    sr = _cLightning.ReadString(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s, "");
                    if (sr.Length > 0)
                    {
                        if (!_cLightning.KeyExists(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s, STR_DEFAULT, sr, RESULT_TYPE.FullClassName);
                        }
                    }
                }
                else
                {
                    // clsid test
                    if (_cLightning.KeyExists(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s + CHR_BSLASH + STR_CLASS))
                    {
                        sr = _cLightning.ReadString(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s + CHR_BSLASH + STR_CLASS, "");
                        if (sr.Length > 0)
                        {
                            if (sr.StartsWith("{"))
                            {
                                if (!_cLightning.KeyExists(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, STR_CLASSB + sr))
                                {
                                    StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s + CHR_BSLASH + STR_CLASS, STR_DEFAULT, sr, RESULT_TYPE.FullClsid);
                                }
                            }
                        }
                    }
                }

                // default icon
                if (_cLightning.KeyExists(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s + CHR_BSLASH + "DefaultIcon"))
                {
                    sr = _cLightning.ReadString(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s + STR_DEFICON, "");
                    if (sr.Length > 0 && IsFileCandidate(sr))
                    {
                        sr = CleanPath(sr);
                        if (IsValidRoot(sr) && !FileExists(CleanPath(sr)) && IsFileCandidate(sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, s + STR_DEFICON, STR_DEFAULT, sr, RESULT_TYPE.FullIcon);
                        }
                    }
                }
                CurrentPath(REG_HKCUB, s);
                KeyCount();
            }
        }
        #endregion

        #region Phase 4 : Fonts
        ///Locations:                 1) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts
        ///References:                From HKLM -> fonts folder
        ///Method:                    Path testing for valid occurence.

        void FontScan(cLightning.ROOT_KEY Key, string SubKey)
        {
            LabelChange(rm.GetString("FontPaths"), rm.GetString("CheckingFontPaths"));
            // 15- delete value
            ArrayList al = _cLightning.EnumValues(Key, SubKey);
            string sr = "";

            CurrentPath(REG_HKLMB, REG_HKLMFONTS);
            KeyCount();

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (s.Length > 0)
                {
                    string v = _cLightning.ReadString(Key, SubKey, s);
                    if (IsValidPath(v))
                    {
                        sr = CleanPath(v);
                        if (IsValidRoot(sr) && !FileExists(sr) && IsFileCandidate(sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, REG_HKLMFONTS, s, sr, RESULT_TYPE.Font);
                        }
                    }
                    else
                    {
                        sr = _sFontsDirectory + CleanPath(v);
                        if (!FileExists(sr) && HasExtension(sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, REG_HKLMFONTS, s, v, RESULT_TYPE.Font);
                        }
                    }
                }

            }
        }
        #endregion

        #region Phase 5 : Help Files
        ///Locations:                 1) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Help
        ///References:                From HKLM -> Help registration
        ///Method:                    Path testing for valid occurence.
        void HelpScan(cLightning.ROOT_KEY Key, string SubKey)
        {
            LabelChange(rm.GetString("ApplicationHelpFiles"), rm.GetString("CheckingApplicationHelpFiles"));
            ArrayList al = _cLightning.EnumValues(Key, SubKey);
            string sr = "";

            CurrentPath(REG_HKLMB, SubKey);
            KeyCount();

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (s.Length > 0)
                {
                    sr = _cLightning.ReadString(Key, SubKey, s);
                    if (sr.Length > 0)
                    {
                        // combine file name and path
                        if (!sr.EndsWith(CHR_BSLASH))
                        {
                            sr += CHR_BSLASH;
                        }
                        sr += s;
                        if (IsValidPath(sr))
                        {
                            if (IsValidRoot(sr) && !FileExists(sr) && IsFileCandidate(sr))
                            {
                                StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, SubKey, s, sr, RESULT_TYPE.Help);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Phase 6 : Shared DLLs
        ///Locations:                 1) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs
        ///References:                From HKLM -> path test
        ///Method:                    Path testing for valid occurence.
        void SharedDllScan(cLightning.ROOT_KEY Key, string SubKey)
        {
            LabelChange(rm.GetString("SharedLibraries"), rm.GetString("CheckingSharedLibraries"));
            // 17- delete value
            ArrayList al = _cLightning.EnumValues(Key, SubKey);

            CurrentPath(REG_HKLMB, SubKey);
            KeyCount();

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (IsValidPath(s))
                {
                    if (IsValidRoot(s) && !FileExists(s) && IsFileCandidate(s))
                    {
                        StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, REG_HKLMSHARE, s, s, RESULT_TYPE.Shared);
                    }
                }
            }
        }
        #endregion

        #region Phase 7 : Startup Entries
        ///Location:                  1) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
        ///Location:                  2) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce
        ///Location:                  3) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnceEx
        ///References:                From HKLM -> path test
        ///Method:                    Path testing for valid occurence.
        void StartupEntries(cLightning.ROOT_KEY Key, string SubKey)
        {
            LabelChange(rm.GetString("StartupApplicationPaths"), rm.GetString("CheckingStartupApplicationPaths"));
            // 18- delete value
            ArrayList al = _cLightning.EnumValues(Key, SubKey);
            string sr = "";
            CurrentPath(REG_HKLMB, SubKey);
            KeyCount();

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                {
                    return;
                }

                sr = _cLightning.ReadString(Key, SubKey, s);
                // empty value
                if (sr.Length == 0)
                {
                    StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, SubKey, s, STR_EMPTYVALUE, RESULT_TYPE.Startup);
                }
                else
                {
                    // test for shell directory shorthand
                    sr = TestSystemPaths(sr);
                    sr = CleanPath(sr);
                    if (IsValidRoot(sr) && !FileExists(CleanPath(sr)) && HasExtension(sr))
                    {
                        StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, SubKey, s, sr, RESULT_TYPE.Startup);
                    }
                }
            }
        }
        #endregion

        #region Phase 8 : Installed Software
        ///Locations:                 1) HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
        ///References:                From HKLM -> path test
        ///Method:                    Path testing for valid occurence.
        void UninstallStringsScan(cLightning.ROOT_KEY Key, string SubKey)
        {
            LabelChange(rm.GetString("UninstallExecutablePaths"), rm.GetString("CheckingUninstallExecutablePaths"));
            // 18- delete value
            ArrayList al = _cLightning.EnumKeys(Key, SubKey);
            string sr = "";

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                {
                    return;
                }

                sr = s.ToUpper();
                // ms stuff to skip
                if (!sr.Contains(STR_KILO) && !sr.Contains(STR_PACK))
                {
                    sr = _cLightning.ReadString(Key, SubKey + CHR_BSLASH + s, STR_UIST);
                    if (sr.Length != 0)
                    {
                        sr = CleanPath(sr);
                        if (IsValidRoot(sr) && !FileExists(sr) && HasExtension(sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, REG_HKLMUISL + s, STR_UIST, sr, RESULT_TYPE.Uninstall);
                        }
                    }
                }
                CurrentPath(REG_HKLMB, s);
                KeyCount();
            }
        }
        #endregion

        #region Phase 9 : Virtual Devices
        ///Locations:                 1) HKEY_LOCAL_MACHINE\SYSTEM\ControlSet\Control\VirtualDeviceDrivers
        ///References:                From HKLM -> fix for 16bit VDM value type mismatch
        ///Method:                    Value type testing for valid entry
        void VDMScan(cLightning.ROOT_KEY Key, string SubKey)
        {
            if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
            {
                return;
            }

            LabelChange(rm.GetString("VirtualDeviceRegistration"), rm.GetString("CheckingVirtualDeviceRegistration"));
            CurrentPath(REG_HKLMB, SubKey);
            KeyCount();

            if (_cLightning.ReadBinary(Key, SubKey, STR_VDD).Length > 0)
            {
                StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, REG_HKLMVDEV, SubKey, STR_VDD, RESULT_TYPE.Vdf);
            }
        }
        #endregion

        #region Phase 10: History and Start Menu
        ///References:                From HKLM -> scan for valid link paths
        ///Method:                    Value type testing for valid entry
        ///Location:                  HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\MenuOrder\Start Menu\Programs
        ///Location:                  HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\...\OpenWithList | OpenWithProgids
        ///Location:                  HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSaveMRU
        ///Location:                  HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist\...\Count
        void HistoryScan()
        {
            LabelChange(rm.GetString("HistorySettings"), rm.GetString("CheckingHistorySettings"));
            // test FileExts lists - EXPLORER HISTORY
            // 18- delete value
            ArrayList al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CURRENT_USER, HIST_EXPLORER);
            ArrayList av;
            cLightning.ROOT_KEY Key = cLightning.ROOT_KEY.HKEY_CURRENT_USER;
            string sr = "";
            al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CURRENT_USER, HIST_FILE);
            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                av = _cLightning.EnumValues(cLightning.ROOT_KEY.HKEY_CURRENT_USER, HIST_FILEB + s);
                foreach (string v in av)
                {
                    if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                        return;

                    if (v.Length > 0 && IsValidPath(v))
                    {
                        sr = CleanPath(v);
                        if (IsValidRoot(sr) && !FileExists(CleanPath(sr)) && IsFileCandidate(sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CURRENT_USER, HIST_FILEB + s, v, sr, RESULT_TYPE.HistoryStart);
                        }
                    }
                }
                CurrentPath(REG_HKCUB, s);
                KeyCount();
            }
            // test user assist - START MENU HISTORY
            al = KeyCollector(Key, HIST_START);
            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (s.Contains("Count"))
                {
                    av = _cLightning.EnumValues(Key, s);
                    foreach (string v in av)
                    {
                        if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                            return;

                        // decrypt
                        sr = _cLightning.Rot13(v);
                        sr = sr.Substring(sr.IndexOf(CHR_COLAN) + 1);
                        sr = CleanPath(sr);
                        if (IsValidRoot(sr) && !FileExists(CleanPath(sr)) && HasExtension(sr))
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CURRENT_USER, s, v, sr, RESULT_TYPE.HistoryMenu);
                        }
                    }
                }
                CurrentPath(REG_HKCUB, s);
                KeyCount();
            }
            // test start menu links - START MENU LINKS
            al = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CURRENT_USER, LINK_START);
            string sm = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs\";
            string su = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\Programs\";
            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                {
                    return;
                }

                if (!FileExists(sm + s + CHR_BSLASH))
                {
                    if (!FileExists(su + s + CHR_BSLASH))
                    {
                        if (_cLightning.EnumKeys(Key, LINK_STARTB + s).Count == 0)
                        {
                            StoreResults(cLightning.ROOT_KEY.HKEY_CURRENT_USER, LINK_STARTB + s, s, sm + s, RESULT_TYPE.HistoryLink);
                        }
                    }
                }
                CurrentPath(REG_HKCUB, s);
                KeyCount();
            }
        }
        #endregion

        #region Phase 11 : Deep Scan
        ///References:                From HKLM -> scan for valid link paths
        ///Method:                    Value type testing for valid entry
        ///Locations:                 HKEY_LOCAL_MACHINE\SOFTWARE
        ///Locations:                 HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\...\Products
        void DeepScan()
        {
            LabelChange(rm.GetString("DeepNativeSoftwareStrings"), rm.GetString("DeepSoftwareScan"));
            // 25- delete value
            // 26- delete value
            cLightning.ROOT_KEY Key = cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE;
            ArrayList al = KeyCollector(Key, "SOFTWARE");
            ArrayList av;
            string sr = "";

            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                {
                    return;
                }

                // microsoft keys
                if (s.Contains(STR_MSPATH))
                {
                    if (s.Contains(STR_INSPRP))
                    {
                        av = _cLightning.EnumValues(Key, s);
                        foreach (string v in av)
                        {
                            if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                                return;

                            if (v.Length > 4 && IsFileCandidate(v))
                            {
                                sr = CleanPath(v);
                                if (IsValidRoot(sr) && !FileExists(sr) && HasExtension(sr))
                                {
                                    StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, s, v, sr, RESULT_TYPE.DeepMs);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // software keys
                    av = _cLightning.EnumValues(Key, s);
                    foreach (string v in av)
                    {
                        if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                        {
                            return;
                        }

                        if (v.Length > 4 && IsFileCandidate(v))
                        {
                            sr = CleanPath(v);
                            if (IsValidRoot(sr) && !FileExists(sr) && HasExtension(sr))
                            {
                                StoreResults(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, s, v, sr, RESULT_TYPE.DeepSft);
                            }
                        }
                    }
                }
                CurrentPath(REG_HKLMB, s);
                KeyCount();
            }
        }
        #endregion

        #region Phase 12 : MRU Lists
        ///References:                From HKCU -> scan for valid link paths
        ///Method:                    Value type testing for valid entry
        ///Locations:                 HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RecentDocs
        ///Locations:                 HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\TypedURLs
        ///Locations:                 HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU
        ///Locations:                 HKEY_CURRENT_USER\Software\Microsoft\Search Assistant\ACMru\5603
        ///Locations:                 HKEY_CURRENT_USER\Software\Microsoft\Search Assistant\ACMru\5001
        ///Locations:                 HKEY_CURRENT_USER\Software\Microsoft\Search Assistant\ACMru\5647
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\Windows\CurrentVersion\Applets\Wordpad\Recent File List"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit\Favorites"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\Windows\CurrentVersion\Applets\Paint\Recent File List"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedMRU"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpaper\MRU"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\MediaPlayer\Player\RecentFileList"
        ///Locations:                 HKEY_CURRENT_USER\"Software\Microsoft\MediaPlayer\Player\RecentURLList"
        void MruScan(cLightning.ROOT_KEY Key, string SubKey)
        {
            LabelChange(rm.GetString("MRUScan"), rm.GetString("SearchingMRULists"));
            ArrayList al = _cLightning.EnumKeys(Key, SubKey);
            ArrayList cv = new ArrayList();

            foreach (string k in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                {
                    return;
                }

                cv = _cLightning.EnumValues(Key, SubKey + CHR_BSLASH + k);
                foreach (string v in cv)
                {
                    if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                        return;

                    if (v.Length > 0)
                    {
                        if (MruFilter(v))
                        {
                            string nk = SubKey + CHR_BSLASH + k;
                            StoreResults(cLightning.ROOT_KEY.HKEY_CURRENT_USER, nk, v, STR_EMPTYVALUE, RESULT_TYPE.Mru);
                        }
                    }
                }
                CurrentPath(REG_HKCUB, k);
                KeyCount();
            }
        }
        #endregion

        #region Helpers
        void AddKeys(cLightning.ROOT_KEY Key, string SubKey, ref ArrayList Keys)
        {
            ArrayList al = _cLightning.EnumKeys(Key, SubKey);
            // scan hkcr keys
            foreach (string s in al)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                Keys.Add(SubKey + CHR_BSLASH + s);
                if (s.Length > 0 && (!s.Contains("Wow64")))//ignore wow key
                {
                    AddKeys(Key, SubKey + CHR_BSLASH + s, ref Keys);
                }
            }
        }

        string CleanPath(string Path)
        {
            Match mc = _regPath.Match(Path);

            // test fast way first
            if (mc.Success && FileExists(mc.Groups[0].Value))
            {
                return mc.Groups[0].Value;
            }
            else
            {
                // extract path upon failure of regexp
                return ExtractPath(Path);
            }
        }

        bool DirectoryExists(string Path)
        {
            return (Directory.Exists(Path));
        }

        string ExtractPath(string Path)
        {
            string sp = Path.ToUpper();

            // test path first
            if (!FileExists(sp) && IsFileCandidate(sp)) //needed?? better spot maybe??
            {
                // trim to drive root
                if (sp.Substring(1, 1) != CHR_COLAN)
                {
                    sp = sp.Substring(sp.IndexOf(CHR_COLAN) - 1);
                }
                // truncate leading path
                if (sp.Substring(3).Contains(STR_PATH))
                {
                    sp = sp.Substring(sp.IndexOf(STR_PATH, 3) - 1);//PathFilter(path);
                }
                // find and trim to extension
                foreach (string s in _aExtensions)
                {
                    if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                        break;

                    if (sp.Contains(s))
                    {
                        sp = sp.Substring(0, sp.IndexOf(s) + s.Length);
                        break;
                    }
                }
                // get the long path
                if (sp.Contains(CHR_TILDE))
                {
                    sp = GetLongName(sp);
                }
            }
            return sp;
        }

        /// <summary>
        /// Checks if <paramref name="File"/> exists
        /// </summary>
        /// <param name="File">File</param>
        /// <returns>True if <paramref name="File"/> exists</returns>
        public bool FileExists(string File)
        {
            return (GetFileAttributes(File) != INVALID_HANDLE_VALUE);
        }

        ArrayList GetFileExtensions()
        {
            ArrayList md = _cLightning.EnumValues(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, MEDIATYPES);
            ArrayList lt = _cLightning.EnumKeys(cLightning.ROOT_KEY.HKEY_CLASSES_ROOT, "");
            ArrayList rs = new ArrayList();

            foreach (string s in md)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    break;

                if (s.StartsWith("."))
                {
                    rs.Add(s.ToUpper());
                }
            }
            foreach (string s in lt)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    break;

                if (s.StartsWith("."))
                {
                    if (!rs.Contains(s))
                    {
                        rs.Add(s.ToUpper());
                    }
                }
            }
            return rs;
        }

        string GetLongName(string Path)
        {
            StringBuilder sb = new StringBuilder(255);
            GetLongPathName(Path, sb, 255);

            return sb.ToString();
        }

        bool HasExtension(string Path)
        {
            bool result = false;
            foreach (string s in _aExtensions)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    break;

                if (Path.Contains(s))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        string IdConverter(RESULT_TYPE id)
        {
            switch (id)
            {
                case RESULT_TYPE.ControlAppID:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlClassSubEdit:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlClassSubExt:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlClassSubOpen:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlInterfaceProxy:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlInterfaceType:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlProcServer:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlTypeHelp:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlTypeLib:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.ControlTypeWin32:
                    return rm.GetString("SystemScan");
                case RESULT_TYPE.DeepMs:
                    return rm.GetString("DeepSystemScan");
                case RESULT_TYPE.DeepSft:
                    return rm.GetString("DeepSystemScan");
                case RESULT_TYPE.Font:
                    return rm.GetString("SystemFonts");
                case RESULT_TYPE.FullClassName:
                    return rm.GetString("SystemSoftware");
                case RESULT_TYPE.FullClsid:
                    return rm.GetString("SystemSoftware");
                case RESULT_TYPE.FullIcon:
                    return rm.GetString("SystemSoftware");
                case RESULT_TYPE.Help:
                    return rm.GetString("HelpFiles");
                case RESULT_TYPE.HistoryExplorer:
                    return rm.GetString("HistoryandStartMenu");
                case RESULT_TYPE.HistoryLink:
                    return rm.GetString("HistoryandStartMenu");
                case RESULT_TYPE.HistoryMenu:
                    return rm.GetString("HistoryandStartMenu");
                case RESULT_TYPE.HistoryStart:
                    return rm.GetString("HistoryandStartMenu");
                case RESULT_TYPE.Mru:
                    return rm.GetString("MRULists");
                case RESULT_TYPE.Shared:
                    return rm.GetString("SharedLibraries");
                case RESULT_TYPE.Startup:
                    return rm.GetString("StartupEntries");
                case RESULT_TYPE.Uninstall:
                    return rm.GetString("InstallationStrings");
                case RESULT_TYPE.User:
                    return rm.GetString("UserScan");
                case RESULT_TYPE.Vdf:
                    return rm.GetString("VirtualDevices");
                default:
                    return "";
            }
        }

        int IdToScope(int id)
        {
            switch (id)
            {
                case 1:
                    return 10;
                case 2:
                    return 10;
                case 3:
                    return 9;
                case 4:
                    return 9;
                case 5:
                    return 6;
                case 6:
                    return 6;
                case 7:
                    return 6;
                case 8:
                    return 10;
                case 9:
                    return 10;
                case 10:
                    return 6;
                case 11:
                    return 9;
                case 12:
                    return 9;
                case 13:
                    return 10;
                case 14:
                    return 7;
                case 15:
                    return 7;
                case 16:
                    return 4;
                case 17:
                    return 10;
                case 18:
                    return 8;
                case 19:
                    return 5;
                case 20:
                    return 4;
                case 21:
                    return 8;
                case 22:
                    return 8;
                case 23:
                    return 5;
                case 24:
                    return 5;
                case 25:
                    return 7;
                case 26:
                    return 8;
                default:
                    return 6;
            }
        }

        bool IsFileCandidate(string Path)
        {
            bool result = false;
            if (Path.Contains(STR_PATH) && Path.Contains(CHR_PERIOD))
                result = true;
            return result;
        }

        bool IsValidPath(string Path)
        {
            return (Path.Contains(STR_PATH));
        }

        bool IsValidRoot(string Path)
        {
            bool result = false;
            if (Path.Contains(STR_PATH))
            {
                string d = Path.Substring(0, Path.IndexOf(CHR_BSLASH) + 1);
                foreach (string s in _aDriveRoot)
                {
                    if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                        break;

                    if (s.Contains(d))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        ArrayList KeyCollector(cLightning.ROOT_KEY Key, string SubKey)
        {
            ArrayList al = new ArrayList();

            al.Add(SubKey);
            AddKeys(Key, SubKey, ref al);
            return al;
        }

        bool MruFilter(string value)
        {
            string s = value.ToUpper();

            if (IsNumeric(s))
            {
                return true;
            }
            else if (s.Contains("MRU"))
            {
                return true;
            }
            else if (s.Contains("FILE"))
            {
                return true;
            }
            else if (s.Contains("HISTORY"))
            {
                return true;
            }
            else if (s.Contains("LIST"))
            {
                return true;
            }
            else if (s.Contains("URI"))
            {
                return true;
            }
            else if (s.Contains("RECENT"))
            {
                return true;
            }
            else if (s.Contains("LAST"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if current Windows is 64-bit
        /// </summary>
        /// <returns></returns>
        public bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8)  // 64-bit programs run only on Win64
            {
                return true;
            }
            else  // 32-bit programs run on both 32-bit and 64-bit Windows
            {
                // Detect whether the current process is a 32-bit process 
                // running on a 64-bit system.
                bool flag;
                return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
                    IsWow64Process(GetCurrentProcess(), out flag)) && flag);
            }
        }

        bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
            {
                return false;
            }
            return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
        }

        bool IsNumeric(string value)
        {
            short x = 0;
            return (Int16.TryParse(value, out x));
        }

        void StoreCommonPaths()
        {
            _sSystem32Directory = Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper() + CHR_BSLASH;
            _sUserProgramsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Programs).ToUpper() + CHR_BSLASH;
            _sWindowsDirectory = _sSystem32Directory.Substring(0, _sSystem32Directory.IndexOf("SYSTEM32"));
            _sFontsDirectory = _sWindowsDirectory + @"FONTS\";
            _sProgramsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        }

        void StoreLogicalDrives()
        {
            string[] a = Directory.GetLogicalDrives();
            int ct = 0;
            _aDriveRoot = new string[1];

            foreach (string s in a)
            {
                if (_oProcessAsyncBackgroundWorker != null && _oProcessAsyncBackgroundWorker.CancellationPending)
                    return;

                if (Directory.Exists(s))
                {
                    ct += 1;
                    Array.Resize(ref _aDriveRoot, ct);
                    _aDriveRoot[ct - 1] = s;
                }
            }
        }

        string TestSystemPaths(string Path)
        {
            string sp = Path.ToUpper();

            if (sp.Contains("%"))
            {
                if (sp.Contains("%PROGRAMFILES%"))
                {
                    sp.Replace("%PROGRAMFILES%", _sProgramsDirectory);
                }
                else if (sp.Contains("%SYSTEM%"))
                {
                    sp.Replace("%SYSTEM%", _sSystem32Directory);
                }
                else if (sp.Contains("%WINDOWS%"))
                {
                    sp.Replace("%WINDOWS%", _sWindowsDirectory);
                }
            }
            return sp;
        }
        #endregion
    }
}
