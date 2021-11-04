using PaymentClassification.PaymentClassifications;
using PayrollDatabase;
using PayrollDomains;
using Schedule;

namespace PaymentClassification.ChangeClassification
{
    public class ChangeHourlyEmployee:ChangeEmployeeClassification
    {
        private double hourlyRate;

        public ChangeHourlyEmployee(IPayrollDb database, int empId, double hourlyRate) : base(database, empId)
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