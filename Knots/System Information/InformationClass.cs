#region " Imported Namespaces "

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Reflection;
using System.Resources;
using System.Security.Permissions;
using System.Security.Principal;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

#endregion

namespace SystemInformation
{
    /// <summary>
    /// Information class model
    /// </summary>
    public class InformationClass
    {
        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
                                                        Assembly.GetExecutingAssembly());

        #region " Constructor "

        /// <summary>
        /// <see cref="InformationClass"/> constructor
        /// </summary>
        public InformationClass()
        {
            myApp = typeof(InformationClass);
        }

        #endregion

        #region " Initialize "

        /// <summary>
        /// Initialize according to property setting.
        /// </summary>
        public void Initialize(Initializers mvarinitializers)
        {
            switch (mvarinitializers)
            {
                case Initializers.GetAll:
                    GetAll();
                    break;
                case Initializers.GetBiosInfo:
                    GetBiosInfo();
                    break;
                case Initializers.GetCpuInfo:
                    GetCpuInfo();
                    break;
                case Initializers.GetDriveInformation:
                    GetDriveInformation();
                    break;
                case Initializers.GetNetAdaptorInfo:
                    GetNetAdaptorInfo();
                    break;
                case Initializers.GetNetInterfaceInfo:
                    GetNetInterfaceInfo();
                    break;
                case Initializers.GetSoundInfo:
                    GetSoundInfo();
                    break;
                case Initializers.GetVideoInfo:
                    GetVideoInfo();
                    break;
                case Initializers.GetVolumeInfo:
                    GetVolumeInfo();
                    break;
                case Initializers.GetNone:
                    break;
                case Initializers.GetServiceInfo:
                    GetServiceInfo();
                    break;
                default:
                    GetAll();
                    break;
            }
        }

        void GetAll()
        {
            GetCpuInfo();
            GetBiosInfo();
            GetVolumeInfo();
            GetVideoInfo();
            GetSoundInfo();
            GetNetAdaptorInfo();
            GetNetInterfaceInfo();
            GetDriveInformation();
            GetServiceInfo();
        }

        #endregion

        #region " Variables, Constants, Declarations, and Enums "

        #region " Class-level Variables, Constants, and Declarations "

        // Class-level variables.

        // Class-level constants.
        const string MRevision = "Revision";
        const string MUnknown = "Unknown";

        // Used to access information from Assembly (application) Attributes.
        readonly Type myApp;
        ManagementObject info;
        DateTime mTempDate;

        #endregion

        #region " Operating System Code HolidayName Constants "

        // code names
        const string mstrChicago = "Chicago";
        const string mstrDetroit = "Detroit";
        const string mstrMemphis = "Memphis";
        const string mstrGeorgia = "Georgia";
        const string mstrCairo = "Cairo";
        const string mstrCairoNT5 = "Cairo/NT5";
        const string mstrWhistler = "Whistler";
        const string mstrWhistlerServer = "Whistler Server";
        const string mstrLonghorn = "Longhorn";

        #endregion

        #region " Local Variables for Property Values "

        readonly Collection<string> mvarBiosFeatures = new Collection<string>();
        readonly Collection<string> mvarDriveCapacity = new Collection<string>();
        readonly Collection<string> mvarDriveInterface = new Collection<string>();
        readonly Collection<string> mvarDriveModelNo = new Collection<string>();
        readonly Collection<string> mvarDriveStatus = new Collection<string>();
        readonly Collection<string> mvarNetAdapterType = new Collection<string>();
        readonly Collection<string> mvarNetAutoSense = new Collection<string>();
        readonly Collection<string> mvarNetConnectionId = new Collection<string>();
        readonly Collection<string> mvarNetConnectionStatus = new Collection<string>();
        readonly Collection<string> mvarNetDefaultTtl = new Collection<string>();
        readonly Collection<bool> mvarNetDhcpEnabled = new Collection<bool>();
        readonly Collection<string> mvarNetDhcpLeaseExpires = new Collection<string>();
        readonly Collection<string> mvarNetDhcpLeaseObtained = new Collection<string>();
        readonly Collection<string> mvarNetDhcpServer = new Collection<string>();
        readonly Collection<string> mvarNetDomain = new Collection<string>();
        readonly Collection<string> mvarNetHostName = new Collection<string>();
        readonly Collection<string> mvarNetIPAddress = new Collection<string>();
        readonly Collection<bool> mvarNetIPEnabled = new Collection<bool>();
        readonly Collection<string> mvarNetMacAddress = new Collection<string>();
        readonly Collection<string> mvarNetManufacturer = new Collection<string>();
        readonly Collection<string> mvarNetMaxSpeed = new Collection<string>();
        readonly Collection<string> mvarNetMtu = new Collection<string>();
        readonly Collection<string> mvarNetProductName = new Collection<string>();
        readonly Collection<string> mvarNetSpeed = new Collection<string>();
        readonly Collection<string> mvarNetTcpNumConnections = new Collection<string>();
        readonly Collection<string> mvarNetTcpWindowSize = new Collection<string>();
        readonly Collection<string> mvarServiceDescription = new Collection<string>();
        readonly Collection<string> mvarServiceDisplayName = new Collection<string>();
        readonly Collection<string> mvarServicePathName = new Collection<string>();
        readonly Collection<string> mvarServiceStartMode = new Collection<string>();
        readonly Collection<string> mvarServiceState = new Collection<string>();
        readonly Collection<string> mvarServiceStatus = new Collection<string>();
        readonly Collection<string> mvarSndController = new Collection<string>();
        readonly Collection<string> mvarSndDMABufferSize = new Collection<string>();
        readonly Collection<string> mvarSndManufacturer = new Collection<string>();
        readonly Collection<string> mvarVidController = new Collection<string>();
        readonly Collection<string> mvarVidRam = new Collection<string>();
        readonly Collection<string> mvarVidRefreshRate = new Collection<string>();
        readonly Collection<string> mvarVidScreenColors = new Collection<string>();
        readonly Collection<string> mvarVolumeFileSystem = new Collection<string>();
        readonly Collection<string> mvarVolumeFreeSpace = new Collection<string>();
        readonly Collection<string> mvarVolumeLabel = new Collection<string>();
        readonly Collection<string> mvarVolumeLetter = new Collection<string>();
        readonly Collection<string> mvarVolumePercentFreeSpace = new Collection<string>();
        readonly Collection<bool> mvarVolumeReady = new Collection<bool>();
        readonly Collection<string> mvarVolumeSerialNumber = new Collection<string>();
        readonly Collection<string> mvarVolumeTotalSize = new Collection<string>();
        readonly Collection<string> mvarVolumeType = new Collection<string>();
        readonly Collection<string> mvarVolumeUsedSpace = new Collection<string>();
        string mvarAppBuild;
        string mvarAppCompanyName;
        string mvarAppCopyright;
        string mvarAppDescription;
        string mvarAppDirectory;
        int mvarAppMajorRevision;
        int mvarAppMajorVersion;
        int mvarAppMinorRevision;
        int mvarAppMinorVersion;
        string mvarAppProductName;
        string mvarAppRevision;
        string mvarAppShortVersion;
        string mvarAppTitle;
        string mvarAppTrademark;
        string mvarAppVersion;
        string mvarBiosManufacturer;
        string mvarBiosName;
        string mvarBiosReleaseDate;
        bool mvarBiosSmBiosPresent;
        string mvarBiosSmBiosVersion;
        string mvarBiosVersion;
        Collection<string> mvarCDDrive = new Collection<string>();
        Collection<string> mvarCDManufacturer = new Collection<string>();
        Collection<string> mvarCDMediaSize = new Collection<string>();
        Collection<string> mvarCDModel = new Collection<string>();
        Collection<string> mvarCDRevisionLevel = new Collection<string>();
        Collection<string> mvarCDStatus = new Collection<string>();
        bool mvarCompAutomaticResetCapability;
        string mvarCompDescription;
        string mvarCompManufacturer;
        string mvarCompModel;
        string mvarCompSystemType;
        string mvarCpuAddressWidth;
        string mvarCpuDescription;
        string mvarCpuFsbSpeed;
        string mvarCpuL2CacheSize;
        string mvarCpuL2CacheSpeed;
        Collection<Int32> mvarCpuLoadPercentage = new Collection<Int32>();
        string mvarCpuManufacturer;
        string mvarCpuName;
        int mvarCpuNumberOfCores;
        int mvarCpuNumberOfLogicalProcessors;
        int mvarCpuNumberOfProcessors;
        string mvarCpuPowerManagementCapabilities;
        bool mvarCpuPowerManagementSupported;
        string mvarCpuProcessorId;
        string mvarCpuSocket;
        string mvarCpuSpeed;
        int mvarFrameworkMajorVersion;
        int mvarFrameworkMinorVersion;
        string mvarFrameworkServicePack;
        string mvarFrameworkShortVersion;
        string mvarFrameworkVersion;
        string mvarMainBoardManufacturer;
        string mvarMainBoardModel;
        string mvarMemAvailPhysical;
        string mvarMemAvailVirtual;
        string mvarMemTotalPhysical;
        string mvarMemTotalVirtual;
        int mvarNetNumberOfAdaptors;
        string mvarOSBootupState;
        string mvarOSBuild;
        string mvarOSCodeName;
        string mvarOSDomain;
        string mvarOSFullName;
        DateTime mvarOSInstallDate;
        string mvarOSMachineName;
        int mvarOSMajorVersion;
        int mvarOSMinorVersion;
        bool mvarOSPartOfDomain;
        PlatformID mvarOSPlatform;
        string mvarOSProductID;
        string mvarOSProductKey;
        string mvarOSServicePack;
        string mvarOSShortVersion;
        string mvarOSType;
        string mvarOSUpTime;
        string mvarOSUserName;
        string mvarOSVersion;
        int mvarSndNumberOfControllers;
        string mvarTimeCurrentTimeZone;
        bool mvarTimeDaylightSavingsInEffect;
        string mvarTimeDaylightSavingsName;
        DateTime mvarTimeLocalDateTime;
        DateTime mvarTimeLocalDaylightEndDate;
        DateTime mvarTimeLocalDaylightEndTime;
        DateTime mvarTimeLocalDaylightStartDate;
        DateTime mvarTimeLocalDaylightStartTime;
        DateTime mvarTimeUniversalDateTime;
        DateTime mvarTimeUniversalDaylightEndDate;
        DateTime mvarTimeUniversalDaylightEndTime;
        DateTime mvarTimeUniversalDaylightStartDate;
        DateTime mvarTimeUniversalDaylightStartTime;
        int mvarUniversalTimeOffset;
        Collection<string> mvarUserAccount = new Collection<string>();
        Collection<int> mvarUserFlags = new Collection<int>();
        Collection<string> mvarUserFullName = new Collection<string>();
        bool mvarUserIsAdministrator;
        Collection<string> mvarUserPrivilege = new Collection<string>();
        string mvarUserRegisteredName;
        string mvarUserRegisteredOrganization;
        int mvarVidNumberOfControllers;
        string mvarVidPrimaryScreenWorkingArea;
        string mvarVidPrimarytScreenDimensions;
        string mvarVstAuthor;
        string mvarVstColorScheme;
        string mvarVstCompany;
        Color mvarVstControlHighlightHot;
        string mvarVstCopyright;
        string mvarVstDescription;
        string mvarVstDisplayName;
        bool mvarVstIsEnabledByUser;
        bool mvarVstIsSupportedByOS;
        int mvarVstMinimumColorDepth;
        string mvarVstSize;
        bool mvarVstSupportsFlatMenus;
        Color mvarVstTextControlBorder;
        string mvarVstUrl;
        string mvarVstVersion;

        #endregion

        #region " Enums "

        /// <summary>
        /// Procedures that can be initialized
        /// </summary>
        public enum Initializers
        {
            GetAll = 0,
            GetCpuInfo = 1,
            GetBiosInfo = 2,
            GetVolumeInfo = 3,
            GetVideoInfo = 4,
            GetSoundInfo = 5,
            GetNetAdaptorInfo = 6,
            GetNetInterfaceInfo = 7,
            GetDriveInformation = 8,
            GetServiceInfo,
            GetNone = 99
        }

        #endregion

        #endregion

        #region " Helper and Formating Methods "

        #region " Return BIOS Feature "

        string ReturnBiosFeature(short shtFeature)
        {
            string retValue = string.Empty;

            if (shtFeature > 39 && shtFeature < 48) // Eliminated complex case statements for C# compatibility.
            {
                return rm.GetString("infoclass_reserved_for_bios");
            }
            if (shtFeature > 47 && shtFeature < 64)
            {
                return rm.GetString("infoclass_reserved_for_system");
            }
            switch (shtFeature)
            {
                case 0:
                    retValue = "Reserved";
                    break;
                case 1:
                    retValue = "Reserved";
                    break;
                case 2:
                    retValue = MUnknown;
                    break;
                case 3:
                    retValue = "BIOS Characteristics Not Supported ";
                    break;
                case 4:
                    retValue = "ISA is supported";
                    break;
                case 5:
                    retValue = "MCA is supported";
                    break;
                case 6:
                    retValue = "EISA is supported";
                    break;
                case 7:
                    retValue = "PCI is supported";
                    break;
                case 8:
                    retValue = "PC Card(PCMCIA) is supported";
                    break;
                case 9:
                    retValue = "Plug and Play is supported";
                    break;
                case 10:
                    retValue = "APM is supported";
                    break;
                case 11:
                    retValue = "BIOS is Upgradable (Flash)";
                    break;
                case 12:
                    retValue = "BIOS shadowing is allowed";
                    break;
                case 13:
                    retValue = "VL-VESA is supported";
                    break;
                case 14:
                    retValue = "ESCD support is available";
                    break;
                case 15:
                    retValue = "Boot from CD is supported";
                    break;
                case 16:
                    retValue = "Selectable Boot is supported";
                    break;
                case 17:
                    retValue = "BIOS ROM is socketed";
                    break;
                case 18:
                    retValue = "Boot From PC Card (PCMCIA) is supported";
                    break;
                case 19:
                    retValue = "EDD (Enhanced Disk Drive) Specification is supported";
                    break;
                case 20:
                    retValue = "Int 13h - Japanese Floppy for NEC 9800 1.2mb (3.5, 1k Bytes/Sector, 360 RPM) is supported";
                    break;
                case 21:
                    retValue = "Int 13h - Japanese Floppy for Toshiba 1.2mb (3.5, 360 RPM) is supported";
                    break;
                case 22:
                    retValue = "Int 13h - 5.25 / 360 KB Floppy Services are supported";
                    break;
                case 23:
                    retValue = "Int 13h - 5.25 /1.2MB Floppy Services are supported";
                    break;
                case 24:
                    retValue = "Int 13h - 3.5 / 720 KB Floppy Services are supported";
                    break;
                case 25:
                    retValue = "Int 13h - 3.5 / 2.88 MB Floppy Services are supported";
                    break;
                case 26:
                    retValue = "Int 5h, Print Screen Service is supported";
                    break;
                case 27:
                    retValue = "Int 9h, 8042 Keyboard services are supported";
                    break;
                case 28:
                    retValue = "Int 14h, Serial Services are supported";
                    break;
                case 29:
                    retValue = "Int 17h, printer services are supported";
                    break;
                case 30:
                    retValue = "Int 10h, CGA/Mono Video Services are supported";
                    break;
                case 31:
                    retValue = "NEC PC - 98";
                    break;
                case 32:
                    retValue = "ACPI supported";
                    break;
                case 33:
                    retValue = "USB Legacy is supported";
                    break;
                case 34:
                    retValue = "AGP is supported";
                    break;
                case 35:
                    retValue = "I2O boot is supported";
                    break;
                case 36:
                    retValue = "LS-120 boot is supported";
                    break;
                case 37:
                    retValue = "ATAPI ZIP Drive boot is supported";
                    break;
                case 38:
                    retValue = "1394 boot is supported";
                    break;
                case 39:
                    retValue = "Smart Battery supported";
                    break;
                default:
                    retValue = string.Empty;
                    break;
            }

            return retValue;
        }

        #endregion

        #region " Return Colors "

        static string ReturnColors(int bits)
        {
            string retValue;

            // no video card has more than 1.67 million colors
            if (bits > 24)
            {
                bits = 24;
            }

            switch (bits)
            {
                case 2:
                    retValue = "2 Colors (Black and White)";
                    break;
                case 4:
                    retValue = "16 Colors";
                    break;
                case 8:
                    retValue = "256 Colors";
                    break;
                case 16:
                    retValue = "65,535 Colors (High Color)";
                    break;
                case 24:
                    retValue = "16.8 Million Colors (True Color)";
                    break;
                default:
                    retValue = MUnknown;
                    break;
            }

            return retValue;
        }

        #endregion

        #region " Return Network Connection Status "

        static string ReturnNetConnectionStatus(int intStatus)
        {
            string retValue = string.Empty;

            switch (intStatus)
            {
                case 0:
                    retValue = "Disconnected";
                    break;
                case 1:
                    retValue = "Connecting";
                    break;
                case 2:
                    retValue = "Connected";
                    break;
                case 3:
                    retValue = "Disconnecting";
                    break;
                case 4:
                    retValue = "Hardware not present";
                    break;
                case 5:
                    retValue = "Hardware disabled";
                    break;
                case 6:
                    retValue = "Hardware malfunction";
                    break;
                case 7:
                    retValue = "Media disconnected";
                    break;
                case 8:
                    retValue = "Authenticating";
                    break;
                case 9:
                    retValue = "Authentication succeeded";
                    break;
                case 10:
                    retValue = "Authentication failed";
                    break;
                case 11:
                    retValue = "Invalid address";
                    break;
                case 12:
                    retValue = "Credentials required";
                    break;
                default:
                    retValue = string.Empty;
                    break;
            }

            return retValue;
        }

        #endregion

        #region " Return CPU Power Capabilites "

        string ReturnPowerCapabilities(int powerCap)
        {
            string returnValue;

            switch (powerCap)
            {
                case 0:
                    returnValue = "Unknown";
                    break;
                case 1:
                    returnValue = "Not Supported";
                    break;
                case 2:
                    returnValue = "Disabled";
                    break;
                case 3:
                    returnValue = "Enabled";
                    break;
                case 4:
                    returnValue = "Automatic";
                    break;
                case 5:
                    returnValue = "Settable";
                    break;
                case 6:
                    returnValue = "Power Cycling Supported";
                    break;
                case 7:
                    returnValue = "Timed Power On Supported";
                    break;
                default:
                    returnValue = "Not Present or Unknown";
                    break;
            }

            return returnValue;
        }

        #endregion

        #region " Formatting Subroutines "

        static string FormatBytes(double bytes)
        {
            double temp;

            if (bytes >= 1073741824)
            {
                temp = bytes / 1073741824; // GB
                return String.Format("{0:N2}", temp) + " GB";
            }
            if (bytes >= 1048576 && bytes <= 1073741823)
            {
                temp = bytes / 1048576; //MB
                return String.Format("{0:N0}", temp) + " MB";
            }
            if (bytes >= 1024 && bytes <= 1048575)
            {
                temp = bytes / 1024; // KB
                return String.Format("{0:N0}", temp) + " KB";
            }
            if (bytes == 0 && bytes <= 1023)
            {
                temp = bytes; // bytes
                return String.Format("{0:N0}", temp) + " bytes";
            }
            return string.Empty;
        }

        static string FormatHertz(double hertz)
        {
            double temp;

            if (hertz >= 1000000000) //GHz
            {
                temp = hertz / 1000000000;
                return String.Format("{0:N2}", temp) + " GHz";
            }
            if (hertz >= 1048576 && hertz <= 1073741823)
            {
                temp = hertz / 1000000; //MHz
                return String.Format("{0:N2}", temp) + " MHz";
            }
            if (hertz >= 1024 && hertz <= 1048575)
            {
                temp = hertz / 1000; //KHz
                return String.Format("{0:N2}", temp) + " KHz";
            }
            if (hertz >= 0 && hertz <= 1023)
            {
                temp = hertz; // Hz
                return String.Format("{0:N0}", temp) + " Hz";
            }
            return string.Empty;
        }

        #endregion

        #endregion

        #region " Information Retrieval Methods "

        #region " Get Application Information "

        string GetAppCompanyName()
        {
            Type at = typeof(AssemblyCompanyAttribute);
            object[] r = myApp.Assembly.GetCustomAttributes(at, false);
            var ct = (AssemblyCompanyAttribute)(r[0]);
            return ct.Company;
        }

        string GetAppCopyright()
        {
            Type at = typeof(AssemblyCopyrightAttribute);
            object[] r = myApp.Assembly.GetCustomAttributes(at, false);
            var ct = (AssemblyCopyrightAttribute)(r[0]);
            return ct.Copyright;
        }

        string GetAppDescription()
        {
            Type at = typeof(AssemblyDescriptionAttribute);
            object[] r = myApp.Assembly.GetCustomAttributes(at, false);
            var da = (AssemblyDescriptionAttribute)(r[0]);
            return da.Description;
        }

        string GetAppProductName()
        {
            Type at = typeof(AssemblyProductAttribute);
            object[] r = myApp.Assembly.GetCustomAttributes(at, false);
            var pt = (AssemblyProductAttribute)(r[0]);
            return pt.Product;
        }

        string GetAppTitle()
        {
            Type at = typeof(AssemblyTitleAttribute);
            object[] r = myApp.Assembly.GetCustomAttributes(at, false);
            var ta = (AssemblyTitleAttribute)(r[0]);
            return ta.Title;
        }

        string GetAppTrademark()
        {
            Type at = typeof(AssemblyTrademarkAttribute);
            object[] r = myApp.Assembly.GetCustomAttributes(at, false);
            var ma = (AssemblyTrademarkAttribute)(r[0]);
            return ma.Trademark;
        }

        string GetAppVersion()
        {
            return myApp.Assembly.GetName().Version.ToString();
        }

        int GetAppMajorRevision()
        {
            return Convert.ToInt32(myApp.Assembly.GetName().Version.MajorRevision);
        }

        int GetAppMajorVersion()
        {
            return myApp.Assembly.GetName().Version.Major;
        }

        int GetAppMinorRevision()
        {
            return myApp.Assembly.GetName().Version.Major;
        }

        int GetAppMinorVersion()
        {
            return myApp.Assembly.GetName().Version.Minor;
        }

        string GetAppRevision()
        {
            return myApp.Assembly.GetName().Version.Revision.ToString();
        }

        string GetAppShortVersion()
        {
            return myApp.Assembly.GetName().Version.ToString().Substring(0, 3);
        }

        string GetAppBuild()
        {
            return myApp.Assembly.GetName().Version.Build.ToString();
        }

        static string GetAppDirectory()
        {
            return Environment.CurrentDirectory;
        }

        #endregion

        #region " Get BIOS Information "

        void GetBiosInfo()
        {
            var query = new SelectQuery("Win32_BIOS");
            var search = new ManagementObjectSearcher(query);
            short[] features;
            int count;

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    if (Convert.ToBoolean(info["PrimaryBIOS"]))
                    {
                        try
                        {
                            mvarBiosManufacturer = Convert.ToString(info["Manufacturer"]);
                        }
                        catch
                        {
                            mvarBiosManufacturer = MUnknown;
                        }

                        try
                        {
                            mvarBiosName = Convert.ToString(info["Name"]);
                        }
                        catch
                        {
                            mvarBiosName = MUnknown;
                        }

                        try
                        {
                            mvarBiosVersion = Convert.ToString(info["Version"]);
                        }
                        catch
                        {
                            mvarBiosVersion = MUnknown;
                        }

                        try
                        {
                            mvarBiosReleaseDate =
                                Convert.ToString(info["ReleaseDate"]).Substring(0, 8).Insert(4, "-").Insert(7, "-");
                        }
                        catch
                        {
                            mvarBiosReleaseDate = MUnknown;
                        }

                        try
                        {
                            features = (short[])info["BiosCharacteristics"];
                            for (count = 0; count <= (features.Length - 1); count++)
                            {
                                if (!String.IsNullOrEmpty(ReturnBiosFeature(features[count])))
                                {
                                    mvarBiosFeatures.Add(ReturnBiosFeature(features[count]));
                                }
                            }
                        }
                        catch
                        {
                            mvarBiosFeatures.Add("");
                        }
                    }

                    try
                    {
                        mvarBiosSmBiosPresent = Convert.ToBoolean(info["SMBIOSPresent"]);

                        if (mvarBiosSmBiosPresent)
                        {
                            mvarBiosSmBiosVersion = Convert.ToString(info["SMBIOSMajorVersion"]) + "." +
                                                    Convert.ToString(info["SMBIOSMinorVersion"]);
                        }
                        else
                        {
                            mvarBiosSmBiosVersion = "";
                        }
                    }
                    catch
                    {
                        mvarBiosSmBiosPresent = false;
                        mvarBiosSmBiosVersion = "";
                    }
                }
                catch
                {
                    mvarBiosManufacturer = "";
                    mvarBiosName = "";
                    mvarBiosReleaseDate = "";
                    mvarBiosSmBiosPresent = false;
                    mvarBiosSmBiosVersion = "";
                }
            }

            search.Dispose();
        }

        #endregion

        #region " Get CDROM Drive Information "

        Collection<string> GetCDDrive()
        {
            var temp = new Collection<string>();

            var query = new SelectQuery("Win32_CDROMDrive");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp.Add(Convert.ToString(info["Drive"]));
                }
                catch
                {
                    temp.Add("");
                }
            }

            return temp;
        }

        Collection<string> GetCDManufacturer()
        {
            var temp = new Collection<string>();

            var query = new SelectQuery("Win32_CDROMDrive");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp.Add(Convert.ToString(info["Manufacturer"]));
                }
                catch
                {
                    temp.Add("");
                }
            }

            return temp;
        }

        Collection<string> GetCDModel()
        {
            var temp = new Collection<string>();

            var query = new SelectQuery("Win32_CDROMDrive");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp.Add(Convert.ToString(info["Name"]));
                }
                catch
                {
                    temp.Add("");
                }
            }

            return temp;
        }

        Collection<string> GetCDRevisionLevel()
        {
            var temp = new Collection<string>();

            var query = new SelectQuery("Win32_CDROMDrive");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    if (info["MfrAssignedRevisionLevel"] != null)
                    {
                        temp.Add(Convert.ToString(info["MfrAssignedRevisionLevel"]));
                    }
                    else if (info["RevisionLevel"] != null)
                    {
                        temp.Add(Convert.ToString(info["RevisionLevel"]));
                    }
                }
                catch
                {
                    temp.Add("N/A");
                }
            }

            return temp;
        }

        Collection<string> GetCDMediaSize()
        {
            var temp = new Collection<string>();

            var query = new SelectQuery("Win32_CDROMDrive");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp.Add(FormatBytes(Convert.ToDouble(info["Size"])));
                }
                catch
                {
                    temp.Add("Unknown");
                }
            }

            return temp;
        }

        Collection<string> GetCDStatus()
        {
            var temp = new Collection<string>();

            var query = new SelectQuery("Win32_CDROMDrive");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp.Add(info["Status"].ToString());
                }
                catch
                {
                    temp.Add("Unknown");
                }
            }

            return temp;
        }

        #endregion

        #region " Get Computer Information "

        bool GetCompAutomaticResetCapability()
        {
            bool temp = false;

            var query = new SelectQuery("Win32_ComputerSystem");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = Convert.ToBoolean(info["Caption"]);
                }
                catch
                {
                    temp = false;
                }
            }

            return temp;
        }

        string GetCompDescription()
        {
            string temp = "";

            var query = new SelectQuery("Win32_ComputerSystem");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = info["Description"].ToString();
                }
                catch
                {
                    temp = "";
                }
            }

            return temp;
        }

        string GetCompManufacturer()
        {
            string temp = "";

            var query = new SelectQuery("Win32_ComputerSystem");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = info["Manufacturer"].ToString();
                }
                catch
                {
                    temp = "";
                }
            }

            return temp;
        }

        string GetCompModel()
        {
            string temp = "";

            var query = new SelectQuery("Win32_ComputerSystem");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = info["Model"].ToString();
                }
                catch
                {
                    temp = "";
                }
            }

            return temp;
        }

        string GetCompSystemType()
        {
            string temp = "";

            var query = new SelectQuery("Win32_ComputerSystem");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = info["SystemType"].ToString();
                }
                catch
                {
                    temp = "";
                }
            }

            return temp;
        }

        #endregion

        #region " Get CPU Information "

        void GetCpuInfo()
        {
            var query = new SelectQuery("Win32_Processor");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    mvarCpuAddressWidth = info["AddressWidth"] + " bit";
                }
                catch
                {
                    mvarCpuAddressWidth = MUnknown;
                }

                try
                {
                    mvarCpuDescription = info["Description"].ToString();
                }
                catch
                {
                    mvarCpuDescription = MUnknown;
                }

                try
                {
                    mvarCpuFsbSpeed = info["ExtClock"] + " MHz";
                }
                catch
                {
                    mvarCpuFsbSpeed = MUnknown;
                }

                try
                {
                    if (Convert.ToDouble(info["L2CacheSize"]) == 0)
                    {
                        mvarCpuL2CacheSize = MUnknown;
                    }
                    else
                    {
                        mvarCpuL2CacheSize = FormatBytes(Convert.ToDouble(info["L2CacheSize"]) * 1024);
                    }
                }
                catch
                {
                    mvarCpuL2CacheSize = MUnknown;
                }

                try
                {
                    if (Convert.ToDouble(info["L2CacheSpeed"]) == 0)
                    {
                        mvarCpuL2CacheSpeed = MUnknown;
                    }
                    else
                    {
                        mvarCpuL2CacheSpeed = FormatHertz(Convert.ToDouble(info["L2CacheSpeed"]) * 1000000);
                    }
                }
                catch
                {
                    mvarCpuL2CacheSpeed = MUnknown;
                }

                try
                {
                    mvarCpuManufacturer = info["Manufacturer"].ToString();
                }
                catch
                {
                    mvarCpuManufacturer = MUnknown;
                }

                try
                {
                    mvarCpuName = info["Name"].ToString();
                }
                catch
                {
                    mvarCpuName = MUnknown;
                }

                try
                {
                    mvarCpuSocket = info["SocketDesignation"].ToString();
                }
                catch
                {
                    mvarCpuSocket = MUnknown;
                }

                try
                {
                    mvarCpuSpeed = FormatHertz(Convert.ToDouble(info["CurrentClockSpeed"]) * 1000000);
                }
                catch
                {
                    mvarCpuSpeed = MUnknown;
                }

                try
                {
                    mvarCpuProcessorId = info["ProcessorID"].ToString();
                }
                catch
                {
                    mvarCpuProcessorId = MUnknown;
                }

                try
                {
                    mvarCpuPowerManagementSupported = Convert.ToBoolean(info["PowerManagementCapabilities"]);
                }
                catch
                {
                    mvarCpuPowerManagementSupported = false;
                }

                try
                {
                    mvarCpuPowerManagementCapabilities = ReturnPowerCapabilities(Convert.ToInt16(info["PowerManagementCapabilities"]));
                }
                catch
                {
                    mvarCpuPowerManagementCapabilities = "Not Present or Unknown";
                }
            }

            if (search != null)
            {
                search.Dispose();
            }
        }

        Collection<Int32> GetCpuLoadPercentage()
        {
            var query = new SelectQuery("Win32_Processor");
            var search = new ManagementObjectSearcher(query);
            var load = new Collection<Int32>();

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;
                try
                {
                    load.Add(Convert.ToInt32(info["LoadPercentage"]));
                }
                catch
                {
                    load.Add(0);
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return load;
        }

        static int GetCpuNumberOfProcessors()
        {
            return Convert.ToInt32(Environment.ProcessorCount);
        }

        int GetCpuNumberOfCores()
        {
            int number = 0;

            var query = new SelectQuery("Win32_Processor");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    number = Convert.ToInt32(info["NumberOfCores"]);
                }
                catch
                {
                    number = 0;
                }
            }

            return number;
        }

        int GetCpuNumberOfLogicalProcessors()
        {
            int number = 0;

            var query = new SelectQuery("Win32_Processor");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    number = Convert.ToInt32(info["NumberOfLogicalProcessors"]);
                }
                catch
                {
                    number = 0;
                }
            }

            return number;
        }

        #endregion

        #region " Get Hard Drive Information "

        void GetDriveInformation()
        {
            var query = new SelectQuery("Win32_DiskDrive");
            var search = new ManagementObjectSearcher(query);
            int count = 0;

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    mvarDriveCapacity.Add(FormatBytes(Convert.ToDouble
                                                        (Convert.ToUInt64(info["TotalSectors"])
                                                         * Convert.ToUInt32(info["BytesPerSector"]))));
                }
                catch
                {
                    mvarDriveCapacity.Add(MUnknown);
                }


                try
                {
                    mvarDriveInterface.Add(info["InterfaceType"].ToString());
                }
                catch
                {
                    mvarDriveInterface.Add(MUnknown);
                }


                try
                {
                    mvarDriveModelNo.Add(info["Model"].ToString());
                }
                catch
                {
                    mvarDriveModelNo.Add(MUnknown);
                }

                try
                {
                    mvarDriveStatus.Add(info["Status"].ToString());
                }
                catch
                {
                    mvarDriveStatus.Add(MUnknown);
                }

                count++;
            }

            if (search != null)
            {
                search.Dispose();
            }
        }

        #endregion

        #region " Get Mainboard Information "

        string GetMainBoardManufacturer()
        {
            string temp = "";

            var query = new SelectQuery("Win32_BaseBoard");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;
                try
                {
                    temp = info["Manufacturer"].ToString();
                }
                catch
                {
                    temp = "N/A";
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return temp;
        }

        string GetMainBoardModel()
        {
            string temp = "";

            var query = new SelectQuery("Win32_BaseBoard");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVarInfo in search.Get())
            {
                try
                {
                    temp = info["Product"].ToString();
                }
                catch
                {
                    temp = "N/A";
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return temp;
        }

        #endregion

        #region " Get NET Framework Information "

        static string GetFrameworkVersion()
        {
            return Environment.Version.ToString();
        }

        static int GetFrameworkMajorVersion()
        {
            return Environment.Version.Major;
        }

        static int GetFrameworkMinorVersion()
        {
            return Environment.Version.Minor;
        }

        static string GetFrameworkShortVersion()
        {
            return Environment.Version.ToString().Substring(0, 3);
        }

        static string GetFrameworkServicePack()
        {
            string strFrameworkMajorVersion = Environment.Version.Major.ToString();
            string strFrameworkMinorVersion = Environment.Version.Minor.ToString();
            string strFrameworkVersion = "v" + strFrameworkMajorVersion + "." +
                                         strFrameworkMinorVersion + "." + Environment.Version.Build.ToString();

            RegistryKey rk = null;
            string temp = "";

            try
            {
                // try each registry key to determine the version, build, and service pack
                if (strFrameworkMajorVersion.Trim() == "2" && strFrameworkMinorVersion.Trim() == "0")
                {
                    rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\" +
                                                          strFrameworkVersion);

                    temp = rk.GetValue("SP").ToString();

                    if (temp == "0")
                    {
                        temp = "";
                    }
                }
                else
                {
                    temp = "";
                }
            }
            catch
            {
                temp = "";
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }

            return temp;
        }

        #endregion

        #region " Get Network Adaptor Information "

        void GetNetAdaptorInfo()
        {
            var query = new SelectQuery("Win32_NetworkAdapter");
            var search = new ManagementObjectSearcher(query);
            int count = 0;

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    mvarNetAdapterType.Add(info["AdapterType"].ToString());
                }
                catch
                {
                    mvarNetAdapterType.Add("N/A");
                }

                try
                {
                    mvarNetAutoSense.Add(info["AutoSense"].ToString());
                }
                catch
                {
                    mvarNetAutoSense.Add("N/A");
                }

                try
                {
                    mvarNetMacAddress.Add(info["MACAddress"].ToString());
                }
                catch
                {
                    mvarNetMacAddress.Add("N/A");
                }

                try
                {
                    mvarNetManufacturer.Add(info["Manufacturer"].ToString());
                }
                catch
                {
                    mvarNetManufacturer.Add("N/A");
                }

                try
                {
                    mvarNetMaxSpeed.Add(info["MaxSpeed"].ToString());
                }
                catch
                {
                    mvarNetMaxSpeed.Add("N/A");
                }

                try
                {
                    mvarNetConnectionId.Add(info["NetConnectionID"].ToString());
                }
                catch
                {
                    mvarNetConnectionId.Add("N/A");
                }

                try
                {
                    if (GetOSMajorVersion() >= 5 && GetOSMajorVersion() >= 1)
                    {
                        mvarNetConnectionStatus.Add(ReturnNetConnectionStatus
                                                        (Convert.ToInt32(info["NetConnectionStatus"])));
                    }
                    else
                    {
                        mvarNetConnectionStatus.Add("N/A");
                    }
                }
                catch
                {
                    mvarNetConnectionStatus.Add("N/A");
                }

                try
                {
                    mvarNetProductName.Add(info["ProductName"].ToString());
                }
                catch
                {
                    mvarNetProductName.Add("N/A");
                }

                try
                {
                    mvarNetSpeed.Add(info["Speed"].ToString());
                }
                catch
                {
                    mvarNetSpeed.Add("N/A");
                }
                count++;
                mvarNetNumberOfAdaptors = count;
            }

            if (search != null)
            {
                search.Dispose();
            }
        }

        #endregion

        #region " Get Network Interface Information "

        void GetNetInterfaceInfo()
        {
            var query = new SelectQuery("Win32_NetworkAdapterConfiguration");
            var search = new ManagementObjectSearcher(query);
            int count = 0;
            string[] temp = null;

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    mvarNetDefaultTtl.Add(info["DefaultTTL"].ToString());
                }
                catch
                {
                    mvarNetDefaultTtl.Add("N/A");
                }

                try
                {
                    mvarNetDhcpEnabled.Add(Convert.ToBoolean(info["DHCPEnabled"]));
                }
                catch
                {
                    mvarNetDhcpEnabled.Add(false);
                }


                try
                {
                    mvarNetDhcpLeaseExpires.Add(info["DHCPLeaseExpires"].ToString().Substring(
                        0, 8).Insert(4, "-").Insert(7, "-"));
                }
                catch
                {
                    mvarNetDhcpLeaseExpires.Add("N/A");
                }

                try
                {
                    mvarNetDhcpLeaseObtained.Add(info["DHCPLeaseObtained"].ToString().Substring(
                        0, 8).Insert(4, "-").Insert(7, "-"));
                }
                catch
                {
                    mvarNetDhcpLeaseObtained.Add("N/A");
                }

                try
                {
                    mvarNetDhcpServer.Add(info["DHCPServer"].ToString());
                }
                catch
                {
                    mvarNetDhcpServer.Add("N/A");
                }

                try
                {
                    mvarNetHostName.Add(info["DNSHostName"].ToString());
                }
                catch
                {
                    mvarNetHostName.Add("N/A");
                }

                try
                {
                    mvarNetDomain.Add(info["DNSDomain"].ToString());
                }
                catch
                {
                    mvarNetDomain.Add("N/A");
                }

                try
                {
                    mvarNetIPEnabled.Add(Convert.ToBoolean(info["IPEnabled"]));
                }
                catch
                {
                    mvarNetIPEnabled.Add(false);
                }

                try
                {
                    temp = (string[])info["IPAddress"];
                    mvarNetIPAddress.Add(temp[0]);
                }
                catch
                {
                    mvarNetIPAddress.Add("N/A");
                }

                try
                {
                    mvarNetMtu.Add(FormatBytes(Convert.ToDouble(info["MTU"])));
                }
                catch
                {
                    mvarNetMtu.Add("N/A");
                }

                try
                {
                    mvarNetTcpNumConnections.Add(info["TCPNumConnections"].ToString());
                }
                catch
                {
                    mvarNetTcpNumConnections.Add("N/A");
                }

                try
                {
                    mvarNetTcpWindowSize.Add(FormatBytes(Convert.ToDouble(info["TcpWindowSize"])));
                }
                catch
                {
                    mvarNetTcpWindowSize.Add("N/A");
                }

                count++;
            }

            if (search != null)
            {
                search.Dispose();
            }
        }

        #endregion

        #region " Get Operating System Information "

        /// <summary>
        /// Gets OS domain name
        /// </summary>
        /// <returns></returns>
        string GetOSDomain()
        {
            string temp = "";

            var query = new SelectQuery("Win32_ComputerSystem");
            var search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = Convert.ToString(info["Domain"]);
                }
                catch
                {
                    temp = "";
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return temp;
        }

        /// <summary>
        /// Checks if there is a part of domain
        /// </summary>
        /// <returns>true - if yes, false - otherwise</returns>
        bool GetOSPartOfDomain()
        {
            bool temp = false;

            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                try
                {
                    temp = Convert.ToBoolean(info["PartOfDomain"]);
                }
                catch
                {
                    temp = false;
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return temp;
        }

        /// <summary>
        /// Gets OS install date
        /// </summary>
        /// <returns><cref>DateTime</cref>  object containing OS install date</returns>
        DateTime GetOSInstallDate()
        {
            // The install desiredDate/time is stored in the registry as the number of seconds since 01/01/1970 @ midnight.
            RegistryKey rk = null;
            DateTime installDate;
            Double secondsSince1970;

            try
            {
                rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                secondsSince1970 = Convert.ToDouble(rk.GetValue("InstallDate"));
                installDate = new DateTime(1970, 1, 1).AddSeconds(secondsSince1970);
            }
            catch
            {
                installDate = DateTime.Today;
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }

            return installDate;
        }

        /// <summary>
        /// Gets OS Bootup state
        /// </summary>
        /// <returns>string containing the state</returns>
        string GetOSBootupState()
        {
            string temp = string.Empty;

            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = Convert.ToString(info["BootupState"]);
                }
                catch
                {
                    temp = "";
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return temp;
        }

        /// <summary>
        /// Gets OS major version
        /// </summary>
        /// <returns></returns>
        static int GetOSMajorVersion()
        {
            return Environment.OSVersion.Version.Major;
        }

        /// <summary>
        /// Gets OS minor version
        /// </summary>
        /// <returns></returns>
        static int GetOSMinorVersion()
        {
            return Environment.OSVersion.Version.Minor;
        }

        static string GetOSShortVersion()
        {
            return Environment.OSVersion.Version.Major.ToString() + "." +
                   Environment.OSVersion.Version.Minor.ToString();
        }

        /// <summary>
        /// Gets OS Code Names
        /// </summary>
        /// <returns></returns>
        static string GetOSCodeName()
        {
            string retValue = string.Empty;
            int intMinorVersion;
            int intMajorVersion;

            intMajorVersion = Environment.OSVersion.Version.Major;
            intMinorVersion = Environment.OSVersion.Version.Minor;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32Windows:

                    switch (intMinorVersion)
                    {
                        case 0:
                            if (!String.IsNullOrEmpty(MRevision))
                            {
                                retValue = mstrChicago;
                            }
                            else
                            {
                                retValue = mstrDetroit;
                            }
                            break;
                        case 10:
                            retValue = mstrMemphis;
                            break;
                        case 90:
                            retValue = mstrGeorgia;
                            break;
                        default:
                            retValue = MUnknown;
                            break;
                    }

                    break;

                case PlatformID.Win32NT:

                    // get information for Windows NT SP6 and above
                    if (intMajorVersion == 4 && intMinorVersion == 0)
                    {
                        // Windows NT
                        retValue = mstrCairo;
                    }
                    else if (intMajorVersion == 5 && intMinorVersion == 0)
                    {
                        // Windows 2000
                        retValue = mstrCairoNT5;
                    }
                    else if (intMajorVersion == 5 && intMinorVersion == 1)
                    {
                        // Windows XP
                        retValue = mstrWhistler;
                    }
                    else if (intMajorVersion == 5 && intMinorVersion == 2)
                    {
                        // Windows Server 2003
                        retValue = mstrWhistlerServer;
                    }
                    else if (intMajorVersion == 6)
                    {
                        // Windows Vista
                        retValue = mstrLonghorn;
                    }
                    else
                    {
                        retValue = MUnknown;
                    }

                    break;

                default:
                    retValue = MUnknown;
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// Gets OS Service Pack information
        /// </summary>
        /// <returns></returns>
        static string GetOSServicePack()
        {
            return Environment.OSVersion.ServicePack;
        }

        /// <summary>
        /// Gets OS Build
        /// </summary>
        /// <returns></returns>
        static string GetOSBuild()
        {
            return Environment.OSVersion.Version.Build.ToString();
        }

        /// <summary>
        /// Gets OS full name
        /// </summary>
        /// <returns></returns>
        string GetOSFullName()
        {
            string temp = "";

            SelectQuery query = new SelectQuery("Win32_OperatingSystem");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    temp = Convert.ToString(info["Caption"]);
                }
                catch
                {
                    temp = "";
                }
            }

            if (search != null)
            {
                search.Dispose();
            }

            return temp;
        }

        /// <summary>
        /// Gets OS version
        /// </summary>
        /// <returns></returns>
        static string GetOSVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }

        /// <summary>
        /// Gets OS platform
        /// </summary>
        /// <returns></returns>
        static PlatformID GetOSPlatform()
        {
            return Environment.OSVersion.Platform;
        }

        /// <summary>
        /// Gets OS user name
        /// </summary>
        /// <returns></returns>
        static string GetOSUserName()
        {
            return Environment.UserName;
        }

        /// <summary>
        /// Gets OS machine name
        /// </summary>
        /// <returns></returns>
        static string GetOSMachineName()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// Gets OS product ID
        /// </summary>
        /// <returns></returns>
        static string GetOSProductID()
        {
            string result = string.Empty;
            RegistryKey rk = null;

            try
            {
                rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                result = rk.GetValue("ProductID").ToString();
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Gets OS type
        /// </summary>
        /// <returns></returns>
        static string GetOSType()
        {
            string result = string.Empty;
            RegistryKey rk = null;
            try
            {
                rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                result = rk.GetValue("CurrentType", "Not Found").ToString();
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }
            return result;
        }

        #endregion

        #region " Get OS Product Key "

        /// <summary>
        /// Read the value of:
        /// HKLM\SOFTWARE\MICROSOFT\Windows NT\CurrentVersion\DigitalProductId
        /// and decode the Windows CD Key.
        /// </summary>
        /// <returns>
        /// Returns the Windows CD Key if successful.
        /// Returns "Unknown" upon failure.
        /// </returns>
        static string GetOSProductKey()
        {
            string result = MUnknown;
            try
            {
                // Open the Registry Key and then get the value (byte array) from the SubKey.
                using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", false))
                {
                    byte[] digitalPid = (byte[])regKey.GetValue("DigitalProductID");

                    if (digitalPid != null)
                    {
                        // Transfer only the needed bytes into our Key Array.
                        // Key starts at byte 52 and is 15 bytes long.
                        byte[] key = new byte[15]; //0-14 = 15 bytes
                        Array.Copy(digitalPid, 52, key, 0, 15);

                        // Our "Array" of valid CD-Key characters.
                        string characters = "BCDFGHJKMPQRTVWXY2346789";

                        // Finally, our decoded CD-Key to be returned
                        string productKey = "";

                        // How Microsoft encodes this to begin with, I'd love to know...
                        // but here's how we decode the byte array into a string containing the CD-KEY.
                        for (int j = 0; j <= 24; j++)
                        {
                            short curValue = 0;
                            for (int i = 14; i >= 0; i--)
                            {
                                curValue = Convert.ToInt16(curValue * 256 ^ key[i]);
                                key[i] = Convert.ToByte(Convert.ToInt32(curValue / 24));
                                curValue = Convert.ToInt16(curValue % 24);
                            }
                            productKey = characters.Substring(curValue, 1) + productKey;
                        }

                        // Finally, insert the dashes into the string.
                        for (int i = 4; i >= 1; i--)
                        {
                            productKey = productKey.Insert(i * 5, "-");
                        }

                        result = productKey;
                    }
                }
            }
            catch
            {
            }

            return result;
        }

        #endregion

        #region " Get OS Up Time "

        /// <summary>
        /// Gets OS up time
        /// </summary>
        /// <returns></returns>
        static string GetOSUptime()
        {
            string result = string.Empty;

            int intDays;
            int intHours;
            int intMinutes;
            int intSeconds;
            int intRemainder;
            int intTicks;
            string strDays;
            string strHours;
            string strMinutes;
            string strSeconds;

            // initialize string variables
            strDays = "";
            strHours = "";
            strMinutes = "";
            strSeconds = "";

            try
            {
                // updates tick counter intTicks
                intTicks = Environment.TickCount;

                // there are  86400000 milliseconds in one day, compute whole days and get remainder
                do
                {
                    intDays = Convert.ToInt32(intTicks / 86400000);
                    intRemainder = intTicks % 86400000;
                } while (!(intRemainder <= 86400000));

                // there are 3600000 milliseconds in one hour, compute whole hours and get remainder
                do
                {
                    intHours = Convert.ToInt32(intRemainder / 3600000);
                    intRemainder = intRemainder % 3600000;
                } while (!(intRemainder <= 3600000));

                // there are 60000 milliseconds in one minute, compute whole minutes and get remainder
                do
                {
                    intMinutes = Convert.ToInt32(intRemainder / 60000);
                    intRemainder = intRemainder % 60000;
                } while (!(intRemainder <= 60000));

                // there are 1000 milliseconds in one second, compute whole seconds and get remainder
                do
                {
                    intSeconds = Convert.ToInt32(intRemainder / 1000);
                    intRemainder = intRemainder % 1000;
                } while (!(intRemainder <= 1000));

                // format days
                if (intDays == 0)
                {
                    strDays = "";
                }
                else if (intDays.ToString().Trim().Length == 1)
                {
                    strDays = " " + intDays.ToString().Trim() + ":";
                }
                else if (intDays.ToString().Trim().Length == 2)
                {
                    strDays = intDays.ToString().Trim() + ":";
                }

                // format hours
                if (intHours == 0 && intDays == 0)
                {
                    strHours = "";
                }
                else if (intHours.ToString().Trim().Length == 1)
                {
                    strHours = "0" + intHours.ToString().Trim() + ":";
                }
                else if (intHours.ToString().Trim().Length == 2)
                {
                    strHours = intHours.ToString().Trim() + ":";
                }

                // format minutes
                if (intMinutes == 0)
                {
                    strMinutes = "00" + ":";
                }
                else if (intMinutes.ToString().Trim().Length == 1)
                {
                    strMinutes = "0" + intMinutes.ToString().Trim() + ":";
                }
                else if (intMinutes.ToString().Trim().Length == 2)
                {
                    strMinutes = intMinutes.ToString().Trim() + ":";
                }

                // format seconds
                if (intSeconds == 0)
                {
                    strSeconds = "00";
                }
                else if (intSeconds.ToString().Trim().Length == 1)
                {
                    strSeconds = "0" + intSeconds.ToString().Trim();
                }
                else if (intSeconds.ToString().Trim().Length == 2)
                {
                    strSeconds = intSeconds.ToString().Trim();
                }

                //time string
                result = strDays + strHours + strMinutes + strSeconds;
            }
            catch
            {
            }

            return result;
        }

        #endregion

        #region " Get Service Information "

        /// <summary>
        /// Gets service information
        /// </summary>
        void GetServiceInfo()
        {
            SelectQuery query = new SelectQuery("Win32_Service");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);

            foreach (ManagementObject sinfo in search.Get())
            {
                try
                {
                    mvarServiceDisplayName.Add(sinfo["DisplayName"].ToString());
                }
                catch
                {
                    mvarServiceDisplayName.Add(MUnknown);
                }

                try
                {
                    mvarServiceDescription.Add(sinfo["Description"].ToString());
                }
                catch
                {
                    mvarServiceDescription.Add(MUnknown);
                }

                try
                {
                    mvarServiceStartMode.Add(sinfo["StartMode"].ToString());
                }
                catch
                {
                    mvarServiceStartMode.Add(MUnknown);
                }

                try
                {
                    mvarServiceState.Add(sinfo["State"].ToString());
                }
                catch
                {
                    mvarServiceState.Add(MUnknown);
                }

                try
                {
                    mvarServiceStatus.Add(sinfo["Status"].ToString());
                }
                catch
                {
                    mvarServiceStatus.Add(MUnknown);
                }

                try
                {
                    mvarServicePathName.Add(sinfo["PathName"].ToString());
                }
                catch
                {
                    mvarServicePathName.Add(MUnknown);
                }
            }

            if (search != null)
            {
                search.Dispose();
            }
        }

        #endregion

        #region " Get Sound Controller Information "

        /// <summary>
        /// Gets sound information
        /// </summary>
        void GetSoundInfo()
        {
            SelectQuery query = new SelectQuery("Win32_SoundDevice");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);
            int count = 0;

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    try
                    {
                        mvarSndController.Add(info["Name"].ToString());
                    }
                    catch
                    {
                        mvarSndController.Add(MUnknown);
                    }

                    try
                    {
                        mvarSndManufacturer.Add(info["Manufacturer"].ToString());
                    }
                    catch
                    {
                        mvarSndManufacturer.Add(MUnknown);
                    }

                    try
                    {
                        mvarSndDMABufferSize.Add(info["DMABufferSize"].ToString());
                    }
                    catch
                    {
                        mvarSndDMABufferSize.Add(MUnknown);
                    }

                    count++;
                    mvarSndNumberOfControllers = count;
                }
                catch
                {
                    mvarSndNumberOfControllers = 0;
                    mvarSndController.Add("");
                    mvarSndDMABufferSize.Add("");
                    mvarSndManufacturer.Add("");
                }
            }

            if (search != null)
            {
                search.Dispose();
            }
        }

        #endregion

        #region " Get Time Information "

        /// <summary>
        /// Gets current time zone
        /// </summary>
        /// <returns></returns>
        static string GetCurrentTimeZone()
        {
            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now.Date))
            {
                return TimeZone.CurrentTimeZone.DaylightName;
            }
            return TimeZone.CurrentTimeZone.StandardName;
        }

        /// <summary>
        /// Checks if the daylight saving time regime is applied
        /// </summary>
        /// <returns>true - if yes, false - otherwise</returns>
        static bool GetDaylightSavingsInEffect()
        {
            return TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now.Date);
        }

        /// <summary>
        /// Gets daylight name
        /// </summary>
        /// <returns></returns>
        static string GetDaylightSavingsName()
        {
            return TimeZone.CurrentTimeZone.DaylightName;
        }

        /// <summary>
        /// Gets daylight savings offset
        /// </summary>
        /// <returns></returns>
        static int GetDaylightSavingsOffset()
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year);
            return Convert.ToInt32(Daylight.Delta.TotalHours);
        }

        /// <summary>
        /// Gets daylight end date
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetLocalDaylightEndDate(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.End.ToLocalTime();
        }

        /// <summary>
        /// Gets local daylight end time
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetLocalDaylightEndTime(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.End.ToLocalTime();
        }

        /// <summary>
        /// Gets local daylight start date
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetLocalDaylightStartDate(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);

            return Daylight.Start.ToLocalTime();
        }

        /// <summary>
        /// Gets local daylight start time
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetLocalDaylightStartTime(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.Start.ToLocalTime();
        }

        /// <summary>
        /// Gets local date time
        /// </summary>
        /// <returns></returns>
        static DateTime GetLocalDateTime()
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
        }

        /// <summary>
        /// Gets universal date time
        /// </summary>
        /// <returns></returns>
        static DateTime GetUniversalDateTime()
        {
            return TimeZone.CurrentTimeZone.ToUniversalTime(DateTime.Now);
        }

        /// <summary>
        /// Gets unversal daylight end date
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetUniversalDaylightEndDate(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.End.ToUniversalTime();
        }

        /// <summary>
        /// Gets universal daylight end time
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetUniversalDaylightEndTime(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.End.ToUniversalTime();
        }

        /// <summary>
        /// Gets universal daylight start date
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetUniversalDaylightStartDate(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.Start.ToUniversalTime();
        }

        /// <summary>
        /// Gets universal daylight start time
        /// </summary>
        /// <param name="dteDate"></param>
        /// <returns></returns>
        static DateTime GetUniversalDaylightStartTime(DateTime dteDate)
        {
            // Get the DaylightTime object for the current year.
            DaylightTime Daylight = TimeZone.CurrentTimeZone.GetDaylightChanges(dteDate.Year);
            return Daylight.Start.ToUniversalTime();
        }

        /// <summary>
        /// Gets standart time name
        /// </summary>
        /// <returns></returns>
        static string GetStandardTimeName()
        {
            return TimeZone.CurrentTimeZone.StandardName;
        }

        /// <summary>
        /// Gets universal time offset
        /// </summary>
        /// <returns></returns>
        static int GetUniversalTimeOffset()
        {
            return Convert.ToInt32(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now.Date).TotalHours);
        }

        #endregion

        #region " Get/Set User Information "

        /// <summary>
        /// Get user register organiztion
        /// </summary>
        /// <returns></returns>
        static string GetUserRegisteredOrganization()
        {
            string retValue = MUnknown;
            RegistryKey rk = null;

            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", false);
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", false);
                }

                if (rk != null)
                {
                    retValue = rk.GetValue("RegisteredOrganization").ToString();
                }
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
            return retValue;
        }

        /// <summary>
        /// Set registered organization
        /// </summary>
        /// <param name="organization"></param>
        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        static void SetUserRegisteredOrganization(string organization)
        {
            RegistryKey rk = null;
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", true);
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", true);
                }

                if (rk != null)
                {
                    rk.SetValue("RegisteredOrganization", organization);
                }
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
        }

        /// <summary>
        /// Get registered name
        /// </summary>
        /// <returns></returns>
        static string GetUserRegisteredName()
        {
            string retValue = MUnknown;
            RegistryKey rk = null;

            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", false);
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", false);
                }

                if (rk != null)
                {
                    retValue = rk.GetValue("RegisteredOwner").ToString();
                }
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
            return retValue;
        }

        /// <summary>
        /// Set user registered name
        /// </summary>
        /// <param name="name"></param>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        static void SetUserRegisteredName(string name)
        {
            RegistryKey rk = null;

            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", true);
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", true);
                }

                if (rk != null)
                {
                    rk.SetValue("RegisteredOwner", name);
                }
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
        }

        /// <summary>
        /// Checks if user is adiministrator
        /// </summary>
        /// <returns>true - if user is administrator, false - otherwise</returns>
        static bool IsUserAdministrator()
        {
            bool result = false;
            try
            {
                WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                result = wp.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Gets user full names
        /// </summary>
        /// <returns>collection of user full names</returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        static Collection<string> GetUserFullNames()
        {
            var users = new Collection<string>();
            var fullNames = new Collection<string>();

            // Get list of users.
            users = NativeMethods.EnumerateUsers();

            // Get full names
            foreach (string user in users)
            {
                fullNames.Add(NativeMethods.GetUserFullName(user));
            }

            return fullNames;
        }

        /// <summary>
        /// Gets user privileges
        /// </summary>
        /// <returns>collection of user privileges</returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        static Collection<string> GetUserPrivileges()
        {
            var users = new Collection<string>();
            var privileges = new Collection<string>();

            // Get list of users.
            users = NativeMethods.EnumerateUsers();

            // Get full names
            foreach (string user in users)
            {
                privileges.Add(NativeMethods.GetUserPrivledge(user));
            }

            return privileges;
        }

        /// <summary>
        /// Gets user flags
        /// </summary>
        /// <returns>collection of user flags</returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        static Collection<int> GetUserFlags()
        {
            var users = new Collection<string>();
            var flags = new Collection<int>();

            // Get list of users.
            users = NativeMethods.EnumerateUsers();

            // Get flags.
            foreach (string user in users)
            {
                flags.Add(NativeMethods.GetUserFlags(user));
            }

            return flags;
        }

        #endregion

        #region " Get Video Information "
        /// <summary>
        /// Gets video information
        /// </summary>
        void GetVideoInfo()
        {
            SelectQuery query = new SelectQuery("Win32_VideoController");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);
            int count = 0;

            foreach (ManagementObject tempLoopVar_info in search.Get())
            {
                info = tempLoopVar_info;

                try
                {
                    try
                    {
                        mvarVidController.Add(info["Name"].ToString());
                    }
                    catch
                    {
                        mvarVidController.Add(MUnknown);
                    }

                    try
                    {
                        if (Convert.ToInt32(info["AdapterRAM"]) == 0)
                        {
                            mvarVidRam.Add(MUnknown);
                        }
                        else
                        {
                            mvarVidRam.Add(FormatBytes(Convert.ToDouble(info["AdapterRAM"])));
                        }
                    }
                    catch
                    {
                        mvarVidRam.Add(MUnknown);
                    }

                    try
                    {
                        if (Convert.ToUInt32(info["CurrentRefreshRate"]) == 0)
                        {
                            mvarVidRefreshRate.Add("Default");
                        }
                        else if (Convert.ToUInt32(info["CurrentRefreshRate"]) == 0xFFFFFFFF)
                        {
                            mvarVidRefreshRate.Add("Optimal");
                        }
                        else
                        {
                            mvarVidRefreshRate.Add(info["CurrentRefreshRate"] + " Hz");
                        }
                    }
                    catch
                    {
                        mvarVidRefreshRate.Add(MUnknown);
                    }

                    try
                    {
                        mvarVidScreenColors.Add(ReturnColors(Convert.ToInt32(info["CurrentBitsPerPixel"])));
                    }
                    catch
                    {
                        mvarVidScreenColors.Add(MUnknown);
                    }

                    count++;
                    mvarVidNumberOfControllers = count;
                }
                catch
                {
                    mvarVidNumberOfControllers = 0;
                }
            }
            if (search != null)
            {
                search.Dispose();
            }
        }


        static string GetVideoPrimaryScreenDimensions()
        {
            return Screen.PrimaryScreen.Bounds.Width.ToString() + " x " +
                   Screen.PrimaryScreen.Bounds.Height.ToString();
        }

        static string GetVideoPrimaryScreenWorkingArea()
        {
            return Screen.PrimaryScreen.WorkingArea.Width.ToString() + " x " +
                   Screen.PrimaryScreen.WorkingArea.Height.ToString();
        }

        #endregion

        #region " Get Visual Style Information "

        static string GetVstAuthor()
        {
            return VisualStyleInformation.Author;
        }

        static string GetVstColorScheme()
        {
            return VisualStyleInformation.ColorScheme;
        }

        static string GetVstCompany()
        {
            return VisualStyleInformation.Company;
        }

        static Color GetVstControlHighlightHot()
        {
            return VisualStyleInformation.ControlHighlightHot;
        }

        static string GetVstCopyright()
        {
            return VisualStyleInformation.Copyright;
        }

        static string GetVstDescription()
        {
            return VisualStyleInformation.Description;
        }

        static string GetVstDisplayName()
        {
            return VisualStyleInformation.DisplayName;
        }

        static bool GetVstIsEnabledByUser()
        {
            return VisualStyleInformation.IsEnabledByUser;
        }

        static bool GetVstIsSupportedByOS()
        {
            return VisualStyleInformation.IsSupportedByOS;
        }

        static int GetVstMinimumColorDepth()
        {
            return VisualStyleInformation.MinimumColorDepth;
        }

        static string GetVstSize()
        {
            return VisualStyleInformation.Size;
        }

        static bool GetVstSupportsFlatMenus()
        {
            return VisualStyleInformation.SupportsFlatMenus;
        }

        static Color GetVstTextControlBorder()
        {
            return VisualStyleInformation.TextControlBorder;
        }

        static string GetVstUrl()
        {
            return VisualStyleInformation.Url;
        }

        static string GetVstVersion()
        {
            return VisualStyleInformation.Version;
        }

        #endregion

        #region " Get Volume Information "

        /// <summary>
        /// Gets volume information
        /// </summary>
        void GetVolumeInfo()
        {
            DriveInfo[] drives = null;

            try
            {
                drives = DriveInfo.GetDrives();
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }

            if (drives != null)
            {
                foreach (DriveInfo drive in drives)
                {
                    mvarVolumeLetter.Add(drive.RootDirectory.ToString());
                    mvarVolumeType.Add(drive.DriveType.ToString());
                    try
                    {
                        if (drive.IsReady)
                        {
                            mvarVolumeFileSystem.Add(drive.DriveFormat);
                            mvarVolumeLabel.Add(drive.VolumeLabel);
                            mvarVolumeTotalSize.Add(FormatBytes(Convert.ToDouble(drive.TotalSize)));
                            mvarVolumeFreeSpace.Add(FormatBytes(Convert.ToDouble(drive.TotalFreeSpace)));
                            mvarVolumeUsedSpace.Add(FormatBytes(Convert.ToDouble(drive.TotalSize - drive.TotalFreeSpace)));
                            mvarVolumePercentFreeSpace.Add(String.Format("{0:N1}",
                                                                            (Convert.ToDouble(drive.TotalFreeSpace) / Convert.ToDouble(drive.TotalSize) *
                                                                            100.0)) + "%");
                            mvarVolumeSerialNumber.Add(NativeMethods.
                                GetVolumeSerialNumber(drive.RootDirectory.ToString()));
                        }
                        else
                        {
                            mvarVolumeFileSystem.Add(String.Empty);
                            mvarVolumeLabel.Add(String.Empty);
                            mvarVolumeTotalSize.Add(String.Empty);
                            mvarVolumeFreeSpace.Add(String.Empty);
                            mvarVolumeUsedSpace.Add(String.Empty);
                            mvarVolumePercentFreeSpace.Add("0%");
                            mvarVolumeSerialNumber.Add(String.Empty);
                        }

                        mvarVolumeReady.Add(drive.IsReady);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion

        #endregion

        #region " Public Methods "

        /// <param name="inDate">HolidayDate for which the week number is desired.</param>
        /// <returns>
        /// Return the week number as an integer.
        /// </returns>
        /// <summary>
        /// Description:this function will accept any desiredDate as the only parameter and will 
        /// return you the week number the supplied desiredDate lies into.
        /// </summary>>
        public int GetWeekNumber(DateTime inDate)
        {
            int dayOfYear;
            int weekNumber;
            int compensation = 0;
            DateTime firstDayDate;

            try
            {
                dayOfYear = inDate.DayOfYear;
                firstDayDate = new DateTime(inDate.Year, inDate.Month, 1);

                switch (firstDayDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        compensation = 0;
                        break;
                    case DayOfWeek.Monday:
                        compensation = 6;
                        break;
                    case DayOfWeek.Tuesday:
                        compensation = 5;
                        break;
                    case DayOfWeek.Wednesday:
                        compensation = 4;
                        break;
                    case DayOfWeek.Thursday:
                        compensation = 3;
                        break;
                    case DayOfWeek.Friday:
                        compensation = 2;
                        break;
                    case DayOfWeek.Saturday:
                        compensation = 1;
                        break;
                }

                dayOfYear = dayOfYear - compensation;

                if (dayOfYear % 7 == 0)
                {
                    weekNumber = Convert.ToInt16(dayOfYear / 7);
                }
                else
                {
                    weekNumber = (dayOfYear / 7) + 1;
                }
            }
            catch
            {
                weekNumber = 0; // Trap errors by returning a zero.
            }

            return weekNumber;
        }

        #endregion

        #region " Public Properties "

        #region " Application Public Properties "

        /// <summary>
        /// "Application Build"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Build")]
        public string AppBuild
        {
            get
            {
                mvarAppBuild = GetAppBuild();
                return mvarAppBuild;
            }
        }

        /// <summary>
        /// "Application Company (Manufacturer)"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Company (Manufacturer)")]
        public string AppCompanyName
        {
            get
            {
                mvarAppCompanyName = GetAppCompanyName();
                return mvarAppCompanyName;
            }
        }

        /// <summary>
        /// "Application Copyright"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Copyright")]
        public string AppCopyright
        {
            get
            {
                mvarAppCopyright = GetAppCopyright();
                return mvarAppCopyright;
            }
        }

        /// <summary>
        /// "Application Copyright"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Copyright")]
        public string AppDescription
        {
            get
            {
                mvarAppDescription = GetAppDescription();
                return mvarAppDescription;
            }
        }

        /// <summary>
        /// "Application Major Revision"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Major Revision")]
        public int AppMajorRevision
        {
            get
            {
                mvarAppMajorRevision = GetAppMajorRevision();
                return mvarAppMajorRevision;
            }
        }

        /// <summary>
        /// "Application Major Version"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Major Version")]
        public int AppMajorVersion
        {
            get
            {
                mvarAppMajorVersion = GetAppMajorVersion();
                return mvarAppMajorVersion;
            }
        }

        /// <summary>
        /// "Application Minor Revision"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Minor Revision")]
        public int AppMinorRevision
        {
            get
            {
                mvarAppMinorRevision = GetAppMinorRevision();
                return mvarAppMinorRevision;
            }
        }

        /// <summary>
        /// "Application Minor Version"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Minor Version")]
        public int AppMinorVersion
        {
            get
            {
                mvarAppMinorVersion = GetAppMinorVersion();
                return mvarAppMinorVersion;
            }
        }

        /// <summary>
        /// "Application Product HolidayName"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Product Name")]
        public string AppProductName
        {
            get
            {
                mvarAppProductName = GetAppProductName();
                return mvarAppProductName;
            }
        }

        /// <summary>
        /// "Application Revision"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Revision")]
        public string AppRevision
        {
            get
            {
                mvarAppRevision = GetAppRevision();
                return mvarAppRevision;
            }
        }

        /// <summary>
        /// "Application Major and Minor Version Separated by a Decimal"
        /// </summary>
        [Browsable(true), Category("Application"),
         Description("Application Major and Minor Version Separated by a Decimal")]
        public string AppShortVersion
        {
            get
            {
                mvarAppShortVersion = GetAppShortVersion();
                return mvarAppShortVersion;
            }
        }

        /// <summary>
        /// "Application Title"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Title")]
        public string AppTitle
        {
            get
            {
                mvarAppTitle = GetAppTitle();
                return mvarAppTitle;
            }
        }

        /// <summary>
        /// "Application Trademark"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Trademark")]
        public string AppTrademark
        {
            get
            {
                mvarAppTrademark = GetAppTrademark();
                return mvarAppTrademark;
            }
        }

        /// <summary>
        /// "Application Version"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Version")]
        public string AppVersion
        {
            get
            {
                mvarAppVersion = GetAppVersion();
                return mvarAppVersion;
            }
        }

        /// <summary>
        /// "Application Version"
        /// </summary>
        [Browsable(true), Category("Application"), Description("Application Directory")]
        public string AppDirectory
        {
            get
            {
                mvarAppDirectory = GetAppDirectory();
                return mvarAppDirectory;
            }
        }

        #endregion

        #region " BIOS Public Properties "

        /// <summary>
        /// "BIOS Manufacturer"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("BIOS Manufacturer")]
        public string BiosManufacturer
        {
            get { return mvarBiosManufacturer; }
        }

        /// <summary>
        /// "BIOS HolidayName"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("BIOS Name")]
        public string BiosName
        {
            get { return mvarBiosName; }
        }

        /// <summary>
        /// "BIOS Version"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("BIOS Version")]
        public string BiosVersion
        {
            get { return mvarBiosVersion; }
        }

        /// <summary>
        /// "BIOS Release HolidayDate"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("BIOS Release Date")]
        public string BiosReleaseDate
        {
            get { return mvarBiosReleaseDate; }
        }

        /// <summary>
        /// "BIOS Features"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("BIOS Features")]
        public Collection<string> BiosFeatures
        {
            get { return mvarBiosFeatures; }
        }

        /// <summary>
        /// "SMBIOS Present"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("SMBIOS Present")]
        public bool BiosSMBiosPresent
        {
            get { return mvarBiosSmBiosPresent; }
        }

        /// <summary>
        /// "SMBIOS Version"
        /// </summary>
        [Browsable(true), Category("BIOS"), Description("SMBIOS Version")]
        public string BiosSMBiosVersion
        {
            get { return mvarBiosSmBiosVersion; }
        }

        #endregion

        #region " CD Drive Public Properties "

        /// <summary>
        /// "CD Drive"
        /// </summary>
        [Browsable(true), Category("CD Drive"), Description("CD Drive")]
        public Collection<string> CDDrive
        {
            get
            {
                mvarCDDrive = GetCDDrive();
                return mvarCDDrive;
            }
        }

        /// <summary>
        /// "CD Drive Manufacturer"
        /// </summary>
        [Browsable(true), Category("CD Drive"), Description("CD Drive Manufacturer")]
        public Collection<string> CDManufacturer
        {
            get
            {
                mvarCDManufacturer = GetCDManufacturer();
                return mvarCDManufacturer;
            }
        }

        /// <summary>
        /// "CD Drive Model"
        /// </summary>
        [Browsable(true), Category("CD Drive"), Description("CD Drive Model")]
        public Collection<string> CDModel
        {
            get
            {
                mvarCDModel = GetCDModel();
                return mvarCDModel;
            }
        }

        /// <summary>
        /// "CD Drive Media Size"
        /// </summary>
        [Browsable(true), Category("CD Drive"), Description("CD Drive Media Size")]
        public Collection<string> CDMediaSize
        {
            get
            {
                mvarCDMediaSize = GetCDMediaSize();
                return mvarCDMediaSize;
            }
        }

        /// <summary>
        /// "CD Drive Revision Level"
        /// </summary>
        [Browsable(true), Category("CD Drive"), Description("CD Drive Revision Level")]
        public Collection<string> CDRevisionLevel
        {
            get
            {
                mvarCDRevisionLevel = GetCDRevisionLevel();
                return mvarCDRevisionLevel;
            }
        }

        /// <summary>
        /// "CD Drive Status"
        /// </summary>
        [Browsable(true), Category("CD Drive"), Description("CD Drive Status")]
        public Collection<string> CDStatus
        {
            get
            {
                mvarCDStatus = GetCDStatus();
                return mvarCDStatus;
            }
        }

        #endregion

        #region " Computer Public Properties "

        /// <summary>
        /// "Computer Has Automatic Reset Capability"
        /// </summary>
        [Browsable(true), Category("Computer"), Description("Computer Has Automatic Reset Capability")]
        public bool CompAutomaticResetCapability
        {
            get
            {
                mvarCompAutomaticResetCapability = GetCompAutomaticResetCapability();
                return mvarCompAutomaticResetCapability;
            }
        }

        /// <summary>
        /// "Computer Description"
        /// </summary>
        [Browsable(true), Category("Computer"), Description("Computer Description")]
        public string CompDescription
        {
            get
            {
                mvarCompDescription = GetCompDescription();
                return mvarCompDescription;
            }
        }

        /// <summary>
        /// "Computer Manufacturer"
        /// </summary>
        [Browsable(true), Category("Computer"), Description("Computer Manufacturer")]
        public string CompManufacturer
        {
            get
            {
                mvarCompManufacturer = GetCompManufacturer();
                return mvarCompManufacturer;
            }
        }

        /// <summary>
        /// "Computer Model"
        /// </summary>
        [Browsable(true), Category("Computer"), Description("Computer Model")]
        public string CompModel
        {
            get
            {
                mvarCompModel = GetCompModel();
                return mvarCompModel;
            }
        }

        /// <summary>
        /// "Computer System Type"
        /// </summary>
        [Browsable(true), Category("Computer"), Description("Computer System Type")]
        public string CompSystemType
        {
            get
            {
                mvarCompSystemType = GetCompSystemType();
                return mvarCompSystemType;
            }
        }

        #endregion

        #region " CPU Public Properties "

        /// <summary>
        /// "Number of Processors"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("Number of Processors")]
        public int CpuNumberOfProcessors
        {
            get
            {
                mvarCpuNumberOfProcessors = GetCpuNumberOfProcessors();
                return mvarCpuNumberOfProcessors;
            }
        }

        /// <summary>
        /// "Number of Cores"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("Number of Cores")]
        public int CpuNumberOfCores
        {
            get
            {
                mvarCpuNumberOfCores = GetCpuNumberOfCores();
                return mvarCpuNumberOfCores;
            }
        }

        /// <summary>
        /// "Number of Logical Processors"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("Number of Logical Processors")]
        public int CpuNumberOfLogicalProcessors
        {
            get
            {
                mvarCpuNumberOfLogicalProcessors = GetCpuNumberOfLogicalProcessors();
                return mvarCpuNumberOfLogicalProcessors;
            }
        }

        /// <summary>
        /// "CPU Address Width"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Address Width")]
        public string CpuAddressWidth
        {
            get { return mvarCpuAddressWidth; }
        }

        /// <summary>
        /// "CPU Description"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Description")]
        public string CpuDescription
        {
            get { return mvarCpuDescription; }
        }

        /// <summary>
        /// "CPU Front Side Bus (FSB) Speed"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Front Side Bus (FSB) Speed")]
        public string CpuFsbSpeed
        {
            get { return mvarCpuFsbSpeed; }
        }

        /// <summary>
        /// "CPU Level 2 Cache Size"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Level 2 Cache Size")]
        public string CpuL2CacheSize
        {
            get { return mvarCpuL2CacheSize; }
        }

        /// <summary>
        /// "CPU Level 2 Cache Speed"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Level 2 Cache Speed")]
        public string CpuL2CacheSpeed
        {
            get { return mvarCpuL2CacheSpeed; }
        }

        /// <summary>
        /// "CPU Load Percentage"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Load Percentage")]
        public Collection<Int32> CpuLoadPercentage
        {
            get
            {
                mvarCpuLoadPercentage = GetCpuLoadPercentage();
                return mvarCpuLoadPercentage;
            }
        }

        /// <summary>
        /// "CPU Manufacturer"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Manufacturer")]
        public string CpuManufacturer
        {
            get { return mvarCpuManufacturer; }
        }

        /// <summary>
        /// "CPU HolidayName"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Name")]
        public string CpuName
        {
            get { return mvarCpuName; }
        }

        /// <summary>
        /// "CPU Socket"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Socket")]
        public string CpuSocket
        {
            get { return mvarCpuSocket; }
        }

        /// <summary>
        /// "CPU Speed"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Speed")]
        public string CpuSpeed
        {
            get { return mvarCpuSpeed; }
        }

        /// <summary>
        /// "CPU Power Management Supported"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Power Management Supported")]
        public bool CpuPowerManagementSupported
        {
            get { return mvarCpuPowerManagementSupported; }
        }

        /// <summary>
        /// "CPU Power Management Capabilities"
        /// </summary>
        [Browsable(true), Category("CPU"), Description("CPU Power Management Capabilities")]
        public string CpuPowerManagementCapabilities
        {
            get { return mvarCpuPowerManagementCapabilities; }
        }

        [Browsable(true), Category("CPU"), Description("CPU Processor ID")]
        public string CpuProcessorId
        {
            get { return mvarCpuProcessorId; }
        }

        #endregion

        #region " Drive Public Properties "

        /// <summary>
        /// "Drive Capacity"
        /// </summary>
        [Browsable(true), Category("Drive"), Description("Drive Capacity")]
        public Collection<string> DriveCapacity
        {
            get { return mvarDriveCapacity; }
        }

        /// <summary>
        /// "Drive Interface"
        /// </summary>
        [Browsable(true), Category("Drive"), Description("Drive Interface")]
        public Collection<string> DriveInterface
        {
            get { return mvarDriveInterface; }
        }

        /// <summary>
        /// "Drive Model Number"
        /// </summary>
        [Browsable(true), Category("Drive"), Description("Drive Model Number")]
        public Collection<string> DriveModelNo
        {
            get { return mvarDriveModelNo; }
        }


        [Browsable(true), Category("Drive"), Description("Drive Status")]
        public Collection<string> DriveStatus
        {
            get { return mvarDriveStatus; }
        }

        #endregion

        #region " Mainboard Public Properties "

        /// <summary>
        /// "Mainboard (or Chipset) Manufacturer"
        /// </summary>
        [Browsable(true), Category("Mainboard"), Description("Mainboard (or Chipset) Manufacturer")]
        public string MainBoardManufacturer
        {
            get
            {
                mvarMainBoardManufacturer = GetMainBoardManufacturer();
                return mvarMainBoardManufacturer;
            }
        }

        /// <summary>
        /// "Mainboard (or Chipset) Model"
        /// </summary>
        [Browsable(true), Category("Mainboard"), Description("Mainboard (or Chipset) Model")]
        public string MainBoardModel
        {
            get
            {
                mvarMainBoardModel = GetMainBoardModel();
                return mvarMainBoardModel;
            }
        }

        #endregion

        #region " Memory Public Properties "

        /// <summary>
        /// "Available Virtual Memory"
        /// </summary>
        [Browsable(true), Category("Memory"), Description("Available Virtual Memory")]
        public string MemAvailVirtual
        {
            get
            {
                mvarMemAvailVirtual = NativeMethods.GetMemAvailVirtual();
                return mvarMemAvailVirtual;
            }
        }

        /// <summary>
        /// "Available Physical Memory"
        /// </summary>
        [Browsable(true), Category("Memory"), Description("Available Physical Memory")]
        public string MemAvailPhysical
        {
            get
            {
                mvarMemAvailPhysical = NativeMethods.GetMemAvailPhysical();
                return mvarMemAvailPhysical;
            }
        }

        /// <summary>
        /// "Total Virtual Memory"
        /// </summary>
        [Browsable(true), Category("Memory"), Description("Total Virtual Memory")]
        public string MemTotalVirtual
        {
            get
            {
                mvarMemTotalVirtual = NativeMethods.GetMemTotalVirtual();
                return mvarMemTotalVirtual;
            }
        }

        /// <summary>
        /// "Total Physical Memory"
        /// </summary>
        [Browsable(true), Category("Memory"), Description("Total Physical Memory")]
        public string MemTotalPhysical
        {
            get
            {
                mvarMemTotalPhysical = NativeMethods.GetMemTotalPhysical();
                return mvarMemTotalPhysical;
            }
        }

        #endregion

        #region " .NET Framework Public Properties "

        /// <summary>
        /// ".NET Framework Version"
        /// </summary>
        [Browsable(true), Category(".NET Framework"), Description(".NET Framework Version")]
        public string FrameworkVersion
        {
            get
            {
                mvarFrameworkVersion = GetFrameworkVersion();
                return mvarFrameworkVersion;
            }
        }

        /// <summary>
        /// ".NET Framework Major Version"
        /// </summary>
        [Browsable(true), Category(".NET Framework"), Description(".NET Framework Major Version")]
        public int FrameworkMajorVersion
        {
            get
            {
                mvarFrameworkMajorVersion = GetFrameworkMajorVersion();
                return mvarFrameworkMajorVersion;
            }
        }

        /// <summary>
        /// ".NET Framwork Minor Version"
        /// </summary>
        [Browsable(true), Category(".NET Framework"), Description(".NET Framwork Minor Version")]
        public int FrameworkMinorVersion
        {
            get
            {
                mvarFrameworkMinorVersion = GetFrameworkMinorVersion();
                return mvarFrameworkMinorVersion;
            }
        }

        /// <summary>
        /// ".NET Framework Service Pack"
        /// </summary>
        [Browsable(true), Category(".NET Framework"), Description(".NET Framework Service Pack")]
        public string FrameworkServicePack
        {
            get
            {
                mvarFrameworkServicePack = GetFrameworkServicePack();
                return mvarFrameworkServicePack;
            }
        }

        /// <summary>
        /// ".NET Framework Major and Minor Version Separated by a Decimal"
        /// </summary>
        [Browsable(true), Category(".NET Framework"),
         Description(".NET Framework Major and Minor Version Separated by a Decimal")]
        public string FrameworkShortVersion
        {
            get
            {
                mvarFrameworkShortVersion = GetFrameworkShortVersion();
                return mvarFrameworkShortVersion;
            }
        }

        #endregion

        #region " Network Public Properties "

        /// <summary>
        /// "Network Interface Type"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Network Interface Type")]
        public Collection<string> NetAdapterType
        {
            get { return mvarNetAdapterType; }
        }

        /// <summary>
        /// "Network Interface Auto Sense Capability"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Auto Sense Capability")]
        public Collection<string> NetAutoSense
        {
            get { return mvarNetAutoSense; }
        }

        /// <summary>
        /// "Network Interface Media Access Control (MAC) Address"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Media Access Control (MAC) Address")]
        public Collection<string> NetMacAddress
        {
            get { return mvarNetMacAddress; }
        }

        /// <summary>
        /// "Network Interface Manufacturer"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Network Interface Manufacturer")]
        public Collection<string> NetManufacturer
        {
            get { return mvarNetManufacturer; }
        }

        /// <summary>
        /// "Network Interface Maximum Speed"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Maximum Speed")]
        public Collection<string> NetMaxSpeed
        {
            get { return mvarNetMaxSpeed; }
        }

        /// <summary>
        /// "Network Interface Connection ID"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Connection ID")]
        public Collection<string> NetConnectionId
        {
            get { return mvarNetConnectionId; }
        }

        /// <summary>
        /// "Network Interface Connection Status"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Connection Status")]
        public Collection<string> NetConnectionStatus
        {
            get { return mvarNetConnectionStatus; }
        }

        /// <summary>
        /// "Network Interface Internet Protocol (IP) Enabled Status"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Internet Protocol (IP) Enabled Status")]
        public Collection<bool> NetIPEnabled
        {
            get { return mvarNetIPEnabled; }
        }

        /// <summary>
        /// "Number of Network Interfaces"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Number of Network Interfaces")]
        public int NetNumberOfAdapters
        {
            get { return mvarNetNumberOfAdaptors; }
        }

        /// <summary>
        /// "Network Interface Product HolidayName"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Product Name")]
        public Collection<string> NetProductName
        {
            get { return mvarNetProductName; }
        }

        /// <summary>
        /// "Network Interface Speed"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Speed")]
        public Collection<string> NetSpeed
        {
            get { return mvarNetSpeed; }
        }

        /// <summary>
        /// "Network Interface Default Time-To-Live (TTL)"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Default Time-To-Live (TTL)")]
        public Collection<string> NetDefaultTtl
        {
            get { return mvarNetDefaultTtl; }
        }

        /// <summary>
        /// "Network Interface Dynamic Host Configuration Protocol (DHCP) Enabled Status"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Dynamic Host Configuration Protocol (DHCP) Enabled Status")]
        public Collection<bool> NetDhcpEnabled
        {
            get { return mvarNetDhcpEnabled; }
        }

        /// <summary>
        /// "HolidayDate Network Interface Dynamic Host Configuration Protocol (DHCP) Lease Obtained"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Date Network Interface Dynamic Host Configuration Protocol (DHCP) Lease Obtained")]
        public Collection<string> NetDhcpLeaseObtained
        {
            get { return mvarNetDhcpLeaseObtained; }
        }

        /// <summary>
        /// "Network Interface HolidayDate Network Interface Dynamic Host Configuration Protocol (DHCP) Lease Expires"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Date Network Interface Dynamic Host Configuration Protocol (DHCP) Lease Expires")]
        public Collection<string> NetDhcpLeaseExpires
        {
            get { return mvarNetDhcpLeaseExpires; }
        }

        /// <summary>
        /// "Network Interface HolidayDate Network Interface Dynamic Host Configuration Protocol (DHCP) Server"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Date Network Interface Dynamic Host Configuration Protocol (DHCP) Server")]
        public Collection<string> NetDhcpServer
        {
            get { return mvarNetDhcpServer; }
        }

        /// <summary>
        /// "Network Interface Maximum Transmission Unit (MTU)"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Maximum Transmission Unit (MTU)")]
        public Collection<string> NetMtu
        {
            get { return mvarNetMtu; }
        }

        /// <summary>
        /// "Number of Network Interface Transmission Control Protocol (TCP) Connections"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Number of Network Interface Transmission Control Protocol (TCP) Connections")]
        public Collection<string> NetTcpNumConnections
        {
            get { return mvarNetTcpNumConnections; }
        }

        /// <summary>
        /// "Network Interface Transmission Control Protocol (TCP) Window Size"
        /// </summary>
        [Browsable(true), Category("Network"),
         Description("Transmission Control Protocol (TCP) Window Size")]
        public Collection<string> NetTcpWindowSize
        {
            get { return mvarNetTcpWindowSize; }
        }

        /// <summary>
        /// "Network Interface Internet Protocol (IP) Address"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Internet Protocol (IP) Address")]
        public Collection<string> NetIPAddress
        {
            get { return mvarNetIPAddress; }
        }

        /// <summary>
        /// "Network Interface Network Domain"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Network Domain")]
        public Collection<string> NetDomain
        {
            get { return mvarNetDomain; }
        }

        /// <summary>
        /// "Network Interface Network Host HolidayName"
        /// </summary>
        [Browsable(true), Category("Network"), Description("Network Host Name")]
        public Collection<string> NetHostName
        {
            get { return mvarNetHostName; }
        }

        #endregion

        #region " Operating System Public Properties "

        /// <summary>
        /// "Build"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Build")]
        public string OSBuild
        {
            get
            {
                mvarOSBuild = GetOSBuild();
                return mvarOSBuild;
            }
        }

        /// <summary>
        /// "Operating System Code HolidayName"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Code Name")]
        public string OSCodeName
        {
            get
            {
                mvarOSCodeName = GetOSCodeName();
                return mvarOSCodeName;
            }
        }

        /// <summary>
        /// "Machine HolidayName"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Machine Name")]
        public string OSMachineName
        {
            get
            {
                mvarOSMachineName = GetOSMachineName();
                return mvarOSMachineName;
            }
        }

        /// <summary>
        /// "Operating System Version"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Version")]
        public string OSVersion
        {
            get
            {
                mvarOSVersion = GetOSVersion();
                return mvarOSVersion;
            }
        }

        /// <summary>
        /// "Operating System Full HolidayName"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Full Name")]
        public string OSFullName
        {
            get
            {
                mvarOSFullName = GetOSFullName();
                return mvarOSFullName;
            }
        }

        /// <summary>
        /// "Operating System Platform"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Platform")]
        public PlatformID OSPlatform
        {
            get
            {
                mvarOSPlatform = GetOSPlatform();
                return mvarOSPlatform;
            }
        }

        /// <summary>
        /// "Operating System Minor Version"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Minor Version")]
        public int OSMinorVersion
        {
            get
            {
                mvarOSMinorVersion = GetOSMinorVersion();
                return mvarOSMinorVersion;
            }
        }

        /// <summary>
        /// "Operating System Major Version"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Major Version")]
        public int OSMajorVersion
        {
            get
            {
                mvarOSMajorVersion = GetOSMajorVersion();
                return mvarOSMajorVersion;
            }
        }

        /// <summary>
        /// "Operating System Service Pack"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Service Pack")]
        public string OSServicePack
        {
            get
            {
                mvarOSServicePack = GetOSServicePack();
                return mvarOSServicePack;
            }
        }

        /// <summary>
        /// "Logon User HolidayName"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Logon User Name")]
        public string OSUserName
        {
            get
            {
                mvarOSUserName = GetOSUserName();
                return mvarOSUserName;
            }
        }

        /// <summary>
        /// "OS Product Key"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("OS Product Key")]
        public string OSProductKey
        {
            get
            {
                mvarOSProductKey = GetOSProductKey();
                return mvarOSProductKey;
            }
        }

        /// <summary>
        /// "Operating System Major and Minor Version Separated by a Decimal"
        /// </summary>
        [Browsable(true), Category("Operating System"),
         Description("Operating System Major and Minor Version Separated by a Decimal")]
        public string OSShortVersion
        {
            get
            {
                mvarOSShortVersion = GetOSShortVersion();
                return mvarOSShortVersion;
            }
        }

        /// <summary>
        /// "Formatted Time Since Last Start"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Formatted Time Since Last Start")]
        public string OSUpTime
        {
            get
            {
                mvarOSUpTime = GetOSUptime();
                return mvarOSUpTime;
            }
        }

        /// <summary>
        /// "Operating System Bootup State"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Bootup State")]
        public string OSBootupState
        {
            get
            {
                mvarOSBootupState = GetOSBootupState();
                return mvarOSBootupState;
            }
        }

        /// <summary>
        /// "Microsoft's Numeric Product ID"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Microsoft's Numeric Product ID")]
        public string OSProductID
        {
            get
            {
                mvarOSProductID = GetOSProductID();
                return mvarOSProductID;
            }
        }

        /// <summary>
        /// "Type of build, ie. checked/free and single/multi processor"
        /// </summary>
        [Browsable(true), Category("Operating System"),
         Description("Type of build, ie. checked/free and single/multi processor")]
        public string OSType
        {
            get
            {
                mvarOSType = GetOSType();
                return mvarOSType;
            }
        }

        /// <summary>
        /// "Operating System Domain"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Domain")]
        public string OSDomain
        {
            get
            {
                mvarOSDomain = GetOSDomain();
                return mvarOSDomain;
            }
        }

        /// <summary>
        /// "Operating System Part of Domain"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Part of Domain")]
        public bool OSPartOfDomain
        {
            get
            {
                mvarOSPartOfDomain = GetOSPartOfDomain();
                return mvarOSPartOfDomain;
            }
        }

        /// <summary>
        /// "Operating System Install HolidayDate"
        /// </summary>
        [Browsable(true), Category("Operating System"), Description("Operating System Install Date")]
        public DateTime OSInstallDate
        {
            get
            {
                mvarOSInstallDate = GetOSInstallDate();
                return mvarOSInstallDate;
            }
        }

        #endregion

        #region " Service Properties "

        /// <summary>
        /// Display name for service
        /// </summary>
        [Browsable(true), Category("Service"), Description("Display name for service")]
        public Collection<string> ServiceDisplayName
        {
            get { return mvarServiceDisplayName; }
        }

        /// <summary>
        /// Description of service
        /// </summary>
        [Browsable(true), Category("Service"), Description("Description of service")]
        public Collection<string> ServiceDescription
        {
            get { return mvarServiceDescription; }
        }

        /// <summary>
        /// Start mode of service
        /// </summary>
        [Browsable(true), Category("Service"), Description("Start mode of service")]
        public Collection<string> ServiceStartMode
        {
            get { return mvarServiceStartMode; }
        }

        /// <summary>
        /// State of service
        /// </summary>
        [Browsable(true), Category("Service"), Description("State of service")]
        public Collection<string> ServiceState
        {
            get { return mvarServiceState; }
        }

        /// <summary>
        /// Status of service
        /// </summary>
        [Browsable(true), Category("Service"), Description("Status of service")]
        public Collection<string> ServiceStatus
        {
            get { return mvarServiceStatus; }
        }

        /// <summary>
        /// Path name of service
        /// </summary>
        [Browsable(true), Category("Service"), Description("Path name of service")]
        public Collection<string> ServicePathName
        {
            get { return mvarServicePathName; }
        }

        #endregion

        #region " Sound Public Properties "

        /// <summary>
        /// "Sound Controller HolidayName"
        /// </summary>
        [Browsable(true), Category("Sound"), Description("Sound Controller Name")]
        public Collection<string> SndController
        {
            get { return mvarSndController; }
        }

        /// <summary>
        /// "Sound Controller Manufacturer"
        /// </summary>
        [Browsable(true), Category("Sound"), Description("Sound Controller Manufacturer")]
        public Collection<string> SndManufacturer
        {
            get { return mvarSndManufacturer; }
        }

        /// <summary>
        /// "Sound Controller DMA Buffer Size"
        /// </summary>
        [Browsable(true), Category("Sound"), Description("Sound Controller DMA Buffer Size")]
        public Collection<string> SndDmaBufferSize
        {
            get { return mvarSndDMABufferSize; }
        }

        /// <summary>
        /// "Number of Sound Controllers"
        /// </summary>
        [Browsable(true), Category("Sound"), Description("Number of Sound Controllers")]
        public int SndNumberOfControllers
        {
            get { return mvarSndNumberOfControllers; }
        }

        #endregion

        #region " Time Public Properties "

        /// <summary>
        /// "Current Time Zone"
        /// </summary>
        [Browsable(true), Category("Time"), Description("Current Time Zone")]
        public string TimeCurrentTimeZone
        {
            get
            {
                mvarTimeCurrentTimeZone = GetCurrentTimeZone();
                return mvarTimeCurrentTimeZone;
            }
        }

        /// <summary>
        /// "Daylight Savings in Effect"
        /// </summary>
        [Browsable(true), Category("Time"), Description("Daylight Savings in Effect")]
        public bool TimeDaylightSavingsInEffect
        {
            get
            {
                mvarTimeDaylightSavingsInEffect = GetDaylightSavingsInEffect();
                return mvarTimeDaylightSavingsInEffect;
            }
        }

        /// <summary>
        /// "Daylight Savings HolidayName"
        /// </summary>
        [Browsable(true), Category("Time"), Description("Daylight Savings Name")]
        public string TimeDaylightSavingsName
        {
            get
            {
                mvarTimeDaylightSavingsName = GetDaylightSavingsName();
                return mvarTimeDaylightSavingsName;
            }
        }

        /// <summary>
        /// "Daylight Savings Offset"
        /// </summary>
        [Browsable(true), Category("Time"), Description("Daylight Savings Offset")]
        public int TimeDaylightSavingsOffset
        {
            get
            {
                GetDaylightSavingsOffset();
                return GetDaylightSavingsOffset();
            }
        }

        /// <summary>
        /// "(HolidayDate) Local Daylight Saving End HolidayDate"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Local Daylight Saving End Date")]
        public DateTime TimeLocalDaylightEndDate
        {
            get
            {
                mvarTimeLocalDaylightEndDate = GetLocalDaylightEndDate(mTempDate);
                return mvarTimeLocalDaylightEndDate;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Local Daylight Saving End Time"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Local Daylight Saving End Time")]
        public DateTime TimeLocalDaylightEndTime
        {
            get
            {
                mvarTimeLocalDaylightEndTime = GetLocalDaylightEndTime(mTempDate);
                return mvarTimeLocalDaylightEndTime;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Local Daylight Saving Start HolidayDate"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Local Daylight Saving Start Date")]
        public DateTime TimeLocalDaylightStartDate
        {
            get
            {
                mvarTimeLocalDaylightStartDate = GetLocalDaylightStartDate(mTempDate);
                return mvarTimeLocalDaylightStartDate;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Local Daylight Saving Start Time"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Local Daylight Saving Start Time")]
        public DateTime TimeLocalDaylightStartTime
        {
            get
            {
                mvarTimeLocalDaylightStartTime = GetLocalDaylightStartTime(mTempDate);
                return mvarTimeLocalDaylightStartTime;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Local HolidayDate and Time"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Local Date and Time")]
        public DateTime TimeLocalDateTime
        {
            get
            {
                mvarTimeLocalDateTime = GetLocalDateTime();
                return mvarTimeLocalDateTime;
            }
        }

        /// <summary>
        /// "(HolidayDate) Universal HolidayDate and Time"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Universal Date and Time")]
        public DateTime TimeUniversalDateTime
        {
            get
            {
                mvarTimeUniversalDateTime = GetUniversalDateTime();
                return mvarTimeUniversalDateTime;
            }
        }

        /// <summary>
        /// "(HolidayDate) Universal Daylight Saving End HolidayDate"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Universal Daylight Saving End Date")]
        public DateTime TimeUniversalDaylightEndDate
        {
            get
            {
                mvarTimeUniversalDaylightEndDate = GetUniversalDaylightEndDate(mTempDate);
                return mvarTimeUniversalDaylightEndDate;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Universal Daylight Saving End Time"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Universal Daylight Saving End Time")]
        public DateTime TimeUniversalDaylightEndTime
        {
            get
            {
                mvarTimeUniversalDaylightEndTime = GetUniversalDaylightEndTime(mTempDate);
                return mvarTimeUniversalDaylightEndTime;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Universal Daylight Saving Start HolidayDate"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Universal Daylight Saving Start Date")]
        public DateTime TimeUniversalDaylightStartDate
        {
            get
            {
                mvarTimeUniversalDaylightStartDate = GetUniversalDaylightStartDate(mTempDate);
                return mvarTimeUniversalDaylightStartDate;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "(HolidayDate) Universal Daylight Saving Start Time"
        /// </summary>
        [Browsable(true), Category("Time"), Description("(Date) Universal Daylight Saving Start Time")]
        public DateTime TimeUniversalDaylightStartTime
        {
            get
            {
                mvarTimeUniversalDaylightStartTime = GetUniversalDaylightStartTime(mTempDate);
                return mvarTimeUniversalDaylightStartTime;
            }
            set { mTempDate = value; }
        }

        /// <summary>
        /// "Universal Time Offset"
        /// </summary>
        [Browsable(true), Category("Time"), Description("Universal Time Offset")]
        public int TimeUniversalTimeOffset
        {
            get
            {
                mvarUniversalTimeOffset = GetUniversalTimeOffset();
                return mvarUniversalTimeOffset;
            }
        }

        #endregion

        #region " User Account Public Properties "

        /// <summary>
        /// "Administrator Status of Current User"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("Administrator Status of Current User")]
        public bool UserIsAdministrator
        {
            get
            {
                mvarUserIsAdministrator = IsUserAdministrator();
                return mvarUserIsAdministrator;
            }
        }

        /// <summary>
        /// "Registered HolidayName"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("Registered Name")]
        public string UserRegisteredName
        {
            get
            {
                mvarUserRegisteredName = GetUserRegisteredName();
                return mvarUserRegisteredName;
            }
            set
            {
                mvarUserRegisteredName = value;
                SetUserRegisteredName(mvarUserRegisteredName);
            }
        }

        /// <summary>
        /// "Registered Organization"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("Registered Organization")]
        public string UserRegisteredOrganization
        {
            get
            {
                mvarUserRegisteredOrganization = GetUserRegisteredOrganization();
                return mvarUserRegisteredOrganization;
            }
            set
            {
                mvarUserRegisteredOrganization = value;
                SetUserRegisteredOrganization(mvarUserRegisteredOrganization);
            }
        }

        /// <summary>
        /// "User Account Flags"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("User Account Flags")]
        public Collection<int> UserFlags
        {
            get
            {
                mvarUserFlags = GetUserFlags();
                return mvarUserFlags;
            }
        }

        /// <summary>
        /// "User Account HolidayName"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("User Account Name")]
        public Collection<string> UserAccounts
        {
            get
            {
                mvarUserAccount = NativeMethods.EnumerateUsers();
                return mvarUserAccount;
            }
        }

        /// <summary>
        /// "User Full HolidayName"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("User Full Name")]
        public Collection<string> UserFullName
        {
            get
            {
                mvarUserFullName = GetUserFullNames();
                return mvarUserFullName;
            }
        }

        /// <summary>
        /// "User Privilege"
        /// </summary>
        [Browsable(true), Category("User Account"), Description("User Privilege")]
        public Collection<string> UserPrivilege
        {
            get
            {
                mvarUserPrivilege = GetUserPrivileges();
                return mvarUserPrivilege;
            }
        }

        #endregion

        #region " Video Public Properties "

        /// <summary>
        /// "Installed Video Controllers"
        /// </summary>
        [Browsable(true), Category("Video"), Description("Installed Video Controllers")]
        public Collection<string> VidController
        {
            get { return mvarVidController; }
        }

        /// <summary>
        /// "Currently Configured Video Dimensions"
        /// </summary>
        [Browsable(true), Category("Video"), Description("Currently Configured Video Dimensions")]
        public string VidPrimaryScreenDimensions
        {
            get
            {
                mvarVidPrimarytScreenDimensions = GetVideoPrimaryScreenDimensions();
                return mvarVidPrimarytScreenDimensions;
            }
        }

        /// <summary>
        /// "Screen Dimensions Currently Available to Applications"
        /// </summary>
        [Browsable(true), Category("Video"),
         Description("Screen Dimensions Currently Available to Applications")]
        public string VidPrimaryScreenWorkingArea
        {
            get
            {
                mvarVidPrimaryScreenWorkingArea = GetVideoPrimaryScreenWorkingArea();
                return mvarVidPrimaryScreenWorkingArea;
            }
        }

        /// <summary>
        /// "Number of Video Controllers"
        /// </summary>
        [Browsable(true), Category("Video"), Description("Number of Video Controllers")]
        public int VidNumberOfControllers
        {
            get { return mvarVidNumberOfControllers; }
        }

        /// <summary>
        /// "Video Controllers RAM"
        /// </summary>
        [Browsable(true), Category("Video"), Description("Video Controllers RAM")]
        public Collection<string> VidRam
        {
            get { return mvarVidRam; }
        }

        /// <summary>
        /// "Video Controllers Refresh Rate"
        /// </summary>
        [Browsable(true), Category("Video"), Description("Video Controllers Refresh Rate")]
        public Collection<string> VidRefreshRate
        {
            get { return mvarVidRefreshRate; }
        }

        /// <summary>
        /// "Video Controllers Colors"
        /// </summary>
        [Browsable(true), Category("Video"), Description("Video Controllers Colors")]
        public Collection<string> VidScreenColors
        {
            get { return mvarVidScreenColors; }
        }

        #endregion

        #region " Visual Style Public Properties "

        /// <summary>
        /// "Visual Style Author"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Author")]
        public string VstAuthor
        {
            get
            {
                mvarVstAuthor = GetVstAuthor();
                return mvarVstAuthor;
            }
        }

        /// <summary>
        /// "Visual Style ColorScheme"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style ColorScheme")]
        public string VstColorScheme
        {
            get
            {
                mvarVstColorScheme = GetVstColorScheme();
                return mvarVstColorScheme;
            }
        }

        /// <summary>
        /// "Visual Style Company"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Company")]
        public string VstCompany
        {
            get
            {
                mvarVstCompany = GetVstCompany();
                return mvarVstCompany;
            }
        }

        /// <summary>
        /// "(Drawing.Color) Visual Style Control Highlight Hot"
        /// </summary>
        [Browsable(true), Category("Visual Styles"),
         Description("(Drawing.Color) Visual Style Control Highlight Hot")]
        public Color VstControlHighlightHot
        {
            get
            {
                mvarVstControlHighlightHot = GetVstControlHighlightHot();
                return mvarVstControlHighlightHot;
            }
        }

        /// <summary>
        /// "Visual Style Copyright"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Copyright")]
        public string VstCopyright
        {
            get
            {
                mvarVstCopyright = GetVstCopyright();
                return mvarVstCopyright;
            }
        }

        /// <summary>
        /// "Visual Style Description"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Description")]
        public string VstDescription
        {
            get
            {
                mvarVstDescription = GetVstDescription();
                return mvarVstDescription;
            }
        }

        /// <summary>
        /// "Visual Style Display HolidayName"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Display Name")]
        public string VstDisplayName
        {
            get
            {
                mvarVstDisplayName = GetVstDisplayName();
                return mvarVstDisplayName;
            }
        }

        /// <summary>
        /// "Visual Style Enabled by User"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Enabled by User")]
        public bool VstIsEnabledByUser
        {
            get
            {
                mvarVstIsEnabledByUser = GetVstIsEnabledByUser();
                return mvarVstIsEnabledByUser;
            }
        }

        /// <summary>
        /// "Visual Style Supported by Operating System"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Supported by Operating System")]
        public bool VstIsSupportedByOS
        {
            get
            {
                mvarVstIsSupportedByOS = GetVstIsSupportedByOS();
                return mvarVstIsSupportedByOS;
            }
        }

        /// <summary>
        /// "Visual Style Minimum Color Depth"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Minimum Color Depth")]
        public int VstMinimumColorDepth
        {
            get
            {
                mvarVstMinimumColorDepth = GetVstMinimumColorDepth();
                return mvarVstMinimumColorDepth;
            }
        }

        /// <summary>
        /// "Visual Style Size"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Size")]
        public string VstSize
        {
            get
            {
                mvarVstSize = GetVstSize();
                return mvarVstSize;
            }
        }

        /// <summary>
        /// "Visual Style Supports Flat Menus"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Supports Flat Menus")]
        public bool VstSupportsFlatMenus
        {
            get
            {
                mvarVstSupportsFlatMenus = GetVstSupportsFlatMenus();
                return mvarVstSupportsFlatMenus;
            }
        }

        /// <summary>
        /// "(Drawing.Color) Visual Style Text Control Border"
        /// </summary>
        [Browsable(true), Category("Visual Styles"),
         Description("(Drawing.Color) Visual Style Text Control Border")]
        public Color VstTextControlBorder
        {
            get
            {
                mvarVstTextControlBorder = GetVstTextControlBorder();
                return mvarVstTextControlBorder;
            }
        }

        /// <summary>
        /// "Visual Style URL"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style URL")]
        public string VstUrl
        {
            get
            {
                mvarVstUrl = GetVstUrl();
                return mvarVstUrl;
            }
        }

        /// <summary>
        /// "Visual Style Version"
        /// </summary>
        [Browsable(true), Category("Visual Styles"), Description("Visual Style Version")]
        public string VstVersion
        {
            get
            {
                mvarVstVersion = GetVstVersion();
                return mvarVstVersion;
            }
        }

        #endregion

        #region " Volume Public Properties "

        /// <summary>
        /// "Volume Letters"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Letters")]
        public Collection<string> VolumeLetter
        {
            get { return mvarVolumeLetter; }
        }

        /// <summary>
        /// "Volume Types"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Types")]
        public Collection<string> VolumeType
        {
            get { return mvarVolumeType; }
        }

        /// <summary>
        /// "Volume File Systems"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume File Systems")]
        public Collection<string> VolumeFileSystem
        {
            get { return mvarVolumeFileSystem; }
        }

        /// <summary>
        /// "Volume Ready Status"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Ready Status")]
        public Collection<bool> VolumeReady
        {
            get { return mvarVolumeReady; }
        }

        /// <summary>
        /// "Volume Label"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Label")]
        public Collection<string> VolumeLabel
        {
            get { return mvarVolumeLabel; }
        }

        /// <summary>
        /// "Volume Total Size"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Total Size")]
        public Collection<string> VolumeTotalSize
        {
            get { return mvarVolumeTotalSize; }
        }

        /// <summary>
        /// "Volume Free Space"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Free Space")]
        public Collection<string> VolumeFreeSpace
        {
            get { return mvarVolumeFreeSpace; }
        }

        /// <summary>
        /// "Volume Used Space"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Used Space")]
        public Collection<string> VolumeUsedSpace
        {
            get { return mvarVolumeUsedSpace; }
        }

        /// <summary>
        /// "Volume Percent Free Space"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Percent Free Space")]
        public Collection<string> VolumePercentFreeSpace
        {
            get { return mvarVolumePercentFreeSpace; }
        }

        /// <summary>
        /// "Volume Serial Number"
        /// </summary>
        [Browsable(true), Category("Volume"), Description("Volume Serial Number")]
        public Collection<string> VolumeSerialNumber
        {
            get { return mvarVolumeSerialNumber; }
        }

        #endregion

        #endregion
    }
}