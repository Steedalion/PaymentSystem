using System;
using PayrollDomain;

namespace Schedules
{
    public class WeeklySchedule : IPaymentSchedule
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