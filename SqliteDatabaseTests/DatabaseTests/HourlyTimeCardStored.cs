using System;
using NUnit.Framework;
using Payroll.TestBuilders;
using PayrollDomain;

namespace DatabaseTests.DatabaseTests
{
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
}