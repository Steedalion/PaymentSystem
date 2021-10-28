using System;
using NUnit.Framework;
using PaymentClassifications.PaymentClassifications;
using PayrollDomain;

namespace DatabaseTests.DatabaseTests
{
    public class AddEmployeeClassification : AddEmployeeTest
    {
        public const double commSalary = 100;
        public const double commRate = 0.5f;
        public const double salary = 200;
        public const double hourlyRate = 10;
        private Employee e;

        [SetUp]
        public void StartUp()
        {
            database.Clear();
            e = CreateEmployee();
        }

        [Test]
        public void AddHourly()
        {
            e.Classification = new HourlyClassification(hourlyRate);
            database.AddEmployee(id, e);
        }

        [Test]
        public void NoClassification()
        {
            try
            {
                e.Classification = null;
                database.AddEmployee(id, e);
            }
            catch (Exception exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void AddCommision()
        {
            e.Classification = new CommisionClassification(commRate, commSalary);
            database.AddEmployee(id, e);
        }

        [Test]
        public void AddSalary()
        {
            e.Classification = new SalariedClassification(salary);
            database.AddEmployee(id, e);
        }


        [Test]
        public void GetSalary()
        {
            AddSalary();
            Employee e = database.GetEmployee(id);
            Assert.IsTrue(e.Classification is SalariedClassification);
            var s = e.Classification as SalariedClassification;
            Assert.AreEqual(salary, s.Salary);
        }

        [Test]
        public void GetHourly()
        {
            AddHourly();
            Employee e = database.GetEmployee(id);
            Assert.IsTrue(e.Classification is HourlyClassification);
            var h = e.Classification as HourlyClassification;
            Assert.AreEqual(hourlyRate, h.Rate);
        }

        [Test]
        public void GetCommision()
        {
            AddCommision();
            Employee e = database.GetEmployee(id);
            Assert.IsTrue(e.Classification is CommisionClassification);
            var c = e.Classification as CommisionClassification;
            Assert.AreEqual(commSalary, c.Salary);
            Assert.AreEqual(commRate, c.CommisionRate);
        }
    }
}