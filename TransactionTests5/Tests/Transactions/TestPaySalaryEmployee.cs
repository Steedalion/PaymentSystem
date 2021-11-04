using System;
using NUnit.Framework;
using PayrollDomain;
using Transactions.DBTransaction;

namespace TransactionTests.Tests.Transactions
{
    public class TestPaySalaryEmployee : TestPay
    {
        [Test]
        public void MonthEndShouldPaySalaryEmployee()
        {
            AddSalariedEmployeeToDb();

            DateTime monthEnd = new DateTime(2001, 11, 30);
            PayDayTransaction payDay = new PayDayTransaction(database,monthEnd);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.AreEqual(Salary, payCheck.GrossPay, 0.001);
            Assert.AreEqual(monthEnd, payCheck.PayDate);
            Assert.AreEqual(0.0, payCheck.Deductions);
            Assert.AreEqual(Salary, payCheck.NetPay);
        }

        [Test]
        public void NotMonthEndShouldNotPaySalaryEmployee()
        {
            AddSalariedEmployeeToDb();

            DateTime notMonthEnd = new DateTime(2001, 11, 29);
            PayDayTransaction payDay = new PayDayTransaction(database,notMonthEnd);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);
            Assert.IsNull(payCheck);
        }
    }
}