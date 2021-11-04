using PaymentClassification.PaymentClassifications;
using PayrollDatabase;
using PayrollDomains;
using Schedule;
using Transaction.DBTransaction;

namespace PaymentClassification
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

        protected override PayrollDomains.PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }
    }
}