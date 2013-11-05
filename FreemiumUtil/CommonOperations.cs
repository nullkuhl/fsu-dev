using System;
using System.Diagnostics;

namespace FreemiumUtil
{
    /// <summary>
    /// Class for common operations
    /// </summary>
    public static class CommonOperations
    {
        /// <summary>
        /// Opens Url
        /// </summary>
        /// <param name="url">url address</param>
        public static void OpenUrl(string url)
        {
            ProcessStartInfo psi;
            try
            {
                psi = new ProcessStartInfo(url);
                Process.Start(psi);
            }
            catch
            {
                try
                {
                    psi = new ProcessStartInfo("iexplore", "-new " + url);
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
