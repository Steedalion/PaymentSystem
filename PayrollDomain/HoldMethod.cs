using System;

namespace PayrollDomain
{
    public class HoldMethod : IPaymentMethod
    {
        public void Pay(PayCheck payCheck)
        {
            payCheck.SetField("Disposition","Hold");
            Console.WriteLine("Holding Salary  ");
        }
    }
}