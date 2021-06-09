using System;

namespace Payroll.Tests
{
    public class SalesReciept
    {
        public DateTime DATE;
        public double Amount { get; private set; }

        public SalesReciept(DateTime date, double amount)
        {
            DATE = date;
            Amount = amount;
        }
    }
}