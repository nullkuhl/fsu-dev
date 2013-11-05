using System;
using System.ComponentModel;
using FreemiumUtilities.Infrastructure;
using FreeGamingBooster.Models;

namespace FreeGamingBooster.ViewModels
{
	internal class OneClickAppViewModel : INotifyPropertyChanged
	{
		#region Constructors

		public OneClickAppViewModel(String title, String icon, bool selected, OneClickAppStatus status, String description,
		                            OneClickApp instance)
		{
			Title = title;
			Icon = icon;
			Selected = selected;
			Status = status;
			Description = description;
			Instance = instance;
		}

		#endregion

		#region Properties

		String description;
		String icon;
		bool selected;
		OneClickAppStatus status = OneClickAppStatus.NotStarted;
		String statusText;
		String statusTextKey;
		String title;

		public String Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}

		public String Description
		{
			get { return description; }
			set
			{
				description = value;
				// StatusText will be equal to Description on instance init
				if (StatusText == null)
					StatusText = value;
				OnPropertyChanged("Description");
			}
		}

		public String Icon
		{
			get { return icon; }
			set
			{
				icon = value;
				OnPropertyChanged("Icon");
			}
		}

		public bool Selected
		{
			get { return selected; }
			set
			{
				selected = value;
				if (!Selected)
					ResetStatus();
				OnPropertyChanged("Selected");
			}
		}

		public OneClickAppStatus Status
		{
			get { return status; }
			set
			{
				status = value;
				OnPropertyChanged("Status");
			}
		}

		public String StatusText
		{
			get { return statusText; }
			set
			{
				statusText = value;
				OnPropertyChanged("StatusText");
			}
		}

		public String StatusTextKey
		{
			get { return statusTextKey; }
			set
			{
				statusTextKey = value;
				OnPropertyChanged("StatusTextKey");
			}
		}

		public OneClickApp Instance { get; set; }

		#endregion

		#region Public methods

		public void ResetStatus()
		{
			if (Instance != null)
			{
				Instance.ProblemsCount = 0;
			}
			Status = OneClickAppStatus.NotStarted;
			StatusText = Description;
			OneClickAppsViewModel.UpdateOneClickAppsRunningQueue(this);
		}

		//public void

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion
	}
}