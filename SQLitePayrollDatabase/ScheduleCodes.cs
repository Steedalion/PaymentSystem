using PayrollDomain;
using Schedules;

namespace PayrollDataBase
{
    public static class ScheduleCodes
    {
        public static string BiWeekly = "BiWeekly";
        public static string Monthly = "Monthly";
        public static string Weekly = "Weekly";

        public static string Code(PaymentSchedule eSchedule)
        {
            if (eSchedule is Biweekly)
            {
                return BiWeekly;
            }
            else if (eSchedule is MonthlyPaymentSchedule)
            {
                return Monthly;
            }
            else if (eSchedule is WeeklySchedule)
            {
                return Weekly;
            }

            return "UnknownSchedule";
        }
    }
}