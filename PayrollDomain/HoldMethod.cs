using System;

namespace PayrollDomain
{
    public class HoldMethod : PaymentMethod
    {
        public void pay(PayCheck payCheck)
        {
            Console.WriteLine("Holding Salary  ");
        }
    }
}