using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

/// <summary>
/// The <see cref="FreemiumUtilities.StartupManager"/> namespace defines a Startup Manager 1 Click-Maintenance application
/// </summary>
namespace FreemiumUtilities.StartupManager
{
    /// <summary>
    /// Startup Manager 1 Click-Maintenance application <see cref="OneClickApp"/> implementation
    /// </summary>
    public class StartupManagerApp : OneClickApp
    {
        #region Instance Variables

        ProgressUpdate callback;
        CancelComplete cancelComplete;
        ScanComplete complete;
        bool fixAfterScan;

        #endregion

        #region Properties

        /// <summary>
        /// Apps to delete collection
        /// </summary>
        public List<string> AppsToDelete = new List<string>();

        /// <summary>
        /// A list of all the problems this application encountered
        /// </summary>
        public ObservableCollection<Problem> LstProblems = new ObservableCollection<Problem>();
        /// <summary>
        /// ListView items collection
        /// </summary>
        public List<ListViewItem> LstVwItemLst = new List<ListViewItem>();
        /// <summary>
        /// <see cref="FrmDetails"/> instance
        /// </summary>
        public FrmDetails FrmDetails;
        /// <summary>
        /// <see cref="FrmStartupMan"/> instance
        /// </summary>
        public FrmStartupMan FrmSrtUpMan = new FrmStartupMan();
        /// <summary>
        /// Problems count
        /// </summary>
        public override int ProblemsCount { get; set; }
        /// <summary>
        /// App execution termination flag
        /// </summary>
        public bool ABORT { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// constructor for StartupManagerApp
        /// </summary>
        public StartupManagerApp()
        {
            AppsToDelete = FileRW.ReadFile("Resources\\Startup_ToBeRemoved.csv");
        }

        #endregion

        /// <summary>
        /// convert list
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        ObservableCollection<Problem> ConvertLists(IEnumerable<ListViewItem> listData)
        {
            if (listData == null) throw new ArgumentNullException("listData");
            ObservableCollection<Problem> problems = new ObservableCollection<Problem>();

            foreach (ListViewItem lvItem in listData)
            {
                problems.Add(new Problem
                                {
                                    Name = lvItem.SubItems[1].Text,
                                    Description = lvItem.SubItems[0].Text,
                                    Location = lvItem.SubItems[4].Text
                                });
            }

            return problems;
        }

        #region Public Methods

        /// <summary>
        /// start scanning
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="complete"></param>
        /// <param name="cancelComplete"></param>
        /// <param name="fixAfterScan"></param>
        public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete,
                                       bool fixAfterScan)
        {
            ABORT = false;

            try
            {
                this.callback = callback;
                this.complete = complete;
                this.cancelComplete = cancelComplete;
                this.fixAfterScan = fixAfterScan;

                List<ListViewItem> problems = new List<ListViewItem>();
                FrmSrtUpMan.FillListview();

                for (int i = 0; i < AppsToDelete.Count; i++)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    callback((int)(i + 1 / (double)AppsToDelete.Count * 100), AppsToDelete[i]);

                    problems = FrmSrtUpMan.RemoveAppsFromReg(AppsToDelete[i]);
                    ProblemsCount = problems.Count;

                    LstVwItemLst = problems;
                    LstProblems = ConvertLists(problems);
                }
                FrmDetails = new FrmDetails(problems);
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
            complete(fixAfterScan);
        }

        /// <summary>
        /// cancel scanning
        /// </summary>
        public override void CancelScan()
        {
            ABORT = true;
        }

        /// <summary>
        /// start fixing
        /// </summary>
        /// <param name="callback"></param>
        public override void StartFix(ProgressUpdate callback)
        {
            ABORT = false;

            try
            {
                this.callback = callback;

                //Scan and delete
                foreach (ListViewItem item in FrmSrtUpMan.LstProblems)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }
                    FrmSrtUpMan.DeleteItem(item.SubItems[1].Text);
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
            complete(fixAfterScan);
        }

        /// <summary>
        /// cancel fixing
        /// </summary>
        public override void CancelFix()
        {
            ABORT = true;
        }

        #endregion
    }
}