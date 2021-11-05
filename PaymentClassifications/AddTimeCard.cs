using System;
using PaymentClassifications.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Transactions;

namespace PaymentClassifications
{
    public class AddTimeCard:DatabaseTransaction
    {
        private int id;
        private DateTime date;
        private double hours;

        public AddTimeCard(PayrollDB.IPayrollDb database, int id, DateTime date, double hours) : base(database)
        {
            this.id = id;
            this.date = date;
            this.hours = hours;
        }

        public override void Execute()
        {
            Employee e = Database.GetEmployee(id);
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