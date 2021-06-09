using System;

namespace Payroll.Tests
{
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
}