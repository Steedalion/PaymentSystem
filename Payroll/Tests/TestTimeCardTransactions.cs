using System;
using System.CodeDom;
using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestTimeCardTransactions
    {

        [Test]
        public void AddedTimeCardShouldBe()
        {
            int empId = 5;
            string name = "bob";
            string address = "Home";
            AddHourlyEmployee addHourlyEmployee = new AddHourlyEmployee(empId, name, address);
            addHourlyEmployee.Execute();

            TimeCardTransaction timeCardTransaction = new TimeCardTransaction(empId, new DateTime(2005, 7, 31),  8.00);
            timeCardTransaction.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.AreEqual(8.0,tc.Hours,0.01);
        }
    }

    public class TimeCard
    {
        public DateTime Date;
        public double Hours;

        public TimeCard(DateTime date, double hours)
        {
            Date = date;
            Hours = hours;
        }
    }

    public class TimeCardTransaction:DbTransaction
    {
        private int id;
        private double hours;
        private DateTime date;
        public TimeCardTransaction(int empId, DateTime dateTime, double hoursLogged)
        {
            id = empId;
            date = dateTime;
             hours = hoursLogged;
        }

        public void Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(id);
            if (e.isNull)
            {
                throw new EmployeeNotFound();
            }
            HourlyClassification hc = e.Classification as HourlyClassification;
            TimeCard tc = new TimeCard(date, hours);
            hc.AddTimeCard(tc);
        }
    }

    public class EmployeeNotFound : Exception
    {
    }
}