using NUnit.Framework;
using PaymentClassifications.PaymentClassifications;
using PaymentMethods;
using Payroll.TestBuilders;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.DatabaseTests
{
    public class GetEmployees : AddEmployeeTest
    {
        private Employee AddAndGet(Employee e)
        {
            database.AddEmployee(1, e);
            var e2 = database.GetEmployee(1);
            return e2;
        }

        [Test]
        public void GetWeeklyPaymentSchedule()
        {
            Employee e = An.GenericEmployee.WeeklySchedule();
            Employee e2 = AddAndGet(e);
            Assert.IsTrue(e2.Schedule is WeeklySchedule);
        }

        [Test]
        public void GetMonthly()
        {
            Employee e = An.GenericEmployee.MonthlySchedule();
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Schedule is MonthlyPaymentSchedule);
        }

        [Test]
        public void GetBiWeekly()
        {
            Employee e = An.GenericEmployee.BiweeklySchedule();
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Schedule is Biweekly);
        }

        [Test]
        public void GetSalaried()
        {
            Employee e = An.GenericEmployee.SalariedClass(100);
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Classification is SalariedClassification);
            var classification = e2.Classification as SalariedClassification;
            Assert.AreEqual(100, classification.Salary);
        }

        [Test]
        public void GetHourly()
        {
            Employee e = An.GenericEmployee.Hourly(8);
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Classification is HourlyClassification);
            var classification = e2.Classification as HourlyClassification;
            Assert.AreEqual(8, classification.Rate);
        }

        [Test]
        public void GetCommision()
        {
            Employee e = An.GenericEmployee.Commision(0.3,100);
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Classification is CommisionClassification);
            var classification = e2.Classification as CommisionClassification;
            Assert.AreEqual(100, classification.Salary);
            Assert.AreEqual(0.3, classification.CommisionRate,0.01);
        }

        [Test]
        public void GetMail()
        {
            Employee e = An.GenericEmployee.Mail("Home");
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Paymentmethod is MailPaymentMethod);
            var classification = e2.Paymentmethod as MailPaymentMethod;
            Assert.AreEqual("Home", classification.Address);
            
        }

        [Test]
        public void GetBank()
        {
            Employee e = An.GenericEmployee.Bank("Bank",1234);
            var e2 = AddAndGet(e);
            Assert.IsTrue(e2.Paymentmethod is AccountPaymentMethod);
            var classification = e2.Paymentmethod as AccountPaymentMethod;
            Assert.AreEqual("Bank", classification.bank);
            Assert.AreEqual(1234, classification.AccountNumber,0.01);
            
        }
        
    }
}