//
// Copyright © 2006 Herbert N Swearengen III (hswear3@swbell.net)
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//   - Redistributions of source code must retain the above copyright notice, 
//     this list of conditions and the following disclaimer.
//
//   - Redistributions in binary form must reproduce the above copyright notice, 
//     this list of conditions and the following disclaimer in the documentation 
//     and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.
//

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace SystemInformation
{
	/// <summary>
	/// Date and time panel of the System Information utility
	/// </summary>
	public partial class DateAndTime : SystemInformation.TaskPanelBase
	{
		static DateAndTime panelInstance;
		static InformationClass info = new InformationClass();

		static string TimeCurrentTimeZone;
		static int TimeUniversalTimeOffset;
		static DateTime TimeLocalDateTime;
		static DateTime TimeUniversalDateTime;
		static DateTime TimeLocalDaylightStartDate;
		static DateTime TimeLocalDaylightStartTime;
		static DateTime TimeLocalDaylightEndDate;
		static DateTime TimeLocalDaylightEndTime;

		/// Get a global instance of this panel
		public static DateAndTime CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new DateAndTime();
				TimeCurrentTimeZone = info.TimeCurrentTimeZone;
				TimeUniversalTimeOffset = info.TimeUniversalTimeOffset;
				TimeLocalDateTime = info.TimeLocalDateTime;
				TimeUniversalDateTime = info.TimeUniversalDateTime;
				TimeLocalDaylightStartDate = info.TimeLocalDaylightStartDate;
				TimeLocalDaylightStartTime = info.TimeLocalDaylightStartTime;
				TimeLocalDaylightEndDate = info.TimeLocalDaylightEndDate;
				TimeLocalDaylightEndTime = info.TimeLocalDaylightEndTime;
			}
			return panelInstance;
		}

		Calendar currentCalendar = new GregorianCalendar();

		int currentYear;            // Used to track year changes in month calendar.
		string[] holidayName;       // Array of holiday names for the year.
		DateTime[] holidayDate;     // Array of holiday dates for the year.

		#region " DateTime Events "

		void DateAndTime_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_datetime").Replace("&", "&&");
			this.labelUniversal.Text = rm.GetString("datetime_univ") + ":";
			this.labelOffset.Text = rm.GetString("datetime_offset") + ":";
			this.labelDaylightStartDesc.Text = rm.GetString("datetime_save_start") + ":";
			this.labelDaylightEndDesc.Text = rm.GetString("datetime_save_end") + ":";
			this.labelSelectedDayOfYearDesc.Text = rm.GetString("datetime_dayofyear") + ":";
			this.labelSelectedDaysLeftDesc.Text = rm.GetString("datetime_daysleft") + ":";
			this.labelOffsetFromTodayDesc.Text = rm.GetString("datetime_offset_from_today") + ":";
			this.labelSelectedDateDesc.Text = rm.GetString("datetime_selected_date") + ":";
			this.labelTCurrentDateDesc.Text = rm.GetString("today") + ":";
			this.labelCurrentDayOfYearDesc.Text = rm.GetString("datetime_dayofyear") + ":";
			this.labelCurrentDaysLeftDesc.Text = rm.GetString("datetime_daysleft") + ":";
			this.labelCurrentWeekOfYearDesc.Text = rm.GetString("datetime_weekofyear") + ":";
			this.labelCurrentWeeksLeftDesc.Text = rm.GetString("datetime_weeksleft") + ":";
			this.labelSelectedWeekOfYearDesc.Text = rm.GetString("datetime_weekofyear") + ":";
			this.labelSelectedWeeksLeftDesc.Text = rm.GetString("datetime_weeksleft") + ":";
			this.labelLocal.Text = rm.GetString("datetime_local") + ":";

			try
			{
				// Allow panel to paint.
				Application.DoEvents();

				// Get the Time Zone.
				labelTimeZone.Text = TimeCurrentTimeZone;

				// Get the offset from UTC.
				labelOffsetFromUTC.Text = TimeUniversalTimeOffset + " " + rm.GetString("hours");

				// Get the local desiredDate and time.
				labelLocalDateTime.Text = TimeLocalDateTime.ToLongDateString() + " " +
					TimeLocalDateTime.ToLongTimeString();

				// Get UTC desiredDate and time.
				labelUniversalDateTime.Text = TimeUniversalDateTime.ToLongDateString() + " " +
					TimeUniversalDateTime.ToLongTimeString();

				// Get daylight savings start.
                var currentYearTimeLocalDaylightStartDate = TimeLocalDaylightStartDate.AddYears(DateTime.Now.Year - 1);
                labelDaylightStart.Text = currentYearTimeLocalDaylightStartDate.ToLongDateString() + " " +
					TimeLocalDaylightStartTime.ToLongTimeString();

				// Get daylight end.
                var currentYearTimeLocalDaylightEndDate = TimeLocalDaylightEndDate.AddYears(DateTime.Now.Year - 1);
                labelDaylightEnd.Text = currentYearTimeLocalDaylightEndDate.ToLongDateString() + " " +
					TimeLocalDaylightEndTime.ToLongTimeString();

				// Get the current desiredDate.
				labelCurrentDate.Text = TimeLocalDateTime.ToShortDateString();

				// Get the day of the year.
				labelCurrentDayOfYear.Text = DateTime.Today.DayOfYear.ToString();

				// Get the days remaining in the year.
				labelCurrentDaysLeft.Text = ((currentCalendar.GetDaysInYear(DateTime.Today.Year)
						- currentCalendar.GetDayOfYear(DateTime.Today))).ToString();

				// Get the current week of the year.
				labelCurrentWeekOfYear.Text = info.GetWeekNumber(DateTime.Today).ToString();

				// Get weeks left for the current desiredDate.
				labelCurrentWeeksLeft.Text = (52 - info.GetWeekNumber(DateTime.Today)).ToString();

				// Store the current year so we can check when it changes.
				currentYear = DateTime.Today.Year;

				// Get the holidays.
				if (GetHolidays(currentYear))
				{
					// Add holidays as bolded dates to the MonthCalendar control.
					calendarDateTime.BoldedDates = holidayDate;
				}
				else
				{
					// Unable to get holidays, so remove any bolded dates.
					calendarDateTime.RemoveAllBoldedDates();
					calendarDateTime.UpdateBoldedDates();
				}

				// Get the holiday name (if any).
				string holiday = GetHolidayName(DateTime.Today);

				// Display the holiday name.
				if (!String.IsNullOrEmpty(holiday))
				{
					labelSelectedHoliday.Text = holiday;
				}
				else
				{
					labelSelectedHoliday.Text = "";
				}

				// Enable timer.
				timerDateTime.Enabled = true;

			}
			catch (NullReferenceException)
			{
				// Do nothing - this means the Holiday.xml file is missing.
			}
			catch (Exception) { }
		}

		#endregion

		#region " Timer Events"

		void timerDateTime_Tick(System.Object sender, System.EventArgs e)
		{

			// update desiredDate and time labels
			labelLocalDateTime.Text = info.TimeLocalDateTime.ToLongDateString() + " " +
				info.TimeLocalDateTime.ToLongTimeString();
			labelUniversalDateTime.Text = info.TimeUniversalDateTime.ToLongDateString() + " " +
				info.TimeUniversalDateTime.ToLongTimeString();

			// Check if the year in the month calendar has changed.
			if (calendarDateTime.SelectionStart.Year != currentYear)
			{

				// Make sure the year has not been accidentally set to the beginning (1776).
				if (calendarDateTime.SelectionStart.Year == 1776)
				{
					// Reset the calendar to today's date.
					calendarDateTime.SetDate(DateTime.Today);
				}
				else
				{
					// Store the new year.
					currentYear = calendarDateTime.SelectionStart.Year;

					// Get the holidays.
					if (GetHolidays(currentYear))
					{
						// Add holidays as bolded dates to the MonthCalendar control.
						calendarDateTime.BoldedDates = holidayDate;
					}
					else
					{
						// Unable to get holidays, so remove any bolded dates.
						calendarDateTime.RemoveAllBoldedDates();
						calendarDateTime.UpdateBoldedDates();
					}
				}
			}

			// Get the holiday name (if any).
			string holiday = GetHolidayName(DateTime.Today);

			// Display the holiday name.
			if (!String.IsNullOrEmpty(holiday))
			{
				labelCurrentHoliday.Text = holiday;
			}
			else
			{
				labelCurrentHoliday.Text = "";
			}

		}

		#endregion

		#region " Month Calendar Events "

		void calendarDateTime_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			// Get the selected desiredDate.
			labelSelectedDate.Text = e.Start.ToShortDateString();

			// Get the day of the year.
			labelSelectedDayOfYear.Text = e.Start.DayOfYear.ToString();

			// Get the days remaining in the year.
			labelSelectedDaysLeft.Text = ((currentCalendar.GetDaysInYear(e.Start.Year)
				- currentCalendar.GetDayOfYear(e.Start))).ToString();

			// Get the selected week of the year.
			labelSelectedWeekOfYear.Text = info.GetWeekNumber(e.Start).ToString();

			// Get weeks left for the selected desiredDate.
			labelSelectedWeeksLeft.Text = (52 - info.GetWeekNumber(e.Start)).ToString();

			// Get the holiday name (if any).
			string holiday = GetHolidayName(e.Start);

			// Display the holiday name.
			if (!String.IsNullOrEmpty(holiday))
			{
				labelSelectedHoliday.Text = holiday;
			}
			else
			{
				labelSelectedHoliday.Text = "";
			}

			// Get the offset from today.
			int offset = e.Start.Subtract(DateTime.Today).Days;

			// Format outout.
			if (offset == +1)
			{
				labelOffsetFromToday.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
				labelOffsetFromToday.Text = offset.ToString() + " " + rm.GetString("datetime_daysahead");
			}
			else if (offset == -1)
			{
				labelOffsetFromToday.ForeColor = Color.DarkRed;
				labelOffsetFromToday.Text = Math.Abs(offset).ToString() + " " + rm.GetString("datetime_daysbehind");
			}
			else if (offset > 1)
			{
				labelOffsetFromToday.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
				labelOffsetFromToday.Text = String.Format("{0:N0}", offset) + " " + rm.GetString("datetime_daysahead");
			}
			else if (offset < -1)
			{
				labelOffsetFromToday.ForeColor = Color.DarkRed;
				labelOffsetFromToday.Text = String.Format("{0:N0}", Math.Abs(offset)) + " " + rm.GetString("datetime_daysbehind");
			}
			else
			{
				labelOffsetFromToday.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
				labelOffsetFromToday.Text = "";
			}
		}

		#endregion

		#region " Methods "

		bool GetHolidays(int desiredYear)
		{
			DateTime desiredDate;

			try
			{
				desiredDate = new DateTime(desiredYear, 1, 1);
				string xmlPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Holidays.xml";

				if (File.Exists(xmlPath))
				{
					HolidayCalculator hc = new HolidayCalculator(desiredDate, xmlPath);
					holidayName = new string[hc.OrderedHolidays.Count];
					holidayDate = new DateTime[hc.OrderedHolidays.Count];
					int i = 0;

					foreach (HolidayCalculator.Holiday h in hc.OrderedHolidays)
					{

						// Add name to string array.
						holidayName[i] = h.HolidayName;

						// Add desiredDate to desiredDate array.
						holidayDate[i] = h.HolidayDate;

						i++;
					}

					return true;
				}
				else
				{
					return false;
				}
			}
			catch (NullReferenceException)
			{
				// Do nothing - this means the Holiday.xml file is missing.
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		string GetHolidayName(DateTime inDate)
		{
			try
			{
				int index = 0;
				string returnName = "";

				// Try to find the inDate in the holidayDate array.
				foreach (DateTime testDate in holidayDate)
				{
					if (testDate == inDate)
					{
						returnName = holidayName[index];
						break;
					}

					index++;

				}

				return returnName;
			}
			catch (NullReferenceException)
			{
				// Do nothing - this means the Holiday.xml file is missing.
				return "";
			}
			catch (Exception)
			{
				return "";
			}

		}

		#endregion

	}

}
