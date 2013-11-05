using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.Win32;
using System.Drawing;
using System.Security.AccessControl;

namespace Context_Menu_Manager
{
    /// <summary>
    /// Context menu manager knot main form
    /// </summary>
    public partial class FormMain : Form
    {
        //////////
        ImageList New_LargeIconeList = new ImageList();
        ImageList sendTO_LargeIconeList = new ImageList();

        List<string> listIcons = new List<string>();
        bool flagNewListChecked = false;
        bool flagFileandFolderChecked = false;
        ///////////
        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// App path
        /// </summary>
        public string AppPath
        {
            get { return Path.GetFullPath("."); }
        }

        string ExsKeys
        {
            get
            {
                if (File.Exists(AppPath + "\\context_items.bak"))
                    return File.ReadAllText(AppPath + "\\context_items.bak");

                return "";
                //return File.Exists(AppPath + "\\context_items.bak") ? File.ReadAllText(AppPath + "\\context_items.bak") : "";
            }
        }

        string ExsExtns
        {
            get
            {
                if (File.Exists(AppPath + "\\context_new_items.bak"))
                {
                    return File.ReadAllText(AppPath + "\\context_new_items.bak");
                }
                return "";
            }
        }

        string IconsUnchecked
        {
            get
            {
                if (File.Exists(AppPath + "\\context_new_items_icons.bak"))
                {
                    return File.ReadAllText(AppPath + "\\context_new_items_icons.bak");
                }
                return "";
            }
        }

        IEnumerable<FileSystemInfo> SendToFiles
        {
            get
            {
                if (!Directory.Exists(AppPath + @"\SendTo\"))
                    return new FileSystemInfo[] { };
                var di = new DirectoryInfo(AppPath + @"\SendTo\");
                return di.GetFileSystemInfos();
            }
        }

        /// <summary>
        /// handle Click event to remove item from send to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void removeSendToButton_Click3(object sender, EventArgs e)
        {
            try
            {
                if (sendToListView.SelectedItems.Count == 1)
                {
                    if (rm != null)
                    {
                        DialogResult result = MessageBox.Show(
                            String.Format(rm.GetString("sure_remove"), sendToListView.SelectedItems[0].Text), rm.GetString("confirm_delete"),
                            MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {


                            ListViewItem lvi = sendToListView.SelectedItems[0];
                            //if (lvi.Checked)
                            //{

                            //    lvi.Checked = false;
                            //}
                            //else
                            //{

                            File.Delete(lvi.SubItems[2].Text);
                            DeleteTempSendToFile(lvi.SubItems[1].Text);
                            sendToListView.Items.Remove(lvi);
                            //}
                            //for (int i = 0; i < sendToListView.Items.Count; i++)
                            //{
                            //    if (sendToListView.Items[i].Checked)
                            //    {
                            //        if (!IsTempPath(sendToListView.Items[i].SubItems[2].Text))
                            //        {
                            //            File.Delete(sendToListView.Items[i].SubItems[2].Text);
                            //        }
                            //        else
                            //        {
                            //   DeleteTempSendToFile(sendToListView.Items[i].SubItems[1].Text);
                            //            //sendToListView.Items[i].Remove();
                            //        }
                            //    }
                            //}
                            //tabPageSendTo_Enter(null, null);
                            ////////////////
                            ///////////////////////////

                        }
                    }
                }
                else
                    MessageBox.Show(rm.GetString("select_item"));
            }
            catch
            {
            }
        }

        /// <summary>
        /// initialize FormMain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));

        }

        /// <summary>
        /// handle FormClosing event to save new setting before closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFilesAndFolders();
            SaveNewFiles();
            SaveSendTo();
        }

        bool IsTempKey(string mode, string keyname)
        {
            var result = false;
            foreach (var s in ExsKeys.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (s == "") continue;
                var rk = s.Split('|');
                if (rk[0] != mode || keyname != rk[4]) continue;
                result = true;
                break;
            }
            return result;
        }

        void DeleteTempKey(string mode, string keyname)
        {
            var sb = new StringBuilder();
            foreach (var s in ExsKeys.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (s == "") continue;
                var rk = s.Split('|');
                if (rk[0] != mode || keyname != rk[1])
                {
                    sb.AppendLine(s);
                }
            }
            if (File.Exists(AppPath + "\\context_items.bak"))
            {
                File.Delete(AppPath + "\\context_items.bak");
            }
            if (sb.ToString() != "")
                File.WriteAllText(AppPath + "\\context_items.bak", sb.ToString());
        }

        bool IsTempExtension(string extn)
        {
            var result = false;
            foreach (var s in ExsExtns.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (s == "") continue;
                var rk = s.Split('|');
                if (rk[0] != extn) continue;
                result = true;
                break;
            }
            return result;
        }

        void DeleteTempExtension(string extn)
        {
            var sb = new StringBuilder();
            foreach (var s in ExsExtns.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (s == "") continue;
                var rk = s.Split('|');
                if (rk[0] != extn)
                {
                    sb.AppendLine(s);
                }
            }
            if (File.Exists(AppPath + "\\context_new_items.bak"))
            {
                File.Delete(AppPath + "\\context_new_items.bak");
            }
            if (sb.ToString() != "")
                File.WriteAllText(AppPath + "\\context_new_items.bak", sb.ToString());
        }

        static string GetCorrectSendToPath()
        {
            if (Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Microsoft\Windows\SendTo\"))
                return Environment.GetEnvironmentVariable("APPDATA") + @"\Microsoft\Windows\SendTo\";
            return @"C:\Documents and Settings\" + Environment.UserName + "\\SendTo\\";
        }

        static bool IsTempPath(string path)
        {
            return File.Exists(path);
        }

        void DeleteTempSendToFile(string fileName)
        {
            if (File.Exists(AppPath + @"\SendTo\" + fileName))
                File.Delete(AppPath + @"\SendTo\" + fileName);
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            var resourceManager = new ResourceManager("Context_Menu_Manager.Resources", typeof(FormMain).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;

            tabPageFilesFolders.Text = resourceManager.GetString("files_folders", culture);
            clhFirst.Text = resourceManager.GetString("column_header", culture);
            clhSecond.Text = resourceManager.GetString("column_header", culture);
            removeFilesFoldersButton.Text = resourceManager.GetString("remove", culture);
            tabPageNew.Text = resourceManager.GetString("new", culture);
            SaveFilesFolders.Text = resourceManager.GetString("save", culture);

            lblSelect.Text = resourceManager.GetString("click_check_mark", culture);

            removeNewButton.Text = resourceManager.GetString("remove", culture);
            tabPageSendTo.Text = resourceManager.GetString("send_to", culture);
            lblChoose.Text = resourceManager.GetString("click_check_mark", culture);
            SaveNew.Text = resourceManager.GetString("save", culture);

            removeSendToButton.Text = resourceManager.GetString("remove", culture);

            Text = resourceManager.GetString("context_menu_manager", culture);
            lblCheck.Text = resourceManager.GetString("click_check_mark", culture);
            ucTop.Text = resourceManager.GetString("context_menu_manager", culture);
            SaveSend.Text = resourceManager.GetString("save", culture);
        }

        /// <summary>
        /// handle Click event to save files folders list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveFilesAndFolders_Click(object sender, EventArgs e)
        {
            SaveFilesAndFolders();
        }

        /// <summary>
        /// handle Click event to save new list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveNew_Click(object sender, EventArgs e)
        {
            SaveNewFiles();
        }

        /// <summary>
        /// handle Click event to save send to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveSend_Click(object sender, EventArgs e)
        {
            SaveSendTo();
        }

        #region Saving helpers

        /// <summary>
        /// save files folders list
        /// </summary>
        void SaveFilesAndFolders()
        {
            try
            {
                //FileStream SavedKeys= new FileStream(AppPath + "\\context_items.bak", FileMode.CreateNew,FileAccess.ReadWrite);
                var savedKeys = new StringBuilder();

                for (var i = 0; i < filesFoldersListView.Items.Count; i++)
                {
                    bool flagForDirectoryShell = false;
                    string itemName = filesFoldersListView.Items[i].SubItems[0].Text;
                    string keyName = filesFoldersListView.Items[i].SubItems[4].Text;
                    string directoryKey = filesFoldersListView.Items[i].SubItems[3].Text;
                    string keyType = filesFoldersListView.Items[i].SubItems[1].Text;
                    //  if (filesFoldersListView.Items[i].SubItems[1].Text.Equals("Directory\\shell"))
                    //  {
                    //foreach (var pair in regShellPaths)
                    //{
                    //    if (pair.Value.ToString() == filesFoldersListView.Items[i].Text)
                    //    {
                    //        showText = pair.Key.ToString();
                    //        directoryKey = "Directory\\shell";
                    //        flagForDirectoryShell = true;
                    //    }
                    //}
                    //  }

                    if (!filesFoldersListView.Items[i].Checked)
                    {


                        //if (!filesFoldersListView.Items[i].SubItems[1].Text.Equals("Directory\\shell"))
                        //    savedKeys.AppendLine(filesFoldersListView.Items[i].SubItems[1].Text + "|" + filesFoldersListView.Items[i].Text +
                        //                         "|" + filesFoldersListView.Items[i].SubItems[2].Text);
                        //else
                        //    if (flagForDirectoryShell)
                        //        savedKeys.AppendLine("Directory\\shell" + "|" + showText +
                        //                        "|" + filesFoldersListView.Items[i].SubItems[2].Text);
                        //else
                        savedKeys.AppendLine(filesFoldersListView.Items[i].SubItems[1].Text + "|" + directoryKey + "|" + filesFoldersListView.Items[i].SubItems[0].Text +
                                        "|" + filesFoldersListView.Items[i].SubItems[2].Text + "|" + filesFoldersListView.Items[i].SubItems[4].Text);


                        if (!IsTempKey(keyType, keyName))
                        {
                            try
                            {
                                //if (flagForDirectoryShell)
                                //     Registry.ClassesRoot.DeleteSubKeyTree(regPaths["Directory\\shell"] + @"\" +
                                //                                     showText);
                                //else 
                                Registry.ClassesRoot.DeleteSubKeyTree(directoryKey + @"\" +
                                                                     keyName);

                            }
                            catch
                            {
                                //foreach (var pair in regShellPaths)
                                //{
                                //    if (pair.Value.ToString() == filesFoldersListView.Items[i].Text)
                                //    {
                                //        Registry.ClassesRoot.DeleteSubKeyTree(pair.Key.ToString());
                                //    }
                                //}
                            }



                        }
                    }
                    else if (IsTempKey(keyType, keyName))
                    {
                        //Create
                        RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                                directoryKey + @"\" + keyName,
                                RegistryKeyPermissionCheck.ReadWriteSubTree);
                        if (key != null)
                        {
                            if (directoryKey == @"*\shell" || directoryKey == @"Drive\shell" || directoryKey == @"Directory\shell")
                            {
                                key.SetValue("", filesFoldersListView.Items[i].SubItems[0].Text);
                                RegistryKey tmpKey = key.CreateSubKey("command", RegistryKeyPermissionCheck.ReadWriteSubTree);
                                if (tmpKey != null)
                                    tmpKey.SetValue("", filesFoldersListView.Items[i].SubItems[2].Text);
                            }
                            else
                            {
                                key.SetValue("", filesFoldersListView.Items[i].SubItems[2].Text);
                            }
                        }
                    }

                }

                if (File.Exists(AppPath + "\\context_items.bak"))
                {
                    File.Delete(AppPath + "\\context_items.bak");
                }
                if (savedKeys.ToString() != "")
                    File.WriteAllText(AppPath + "\\context_items.bak", savedKeys.ToString());
            }
            catch
            {
            }
        }

        /// <summary>
        /// save new list
        /// </summary>
        void SaveNewFiles()
        {
            try
            {
                //New Files
                StringBuilder newValue = new StringBuilder();

                var extentions = new StringBuilder();
                //var icons = new StringBuilder();

                for (int i = 0; i < newListView.Items.Count; i++)
                {
                    if (!newListView.Items[i].Checked)
                    {
                        {
                            extentions.AppendLine(paths[i] + "|" + newListView.Items[i].Text);
                            //icons.AppendLine(newListView.Items[i].Tag.ToString());
                            var key = Registry.ClassesRoot.OpenSubKey(paths[i].ToString(),
                                                                              RegistryKeyPermissionCheck.ReadWriteSubTree);
                            if (key != null && key.GetSubKeyNames().Contains("ShellNew"))
                                key.RenameSubKey("ShellNew", "old_ShellNew");

                            //if (paths[i].ToString() == ".zip")
                            //{
                            //    key = Registry.ClassesRoot.OpenSubKey(paths[i].ToString() + "//CompressedFolder",
                            //                                                  RegistryKeyPermissionCheck.ReadWriteSubTree);
                            //    key.RenameSubKey("ShellNew", "old_ShellNew");
                            //}
                        }
                    }
                    else if (IsTempExtension(paths[i].ToString()))
                    {
                        newValue.AppendLine(paths[i].ToString());
                        var key = Registry.ClassesRoot.OpenSubKey(paths[i].ToString(), RegistryKeyPermissionCheck.ReadWriteSubTree);
                        if (key != null && key.GetSubKeyNames().Contains("old_ShellNew"))
                            key.RenameSubKey("old_ShellNew", "ShellNew");
                    }
                }



                if (File.Exists(AppPath + "\\context_new_items.bak"))
                {
                    File.Delete(AppPath + "\\context_new_items.bak");
                }

                //if (File.Exists(AppPath + "\\context_new_items_icons.bak"))
                //{
                //    File.Delete(AppPath + "\\context_new_items_icons.bak");
                //}


                if (extentions.ToString() != string.Empty)
                {
                    File.WriteAllText(AppPath + "\\context_new_items.bak", extentions.ToString());
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// save send to list
        /// </summary>
        void SaveSendTo()
        {
            try
            {
                //Send To
                for (var i = 0; i < sendToListView.Items.Count; i++)
                {
                    if (!sendToListView.Items[i].Checked &&
                        !File.Exists(AppPath + @"\SendTo\" + sendToListView.Items[i].SubItems[1].Text))
                    {
                        if (!Directory.Exists(AppPath + @"\SendTo\"))
                        {
                            Directory.CreateDirectory(AppPath + @"\SendTo\");
                        }
                        try
                        {
                            File.Move(sendToListView.Items[i].SubItems[2].Text,
                                      AppPath + @"\SendTo\" + sendToListView.Items[i].SubItems[1].Text);
                        }
                        catch
                        {
                        }
                    }
                    else if (sendToListView.Items[i].Checked &&
                             File.Exists(AppPath + @"\SendTo\" + sendToListView.Items[i].SubItems[1].Text))
                    {
                        File.Move(AppPath + @"\SendTo\" + sendToListView.Items[i].SubItems[1].Text,
                                  GetCorrectSendToPath() + sendToListView.Items[i].SubItems[1].Text);
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Files & Folders

        string[] keys;
        Dictionary<string, string[]> regPaths;
        Dictionary<string, string> regShellPaths;
        // List<string,string>  regPaths;


        string CLSIDToFile(string clsid)
        {
            RegistryKey key;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(@"CLSID\" + clsid + @"\InProcServer32");
                if (key != null)
                    return key.GetValue("").ToString();
                else
                {
                    key = Registry.ClassesRoot.OpenSubKey(@"Wow6432Node\CLSID\" + clsid + @"\InProcServer32");
                    if (key != null) return key.GetValue("").ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// update file description
        /// </summary>
        /// <param name="file"></param>
        void FileDescription(string file)
        {
            fileDescriptionListView.Items.Clear();
            if (File.Exists(file))
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(file);
                fileDescriptionListView.Items.Add(rm.GetString("description"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.FileDescription);
                fileDescriptionListView.Items.Add(rm.GetString("file"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(file);
                fileDescriptionListView.Items.Add("");
                fileDescriptionListView.Items.Add(rm.GetString("version_info"));
                fileDescriptionListView.Items.Add(rm.GetString("company"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.CompanyName);
                fileDescriptionListView.Items.Add(rm.GetString("description"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.FileDescription);
                fileDescriptionListView.Items.Add(rm.GetString("version"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.FileVersion);
                fileDescriptionListView.Items.Add(rm.GetString("internal_name"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.InternalName);
                fileDescriptionListView.Items.Add(rm.GetString("copyright"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.LegalCopyright);
                fileDescriptionListView.Items.Add(rm.GetString("trademark"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.LegalTrademarks);
                fileDescriptionListView.Items.Add(rm.GetString("original_file_name"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.OriginalFilename);
                fileDescriptionListView.Items.Add(rm.GetString("product_name"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.ProductName);
                fileDescriptionListView.Items.Add(rm.GetString("product_version"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.ProductVersion);
                fileDescriptionListView.Items.Add(rm.GetString("comments"));
                fileDescriptionListView.Items[fileDescriptionListView.Items.Count - 1].SubItems.Add(info.Comments);
            }
        }

        /// <summary>
        /// handle Enter event to open files folders tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        void tabPageFilesFolders_Enter(object sender, EventArgs e)
        {
            try
            {
                flagFileandFolderChecked = false;
                //acquire context menu handlers data
                if ((fileDescriptionListView.Items.Count > 0 || filesFoldersListView.Items.Count > 0) && sender != null)
                {
                    return;
                }
                fileDescriptionListView.Items.Clear();
                filesFoldersListView.Items.Clear();
                regPaths = new Dictionary<string, string[]>();
                regShellPaths = new Dictionary<string, string>();
                keys = new[]
				       	{
				       		rm.GetString("all_files"), rm.GetString("directory"), rm.GetString("drive"), rm.GetString("folder"),
                            rm.GetString("all_filesystem_objects"),rm.GetString("background"), rm.GetString("lnkfile"),
                            rm.GetString("directory_shell"), rm.GetString("file")
				       	};

                regPaths.Add(keys[0], new string[] { @"*\shell", @"*\shellex\ContextMenuHandlers" });
                regPaths.Add(keys[1], new string[] { @"Directory\shellex\ContextMenuHandlers", @"Directory\shell" });

                //regPaths.Add(keys[1], @"Directory\shell");
                regPaths.Add(keys[2], new string[] { @"Drive\shellex\ContextMenuHandlers", @"Drive\shell" });
                //regPaths.Add(keys[3], @"file\ShellEx\ContextMenuHandlers");
                regPaths.Add(keys[3], new string[] { @"Folder\ShellEx\ContextMenuHandlers" });
                regPaths.Add(keys[4], new string[] { @"AllFilesystemObjects\shellex\ContextMenuHandlers" });
                regPaths.Add(keys[5], new string[] { @"Directory\Background\shellex\ContextMenuHandlers" });
                //regPaths.Add(keys[6], new string[] { @"Directory\shell" });               
                regPaths.Add(keys[6], new string[] { @"lnkfile\shellex\ContextMenuHandlers" });
                //process keys
                for (int i = 0; i < regPaths.Count; i++)
                {
                    if (i == 6)
                    { }
                    foreach (string keyItem in regPaths[keys[i]])
                    {
                        var key = Registry.ClassesRoot.OpenSubKey(keyItem);
                        var subKeyNames = new string[] { };
                        try
                        {
                            if (key != null)
                            {
                                subKeyNames = key.GetSubKeyNames();
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        //process sub keys
                        for (int j = 0; j < key.SubKeyCount; j++)
                        {
                            var regCLSID = Registry.ClassesRoot.OpenSubKey(keyItem + @"\" + subKeyNames[j]);
                            string CLSID;
                            if (keyItem == @"*\shell" || keyItem == @"Drive\shell" || keyItem == @"Directory\shell")
                            {
                                string keyName = regCLSID.Name.Substring(regCLSID.Name.LastIndexOf(@"\") + 1,
                                                                       regCLSID.Name.Length - regCLSID.Name.LastIndexOf(@"\") - 1);

                                var key1 = Registry.ClassesRoot.OpenSubKey(keyItem + @"\" + keyName);
                                string keyName1 = string.Empty;

                                if (key1.SubKeyCount == 0)
                                    continue;

                                string displayKeyName = string.Empty;

                                try
                                {
                                    displayKeyName = key1.GetValue("").ToString();
                                }
                                catch
                                {
                                }

                                foreach (var sbName in key1.GetSubKeyNames())
                                {
                                    if (sbName.ToLower() == "command")
                                    {
                                        var tempKey = key1.OpenSubKey(sbName);
                                        try
                                        {
                                            keyName1 = tempKey.GetValue("").ToString();
                                        }
                                        catch
                                        {
                                            continue;
                                        }

                                        string fileName = keyName1;

                                        if (keyItem == @"Drive\shell" || keyItem == @"Directory\shell")
                                        {
                                            CommandLineParser parser = new CommandLineParser();
                                            string commandLine = keyName1;

                                            IEnumerable<string> args = parser.Parse(commandLine);
                                            if (args != null)
                                                fileName = args.FirstOrDefault();

                                            fileName = Environment.ExpandEnvironmentVariables(fileName);
                                            if (!File.Exists(fileName))
                                                continue;

                                            if (!string.IsNullOrEmpty(fileName) && fileName.Contains(Environment.GetEnvironmentVariable("WINDIR")) || string.IsNullOrEmpty(fileName))
                                            {
                                                continue;
                                            }
                                        }

                                        if (!regShellPaths.ContainsKey(keyName))
                                            regShellPaths.Add(keyName, keyName1);

                                        if (String.IsNullOrEmpty(displayKeyName))
                                            displayKeyName = keyName;

                                        ListViewItem lv = new ListViewItem(displayKeyName);

                                        lv.Tag = "FilePath";
                                        lv.SubItems.Add(keys[i]);                                        
                                        lv.SubItems.Add(fileName);
                                        lv.SubItems.Add(keyItem);
                                        lv.SubItems.Add(keyName);
                                        lv.Checked = true;
                                        filesFoldersListView.Items.Add(lv);
                                        break;
                                    }
                                }
                                continue;
                            }

                            try
                            {
                                CLSID = regCLSID.GetValue("").ToString();
                            }
                            catch
                            {
                                CLSID = regCLSID.Name.Substring(regCLSID.Name.LastIndexOf(@"\") + 1,
                                                                   regCLSID.Name.Length - regCLSID.Name.LastIndexOf(@"\") - 1);
                                if (!CLSID.StartsWith("{"))
                                    continue;
                            }

                            var file = CLSIDToFile(CLSID);
                            if (file != null && file.Contains(Environment.GetEnvironmentVariable("WINDIR")) || file == null)
                            {
                                continue;
                            }
                            if (keyItem == @"Directory\shell")
                            {
                                if (regCLSID.ValueCount > 1)
                                {
                                    continue;
                                }

                                string keyName = regCLSID.Name.Substring(regCLSID.Name.LastIndexOf(@"\") + 1,
                                                                        regCLSID.Name.Length - regCLSID.Name.LastIndexOf(@"\") - 1);

                                var key1 = Registry.ClassesRoot.OpenSubKey(keyItem + @"\" + keyName);
                                string keyName1 = key1.GetValue("").ToString();
                                regShellPaths.Add(keyName, keyName1);

                                filesFoldersListView.Items.Add(keyName1);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rm.GetString("directory"));
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(CLSID);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keyItem);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keyName1);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].Checked = true;
                            }
                            else
                            {
                                string keyName = regCLSID.Name.Substring(regCLSID.Name.LastIndexOf(@"\") + 1,
                                                                        regCLSID.Name.Length - regCLSID.Name.LastIndexOf(@"\") - 1);
                                filesFoldersListView.Items.Add(keyName);
                                if (keys[i] == rm.GetString("directory_background"))
                                {
                                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rm.GetString("background"));
                                }
                                else
                                {
                                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keys[i]);
                                }
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(CLSID);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keyItem);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keyName);
                                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].Checked = true;
                            }
                        }                        
                    }                   
                }
                //reading from file
                foreach (string s in ExsKeys.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (s == "") continue;
                    string[] rk = s.Split('|');

                    string keyName = rk[2]; //display keyname

                    filesFoldersListView.Items.Add(keyName);
                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rk[0]); //type
                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rk[3]); //CLSID
                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rk[1]); //path in registry
                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rk[4]); //registry keyname
                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].Checked = false;

                    //if (rk[0] == keys[i])
                    //{
                    //    string CLSID = rk[2];
                    //    string file = CLSIDToFile(CLSID);
                    //    if (file != null && file.Contains(Environment.GetEnvironmentVariable("WINDIR")))
                    //    {
                    //        continue;
                    //    }

                    //    //foreach (ListViewItem item in filesFoldersListView.Items)
                    //    //{
                    //    //    if (item.Text == rk[1] && item.SubItems[1].Text == rk[0])
                    //    //        item.Checked = false;
                    //    //}
                    //    string keyName = rk[1];

                    //    filesFoldersListView.Items.Add(keyName);

                    //    if (keys[i] == @"Directory\shell")
                    //    {
                    //        regShellPaths.Add(rk[1], rk[2]);
                    //        filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(rm.GetString("directory"));
                    //    }
                    //    else
                    //        filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keys[i]);


                    //    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(CLSID);
                    //    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].Checked = false;

                    //    Registry.ClassesRoot.DeleteSubKeyTree(regPaths[filesFoldersListView.Items[i].SubItems[1].Text] + @"\" + filesFoldersListView.Items[i].Text);
                    //}
                }
                if (filesFoldersListView.Items.Count > 0)
                {
                    filesFoldersListView.Items[0].Selected = true;
                }
                filesFoldersListView.ListViewItemSorter = new ListViewItemComparer(1);

                filesFoldersListView.Sort();
            }
            catch
            {
            }
        }
        //void tabPageFilesFolders_Enter(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //acquire context menu handlers data
        //        if ((fileDescriptionListView.Items.Count > 0 || filesFoldersListView.Items.Count > 0) && sender != null)
        //        {
        //            return;
        //        }
        //        fileDescriptionListView.Items.Clear();
        //        filesFoldersListView.Items.Clear();
        //        regPaths = new Dictionary<string, string>();

        //        keys = new[]
        //                {
        //                    rm.GetString("all_files"), rm.GetString("directory"), rm.GetString("drive"), rm.GetString("file"),
        //                    rm.GetString("folder"),"AllFilesystemObjects"
        //                };

        //        regPaths.Add(keys[0], @"*\shellex\ContextMenuHandlers");
        //        regPaths.Add(keys[1], @"Directory\shellex\ContextMenuHandlers");
        //        regPaths.Add(keys[2], @"Drive\shellex\ContextMenuHandlers");
        //        regPaths.Add(keys[3], @"file\ShellEx\ContextMenuHandlers");
        //        regPaths.Add(keys[4], @"Folder\ShellEx\ContextMenuHandlers");
        ////        regPaths = new System.Collections.Specialized.NameValueCollection(); 

        //      //  regPaths.Add(keys[0], @"*\shellex\ContextMenuHandlers");
        //      //  regPaths.Add(keys[1], @"Directory\shellex\ContextMenuHandlers");
        //    //    regPaths.Add(keys[2], @"Drive\shellex\ContextMenuHandlers");
        //    //    regPaths.Add(keys[3], @"file\ShellEx\ContextMenuHandlers");
        //    //    regPaths.Add(keys[4], @"Folder\ShellEx\ContextMenuHandlers");
        //      // regPaths.Add(keys[1], @"Directory\shell\AddToPlaylistVLC");
        //    //    regPaths.Add(keys[1], @"Directory\shell\OneNote.Open");
        //    //    regPaths.Add(keys[1], @"Directory\shell\PlayWithVLC");
        //      //  regPaths.Add(keys[2], @"Drive\shell\unlock-bde");
        //   //     regPaths.Add(keys[5], @"AllFilesystemObjects\shellex\ContextMenuHandlers");
        //    //    regPaths.Add(keys[1], @"Directory\Background\shellex\ContextMenuHandlers");


        //        //   regPaths.Add(keys[1], @"Directory\shell\AddToPlaylistVLC\command");


        //        //process keys
        //        for (int i = 1; i < regPaths.Count; i++)
        //        {
        //            var key = Registry.ClassesRoot.OpenSubKey(regPaths[keys[i]]);
        //            var subKeyNames = new string[] { };
        //            try
        //            {
        //                if (key != null)
        //                {
        //                    subKeyNames = key.GetSubKeyNames();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                continue;
        //            }
        //            //process sub keys
        //            for (int j = 0; j < key.SubKeyCount; j++)
        //            {
        //                var regCLSID = Registry.ClassesRoot.OpenSubKey(regPaths[keys[i]] + @"\" + subKeyNames[j]);
        //                System.IO.File.AppendAllText("allFile.txt", regPaths[keys[i]] + @"\" + subKeyNames[j] + "\r\n");


        //                string CLSID;
        //                try
        //                {
        //                    CLSID = regCLSID.GetValue("").ToString();
        //                }
        //                catch (Exception)
        //                {
        //                    System.IO.File.AppendAllText("FileIssue.txt", regPaths[keys[i]] + @"\" + subKeyNames[j] + "\r\n");
        //                    continue;

        //                }
        //                var file = CLSIDToFile(CLSID);
        //                // 
        //                // if (file == null  )
        //                if (file == null || file.Contains(Environment.GetEnvironmentVariable("WINDIR")))
        //                {

        //                    continue;
        //                }
        //                string keyName = regCLSID.Name.Substring(regCLSID.Name.LastIndexOf(@"\") + 1,
        //                                                         regCLSID.Name.Length - regCLSID.Name.LastIndexOf(@"\") - 1);
        //                filesFoldersListView.Items.Add(keyName);
        //                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keys[i]);
        //                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(CLSID);
        //                filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].Checked = true;
        //            }
        //            //reading from file
        //            foreach (string s in ExsKeys.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
        //            {
        //                if (s == "") continue;
        //                string[] rk = s.Split('|');
        //                if (rk[0] == keys[i])
        //                {
        //                    string CLSID = rk[2];
        //                    string file = CLSIDToFile(CLSID);
        //                    if (file == null || file.Contains(Environment.GetEnvironmentVariable("WINDIR")))
        //                    {
        //                        continue;
        //                    }
        //                    string keyName = rk[1];
        //                    filesFoldersListView.Items.Add(keyName);
        //                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(keys[i]);
        //                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].SubItems.Add(CLSID);
        //                    filesFoldersListView.Items[filesFoldersListView.Items.Count - 1].Checked = false;

        //                    //    Registry.ClassesRoot.DeleteSubKeyTree(regPaths[filesFoldersListView.Items[i].SubItems[1].Text] + @"\" + filesFoldersListView.Items[i].Text);
        //                }
        //            }
        //        }
        //        if (filesFoldersListView.Items.Count > 0)
        //        {
        //            filesFoldersListView.Items[0].Selected = true;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        /// <summary>
        /// handle SelectedIndexChanged event to update file description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void filesFoldersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update description
            if (filesFoldersListView.SelectedItems.Count == 0)
            {
                return;
            }
            //RegistryKey key = Registry.ClassesRoot.OpenSubKey(
            //    regPaths[filesFoldersListView.SelectedItems[0].SubItems[1].Text]
            //    + @"\" + filesFoldersListView.SelectedItems[0].Text);
            if (filesFoldersListView.SelectedItems[0].Tag == "FilePath")
                FileDescription(filesFoldersListView.SelectedItems[0].SubItems[2].Text);
            else
                FileDescription(CLSIDToFile(filesFoldersListView.SelectedItems[0].SubItems[2].Text));
        }

        /// <summary>
        /// handle Click event to remove item from files folders list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void removeFilesFoldersButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (filesFoldersListView.SelectedItems.Count == 1)
                {
                    DialogResult result =
                        MessageBox.Show(String.Format(rm.GetString("sure_remove"), filesFoldersListView.SelectedItems[0].Text),
                                        rm.GetString("sure"), MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {

                        var savedKeys = new StringBuilder();
                        ListViewItem lvi = filesFoldersListView.SelectedItems[0];
                        if (lvi.Checked)
                        {

                            try
                            {

                                string showText = lvi.SubItems[4].Text;
                                string directoryKey = lvi.SubItems[3].Text;
                                string keyType = lvi.SubItems[1].Text;

                                foreach (var pair in regShellPaths)
                                {
                                    if (pair.Value.ToString() == lvi.Text)
                                    {
                                        showText = pair.Key.ToString();
                                        directoryKey = "Directory\\shell";

                                    }
                                }


                                if (!IsTempKey(keyType, showText))
                                {
                                    try
                                    {

                                        Registry.ClassesRoot.DeleteSubKeyTree(directoryKey + @"\" +
                                                                             showText);

                                    }
                                    catch
                                    {

                                    }



                                }
                                filesFoldersListView.Items.Remove(lvi);

                            }
                            catch
                            {
                            }

                        }
                        else
                        {
                            ////////////////
                            foreach (string s in ExsKeys.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                            {
                                if (s == "") continue;
                                string[] rk = s.Split('|');


                                string keyName = rk[2];
                                if (lvi.Text.Equals(keyName))
                                {
                                    filesFoldersListView.Items.Remove(lvi);
                                }
                                else
                                {
                                    savedKeys.AppendLine(s);
                                }

                                //    Registry.ClassesRoot.DeleteSubKeyTree(regPaths[filesFoldersListView.Items[i].SubItems[1].Text] + @"\" + filesFoldersListView.Items[i].Text);
                            }

                            if (File.Exists(AppPath + "\\context_items.bak"))
                            {
                                File.Delete(AppPath + "\\context_items.bak");
                            }
                            if (savedKeys.ToString() != "")
                                File.WriteAllText(AppPath + "\\context_items.bak", savedKeys.ToString());
                            ////////////////

                        }
                        //for (int i = 0; i < filesFoldersListView.CheckedItems.Count; i++)
                        //{
                        //    if (!IsTempKey(filesFoldersListView.CheckedItems[i].SubItems[1].Text, filesFoldersListView.CheckedItems[i].Text))
                        //        Registry.ClassesRoot.DeleteSubKeyTree(regPaths[filesFoldersListView.CheckedItems[i].SubItems[1].Text] + @"\" +
                        //                                              filesFoldersListView.CheckedItems[i].Text);
                        //    else
                        //    {
                        //        DeleteTempKey(filesFoldersListView.CheckedItems[i].SubItems[1].Text, filesFoldersListView.CheckedItems[i].Text);
                        //    }
                        //}
                        // tabPageFilesFolders_Enter(null, null);
                    }
                }
            }
            catch
            {
            }
            // 
        }

        #endregion

        #region New

        ArrayList paths;

        /// <summary>
        /// handle Enter event to open new tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tabPageNew_Enter(object sender, EventArgs e)
        {
            try
            {
                flagNewListChecked = false;
                paths = new ArrayList();
                //if (newListView.Items.Count > 0 && sender != null)
                //    return;
                newListView.Items.Clear();
                //var shell = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Discardable\\PostSetup\\ShellNew");
                //var items = (string[])shell.GetValue("Classes");
                //foreach (string s in items)
                //{
                //    System.IO.File.AppendAllText("main.txt", s + "\r\n");
                //    if (s[0] != '.' || s.Equals(".lnk"))
                //    {
                //        continue;
                //    }

                //    RegistryKey key = Registry.ClassesRoot.OpenSubKey(s);
                //    RegistryKey keyName = Registry.ClassesRoot.OpenSubKey(key.GetValue("").ToString());
                //    if (keyName != null && !keyName.GetValue("").ToString().Equals(""))
                //    {
                //        System.IO.File.AppendAllText("main1.txt", keyName.GetValue("").ToString() + "\r\n");
                //        newListView.Items.Add(keyName.GetValue("").ToString());

                //        newListView.Items[newListView.Items.Count - 1].Checked = true;
                //        paths.Add(s);
                //    }
                //}

                string[] subKeysNames = Registry.ClassesRoot.GetSubKeyNames();
                // populate list
                for (int i = 0; i < subKeysNames.GetLength(0); i++)
                {
                    bool found = false;
                    if (subKeysNames[i][0] != '.')
                    {
                        continue;
                    }

                    if (Registry.ClassesRoot.OpenSubKey(subKeysNames[i] + @"\" + "ShellNew") != null)
                    {
                        try
                        {
                            found = true;
                            RegistryKey key = Registry.ClassesRoot.OpenSubKey(subKeysNames[i]);
                            RegistryKey keyName = Registry.ClassesRoot.OpenSubKey(key.GetValue("").ToString());
                            if (keyName != null && !keyName.GetValue("").ToString().Equals(""))
                            {


                                newListView.SmallImageList = New_LargeIconeList;
                                newListView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

                                listIcons.Add(subKeysNames[i]);
                                Icon ico = FileIcon.FindAssocIcon(subKeysNames[i]);
                                New_LargeIconeList.ColorDepth = ColorDepth.Depth32Bit;
                                New_LargeIconeList.Images.Add(subKeysNames[i], ico);
                                ListViewItem liv = new ListViewItem(keyName.GetValue("").ToString(), subKeysNames[i]);
                                newListView.Items.Add(liv);



                                newListView.Items[newListView.Items.Count - 1].Checked = true;
                                newListView.Items[newListView.Items.Count - 1].Tag = subKeysNames[i];
                                paths.Add(subKeysNames[i]);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    //subkey search, comment the following if and contents for less results,for 1 level recursion this is
                    if (!found)
                    {
                        RegistryKey subKey = Registry.ClassesRoot.OpenSubKey(subKeysNames[i]);
                        if (subKey != null)
                        {
                            string[] subsubKeysNames = subKey.GetSubKeyNames();
                            //process sub keys
                            for (int j = 0; j < subKey.SubKeyCount; j++)
                            {
                                if (Registry.ClassesRoot.OpenSubKey(subKeysNames[i] + @"\" + subsubKeysNames[j] + @"\" + "ShellNew") != null)
                                {
                                    try
                                    {
                                        var shell = Registry.ClassesRoot.OpenSubKey(subKeysNames[i] + "\\" + subsubKeysNames[j] + "\\" + "ShellNew");
                                        //var items = (string[])shell.GetValue("FileName");
                                        if (shell.GetValueNames().Length > 0)
                                        {
                                            //  System.IO.File.AppendAllText("sub1.txt", subKeysNames[i] + @"\" + subsubKeysNames[j] + "\r\n");
                                            RegistryKey key = Registry.ClassesRoot.OpenSubKey(subKeysNames[i]);
                                            RegistryKey keyName = Registry.ClassesRoot.OpenSubKey(key.GetValue("").ToString());

                                            listIcons.Add(subKeysNames[i]);
                                            Icon ico = FileIcon.FindAssocIcon(subKeysNames[i]);
                                            New_LargeIconeList.ColorDepth = ColorDepth.Depth32Bit;
                                            New_LargeIconeList.Images.Add(subKeysNames[i], ico);
                                            ListViewItem liv = new ListViewItem(keyName.GetValue("").ToString(), subKeysNames[i]);
                                            newListView.Items.Add(liv);

                                            //newListView.Items.Add(keyName.GetValue("").ToString());
                                            newListView.Items[newListView.Items.Count - 1].Checked = true;
                                            paths.Add(subKeysNames[i] + @"\" + subsubKeysNames[j]);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }

                            }
                        }
                    }
                }



                //using (var classRoot = Registry.ClassesRoot)
                //{
                //    foreach (var typeName in classRoot.GetSubKeyNames())
                //    {
                //        using (var type = classRoot.OpenSubKey(typeName))
                //        {
                //            foreach (var subkeyName in type.GetSubKeyNames())
                //            {
                //                using (var subkey = type.OpenSubKey(subkeyName))
                //                using (var shellNew = subkey.OpenSubKey("ShellNew"))
                //                using (var xshellNew = subkey.OpenSubKey("XShellNew"))
                //                {
                //                    bool enabled = shellNew != null && shellNew.GetValueNames().Length > 0;
                //                    if (enabled || xshellNew != null)
                //                    {
                //                        System.IO.File.AppendAllText("sub1.txt", subkeyName + "\r\n");

                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                string[] iconEntries = IconsUnchecked.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                int count = 0;
                foreach (string s in ExsExtns.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (s == "") continue;
                    string[] rk = s.Split('|');

                    if (!paths.Contains(rk[0]))
                    {

                        newListView.SmallImageList = New_LargeIconeList;
                        newListView.SmallImageList.ColorDepth = ColorDepth.Depth16Bit;


                        Icon ico = FileIcon.FindAssocIcon(rk[0]);
                        New_LargeIconeList.ColorDepth = ColorDepth.Depth16Bit;
                        New_LargeIconeList.Images.Add(rk[0], ico);


                        newListView.Items.Add(rk[1], New_LargeIconeList.Images.Count - 1);
                        newListView.Items[newListView.Items.Count - 1].Checked = false;
                        paths.Add(rk[0]);
                        count++;
                    }
                }
                //   flagNewListChecked = true;
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle Click event to remove item from new list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void removeNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (newListView.CheckedItems.Count > 0)
                {
                    DialogResult result = MessageBox.Show(String.Format(rm.GetString("sure_remove"), newListView.SelectedItems[0].Text),
                                                          rm.GetString("confirm_delete"), MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {

                        ///////////////////
                        var extentions = new StringBuilder();
                        ListViewItem lvi = newListView.SelectedItems[0];
                        if (lvi.Checked)
                        {
                            flagNewListChecked = true;
                            lvi.Checked = false;

                            foreach (string s in ExsExtns.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                            {
                                if (s == "") continue;
                                string[] rk = s.Split('|');


                                string keyName = rk[1];
                                if (lvi.Text.Equals(keyName))
                                {
                                    newListView.Items.Remove(lvi);
                                }
                                else
                                {
                                    extentions.AppendLine(s);
                                }

                                //    Registry.ClassesRoot.DeleteSubKeyTree(regPaths[filesFoldersListView.Items[i].SubItems[1].Text] + @"\" + filesFoldersListView.Items[i].Text);
                            }

                            if (File.Exists(AppPath + "\\context_new_items.bak"))
                            {
                                File.Delete(AppPath + "\\context_new_items.bak");
                            }
                            if (extentions.ToString() != "")
                                File.WriteAllText(AppPath + "\\context_new_items.bak", extentions.ToString());


                            foreach (ListViewItem eachItem in newListView.SelectedItems)
                            {
                                newListView.Items.Remove(eachItem);
                            }
                        }
                        else
                        {

                            ////////////////
                            foreach (string s in ExsExtns.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                            {
                                if (s == "") continue;
                                string[] rk = s.Split('|');


                                string keyName = rk[1];
                                if (lvi.Text.Equals(keyName))
                                {
                                    newListView.Items.Remove(lvi);
                                }
                                else
                                {
                                    extentions.AppendLine(s);
                                }

                                //    Registry.ClassesRoot.DeleteSubKeyTree(regPaths[filesFoldersListView.Items[i].SubItems[1].Text] + @"\" + filesFoldersListView.Items[i].Text);
                            }

                            if (File.Exists(AppPath + "\\context_new_items.bak"))
                            {
                                File.Delete(AppPath + "\\context_new_items.bak");
                            }
                            if (extentions.ToString() != "")
                                File.WriteAllText(AppPath + "\\context_new_items.bak", extentions.ToString());
                            ////////////////////


                            //for (int i = 0; i < newListView.Items.Count; i++)
                            //{
                            //    if (newListView.Items[i].Checked)
                            //    {
                            //        if (!IsTempExtension(paths[i].ToString()))
                            //            Registry.ClassesRoot.DeleteSubKeyTree(paths[i] + @"\" + "ShellNew");
                            //        else
                            //        {
                            //            DeleteTempExtension(paths[i].ToString());
                            //        }
                            //    }
                            //}
                            //tabPageNew_Enter(null, null);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region SendTo

        /// <summary>
        /// handle Enter event to open send to tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tabPageSendTo_Enter(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetEnvironmentVariable("APPDATA") + @"\Microsoft\Windows\SendTo";
                //
                if (sendToListView.Items.Count > 0 && sender != null)
                    return;
                paths = new ArrayList();
                DirectoryInfo di = null;
                FileSystemInfo[] files = null;
                try
                {
                    di = new DirectoryInfo(path);
                    files = di.GetFileSystemInfos();
                }
                catch
                {
                    path = @"C:\Documents and Settings\" + Environment.UserName + "\\SendTo";
                    di = new DirectoryInfo(path);
                    files = di.GetFileSystemInfos();
                }
                sendToListView.Items.Clear();
                sendToListView.SmallImageList = sendTO_LargeIconeList;
                sendTO_LargeIconeList.ColorDepth = ColorDepth.Depth32Bit;
                foreach (FileSystemInfo file in files)
                {
                    try
                    {
                        //	sendToListView.Items.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));

                        //sendToListView.SmallImageList = sendTO_LargeIconeList;

                        string key = System.IO.Path.GetFileNameWithoutExtension(file.FullName);
                        Icon ico = FileIcon.GetSmallIcon(file.FullName);
                        sendTO_LargeIconeList.Images.Add(key, ico);


                        //    string key =  file.Extension;
                        //    Icon ico = FileIcon.FindAssocIcon( key);
                        //   LargeIconeList.Images.Add(key, ico);

                        ListViewItem liv = new ListViewItem(file.Name.Substring(0, file.Name.LastIndexOf('.')), key);
                        sendToListView.Items.Add(liv);

                        paths.Add(file.FullName);
                    }
                    catch (Exception)
                    {
                        sendToListView.Items.Add(file.Name.Substring(0, file.Name.Length));
                        paths.Add(file.FullName);
                    }
                    sendToListView.Items[sendToListView.Items.Count - 1].Checked = true;
                    sendToListView.Items[sendToListView.Items.Count - 1].SubItems.Add(file.Name);
                    sendToListView.Items[sendToListView.Items.Count - 1].SubItems.Add(file.FullName);
                }
                /*
                foreach (FileSystemInfo file in SendToFiles)
                {
                    sendToListView.Items.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));
                    sendToListView.Items[sendToListView.Items.Count - 1].Checked = false;
                    sendToListView.Items[sendToListView.Items.Count - 1].SubItems.Add(file.Name);
                    sendToListView.Items[sendToListView.Items.Count - 1].SubItems.Add(file.FullName);
                }
                 */

                string pathAppFolder = Application.StartupPath + "\\SendTo";
                if (Directory.Exists(pathAppFolder))
                {
                    FileSystemInfo[] filesAppFolder = null;
                    DirectoryInfo diAppFolder = null;
                    try
                    {
                        diAppFolder = new DirectoryInfo(pathAppFolder);
                        filesAppFolder = diAppFolder.GetFileSystemInfos();
                        foreach (FileSystemInfo file in filesAppFolder)
                        {
                            try
                            {
                                string key = System.IO.Path.GetFileNameWithoutExtension(file.FullName);
                                Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                                sendTO_LargeIconeList.Images.Add(key, ico);

                                ListViewItem liv = new ListViewItem(file.Name.Substring(0, file.Name.LastIndexOf('.')), key);
                                sendToListView.Items.Add(liv);

                                paths.Add(file.FullName);
                            }
                            catch (Exception)
                            {
                                sendToListView.Items.Add(file.Name.Substring(0, file.Name.Length));
                                paths.Add(file.FullName);
                            }
                            sendToListView.Items[sendToListView.Items.Count - 1].Checked = false;
                            sendToListView.Items[sendToListView.Items.Count - 1].SubItems.Add(file.Name);
                            sendToListView.Items[sendToListView.Items.Count - 1].SubItems.Add(file.FullName);
                        }
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

        #endregion




        private void sendToListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SaveSendTo();
        }

        private void newListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (flagNewListChecked)
                SaveNewFiles();
        }

        private void filesFoldersListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (flagFileandFolderChecked)
                SaveFilesAndFolders();
        }

        private void newListView_Click(object sender, EventArgs e)
        {
            flagNewListChecked = true;
        }

        private void filesFoldersListView_Click(object sender, EventArgs e)
        {
            flagFileandFolderChecked = true;

        }
    }

    //C#
    // Implements the manual sorting of items by column.
    class ListViewItemComparer : IComparer
    {
        private int col;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            return returnVal;
        }
    }

}