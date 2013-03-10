using System;
using System.Threading;

namespace ScanFiles
{
    public class ScanManager
    {
        private Thread scanThread;
        private bool? scanResult = null;
        private Exception unmanagedException = null;

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
                extFilterString = "";

                foreach (string ext in extFilter)
                {
                    if (ext.Length > 0)
                        extFilterString = extFilterString + ext + "|";
                }

                if (extFilterString.Length == 0)
                    extFilterString = null;
                else
                    extFilterString = extFilterString.Remove(extFilterString.Length - 1);
            }

            ThreadStart ts =
                delegate
                    {
                        try
                        {
                            CSWrapper.SetSearchInRecycledBin(recycleBinSearch);
                            scanResult = CSWrapper.ScanDrive(drive, updateProgress, itemFound, extFilterString, advancedSearch);
                        }
                        catch(Exception exc)
                        {
                            scanResult = false;
                            //if(!(exc is ThreadAbortException))
                                unmanagedException = exc;
                        }
                        finally
                        {
                            scanThread = null;
                        }
                    };
            scanThread = new Thread(ts);
            scanThread.Start();
        }

        public void Abort()
        {
            if(scanThread != null)
            {
                if(!scanThread.Join(5000))
                {
                    scanThread.Abort();
                }
            }
        }

        public Exception InnerException
        {
            get
            {
                return unmanagedException;
            }
        }

        public bool IsFinished
        {
            get
            {
                return scanThread == null && scanResult != null;
            }
        }

        /// <summary>
        /// null - if not finished
        /// true - ok
        /// false - error in ScanDrive
        /// </summary>
        public bool? ScanResult
        {
            get
            {
                return scanResult;
            }
        }
    }
}
