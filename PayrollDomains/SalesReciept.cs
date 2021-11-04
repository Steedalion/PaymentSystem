using System;

namespace PayrollDomains
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