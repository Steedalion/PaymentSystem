using System;
using PaymentClassification.PaymentClassifications;
using PayrollDatabase;
using PayrollDomains;
using Transaction;

namespace PaymentClassification
{
    public class AddTimeCard:DbTransaction
    {
        private int id;
        private DateTime date;
        private double hours;

        public AddTimeCard(IPayrollDb database, int id, DateTime date, double hours) : base(database)
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