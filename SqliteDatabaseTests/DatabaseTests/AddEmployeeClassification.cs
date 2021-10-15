using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.DatabaseTests
{
    public class AddEmployeeSchedule : AddEmployeeTest
    {
        private Employee e;

        [SetUp]
        public void StartUp()
        {
            database.Clear();
            e = CreateEmployee();
        }

        [Test]
        public void AddWeekly()
        {
            e.Schedule = new WeeklySchedule();
            database.AddEmployee(id, e);
        }

        [Test]
        public void AddMonthly()
        {
        e.Schedule = new MonthlyPaymentSchedule();
            database.AddEmployee(id, e);
            
        }

        [Test]
        public void AddBiweekly()
        {
            e.Schedule = new Biweekly();
            database.AddEmployee(id, e);
        }

        [Test]
        public void GetBiweekly()
        {
            AddBiweekly();
            e = database.GetEmployee(id);
            Assert.IsTrue(e.Schedule is Biweekly);
        }

        [Test]
        public void GetMonthly()
        {AddMonthly();
            e = database.GetEmployee(id);
            Assert.IsTrue(e.Schedule is MonthlyPaymentSchedule);
        }

        [Test]
        public void GetWeekly()
        {
            AddWeekly();
            e = database.GetEmployee(id);
            Assert.IsTrue(e.Schedule is WeeklySchedule);
        }
    }
    public class AddEmployeeClassification : AddEmployeeTest
    {
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
            e.Classification = new HourlyClassification(10.00);
            database.AddEmployee(id,e);
        }

        [Test] public void NoClassification()
        {
            e.Classification = null;
            database.AddEmployee(id,e);
        }

        [Test]
        public void AddCommision()
        {
            e.Classification = new CommisionClassification(0.5f, 100);
            database.AddEmployee(id,e);
        }

        [Test]
        public void AddSalary()
        {
            e.Classification = new SalariedClassification(200);
            database.AddEmployee(id,e);
        }

        [Test]
        public void GetSalary()
        {
            AddSalary();
            Employee e = database.GetEmployee(id);
            Assert.IsTrue(e.Classification is SalariedClassification);
        }
    }
}