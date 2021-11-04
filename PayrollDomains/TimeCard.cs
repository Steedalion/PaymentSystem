using System;

namespace PayrollDomains
{
    public class TimeCard
    {
        public DateTime Date;
        public double Hours;

        public TimeCard(DateTime date, double hours)
        {
            Date = date;
            Hours = hours;
        }
    }
}