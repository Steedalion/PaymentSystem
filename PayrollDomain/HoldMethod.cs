using System;

namespace PayrollDomain
{
    public class HoldMethod : PaymentMethod
    {
        public void pay(PayCheck payCheck)
        {
            payCheck.SetField("Disposition","Hold");
            Console.WriteLine("Holding Salary  ");
        }
    }
}