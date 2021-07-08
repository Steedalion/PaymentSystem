using System;

namespace Payroll
{
    public class HoldMethod : PaymentMethod
    {
        public void pay(PayCheck payCheck)
        {
            Console.WriteLine("Holding Salary  ");
        }
    }
}