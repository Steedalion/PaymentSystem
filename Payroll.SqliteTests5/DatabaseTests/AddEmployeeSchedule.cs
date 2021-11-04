using NUnit.Framework;
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
        {
            AddMonthly();
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
}