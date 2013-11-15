using FreemiumUtilities.Infrastructure;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Data.SQLite;
using System.IO;

namespace FreemiumUtilities.MozillaToolbarRemover
{
    public class MozillaToolbarAndAddOnRemoverApp : OneClickApp
    {
        #region Instance Variables

        ProgressUpdate callback;
        CancelComplete cancelComplete;
        ScanComplete complete;
        bool fixAfterScan;
        BackgroundWorker fixBackgroundWorker;
        int progressMax;
        private bool isCancel = false;

        BackgroundWorker scanBackgroundWorker;
        List<string> FirefoxToolbarList = new List<string>();

        /// <summary>
        /// Problems count
        /// </summary>
        public override int ProblemsCount { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Firefox toolbars. It should be called
        /// after <code>StartScan</code>method.
        /// </summary>
        public List<FirefoxExtension> MozillaToolbarAndAddOnFound { get; set; }

        #endregion



        #region Constructors

        /// <summary>
        /// Creates a new instance of spyware remover tool
        /// </summary>
        /// <param name="defFilePath">The path to the file containing spyware file names to check for</param>
        public MozillaToolbarAndAddOnRemoverApp()
        {
            MozillaToolbarAndAddOnFound = new List<FirefoxExtension>();
        }

        #endregion

        #region Scan

        /// <summary>
        /// Starts the scanning process
        /// </summary>
        /// <param name="callback">The callback method to update progress</param>
        public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete,
                                       bool fixAfterScan)
        {
            this.callback = callback;
            this.complete = complete;
            this.cancelComplete = cancelComplete;
            this.fixAfterScan = fixAfterScan;

            isCancel = false;
            scanBackgroundWorker = new BackgroundWorker();
            scanBackgroundWorker.WorkerSupportsCancellation = true;
            scanBackgroundWorker.WorkerReportsProgress = true;
            scanBackgroundWorker.DoWork += ScanBackgroundWorkerDoWork;
            scanBackgroundWorker.RunWorkerCompleted += scanBackgroundWorker_RunWorkerCompleted;
            scanBackgroundWorker.ProgressChanged += scanBackgroundWorker_ProgressChanged;

            scanBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// cancel the scanning process
        /// </summary>
        public override void CancelScan()
        {
            isCancel = true;
            if (scanBackgroundWorker.IsBusy)
                scanBackgroundWorker.CancelAsync(); //makes the backgroundworker stop
        }

        /// <summary>
        /// handle DoWork event to start scanning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScanBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            progressMax = 1;
            MozillaToolbarAndAddOnFound.Clear();
            try
            {
                MozillaToolbarAndAddOnFound = FirefoxExtension.List().ToList();
                ProblemsCount = MozillaToolbarAndAddOnFound.Count;
                scanBackgroundWorker.ReportProgress(100, "Done"); //reports a percentage between 0 and 100
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }


        /// <summary>
        /// handle ProgressChanged event to show progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void scanBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!isCancel)
            {
                callback(e.ProgressPercentage, e.UserState.ToString());
            }
        }

        /// <summary>
        /// handle RunWorkerCompleted to finish scanning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void scanBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || isCancel)
            {
                cancelComplete();
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                complete(fixAfterScan);
            }
        }

        #endregion

        #region Fix
        /// <summary>
        /// Deletes found spyware
        /// </summary>
        public override void StartFix(ProgressUpdate callback)
        {
            isCancel = false;
            this.callback = callback;
            fixBackgroundWorker = new BackgroundWorker();
            fixBackgroundWorker.WorkerSupportsCancellation = true;
            fixBackgroundWorker.WorkerReportsProgress = true;
            fixBackgroundWorker.DoWork += FixBackgroundWorkerDoWork;
            fixBackgroundWorker.RunWorkerCompleted += fixBackgroundWorker_RunWorkerCompleted;
            fixBackgroundWorker.ProgressChanged += fixBackgroundWorker_ProgressChanged;

            fixBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// cancel spyware deleting
        /// </summary>
        public override void CancelFix()
        {
            isCancel = true;
            if (fixBackgroundWorker.IsBusy)
                fixBackgroundWorker.CancelAsync(); //makes the backgroundworker stop
        }


        /// <summary>
        /// handle DoWork event to start fixing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FixBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FirefoxExtension.ReportProgressEvent += new FirefoxExtension.ReportProgressEventHandler(FirefoxExtension_ReportProgressEvent);
                FirefoxExtension.SaveChanges(MozillaToolbarAndAddOnFound);
                FirefoxExtension.ReportProgressEvent -= FirefoxExtension_ReportProgressEvent;
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        void FirefoxExtension_ReportProgressEvent(int progressValue, string name)
        {
            fixBackgroundWorker.ReportProgress(progressValue, name); //reports a percentage between 0 and 100
        }

        /// <summary>
        /// rename registry entry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        static void RenameKey(RegistryKey parent, string oldName, string newName)
        {
            try
            {
                using (RegistryKey oldKey = parent.OpenSubKey(oldName))
                {
                    using (RegistryKey newKey = parent.CreateSubKey(newName))
                    {

                        if (oldKey != null)
                            foreach (string valueName in oldKey.GetValueNames())
                            {
                                object value = oldKey.GetValue(valueName);
                                RegistryValueKind kind = oldKey.GetValueKind(valueName);

                                if (newKey != null) newKey.SetValue(valueName, value, kind);
                            }

                        parent.DeleteSubKeyTree(oldName);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// moves reg value from Subkey of disabled entries to Subkey of enabled entries
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="valName"></param>
        static void moveToEnabled(RegistryKey parent, string valName)
        {
            try
            {
                using (RegistryKey enabledKey = parent.OpenSubKey("Toolbar", true))
                {
                    using (RegistryKey disabledKey = parent.OpenSubKey("ToolbarDisabled", true))
                    {
                        object valValue = disabledKey.GetValue(valName);
                        RegistryValueKind valKind = disabledKey.GetValueKind(valName);

                        enabledKey.SetValue(valName, valValue, valKind);
                        disabledKey.DeleteValue(valName);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// moves reg value from Subkey of enabled entries to Subkey of disabled entries
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="valName"></param>
        static void moveToDisabled(RegistryKey parent, string valName)
        {
            try
            {
                using (RegistryKey enabledKey = parent.OpenSubKey("Toolbar", true))
                {
                    using (RegistryKey disabledKey = parent.OpenSubKey("ToolbarDisabled", true))
                    {
                        object valValue = enabledKey.GetValue(valName);
                        RegistryValueKind valKind = enabledKey.GetValueKind(valName);

                        disabledKey.SetValue(valName, valValue, valKind);
                        enabledKey.DeleteValue(valName);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle ProgressChanged event to show progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fixBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!isCancel)
            {
                callback(e.ProgressPercentage, e.UserState.ToString());
            }
        }

        /// <summary>
        /// handle RunWorkerCompleted event to finish fixing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fixBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || isCancel)
            {
                //TODO: Think is it needed here!
                cancelComplete();
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                complete(fixAfterScan);
            }
        }

        #endregion
    }
}
