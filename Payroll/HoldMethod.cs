using System;
using System.Diagnostics;
using Payroll.Tests.Transactions;

namespace Payroll
{
    class HoldMethod : PaymentMethod
    {
        public void pay(PayCheck payCheck)
        {
            Console.WriteLine("Holding Salary  ");
        }
    }
}