using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Tools
{
    public static class DateTools
    {
        public static string ToDateTimeFa(DateTime? dateTime=null)
        {
            if (dateTime is null)
                dateTime = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime.Value), pc.GetMonth(dateTime.Value).ToString().PadLeft(2, '0'), pc.GetDayOfMonth(dateTime.Value).ToString().PadLeft(2, '0'));
        }
    }
}
