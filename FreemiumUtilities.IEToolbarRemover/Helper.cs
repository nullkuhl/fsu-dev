using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Windows;

namespace FreemiumUtilities.IEToolbarRemover
{
    public static class Helper
    {
        /// <summary>
        /// Gets name from Clsid
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        public static string GetNameFromClsid(string clsid)
        {
            object name = Registry.GetValue("HKEY_CLASSES_ROOT\\CLSID\\" + clsid, string.Empty, null);
            if (name == null || name.ToString() == "")
                name = Registry.GetValue("HKEY_CLASSES_ROOT\\CLSID\\" + clsid + "\\ProgID", string.Empty, null);
            return (string)name;
        }

        /// <summary>
        /// Gets path from Clsid
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        public static string GetPathFromClsid(string clsid)
        {
            return (string)Registry.GetValue("HKEY_CLASSES_ROOT\\CLSID\\" + clsid + "\\InprocServer32", string.Empty, null);
        }

        /// <summary>
        /// Shows exception
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowException(Exception ex)
        {
#if DEBUG
            MessageBox.Show("Error:\r\n" + ex.Message + "\r\nStack Trace:\r\n" + ex.StackTrace);
#else
			MessageBox.Show("Error:\r\n" + ex.Message);
#endif
        }
    }
}
