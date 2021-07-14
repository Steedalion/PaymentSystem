using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace PaymentClassification.ChangeClassification
{
    public class ChangeHourlyEmployee:ChangeEmployeeClassification
    {
        private double hourlyRate;

        public ChangeHourlyEmployee(IPayrollDB database, int empId, double hourlyRate) : base(database, empId)
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