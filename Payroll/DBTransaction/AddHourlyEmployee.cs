namespace Payroll
{
    public class AddHourlyEmployee : AddEmployee
    {
        public AddHourlyEmployee(int id, string name, string address) : base(id, name, address)
        {
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification();
        }
    }
}