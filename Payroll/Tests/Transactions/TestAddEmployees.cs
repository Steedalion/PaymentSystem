using System;
using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    public class TestAddEmployees : TestSetupTransactions
    {
        [Test]
        public void TestAddSalariedEmployee()
        {
            AddSalaryEmployee t = new AddSalaryEmployee(database,EmpId, Name, Address, Salary);
            t.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.AreEqual(Name, e.Name);
            Assert.AreEqual(Address, e.myAddress);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, 0.001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlyPaymentSchedule);
            PaymentMethod pm = e.Paymentmethod;
            Assert.IsTrue(pm is HoldMethod);
        }

        [Test]
        public void TestAddHourlyEmployee()
        {
            EmpId = 3;
            AddEmployee t = new AddHourlyEmployee(database,EmpId, Name, Address, 25);
            t.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.AreEqual(Name, e.Name);
            Assert.AreEqual(Address, e.myAddress);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hourly = pc as HourlyClassification;
            PaymentSchedule sc = e.Schedule;
            Assert.IsTrue(sc is WeeklySchedule);
            PaymentMethod pm = e.Paymentmethod;
            Assert.IsTrue(pm is HoldMethod);
            Assert.AreEqual(25, hourly.Rate);
        }

        [Test]
        public void TestAddCommissionedEmployee()
        {
            EmpId = 4;
            float commisionRate = 0.14f;
            AddCommissionedEmployee t = new AddCommissionedEmployee(database,EmpId, Name, Address, Salary, commisionRate);
            t.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.NotNull(e);

            Assert.AreEqual(Name, e.Name);
            Assert.AreEqual(Address, e.myAddress);

            PaymentClassification pc = e.Classification;
            PaymentSchedule ps = e.Schedule;
            PaymentMethod pm = e.Paymentmethod;

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
            AddCommissionedEmployee ce = new AddCommissionedEmployee(database,EmpId, Name, Address, 1000, 0.1);
            ce.Execute();

            AddSalesReceipt sr = new AddSalesReceipt(database,EmpId, new DateTime(2005, 8, 8),200);
            sr.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommisionClassification);
            CommisionClassification cc = pc as CommisionClassification;

            SalesReciept sp = cc.GetSalesReciept(new DateTime(2005, 8, 8));
            Assert.AreEqual(200, sp.Amount, 0.001);
            Assert.Throws<SalesReceiptNotFound>(() => cc.GetSalesReciept(new DateTime(2005, 8, 9)));
        }
    }
}