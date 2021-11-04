using PaymentClassifications.PaymentClassifications;
using PayrollDatabase;
using PayrollDomain;
using Schedules;

namespace PaymentClassifications.ChangeClassification
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

        protected override PayrollDomain.PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

    }
}