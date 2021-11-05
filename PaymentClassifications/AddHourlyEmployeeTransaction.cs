using PaymentClassifications.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace PaymentClassifications
{
    public class AddHourlyEmployeeTransaction : AddEmployeeTransaction
    {
        private double hourlyRate;


        public AddHourlyEmployeeTransaction(PayrollDB.IPayrollDb database, int id, string name, string address, double hourlyRate) : base(database, id, name, address)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override IPaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        }

        protected override PayrollDomain.IPaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }
    }
}