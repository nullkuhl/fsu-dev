using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Timer=System.Windows.Forms.Timer;

namespace ScanFiles
{
    public partial class Form1 : Form
    {
        private int currentProgress = 0;
        private bool needAbort = false;
        private string currentDrive;
        private DataTable foundFilesTable;
        private readonly ScanManager scanMgr = new ScanManager();
        private IList<FileToRestore> filesToRestore = new List<FileToRestore>();
        private bool changed = false; //by count?
        private DateTime startTime;
        private int recoverableCount = 0;
        private bool isClosing = false;

        public abstract class Constants
        {
            public const string FilePathFieldName = "filePath";
            public const string FileIdFieldName = "fileId";
            public const string RecoverableFieldName = "isRecoverable";
            public const string SizeFieldName = "size";

            public const string ButtonColumn = "restoreButtonColumn";
            public const string FilePathColumn = "filePathColumn";
            public const string FileIdColumn = "fileIdColumn";
            public const string RecoverableColumn = "isRecoverableColumn";
            public const string SizeColumn = "sizeColumn";
        }

        private class FileToRestore
        {
            public FileToRestore(string path, ulong id, bool canRecover, uint size)
            {
                FilePath = path;
                FileId = id;
                IsRecoverable = canRecover;
                Size = size;
            }

            public readonly string FilePath;
            public readonly ulong FileId;
            public readonly bool IsRecoverable;
            public readonly uint Size;
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] drives = CSWrapper.GetDrivesList();
                drivesCombo.DataSource = drives;
            } 
            catch(Exception exc)
            {
                MessageBox.Show("exc=" + exc, "Error");
            }

            FilesToTable();
            filesGrid.AutoGenerateColumns = false;
//            filesGrid.Columns[Constants.FilePathColumn].SortMode = DataGridViewColumnSortMode.Automatic;
//            filesGrid.Columns[Constants.ButtonColumn].Width = 70;
//            filesGrid.Columns[Constants.RecoverableColumn].Width = 50;
//            filesGrid.Columns[Constants.SizeColumn].FillWeight = 70;
//            filesGrid.Columns[Constants.FilePathColumn].Width = 250;
//            filesGrid.Columns[Constants.FileIdColumn].Width = 80;
//            filesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.;
            filesGrid.DataSource = foundFilesTable;

        }

        private bool UpdateProgress(int progress)
        {
            if(needAbort)
                return false;

            currentProgress = progress;
            return !needAbort;
        }

        private bool ItemFound(string filePath, ulong fileId, bool flag, uint size)
        {
            if (needAbort)
                return false;
            lock (filesToRestore)
            {
                changed = true;
                filesToRestore.Add(new FileToRestore(filePath, fileId, flag, size));
                if(flag)
                    recoverableCount++;
            }
            return !needAbort;
        }

        private void SwitchMode(bool isRunning)
        {
            if(isRunning)
            {
                startScanBtn.Enabled = false;
                abortScanBtn.Enabled = true;                
            }
            else
            {
                startScanBtn.Enabled = true;
                abortScanBtn.Enabled = false;
            }
        }

        private void abortScanBtn_Click(object sender, EventArgs e)
        {
            scanProgress.Text = "Aborting..";
            needAbort = true;
            scanMgr.Abort();
        }

        private void startScanBtn_Click(object sender, EventArgs e)
        {
            needAbort = false;
            progressBar1.Value = 0;
            currentProgress = 0;
            recoverableCount = 0;
            filesGrid.DataSource = null;
            filesToRestore = new List<FileToRestore>();
            changed = true;
            currentDrive = "" + drivesCombo.SelectedItem;
            SwitchMode(true);

            startTime = DateTime.Now;
            scanMgr.StartScan(currentDrive, UpdateProgress, ItemFound, cbxImages.Checked ? CSWrapper.ImagesFilter : null, cbxAdvancedSearch.Checked, cbxRecycleBinSearch.Checked);
            scanTimer.Start();
        }


        private void scanTimer_Tick(object sender, EventArgs e)
        {
            if (!needAbort)
            {
                scanProgress.Text = "scanning drive " + currentDrive +
                                    " found " + filesToRestore.Count + " files " +
                                    " (recoverable - " + recoverableCount + ")";
//                +
//                                    "\r\nprogress ... " + currentProgress + " at " +
//                                    new DateTime((DateTime.Now - startTime).Ticks).ToString("mm:ss.fff");
                if(currentProgress == -1)
                {
                    currentProgress = 100;
                }
                progressBar1.Value = currentProgress;

                FilesToTable();
                filesGrid.DataSource = foundFilesTable;
            }
            if(scanMgr.IsFinished)
            {
                OnFinish();
            }
        }

        private void OnFinish()
        {
            scanTimer.Stop();
            SwitchMode(false);
            FilesToTable();
            filesGrid.DataSource = foundFilesTable;

            if (scanMgr.InnerException != null)
            {
                MessageBox.Show("Exception Occured: " + scanMgr.InnerException, "Scan Finished");
            }
            else if (scanMgr.ScanResult == false)
            {
                MessageBox.Show("Finished with error", "Scan Finished");
            }
            else
            {
                MessageBox.Show("Finished successfully", "Scan Finished");
            }
            scanProgress.Text = "Finished. Total files = " + filesToRestore.Count +
                                " (recoverable - " + recoverableCount + ")";
        }


        public void FilesToTable()
        {
            lock (filesToRestore)
            {
                if (changed)
                {
                    DataTable result = new DataTable();
                    result.Columns.Add(Constants.FilePathFieldName, typeof(string));
                    result.Columns.Add(Constants.FileIdFieldName, typeof(ulong));
                    result.Columns.Add(Constants.RecoverableFieldName, typeof(bool));
                    result.Columns.Add(Constants.SizeFieldName, typeof(uint));

                    foreach (FileToRestore fileToRestore in filesToRestore)
                    {
                        DataRow row = result.NewRow();
                        row[Constants.FilePathFieldName] = fileToRestore.FilePath;
                        row[Constants.FileIdFieldName] = fileToRestore.FileId;
                        row[Constants.RecoverableFieldName] = fileToRestore.IsRecoverable;
                        row[Constants.SizeFieldName] = fileToRestore.Size;
                        result.Rows.Add(row);
                    }
                    foundFilesTable = result;
                    changed = false;
                }
            }
        }

        private void filesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Constants.ButtonColumn == filesGrid.Columns[e.ColumnIndex].Name
                && e.RowIndex >= 0)
            {
                int indexFilePath = filesGrid.Columns[Constants.FilePathColumn].DisplayIndex;
                string filePath = (string)filesGrid.Rows[e.RowIndex].Cells[indexFilePath].Value;
                RecoverPathDialog dlg = new RecoverPathDialog(filePath);
                if (dlg.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        int indexFileId = filesGrid.Columns[Constants.FileIdColumn].DisplayIndex;
                        ulong fileId = (ulong) filesGrid.Rows[e.RowIndex].Cells[indexFileId].Value;

                        string message = null;
                        ThreadStart ts =
                            delegate
                                {
                                    try
                                    {
                                        bool b = CSWrapper.RecoverFile(fileId, dlg.SelectedPath);
                                        message = b ? "Success" : "Failure";
                                    }
                                    catch(Exception exc)
                                    {
                                        message = "Unexpected error: " + exc;
                                    }
                                };
                        Thread thread = new Thread(ts);
                        thread.Start();
                        Timer timer = new Timer();
                        timer.Interval = 200;
                        timer.Tick +=
                            delegate
                                {
                                    if(isClosing)
                                    {
                                        timer.Stop();
                                        thread.Abort();
                                        return;
                                    }
                                    if(!thread.IsAlive) //finished
                                    {
                                        timer.Stop();                                        
                                        MessageBox.Show(message, "File Recovery - " + fileId);
                                    }
                                };
                        timer.Start();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("exc=" + exc, "Error");
                    }
                }
                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            needAbort = true;
            scanMgr.Abort();
            scanTimer.Stop();
            isClosing = true;
        }
    }
}
