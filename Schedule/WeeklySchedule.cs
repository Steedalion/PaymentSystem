using System;
using PayrollDomains;

namespace Schedule
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return isFriday(payDate);
        }

        public DateTime GetStartDate(DateTime payDate)
        {
            return payDate.AddDays(-7);
        }

        private bool isFriday(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday;
        }
    }
}