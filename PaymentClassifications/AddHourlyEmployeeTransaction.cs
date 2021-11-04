using PaymentClassifications.PaymentClassifications;
using PayrollDatabase;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace PaymentClassifications
{
    public class AddHourlyEmployeeTransaction : AddEmployeeTransaction
    {
        private double hourlyRate;


        public AddHourlyEmployeeTransaction(IPayrollDb database, int id, string name, string address, double hourlyRate) : base(database, id, name, address)
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