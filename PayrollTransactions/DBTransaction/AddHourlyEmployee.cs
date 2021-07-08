using PayrollDomain.Payroll_Domain;

namespace Payroll
{
    public class AddHourlyEmployee : AddEmployee
    {
        private double hourlyRate;


        public AddHourlyEmployee(IPayrollDB database, int id, string name, string address, double hourlyRate) : base(database, id, name, address)
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