using System;
using System.Globalization;

namespace Core.Utils
{
    public class DateUtils
    {
        public int GetWeekOfYear(DateTime date)
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var date1 = new DateTime(date.Year, date.Month, date.Day);

            if (dfi == null) return -1;
            var cal = dfi.Calendar;
            var week = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            return week;
        }

        public int CurrentYear()
        {
            return DateTime.Now.Year;
        }
    }
}
