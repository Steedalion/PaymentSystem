using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace PaymentClassification
{
    public class AddHourlyEmployee : AddEmployee
    {
        private double hourlyRate;


        public AddHourlyEmployee(PayrollDB.IPayrollDb database, int id, string name, string address, double hourlyRate) : base(database, id, name, address)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        }

        protected override PayrollDomain.PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }
    }
}