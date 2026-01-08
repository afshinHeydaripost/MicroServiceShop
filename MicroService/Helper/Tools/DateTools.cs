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
    public static string ToDateFa(this DateTime dateTime)
    {

        PersianCalendar pc = new PersianCalendar();
        return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime), pc.GetMonth(dateTime).ToString().PadLeft(2, '0'), pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0'));
    }
    public static string ToTimeFa(this DateTime dateTime)
    {
        PersianCalendar pc = new PersianCalendar();
        return string.Format("{0}:{1}", pc.GetHour(dateTime), pc.GetMinute(dateTime).ToString().PadLeft(2, '0'));
    }
    public static string ToDateTimeFa(this DateTime dateTime)
    {
        PersianCalendar pc = new PersianCalendar();
        return string.Format("{0}/{1}/{2} {3}:{4}", pc.GetYear(dateTime), pc.GetMonth(dateTime).ToString().PadLeft(2, '0'), pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0'), pc.GetHour(dateTime), pc.GetMinute(dateTime).ToString().PadLeft(2, '0'));
    }
    public static DateTime ToDateTime(this string dateTime)
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

