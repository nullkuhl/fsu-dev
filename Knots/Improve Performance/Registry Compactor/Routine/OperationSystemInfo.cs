using System;
using System.Runtime.InteropServices;

namespace RegistryCompactor
{
	/// <summary>
	/// Operation system info
	/// </summary>
	public static class OperationSystemInfo
	{
		#region PInvoke Signatures

		public const byte VER_NT_WORKSTATION = 1;
		public const byte VER_NT_DOMAIN_CONTROLLER = 2;
		public const byte VER_NT_SERVER = 3;

		public const ushort VER_SUITE_SMALLBUSINESS = 1;
		public const ushort VER_SUITE_ENTERPRISE = 2;
		public const ushort VER_SUITE_TERMINAL = 16;
		public const ushort VER_SUITE_DATACENTER = 128;
		public const ushort VER_SUITE_SINGLEUSERTS = 256;
		public const ushort VER_SUITE_PERSONAL = 512;
		public const ushort VER_SUITE_BLADE = 1024;
		public const ushort VER_SUITE_WH_SERVER = 32768;

		public const uint PRODUCT_UNDEFINED = 0x00000000;
		public const uint PRODUCT_ULTIMATE = 0x00000001;
		public const uint PRODUCT_HOME_BASIC = 0x00000002;
		public const uint PRODUCT_HOME_PREMIUM = 0x00000003;
		public const uint PRODUCT_ENTERPRISE = 0x00000004;
		public const uint PRODUCT_HOME_BASIC_N = 0x00000005;
		public const uint PRODUCT_BUSINESS = 0x00000006;
		public const uint PRODUCT_STANDARD_SERVER = 0x00000007;
		public const uint PRODUCT_DATACENTER_SERVER = 0x00000008;
		public const uint PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
		public const uint PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
		public const uint PRODUCT_STARTER = 0x0000000B;
		public const uint PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
		public const uint PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
		public const uint PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
		public const uint PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
		public const uint PRODUCT_BUSINESS_N = 0x00000010;
		public const uint PRODUCT_WEB_SERVER = 0x00000011;
		public const uint PRODUCT_CLUSTER_SERVER = 0x00000012;
		public const uint PRODUCT_HOME_SERVER = 0x00000013;
		public const uint PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
		public const uint PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
		public const uint PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
		public const uint PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
		public const uint PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
		public const uint PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
		public const uint PRODUCT_HOME_PREMIUM_N = 0x0000001A;
		public const uint PRODUCT_ENTERPRISE_N = 0x0000001B;
		public const uint PRODUCT_ULTIMATE_N = 0x0000001C;
		public const uint PRODUCT_WEB_SERVER_CORE = 0x0000001D;
		public const uint PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
		public const uint PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
		public const uint PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
		public const uint PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
		public const uint PRODUCT_STANDARD_SERVER_V = 0x00000024;
		public const uint PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
		public const uint PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
		public const uint PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
		public const uint PRODUCT_HYPERV = 0x0000002A;

		public const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
		public const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
		public const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
		public const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

		public const int SM_SERVERR2 = 89;

		[DllImport("Kernel32.dll")]
		internal static extern bool GetProductInfo(
			uint majorVersion,
			uint minorVersion,
			uint servicePackMajorVersion,
			uint servicePackMinorVersion,
			out uint edition);

		[DllImport("kernel32.dll")]
		internal static extern bool GetVersionEx(ref OSVERSIONINFOEX versionInfo);

		[DllImport("kernel32.dll")]
		internal static extern void GetSystemInfo(ref SYSTEM_INFO systemInfo);

		[DllImport("user32.dll")]
		internal static extern int GetSystemMetrics(int index);

		#region Nested type: OSVERSIONINFOEX

		/// <summary>
		/// OS version extended info
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct OSVERSIONINFOEX
		{
			public uint OperatingSystemInfoSize;
			public uint MajorVersion;
			public uint MinorVersion;
			public uint BuildNumber;
			public uint PlatformId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string CSDVersion;
			public ushort ServicePackMajorVersion;
			public ushort ServicePackMinorVersion;
			public ushort SuiteMask;
			public byte ProductType;
			public byte Reserved;
		}

		#endregion

		#region Nested type: SYSTEM_INFO

		/// <summary>
		/// System info
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct SYSTEM_INFO
		{
			public uint ProcessorArchitecture;
			public uint Reserved;
			public uint PageSize;
			public uint MinimumApplicationAddress;
			public uint MaximumApplicationAddress;
			public uint ActiveProcessorMask;
			public uint NumberOfProcessors;
			public uint ProcessorType;
			public uint AllocationGranularity;
			public uint ProcessorLevel;
			public uint ProcessorRevision;
		}

		#endregion

		#endregion

		/// <summary>
		/// Gets OS version
		/// </summary>
		/// <returns></returns>
		public static string GetVersion()
		{
			var versionInfo = new OSVERSIONINFOEX {OperatingSystemInfoSize = (uint) Marshal.SizeOf(typeof (OSVERSIONINFOEX))};

			var systemInfo = new SYSTEM_INFO();
			GetSystemInfo(ref systemInfo);

			string name = "Microsoft ";

			if (!GetVersionEx(ref versionInfo))
				return string.Empty;

			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32Windows:
					{
						switch (versionInfo.MajorVersion)
						{
							case 4:
								{
									switch (versionInfo.MinorVersion)
									{
										case 0:
											if (versionInfo.CSDVersion == "B" ||
											    versionInfo.CSDVersion == "C")
												name += "Windows 95 R2";
											else
												name += "Windows 95";
											break;
										case 10:
											if (versionInfo.CSDVersion == "A")
												name += "Windows 98 SE";
											else
												name += "Windows 98";
											break;
										case 90:
											name += "Windows ME";
											break;
									}
								}
								break;
						}
					}
					break;

				case PlatformID.Win32NT:
					{
						switch (versionInfo.MajorVersion)
						{
							case 3:
								name += "Windows NT 3.5.1";
								break;

							case 4:
								switch (versionInfo.ProductType)
								{
									case 1:
										name += "Windows NT 4.0";
										break;
									case 3:
										name += "Windows NT 4.0 Server";
										break;
								}
								break;

							case 5:
								{
									switch (versionInfo.MinorVersion)
									{
										case 0:
											name += "Windows 2000";
											break;
										case 1:
											name += "Windows XP";
											break;
										case 2:
											{
												if (versionInfo.SuiteMask == VER_SUITE_WH_SERVER)
													name += "Windows Home Server";
												else if (versionInfo.ProductType == VER_NT_WORKSTATION &&
												         systemInfo.ProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
													name += "Windows XP Professional";
												else
													name += GetSystemMetrics(SM_SERVERR2) == 0 ? "Windows Server 2003" : "Windows Server 2003 R2";
											}
											break;
									}
								}
								break;

							case 6:
								{
									switch (versionInfo.MinorVersion)
									{
										case 0:
											name += versionInfo.ProductType == VER_NT_WORKSTATION ? "Windows Vista" : "Windows Server 2008";
											break;

										case 1:
											name += versionInfo.ProductType == VER_NT_WORKSTATION ? "Windows 7" : "Windows Server 2008 R2";
											break;

										case 2:
											name += "Windows 8";
											break;
									}
								}
								break;
						}
					}
					break;
			}

			name += " ";

			switch (versionInfo.MajorVersion)
			{
				case 4:
					{
						switch (versionInfo.ProductType)
						{
							case VER_NT_WORKSTATION:
								name += "Workstation";
								break;

							case VER_NT_SERVER:
								name += (versionInfo.SuiteMask & VER_SUITE_ENTERPRISE) != 0 ? "Enterprise Server" : "Standard Server";
								break;
						}
					}
					break;

				case 5:
					{
						switch (versionInfo.ProductType)
						{
							case VER_NT_WORKSTATION:
								name += (versionInfo.SuiteMask & VER_SUITE_PERSONAL) != 0 ? "Home" : "Professional";
								break;

							case VER_NT_SERVER:
								{
									switch (versionInfo.MinorVersion)
									{
										case 0:
											{
												if ((versionInfo.SuiteMask & VER_SUITE_DATACENTER) != 0)
													name += "Data Center Server";
												else if ((versionInfo.SuiteMask & VER_SUITE_ENTERPRISE) != 0)
													name += "Advanced Server";
												else
													name += "Server";
											}
											break;

										default:
											{
												if ((versionInfo.SuiteMask & VER_SUITE_DATACENTER) != 0)
													name += "Data Center Server";
												else if ((versionInfo.SuiteMask & VER_SUITE_ENTERPRISE) != 0)
													name += "Enterprise Server";
												else if ((versionInfo.SuiteMask & VER_SUITE_BLADE) != 0)
													name += "Web Edition";
												else
													name += "Standard Server";
											}
											break;
									}
								}
								break;
						}
					}
					break;

				case 6:
					{
						uint ed;
						if (GetProductInfo(versionInfo.MajorVersion, versionInfo.MinorVersion, versionInfo.ServicePackMajorVersion,
						                   versionInfo.ServicePackMinorVersion, out ed))
						{
							switch (ed)
							{
								case PRODUCT_BUSINESS:
									name += "Business";
									break;
								case PRODUCT_BUSINESS_N:
									name += "Business N";
									break;
								case PRODUCT_CLUSTER_SERVER:
									name += "HPC Edition";
									break;
								case PRODUCT_DATACENTER_SERVER:
									name += "Data Center Server";
									break;
								case PRODUCT_DATACENTER_SERVER_CORE:
									name += "Data Center Server Core";
									break;
								case PRODUCT_ENTERPRISE:
									name += "Enterprise";
									break;
								case PRODUCT_ENTERPRISE_N:
									name += "Enterprise N";
									break;
								case PRODUCT_ENTERPRISE_SERVER:
									name += "Enterprise Server";
									break;
								case PRODUCT_ENTERPRISE_SERVER_CORE:
									name += "Enterprise Server Core Installation";
									break;
								case PRODUCT_ENTERPRISE_SERVER_CORE_V:
									name += "Enterprise Server Without Hyper-V Core Installation";
									break;
								case PRODUCT_ENTERPRISE_SERVER_IA64:
									name += "Enterprise Server For Itanium Based Systems";
									break;
								case PRODUCT_ENTERPRISE_SERVER_V:
									name += "Enterprise Server Without Hyper-V";
									break;
								case PRODUCT_HOME_BASIC:
									name += "Home Basic";
									break;
								case PRODUCT_HOME_BASIC_N:
									name += "Home Basic N";
									break;
								case PRODUCT_HOME_PREMIUM:
									name += "Home Premium";
									break;
								case PRODUCT_HOME_PREMIUM_N:
									name += "Home Premium N";
									break;
								case PRODUCT_HYPERV:
									name += "Hyper-V Server";
									break;
								case PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT:
									name += "Essential Business Management Server";
									break;
								case PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING:
									name += "Essential Business Messaging Server";
									break;
								case PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY:
									name += "Essential Business Security Server";
									break;
								case PRODUCT_SERVER_FOR_SMALLBUSINESS:
									name += "Essential Server Solutions";
									break;
								case PRODUCT_SERVER_FOR_SMALLBUSINESS_V:
									name += "Essential Server Solutions Without Hyper-V";
									break;
								case PRODUCT_SMALLBUSINESS_SERVER:
									name += "Small Business Server";
									break;
								case PRODUCT_STANDARD_SERVER:
									name += "Standard Server";
									break;
								case PRODUCT_STANDARD_SERVER_CORE:
									name += "Standard Server Core Installation";
									break;
								case PRODUCT_STANDARD_SERVER_CORE_V:
									name += "Standard Server Without Hyper-V Core Installation";
									break;
								case PRODUCT_STANDARD_SERVER_V:
									name += "Standard Server Without Hyper-V";
									break;
								case PRODUCT_STARTER:
									name += "Starter";
									break;
								case PRODUCT_STORAGE_ENTERPRISE_SERVER:
									name += "Enterprise Storage Server";
									break;
								case PRODUCT_STORAGE_EXPRESS_SERVER:
									name += "Express Storage Server";
									break;
								case PRODUCT_STORAGE_STANDARD_SERVER:
									name += "Standard Storage Server";
									break;
								case PRODUCT_STORAGE_WORKGROUP_SERVER:
									name += "Workgroup Storage Server";
									break;
								case PRODUCT_UNDEFINED:
									break;
								case PRODUCT_ULTIMATE:
									name += "Ultimate";
									break;
								case PRODUCT_ULTIMATE_N:
									name += "Ultimate N";
									break;
								case PRODUCT_WEB_SERVER:
									name += "Web Server";
									break;
								case PRODUCT_WEB_SERVER_CORE:
									name += "Web Server Core Installation";
									break;
							}
						}
					}
					break;
			}


			// If 64 bit OS -> Append (x64)
			if (IsX64())
				name += " (x64)";
			else
				// Otherwise (x86)
				name += " (x86)";

			return name;
		}

		/// <summary>
		/// Determines if the OS is x64
		/// </summary>
		/// <returns></returns>
		public static bool IsX64()
		{
			switch (IntPtr.Size)
			{
				case 8:
					return true;
				default:
					{
						// Detect whether the current process is a 32-bit process 
						// running on a 64-bit system.
						bool flag;
						return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
						         IsWow64Process(GetCurrentProcess(), out flag)) && flag);
					}
			}
		}

		/// <summary>
		/// The function determins whether a method exists in the export 
		/// table of a certain module.
		/// </summary>
		/// <param name="moduleName">The name of the module</param>
		/// <param name="methodName">The name of the method</param>
		/// <returns>
		/// The function returns true if the method specified by methodName 
		/// exists in the export table of the module specified by moduleName.
		/// </returns>
		static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
			{
				return false;
			}
			return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
		}

		[DllImport("kernel32.dll")]
		static extern IntPtr GetCurrentProcess();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr GetModuleHandle(string moduleName);

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr GetProcAddress(IntPtr hModule,
		                                    [MarshalAs(UnmanagedType.LPStr)] string procName);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);
	}
}