using System;
using PayrollDomain;

namespace Schedules
{
    public class MonthlyPaymentSchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return IsLastDayOfMonth(payDate);
        }

        public DateTime GetStartDate(DateTime payDate)
        {
            return payDate.AddMonths(-1);
        }

        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return m1 != m2;
        }
    }
}
