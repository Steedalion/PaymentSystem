using System;
using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.DatabaseTests
{
    public class AddEmployeePaymentMethods : AddEmployeeTest
    {
        [Test]
        public void AddAccountPayment()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new AccountPaymentMethod("Woori", 800);
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void AddHoldPayment()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void AddMailPayment()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new MailPaymentMethod("Home");
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void MethodCannotBeNull()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = null;
            e.Classification = new CommisionClassification(0.5, 1000);
            Assert.Throws<NullReferenceException>(() => { database.AddEmployee(id, e); });
            Assert.AreEqual(0, EmployeeCount());
        }

        [Test]
        public void GetHoldPayment()
        {
            AddHoldPayment();
            Employee got = database.GetEmployee(id);
            HoldMethod acc = got.Paymentmethod as HoldMethod;
            Assert.IsTrue(got.Paymentmethod is HoldMethod);
        }
        
        [Test]
        public void GetAccountPayment()
        {
            AddAccountPayment();
            Employee got = database.GetEmployee(id);
            AccountPaymentMethod acc = got.Paymentmethod as AccountPaymentMethod;
            Assert.IsTrue(got.Paymentmethod is AccountPaymentMethod);
            Assert.AreEqual("Woori", acc.bank);
            Assert.AreEqual(800, acc.AccountNumber);
        }

        [Test]
        public void GetMailPayment()
        {
            AddMailPayment();
            Employee got = database.GetEmployee(id);
            MailPaymentMethod acc = got.Paymentmethod as MailPaymentMethod;
            Assert.IsTrue(got.Paymentmethod is MailPaymentMethod);
            Assert.AreEqual("Home", acc.Address);
        }
    }
}