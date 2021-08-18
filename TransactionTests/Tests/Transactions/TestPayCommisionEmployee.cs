using System;
using NUnit.Framework;
using PaymentClassification;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace TransactionTests.Tests.Transactions
{
    class TestPayCommisionEmployee : TestPay
    {
        [Test]
        public void PayCommisionEmployeeFirstFriday()
        {
            AddCommisionedEmployeeToDB();
            DateTime payDate = new DateTime(2021, 06, 11);
            PayDayTransaction payDay = new PayDayTransaction(database,payDate);

            Assert.IsTrue(Biweekly.isBiweekly(payDate));
            payDay.Execute();

            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.NotNull(payCheck);
            Assert.AreEqual(payCheck.GrossPay, Salary, 0.001);
            Assert.AreEqual(Salary, payCheck.NetPay, 0.001);
            Assert.AreEqual(0, payCheck.Deductions, 0.001);
            Assert.AreEqual(payDate, payCheck.PayDate);
        }
        [Test]
        public void WrongDayPayCommisionedEmployee()
        {
            AddCommisionedEmployeeToDB();
            DateTime payDate = new DateTime(2021, 06, 18);
            PayDayTransaction payDay = new PayDayTransaction(database,payDate);
            payDay.Execute();

            PayCheck payCheck = payDay.GetPayCheck(EmpId);
            Assert.IsNull(payCheck);
        }
        [Test]
        public void PayCommisionEmployeeSecondFriday()
        {
            AddCommisionedEmployeeToDB();
            DateTime payDate = new DateTime(2021, 06, 25);
            PayDayTransaction payDay = new PayDayTransaction(database,payDate);

            Assert.IsTrue(Biweekly.isBiweekly(payDate));
            payDay.Execute();

            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.NotNull(payCheck);
            Assert.AreEqual(payCheck.GrossPay, Salary, 0.001);
            Assert.AreEqual(Salary, payCheck.NetPay, 0.001);
            Assert.AreEqual(0, payCheck.Deductions, 0.001);
            Assert.AreEqual(payDate, payCheck.PayDate);
        }

        

        [Test]
        public void PayCommisionEmployeeWithSale()
        {
            DateTime payDate = new DateTime(2021, 07, 09);

            AddCommisionedEmployeeToDB();

            double saleAmount = 200;
            AddSalesReceipt salesReceipt = new AddSalesReceipt(database,EmpId, payDate, saleAmount);
            salesReceipt.Execute();

            PayDayTransaction payDay = new PayDayTransaction(database, payDate);
            payDay.Execute();

            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.AreEqual(Salary + saleAmount * CommisionRate, payCheck.GrossPay, 0.001);
            Assert.AreEqual(Salary + saleAmount * CommisionRate, payCheck.NetPay, 0.001);
            Assert.AreEqual(0, payCheck.Deductions, 0.001);
            Assert.AreEqual(payDate, payCheck.PayDate);
        }

        [Test]
        public void PayCommisionEmployeeWithMultipleSale()
        {
            DateTime payDate = new DateTime(2021, 07, 09);

            AddCommisionedEmployeeToDB();

            double saleAmount = 200;
            AddSalesReceipt salesReceipt = new AddSalesReceipt(database,EmpId, payDate, saleAmount);
            salesReceipt.Execute();
            
            AddSalesReceipt salesReceipt2 = new AddSalesReceipt(database,EmpId, payDate, saleAmount);
            salesReceipt2.Execute();
            
            AddSalesReceipt early = new AddSalesReceipt(database,EmpId, payDate.AddDays(-14), saleAmount);
            early.Execute();
            
            AddSalesReceipt future = new AddSalesReceipt(database,EmpId, payDate.AddDays(14), saleAmount);
            future.Execute();

            PayDayTransaction payDay = new PayDayTransaction(database,payDate);
            payDay.Execute();

            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.AreEqual(Salary + 2*saleAmount * CommisionRate, payCheck.GrossPay, 0.001);
            Assert.AreEqual(Salary + 2*saleAmount * CommisionRate, payCheck.NetPay, 0.001);
            Assert.AreEqual(0, payCheck.Deductions, 0.001);
            Assert.AreEqual(payDate, payCheck.PayDate);
        }
    }
}