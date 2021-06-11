namespace Payroll
{
    public class AddHourlyEmployee : AddEmployee
    {
        private double hourlyRate;

        public AddHourlyEmployee(int id, string name, string address, double hourlyRate) : base(id, name, address)
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