using System;
using NUnit.Framework;
using PaymentClassifications;
using PaymentClassifications.PaymentClassifications;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace TransactionTests.Tests.Transactions
{
    public class TestAddEmployees : TestSetupTransactions
    {
        [Test]
        public void TestAddSalariedEmployee()
        {
            AddSalaryEmployeeTransaction t = new AddSalaryEmployeeTransaction(database,EmpId, Name, Address, Salary);
            t.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.AreEqual(Name, e.Name);
            Assert.AreEqual(Address, e.myAddress);
            PayrollDomain.IPaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, 0.001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlyPaymentSchedule);
            IPaymentMethod pm = e.Paymentmethod;
            Assert.IsTrue(pm is HoldMethod);
        }

        [Test]
        public void TestAddHourlyEmployee()
        {
            EmpId = 3;
            AddEmployeeTransaction t = new AddHourlyEmployeeTransaction(database,EmpId, Name, Address, 25);
            t.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.AreEqual(Name, e.Name);
            Assert.AreEqual(Address, e.myAddress);

            PayrollDomain.IPaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hourly = pc as HourlyClassification;
            IPaymentSchedule sc = e.Schedule;
            Assert.IsTrue(sc is WeeklySchedule);
            IPaymentMethod pm = e.Paymentmethod;
            Assert.IsTrue(pm is HoldMethod);
            Assert.AreEqual(25, hourly.Rate);
        }

        [Test]
        public void TestAddCommissionedEmployee()
        {
            EmpId = 4;
            float commisionRate = 0.14f;
            AddCommissionedEmployeeTransaction t = new AddCommissionedEmployeeTransaction(database,EmpId, Name, Address, Salary, commisionRate);
            t.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.NotNull(e);

            Assert.AreEqual(Name, e.Name);
            Assert.AreEqual(Address, e.myAddress);

            PayrollDomain.IPaymentClassification pc = e.Classification;
            IPaymentSchedule ps = e.Schedule;
            IPaymentMethod pm = e.Paymentmethod;

            Assert.IsTrue(pm is HoldMethod);
            Assert.IsTrue(pc is CommisionClassification);
            CommisionClassification cc = pc as CommisionClassification;
            Assert.IsTrue(ps is Biweekly);

            Assert.AreEqual(Salary, cc.Salary);
            Assert.AreEqual(commisionRate, cc.CommisionRate);
        }

        [Test]
        public void TestAddedSalesRecieptShouldExist()
        {
            AddCommissionedEmployeeTransaction ce = new AddCommissionedEmployeeTransaction(database,EmpId, Name, Address, 1000, 0.1);
            ce.Execute();

            AddSalesReceipt sr = new AddSalesReceipt(database,EmpId, new DateTime(2005, 8, 8),200);
            sr.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.NotNull(e);
            PayrollDomain.IPaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommisionClassification);
            CommisionClassification cc = pc as CommisionClassification;

            SalesReciept sp = cc.GetSalesReciept(new DateTime(2005, 8, 8));
            Assert.AreEqual(200, sp.Amount, 0.001);
            Assert.Throws<SalesReceiptNotFound>(() => cc.GetSalesReciept(new DateTime(2005, 8, 9)));
        }
    }
}