using System;

namespace Payroll
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return isFriday(payDate);
        }

        private bool isFriday(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday;
        }
    }
}