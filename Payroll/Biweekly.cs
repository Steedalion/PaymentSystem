using System;
using System.Globalization;

namespace Payroll
{
    class Biweekly : PaymentSchedule
    {
        private static GregorianCalendar calendar = new System.Globalization.GregorianCalendar();
        public bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday && isBiweekly(payDate);
        }

        public static bool isBiweekly(DateTime payDate)
        {
            int week = calendar.GetWeekOfYear(payDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            return 0 == (week % 2);

        }

    }
}