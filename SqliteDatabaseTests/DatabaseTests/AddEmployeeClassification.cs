using System;
using DatabaseTests.DatabaseTests;
using NUnit.Framework;
using PaymentClassifications.PaymentClassifications;
using PayrollBuilders;
using PayrollDomain;

namespace DatabaseTests.DatabaseTests
{
    public class SalesReceiptStored : AddEmployeeTest
    {
        [Test]
        public void StoreSalesReceipt()
        {
            Employee e = An.GenericEmployee.Commision(1, 100);
            CommisionClassification commision = e.ClassificationAsCommision();
            commision.AddSalesReciept(DateTime.Today, 10);
            database.AddEmployee(1,e);
            var got = database.GetEmployee(1);
        }

        [Test]
        public void GetSalesReceipt()
        {
            Employee e = An.GenericEmployee.Commision(1, 100);
            CommisionClassification commision = e.ClassificationAsCommision();
            commision.AddSalesReciept(DateTime.Today, 10);
            database.AddEmployee(1,e);
            var got = database.GetEmployee(1);
            var saleReceipt = got.ClassificationAsCommision().GetSalesReciept(DateTime.Today);
            Assert.AreEqual(10,saleReceipt.Amount);
        }
    }

    public class HourlyTimeCardStored : AddEmployeeTest
    {
        [Test]
        public void AddHourlyEmployeeWithTimeCard()
        {
            Employee employee = An.GenericEmployee.Hourly(10f).Build();
            employee.ClassificationAsHourly().AddTimeCard(new TimeCard(DateTime.Today, 10));
            database.AddEmployee(1, employee);
            var got = database.GetEmployee(1);
            var tc = employee.ClassificationAsHourly().GetTimeCard(DateTime.Today);
            // Assert.AreEqual(10,tc.Hours);
            var tc2 = got.ClassificationAsHourly().GetTimeCard(DateTime.Today);
            Assert.AreEqual(10, tc2.Hours);
        }
    }

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