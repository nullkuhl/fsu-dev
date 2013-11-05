using System;
using System.Threading;

namespace FileUndelete
{
    /// <summary>
    /// Scan manager
    /// </summary>
    public class ScanManager
    {
        bool? scanResult;
        Thread scanThread;
        Exception unmanagedException;

        /// <summary>
        /// Inner exception
        /// </summary>
        public Exception InnerException
        {
            get { return unmanagedException; }
        }

        /// <summary>
        /// check if scan is finished
        /// </summary>
        public bool IsFinished
        {
            get { return scanThread == null && scanResult != null; }
        }

        /// <summary>
        /// null - if not finished
        /// true - ok
        /// false - error in ScanDrive
        /// </summary>
        public bool? ScanResult
        {
            get { return scanResult; }
        }

        /// <summary>
        /// start scan for deleted files
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="updateProgress"></param>
        /// <param name="itemFound"></param>
        /// <param name="extFilter"></param>
        /// <param name="advancedSearch"></param>
        /// <param name="recycleBinSearch"></param>
        public void StartScan(string drive,
                              CSWrapper.UpdateProgressCallback updateProgress,
                              CSWrapper.RestorableItemFoundCallback itemFound,
                              string[] extFilter,
                              bool advancedSearch,
                              bool recycleBinSearch)
        {
            if (scanThread != null)
            {
                throw new ApplicationException("Scanning has already started.");
            }

            //reset
            unmanagedException = null;
            scanResult = null;
            string extFilterString = null;

            if (extFilter != null && extFilter.Length > 0)
            {
                extFilterString = string.Empty;

                foreach (string ext in extFilter)
                {
                    if (ext.Length > 0)
                        extFilterString = extFilterString + ext + "|";
                }

                extFilterString = extFilterString.Length == 0 ? null : extFilterString.Remove(extFilterString.Length - 1);
            }

            ThreadStart ts =
                delegate
                {
                    try
                    {
                        CSWrapper.SetSearchInRecycledBin(recycleBinSearch);
                        scanResult = CSWrapper.ScanDrive(drive, updateProgress, itemFound, extFilterString, advancedSearch);
                    }
                    catch (Exception exc)
                    {
                        scanResult = false;
                        unmanagedException = exc;
                    }
                    finally
                    {
                        scanThread = null;
                    }
                };
            scanThread = new Thread(ts);
            scanThread.SetApartmentState(ApartmentState.STA);
            scanThread.Start();
        }

        /// <summary>
        /// abort scan for deleted files
        /// </summary>
        public void Abort()
        {
            if (scanThread != null)
            {
                if (!scanThread.Join(5000))
                {
                    scanThread.Abort();
                }
            }
        }
    }
}