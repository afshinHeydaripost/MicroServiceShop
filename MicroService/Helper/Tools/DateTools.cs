using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper;
public static class DateTools
{
    public static string ToDateTimeFa(DateTime? dateTime = null)
    {
        if (dateTime is null)
            dateTime = DateTime.Now;
        PersianCalendar pc = new PersianCalendar();
        return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime.Value), pc.GetMonth(dateTime.Value).ToString().PadLeft(2, '0'), pc.GetDayOfMonth(dateTime.Value).ToString().PadLeft(2, '0'));
    }
    public static DateTime ToDateTime(string dateTime)
    {
        if (string.IsNullOrEmpty(dateTime))
            return DateTime.Now;
        dateTime = Regex.Replace(dateTime, @"\D", "");
        int year = int.Parse(dateTime.Substring(0, 4));
        int month = int.Parse(dateTime.Substring(4, 2));
        int day = int.Parse(dateTime.Substring(6, 2));
        int hour = int.Parse(dateTime.Substring(8, 2));
        int minute = int.Parse(dateTime.Substring(10, 2));
        PersianCalendar pc = new PersianCalendar();
        DateTime dt = pc.ToDateTime(
                year,
                month,
                day,
                hour,
                minute,
                0,
                0
            );
        return dt;
    }
}

