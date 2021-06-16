using System;

namespace Payroll
{
    class Biweekly : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday && inSecondWeek(payDate);
        }

        public static bool inSecondWeek(DateTime payDate)
        {
            return payDate.Day >= 8 && payDate.Day <= 14;

        }

    }
}