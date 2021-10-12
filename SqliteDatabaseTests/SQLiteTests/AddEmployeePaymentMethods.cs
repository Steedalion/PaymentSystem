using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.SQLiteTests
{
    public class AddEmployeePaymentMethods : AddEmployeeTests
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
    }
}