namespace Payroll.Tests.Transactions
{
    public class ChangeHourlyEmployee:ChangeEmployeeClassification
    {
        private double hourlyRate;

        public ChangeHourlyEmployee(PayrollDB database, int empId, double hourlyRate) : base(database, empId)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

    }
}