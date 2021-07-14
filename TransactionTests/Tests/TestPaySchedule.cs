using System;
using NUnit.Framework;
using PayrollDomain;
using Schedules;

namespace Payroll.Tests
{
    public class TestPaySchedule
    {
        [Test]
        public void MonthlyShouldPayMonthEnd()
        {
            PaymentSchedule monthly = new MonthlyPaymentSchedule();
            
            Assert.IsTrue(monthly.IsPayDate(new DateTime(2001,11,30)));
        }
        [Test]
        public void MonthlyShouldNotPayMonthMiddle()
        {
            PaymentSchedule monthly = new MonthlyPaymentSchedule();
            
            Assert.IsFalse(monthly.IsPayDate(new DateTime(2001,11,29)));
        }
    }
}