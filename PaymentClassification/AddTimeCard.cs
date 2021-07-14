using System;
using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Transactions;

namespace PaymentClassification
{
    public class AddTimeCard:DbTransaction
    {
        private int id;
        private DateTime date;
        private double hours;

        public AddTimeCard(IPayrollDB database, int id, DateTime date, double hours) : base(database)
        {
            this.id = id;
            this.date = date;
            this.hours = hours;
        }

        public override void Execute()
        {
            Employee e = database.GetEmployee(id);
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