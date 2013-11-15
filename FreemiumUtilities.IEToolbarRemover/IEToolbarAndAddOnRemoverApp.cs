using FreemiumUtilities.Infrastructure;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace FreemiumUtilities.IEToolbarRemover
{
    public class IEToolbarAndAddOnRemoverApp : OneClickApp
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
        List<string> IEToolbarList = new List<string>();

        /// <summary>
        /// Problems count
        /// </summary>
        public override int ProblemsCount { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the IE toolbars. It should be called
        /// after <code>StartScan</code>method.
        /// </summary>
        public List<ExplorerToolbarAndAddOn> IEToolbarAndAddOnFound { get; set; }

        #endregion



        #region Constructors

        /// <summary>
        /// Creates a new instance of spyware remover tool
        /// </summary>
        /// <param name="defFilePath">The path to the file containing spyware file names to check for</param>
        public IEToolbarAndAddOnRemoverApp()
        {
            IEToolbarAndAddOnFound = new List<ExplorerToolbarAndAddOn>();
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
            int i = 0;
            IEToolbarAndAddOnFound.Clear();

            try
            {
                //Get toolbars
                using (RegistryKey ieRegKey = Registry.LocalMachine.OpenSubKey("Software")
                    .OpenSubKey("Microsoft").OpenSubKey("Internet Explorer", true))
                {
                    if (ieRegKey.OpenSubKey("Toolbar") == null)
                        ieRegKey.CreateSubKey("Toolbar");
                    if (ieRegKey.OpenSubKey("ToolbarDisabled") == null)
                        ieRegKey.CreateSubKey("ToolbarDisabled");

                    string[] enabledCLSIDs = ieRegKey.OpenSubKey("Toolbar").GetValueNames();
                    string[] disabledCLSIDs = ieRegKey.OpenSubKey("ToolbarDisabled").GetValueNames();

                    progressMax = 4;//enabledCLSIDs.Length + disabledCLSIDs.Length;

                    foreach (string clsid in enabledCLSIDs)
                    {
                        if (scanBackgroundWorker.CancellationPending) //checks for cancel request
                        {
                            e.Cancel = true;
                            return;
                        }
                        string name = Helper.GetNameFromClsid(clsid);
                        string path = Helper.GetPathFromClsid(clsid);
                        AddIEToolbarAndAddOn(clsid, name, path, true, i++, "ToolBar");
                    }
                    foreach (string clsid in disabledCLSIDs)
                    {
                        if (scanBackgroundWorker.CancellationPending) //checks for cancel request
                        {
                            e.Cancel = true;
                            return;
                        }
                        string name = Helper.GetNameFromClsid(clsid);
                        string path = Helper.GetPathFromClsid(clsid);
                        AddIEToolbarAndAddOn(clsid, name, path, false, i++, "ToolBar");
                    }
                }
            }
            catch
            {
            }

            //Get Extensions
            try
            {
                if (Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft")
                    .OpenSubKey("Internet Explorer").OpenSubKey("Extensions") != null)
                {

                    string[] extIds = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft")
                        .OpenSubKey("Internet Explorer").OpenSubKey("Extensions").GetSubKeyNames();

                    foreach (string id in extIds)
                    {
                        string name = null;
                        string path = "(unable to retrieve path)";

                        object clsid = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                            .OpenSubKey("Extensions").OpenSubKey(id).GetValue("ClsidExtension");

                        if (clsid == null)
                            clsid = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                            .OpenSubKey("Extensions").OpenSubKey(id).GetValue("CLSID");

                        if (clsid != null)
                        {
                            name = Helper.GetNameFromClsid((string)clsid);
                            path = Helper.GetPathFromClsid((string)clsid);
                        }
                        else
                        {
                            var exec =
                                (string)Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                                            .OpenSubKey("Extensions").OpenSubKey(id).GetValue("Exec");
                            if (exec != null) path = exec;
                        }

                        if (string.IsNullOrEmpty(name))
                            name =
                                (string)Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                                            .OpenSubKey("Extensions").OpenSubKey(id).GetValue("ButtonText");

                        if (name == null)
                            name =
                                (string)Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                                            .OpenSubKey("Extensions").OpenSubKey(id).GetValue("MenuText");

                        RegistryKey winCurrVer = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                        .OpenSubKey("CurrentVersion", true);

                        if (winCurrVer.OpenSubKey("Ext") == null)
                            winCurrVer.CreateSubKey("Ext");
                        if (winCurrVer.OpenSubKey("Ext").OpenSubKey("Settings") == null)
                            winCurrVer.OpenSubKey("Ext", true).CreateSubKey("Settings");

                        bool enabled = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                        .OpenSubKey("CurrentVersion").OpenSubKey("Ext").OpenSubKey("Settings").OpenSubKey(id) == null;

                        AddIEToolbarAndAddOn(id, name, path, enabled, i++, "Extension");
                    }
                }
            }
            catch
            {
            }

            //Get BHOs
            try
            {
                string[] CLSIDs = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                    .OpenSubKey("CurrentVersion").OpenSubKey("explorer").OpenSubKey("Browser Helper Objects").GetSubKeyNames();

                foreach (string clsid in CLSIDs)
                {
                    try
                    {
                        bool enabled = !clsid.StartsWith("Disabled:");

                        string name = Helper.GetNameFromClsid(enabled ? clsid : clsid.Substring(9));
                        if (string.IsNullOrEmpty(name)) continue;

                        string path = Helper.GetPathFromClsid(enabled ? clsid : clsid.Substring(9));

                        AddIEToolbarAndAddOn(enabled ? clsid : clsid.Substring(9), name, path, enabled, i++, "BHO");
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }


        void AddIEToolbarAndAddOn(string id, string name, string path, bool enabled, int sizeScanned, string typeName)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return;
                IEToolbarAndAddOnFound.Add(new ExplorerToolbarAndAddOn(id, name, path, enabled, typeName));
                ProblemsCount++;
                int progressPercentage = (int)((double)sizeScanned / progressMax * 100);
                scanBackgroundWorker.ReportProgress(progressPercentage, name); //reports a percentage between 0 and 100
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
                //List<ExplorerToolbarAndAddOn> disableExplorerToolbarAndAddOns = IEToolbarAndAddOnFound.Where(wh => wh.IsEnabled = false).ToList();
                int IEToolbarAndAddOnFoundCount = IEToolbarAndAddOnFound.Count;//disableExplorerToolbarAndAddOns.Count;
                for (int i = 0; i < IEToolbarAndAddOnFoundCount; i++)
                {
                    if (fixBackgroundWorker.CancellationPending) //checks for cancel request
                    {
                        e.Cancel = true;
                        return;
                    }

                    ExplorerToolbarAndAddOn ie = IEToolbarAndAddOnFound[i];
                    int progressPercentage = (int)((double)(i + 1) / IEToolbarAndAddOnFoundCount * 100);
                    fixBackgroundWorker.ReportProgress(progressPercentage, ie.Name); //reports a percentage between 0 and 100

                    if (ie.TypeName == "ToolBar")
                    {
                        try
                        {
                            using (RegistryKey ieKey = Registry.LocalMachine.OpenSubKey("Software")
                            .OpenSubKey("Microsoft").OpenSubKey("Internet Explorer"))
                            {
                                if (ie.IsEnabled)
                                    moveToEnabled(ieKey, ie.Id);
                                else
                                    moveToDisabled(ieKey, ie.Id);
                            }
                        }
                        catch
                        {
                        }                     
                    }
                    else if (ie.TypeName == "Extension")
                    {
                        try
                        {
                            if (ie.IsEnabled)
                            {
                                Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                    .OpenSubKey("CurrentVersion").OpenSubKey("Ext").OpenSubKey("Settings", true).DeleteSubKeyTree(ie.Id);
                            }
                            else
                            {
                                RegistryKey newKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                    .OpenSubKey("CurrentVersion").OpenSubKey("Ext").OpenSubKey("Settings", true).CreateSubKey(ie.Id);

                                if (newKey != null)
                                {
                                    newKey.SetValue("Flags", "1", RegistryValueKind.DWord);
                                    newKey.SetValue("Version", "*", RegistryValueKind.String);
                                }
                            }
                        }
                        catch
                        {                            
                        }
                    }
                    else if (ie.TypeName == "BHO")
                    {
                        try
                        {
                            using (RegistryKey bhosKey = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                .OpenSubKey("CurrentVersion").OpenSubKey("explorer").OpenSubKey("Browser Helper Objects", true))
                            {
                                if (ie.IsEnabled)
                                    RenameKey(bhosKey, "Disabled:" + ie.Id, ie.Id);
                                else
                                    RenameKey(bhosKey, ie.Id, "Disabled:" + ie.Id);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
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
