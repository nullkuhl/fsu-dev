using System;
using System.Collections;
using System.Reflection;
using System.Resources;
using System.Xml;

namespace SystemInformation
{
    /// <summary>
    /// Returns all of the holidays occuring in the year following the desiredDate that is passed in the constructor.
    /// Holidays are defined in an XML file.
    /// </summary>
    public class HolidayCalculator
    {
        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
                                                        Assembly.GetExecutingAssembly());

        #region Constructor

        /// <summary>
        /// <see cref="HolidayCalculator"/> constructor
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="xmlPath"></param>
        public HolidayCalculator(DateTime startDate, string xmlPath)
        {
            startingDate = startDate;
            orderedHolidays = new ArrayList();
            xHolidays = new XmlDocument();
            xHolidays.Load(xmlPath);
            ProcessXML();
        }

        #endregion

        #region Properties

        readonly ArrayList orderedHolidays;
        readonly XmlDocument xHolidays;
        DateTime startingDate;

        #endregion

        #region Public Properties

        /// <summary>
        /// The holidays occuring after StartDate listed in chronological order;
        /// </summary>
        public ArrayList OrderedHolidays
        {
            get { return orderedHolidays; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loops through the holidays defined in the XML configuration file,
        /// and adds the next occurance into the OrderHolidays collection if it occurs within one year.
        /// </summary>
        void ProcessXML()
        {
            XmlNodeList xmlNodeList = xHolidays.SelectNodes("/Holidays/Holiday");
            if (xmlNodeList != null)
                foreach (XmlNode n in xmlNodeList)
                {
                    Holiday h = ProcessNode(n);
                    if (h.HolidayDate.Year > 1)
                    {
                        orderedHolidays.Add(h);
                    }
                }

            orderedHolidays.Sort();
        }

        /// <summary>
        /// Processes a Holiday node from the XML configuration file.
        /// </summary>
        /// <param name="n">The Holdiay node to process.</param>
        /// <returns></returns>
        Holiday ProcessNode(XmlNode n)
        {
            Holiday h = new Holiday { HolidayName = n.Attributes["name"].Value };
            ArrayList childNodes = new ArrayList();

            foreach (XmlNode o in n.ChildNodes)
            {
                childNodes.Add(o.Name);
            }

            try
            {
                if (childNodes.Contains("WeekOfMonth"))
                {
                    XmlNode selectSingleNode = n.SelectSingleNode("./Month");
                    if (selectSingleNode != null)
                    {
                        int m = Int32.Parse(selectSingleNode.InnerXml);
                        XmlNode singleNode = n.SelectSingleNode("./WeekOfMonth");
                        if (singleNode != null)
                        {
                            int w = Int32.Parse(singleNode.InnerXml);
                            XmlNode xmlNode = n.SelectSingleNode("./DayOfWeek");
                            if (xmlNode != null)
                            {
                                int wd = Int32.Parse(xmlNode.InnerXml);
                                h.HolidayDate = GetDateByMonthWeekWeekday(m, w, wd, startingDate);
                            }
                        }
                    }
                }
                else if (childNodes.Contains("DayOfWeekOnOrAfter"))
                {
                    XmlNode selectSingleNode = n.SelectSingleNode("./DayOfWeekOnOrAfter/DayOfWeek");
                    if (selectSingleNode != null)
                    {
                        int dow = Int32.Parse(selectSingleNode.InnerXml);

                        if (dow > 6 || dow < 0)
                        {
                            throw new Exception(rm.GetString("holiday_dow"));
                        }

                        XmlNode singleNode = n.SelectSingleNode("./DayOfWeekOnOrAfter/Month");
                        if (singleNode != null)
                        {
                            int m = Int32.Parse(singleNode.InnerXml);
                            XmlNode xmlNode = n.SelectSingleNode("./DayOfWeekOnOrAfter/Day");
                            if (xmlNode != null)
                            {
                                int d = Int32.Parse(xmlNode.InnerXml);
                                h.HolidayDate = GetDateByWeekdayOnOrAfter(dow, m, d, startingDate);
                            }
                        }
                    }
                }
                else if (childNodes.Contains("WeekdayOnOrAfter"))
                {
                    XmlNode selectSingleNode = n.SelectSingleNode("./WeekdayOnOrAfter/Month");
                    if (selectSingleNode != null)
                    {
                        int m = Int32.Parse(selectSingleNode.InnerXml);
                        XmlNode singleNode = n.SelectSingleNode("./WeekdayOnOrAfter/Day");
                        if (singleNode != null)
                        {
                            int d = Int32.Parse(singleNode.InnerXml);
                            DateTime dt = new DateTime(startingDate.Year, m, d);

                            if (dt < startingDate)
                            {
                                dt = dt.AddYears(1);
                            }

                            while (dt.DayOfWeek.Equals(DayOfWeek.Saturday) || dt.DayOfWeek.Equals(DayOfWeek.Sunday))
                            {
                                dt = dt.AddDays(1);
                            }

                            h.HolidayDate = dt;
                        }
                    }
                }
                else if (childNodes.Contains("LastFullWeekOfMonth"))
                {
                    int m = Int32.Parse(n.SelectSingleNode("./LastFullWeekOfMonth/Month").InnerXml);
                    int weekday = Int32.Parse(n.SelectSingleNode("./LastFullWeekOfMonth/DayOfWeek").InnerXml);
                    DateTime dt = GetDateByMonthWeekWeekday(m, 5, weekday, startingDate);

                    if (dt.AddDays(6 - weekday).Month == m)
                    {
                        h.HolidayDate = dt;
                    }
                    else
                    {
                        h.HolidayDate = dt.AddDays(-7);
                    }
                }
                else if (childNodes.Contains("DaysAfterHoliday"))
                {
                    XmlNode basis = xHolidays.SelectSingleNode("/Holidays/Holiday[@name='"
                                                               + n.SelectSingleNode("./DaysAfterHoliday").Attributes["Holiday"].Value +
                                                               "']");
                    Holiday bHoliday = ProcessNode(basis);
                    int days = Int32.Parse(n.SelectSingleNode("./DaysAfterHoliday/Days").InnerXml);

                    // This check was added when this was converted to VB. It does not cause an error in C#, but it
                    // does in Visual Basic. This codes attempts to add negative days to 1/1/0001 which causes the error.
                    if (bHoliday.HolidayDate != new DateTime(1, 1, 1))
                    {
                        h.HolidayDate = bHoliday.HolidayDate.AddDays(days);
                    }
                }
                else if (childNodes.Contains("Easter"))
                {
                    h.HolidayDate = Easter();
                }
                else
                {
                    if (childNodes.Contains("Month") && childNodes.Contains("Day"))
                    {
                        int m = Int32.Parse(n.SelectSingleNode("./Month").InnerXml);
                        int d = Int32.Parse(n.SelectSingleNode("./Day").InnerXml);
                        var dt = new DateTime(startingDate.Year, m, d);

                        if (dt < startingDate)
                        {
                            dt = dt.AddYears(1);
                        }

                        if (childNodes.Contains("EveryXYears"))
                        {
                            int yearMult = Int32.Parse(n.SelectSingleNode("./EveryXYears").InnerXml);
                            int startYear = Int32.Parse(n.SelectSingleNode("./StartYear").InnerXml);

                            if (((dt.Year - startYear) % yearMult) == 0)
                            {
                                h.HolidayDate = dt;
                            }
                        }
                        else
                        {
                            h.HolidayDate = dt;
                        }
                    }
                }
            }
            catch
            {
            }
            return h;
        }

        /// <summary>
        /// Determines the next occurance of Easter (western Christian).
        /// </summary>
        /// <returns></returns>
        DateTime Easter()
        {
            DateTime workDate = getFirstDayOfMonth(startingDate);
            int y = workDate.Year;
            if (workDate.Month > 4)
            {
                y = y + 1;
            }
            return Easter(y);
        }

        /// <summary>
        /// Determines the occurance of Easter in the given year.  
        /// If the result comes before StartDate, recalculates for the following year.
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        DateTime Easter(int y)
        {
            int a = y % 19;
            int b = y / 100;
            int c = y % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int easterMonth = (h + l - 7 * m + 114) / 31;
            int p = (h + l - 7 * m + 114) % 31;
            int easterDay = p + 1;

            DateTime est = new DateTime(y, easterMonth, easterDay);

            if (est < startingDate)
            {
                return Easter(y + 1);
            }
            return new DateTime(y, easterMonth, easterDay);
        }

        /// <summary>
        /// Gets the next occurance of a weekday after a given month and day in the year after StartDate.
        /// </summary>
        /// <param name="weekday">The day of the week (0=Sunday).</param>
        /// <param name="m">The Month</param>
        /// <param name="d">Day</param>
        /// <returns></returns>
        DateTime GetDateByWeekdayOnOrAfter(int weekday, int m, int d, DateTime startDate)
        {
            DateTime workDate = getFirstDayOfMonth(startDate);
            while (workDate.Month != m)
            {
                workDate = workDate.AddMonths(1);
            }

            workDate = workDate.AddDays(d - 1);

            while (weekday != (int)(workDate.DayOfWeek))
            {
                workDate = workDate.AddDays(1);
            }

            //It's possible the resulting desiredDate is before the specified starting desiredDate.  
            //If so we'll calculate again for the next year.
            if (workDate < startingDate)
            {
                return GetDateByWeekdayOnOrAfter(weekday, m, d, startDate.AddYears(1));
            }
            else
            {
                return workDate;
            }
        }

        /// <summary>
        /// Gets the n'th instance of a day-of-week in the given month after StartDate
        /// </summary>
        /// <param name="month">The month the Holiday falls on.</param>
        /// <param name="week">The instance of weekday that the Holiday falls on (5=last instance in the month).</param>
        /// <param name="weekday">The day of the week that the Holiday falls on.</param>
        /// <returns></returns>
        DateTime GetDateByMonthWeekWeekday(int month, int week, int weekday, DateTime startDate)
        {
            DateTime workDate = getFirstDayOfMonth(startDate);

            while (workDate.Month != month)
            {
                workDate = workDate.AddMonths(1);
            }

            while ((int)workDate.DayOfWeek != weekday)
            {
                workDate = workDate.AddDays(1);
            }

            DateTime result;
            if (week == 1)
            {
                result = workDate;
            }
            else
            {
                int addDays = (week * 7) - 7;
                int day = workDate.Day + addDays;

                if (day > DateTime.DaysInMonth(workDate.Year, workDate.Month))
                {
                    day = day - 7;
                }

                result = new DateTime(workDate.Year, workDate.Month, day);
            }

            //It's possible the resulting desiredDate is before the specified starting desiredDate.  
            //If so we'll calculate again for the next year.
            if (result >= startingDate)
            {
                return result;
            }
            return GetDateByMonthWeekWeekday(month, week, weekday, startDate.AddYears(1));
        }

        /// <summary>
        /// Returns the first day of the month for the specified desiredDate.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        DateTime getFirstDayOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        #endregion

        #region Holiday Object

        /// <summary>
        /// Holiday model
        /// </summary>
        internal class Holiday : IComparable
        {
            /// <summary>
            /// Holiday date
            /// </summary>
            public DateTime HolidayDate;

            /// <summary>
            /// Holiday name
            /// </summary>
            public string HolidayName;

            /// <summary>
            /// Resource manager
            /// </summary>
            public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
                                                            Assembly.GetExecutingAssembly());

            #region IComparable Members

            /// <summary>
            /// Holidays comparer
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                if (obj is Holiday)
                {
                    var h = (Holiday)obj;
                    return HolidayDate.CompareTo(h.HolidayDate);
                }
                throw new ArgumentException(rm.GetString("holiday_not"));
            }

            #endregion
        }

        #endregion
    }
}