    using Payroll;
    using PayrollDomain.Payroll_Domain;

    namespace Payroll
{
    public class AddSalaryEmployee : AddEmployee
    {
        private double mySalary;


        public AddSalaryEmployee(IPayrollDB database, int id, string name, string address, double mySalary) : base(database, id, name, address)
        {
            this.mySalary = mySalary;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }

        protected override PaymentClassification MakeClassification()
        {
            SalariedClassification sc = new SalariedClassification(mySalary);
            return sc;
        }
    }
}