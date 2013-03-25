using System;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace Disk_Cleaner
{
	/// <summary>
	/// Preferences form of the Disk cleaner knot
	/// </summary>
	public partial class FormPreferences : Form
	{
		readonly FormAddExtension formExt;
		readonly FormAddFolder formFdr;
		ResourceManager resourceManager;

		/// <summary>
		/// constructor for FormPreferences
		/// </summary>
		public FormPreferences()
		{
			InitializeComponent();
			formExt = new FormAddExtension();
			formFdr = new FormAddFolder();
		}

		/// <summary>
		/// handle Click event to add item to the general list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonGAdd_Click(object sender, EventArgs e)
		{
			formExt.textBoxDescr.Text =
				formExt.textBoxExt.Text = string.Empty;
			if (formExt.ShowDialog() == DialogResult.OK)
			{
				string ext = formExt.textBoxExt.Text;
				string desc = formExt.textBoxDescr.Text;
				if (string.IsNullOrEmpty(ext)) return;
				Option opt;
				foreach (ListViewItem listitem in listViewGeneral.Items)
				{
					opt = listitem.Tag as Option;
					if (opt != null && string.Compare(opt.Value, ext, true) == 0) return;
				}
				opt = new Option(ext, desc, true);
                ListViewItem item = new ListViewItem(opt.Value);
				item.SubItems.Add(opt.Description);
				item.Checked = opt.Checked;
				item.Tag = opt;
				listViewGeneral.Items.Add(item);
			}
		}

		/// <summary>
		/// handle AfterLabelEdit event to update general list after editing item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void listViewGeneral_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label == null)
			{
				e.CancelEdit = true;
				return;
			}
			var ext = e.Label;
			foreach (ListViewItem item in listViewGeneral.Items)
				if (item.Index != e.Item)
					if (string.Compare(item.Text, ext, true) == 0)
					{
						e.CancelEdit = true;
						return;
					}
			var option = listViewGeneral.Items[e.Item].Tag as Option;
			if (option != null) option.Value = ext;
		}

		/// <summary>
		/// handle ItemChecked event to update item state
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void listViewGeneral_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			var option = e.Item.Tag as Option;
			if (option != null) option.Checked = e.Item.Checked;
		}

		/// <summary>
		/// handle Click event to remove item from general list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonGRem_Click(object sender, EventArgs e)
		{
			ListView.SelectedIndexCollection sel = listViewGeneral.SelectedIndices;
			if (sel.Count == 0) return;
			listViewGeneral.BeginUpdate();
			for (int i = listViewGeneral.Items.Count; i >= 0; --i)
				if (sel.IndexOf(i) != -1)
					listViewGeneral.Items.RemoveAt(i);
			listViewGeneral.EndUpdate();
		}

		/// <summary>
		/// handle Click event to load general default list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonGDef_Click(object sender, EventArgs e)
		{
			listViewGeneral.Items.Clear();
			listViewGeneral.BeginUpdate();
			foreach (Option opt in Preferences.DefaultExtensions)
			{
				var item = new ListViewItem(opt.Value);
				item.SubItems.Add(opt.Description);
				item.Checked = opt.Checked;
				item.Tag = opt;
				listViewGeneral.Items.Add(item);
			}
			listViewGeneral.EndUpdate();
		}

		/// <summary>
		/// handle Click event to add item to exclude list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonEAdd_Click(object sender, EventArgs e)
		{
			formFdr.Text = tabPageExclude.Text;
			formFdr.textBoxExt.Text =
				formFdr.textBoxDescr.Text = string.Empty;
			if (formFdr.ShowDialog() == DialogResult.OK)
			{
				string ext = formFdr.textBoxExt.Text;
				string desc = formFdr.textBoxDescr.Text;
				if (string.IsNullOrEmpty(ext)) return;
				Option opt;
				foreach (ListViewItem listitem in listViewExclude.Items)
				{
					opt = listitem.Tag as Option;
					if (opt != null && string.Compare(opt.Value, ext, true) == 0) return;
				}
				opt = new Option(ext, desc, true);
				var item = new ListViewItem(opt.Value);
				item.SubItems.Add(opt.Description);
				item.Checked = opt.Checked;
				item.Tag = opt;
				listViewExclude.Items.Add(item);
			}
		}

		/// <summary>
		/// handle Click event to remove item form exclude list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonERem_Click(object sender, EventArgs e)
		{
			ListView.SelectedIndexCollection sel = listViewExclude.SelectedIndices;
			if (sel.Count == 0) return;
			listViewExclude.BeginUpdate();
			for (int i = listViewExclude.Items.Count; i >= 0; --i)
				if (sel.IndexOf(i) != -1)
					listViewExclude.Items.RemoveAt(i);
			listViewExclude.EndUpdate();
		}

		/// <summary>
		/// handle AfterLabelEdit event to update exclude list after editing item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void listViewExclude_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label == null)
			{
				e.CancelEdit = true;
				return;
			}
			string ext = e.Label;
			foreach (ListViewItem item in listViewExclude.Items)
				if (item.Index != e.Item)
					if (string.Compare(item.Text, ext, true) == 0)
					{
						e.CancelEdit = true;
						return;
					}
			var option = listViewExclude.Items[e.Item].Tag as Option;
			if (option != null) option.Value = ext;
		}

		/// <summary>
		/// handle Click event to add item to include list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonIAdd_Click(object sender, EventArgs e)
		{
			formFdr.Text = tabPageInclude.Text;
			formFdr.textBoxExt.Text =
				formFdr.textBoxDescr.Text = string.Empty;
			if (formFdr.ShowDialog() == DialogResult.OK)
			{
				string ext = formFdr.textBoxExt.Text;
				string desc = formFdr.textBoxDescr.Text;
				if (string.IsNullOrEmpty(ext)) return;
				Option opt;
				foreach (ListViewItem listitem in listViewInclude.Items)
				{
					opt = listitem.Tag as Option;
					if (opt != null && string.Compare(opt.Value, ext, true) == 0) return;
				}
				opt = new Option(ext, desc, true);
				var item = new ListViewItem(opt.Value);
				item.SubItems.Add(opt.Description);
				item.Checked = opt.Checked;
				item.Tag = opt;
				listViewInclude.Items.Add(item);
			}
		}

		/// <summary>
		/// handle Click event to remove item from include list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonIRem_Click(object sender, EventArgs e)
		{
			ListView.SelectedIndexCollection sel = listViewInclude.SelectedIndices;
			if (sel.Count == 0) return;
			listViewInclude.BeginUpdate();
			for (int i = listViewInclude.Items.Count; i >= 0; i--)
				if (sel.IndexOf(i) != -1)
					listViewInclude.Items.RemoveAt(i);
			listViewInclude.EndUpdate();
		}

		/// <summary>
		/// handle AfterLabelEdit event to update include list after editing item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void listViewInclude_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label == null)
			{
				e.CancelEdit = true;
				return;
			}
			string ext = e.Label;
			foreach (ListViewItem item in listViewInclude.Items)
				if (item.Index != e.Item)
					if (string.Compare(item.Text, ext, true) == 0)
					{
						e.CancelEdit = true;
						return;
					}
			var option = listViewInclude.Items[e.Item].Tag as Option;
			if (option != null) option.Value = ext;
		}

		/// <summary>
		/// initialize FormPreferences
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormPreferences_Load(object sender, EventArgs e)
		{
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);

			listViewExclude.ItemChecked += listViewGeneral_ItemChecked;
			listViewGeneral.ItemChecked += listViewGeneral_ItemChecked;
			listViewInclude.ItemChecked += listViewGeneral_ItemChecked;
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			resourceManager = new ResourceManager("Disk_Cleaner.Resources", typeof (FormPreferences).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;

			tabPageGeneral.Text = resourceManager.GetString("general");
			buttonGDef.Text = resourceManager.GetString("default_string");
			buttonGRem.Text = resourceManager.GetString("remove");
			buttonGAdd.Text = resourceManager.GetString("add");
			lblLeave.Text = resourceManager.GetString("if_unsure") + ".";
            lblWaste.Text = resourceManager.GetString("waste_files") + ".";
			clhFileType.Text = resourceManager.GetString("file_type");
			clhDescription.Text = resourceManager.GetString("description");
			lblReviewList.Text = resourceManager.GetString("review_list_junk_files") + ".";
			tabPageExclude.Text = resourceManager.GetString("exclude");
			buttonERem.Text = resourceManager.GetString("remove");
			buttonEAdd.Text = resourceManager.GetString("add");
			clhFolder.Text = resourceManager.GetString("folder");
			clhDesc.Text = resourceManager.GetString("description");
			lblJunk.Text = resourceManager.GetString("not_considered_junk");
			tabPageInclude.Text = resourceManager.GetString("include");
			buttonIRem.Text = resourceManager.GetString("remove");
			buttonIAdd.Text = resourceManager.GetString("add");
			clhDirectory.Text = resourceManager.GetString("folder");
			clhInfo.Text = resourceManager.GetString("description");
			lblFiles.Text = resourceManager.GetString("considered_junk");
			buttonOK.Text = resourceManager.GetString("ok");
			buttonCancel.Text = resourceManager.GetString("cancel");
			buttonHelp.Text = resourceManager.GetString("help");
			Text = resourceManager.GetString("options");
		}

		/// <summary>
		/// handle Click event to open help url
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonHelp_Click(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo(resourceManager.GetString("HelpUrl")));
		}
	}
}