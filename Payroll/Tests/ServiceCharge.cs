using System;

namespace Payroll.Tests
{
    public class ServiceCharge
    {
        public DateTime DATE;

        public double Amount { get; set; }

        public ServiceCharge(DateTime date, double amount)
        {
            DATE = date;
            Amount = amount;
        }
    }
}