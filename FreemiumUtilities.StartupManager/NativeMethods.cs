using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FreemiumUtilities.StartupManager
{
	/// <summary>
	/// Contains a methods to operate with the <c>Icon</c>s
	/// </summary>
	public class NativeMethods
	{
		#region API Declarations

		[DllImport("Shell32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, Int32 nIconIndex);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern Boolean DestroyIcon(IntPtr hIcon);

		#endregion

		#region Extract Icon

		/// <summary>
		/// Return first icon for an executible file.
		/// </summary>
		/// <remarks>
		/// This overload does not require an icon index.
		/// </remarks>
		public Icon GetIconOld(string filePath)
		{
			try
			{
				// Extract icon 0 from filePath.
				const int iconIndex = 0;

				IntPtr hInst = Marshal.GetHINSTANCE(GetType().Module);
				IntPtr iconHandle = ExtractIcon(hInst, filePath, iconIndex);

				// Return a cloned Icon, because we have to free the original ourselves.
				Icon ico = Icon.FromHandle(iconHandle);
				Icon clone = (Icon) ico.Clone();
				ico.Dispose();
				DestroyIcon(iconHandle);
				return clone;
			}
			catch
			{
				// Silently fail and return a null.
				return null;
			}
		}

		/// <summary>
		/// Gets the <c>Icon</c> for the specified <paramref name="filePath"/>
		/// </summary>
		/// <param name="filePath">Path to the file</param>
		/// <returns><c>Icon</c> for the specified <paramref name="filePath"/></returns>
		public Icon GetIcon(string filePath)
		{
			try
			{
				// Extract icon 0 from filePath.
				Icon result = Icon.ExtractAssociatedIcon(filePath);
				return result;
			}
			catch
			{
				// Silently fail and return a null.
				return null;
			}
		}

		/// <summary>
		/// Return first icon for an executible file.
		/// </summary>
		/// <remarks>
		/// This overload requires an icon index.
		/// </remarks>
		public Icon GetIcon(string filePath, int iconIndex)
		{
			try
			{
				var hInst = Marshal.GetHINSTANCE(GetType().Module);
				var iconHandle = ExtractIcon(hInst, filePath, iconIndex);

				// Return a cloned Icon, because we have to free the original ourselves.
				var ico = Icon.FromHandle(iconHandle);
				var clone = (Icon) ico.Clone();
				ico.Dispose();
				DestroyIcon(iconHandle);
				return clone;
			}
			catch
			{
				// Silently fail and return a null.
				return null;
			}
		}

		#endregion
	}
}