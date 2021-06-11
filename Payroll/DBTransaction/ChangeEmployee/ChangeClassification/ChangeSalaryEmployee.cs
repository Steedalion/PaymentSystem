namespace Payroll.Tests.Transactions
{
    public class ChangeSalaryEmployee : ChangeEmployeeClassification
    {
        private double salary;

        public ChangeSalaryEmployee(int empId, double salary) : base(empId)
        {
            this.salary = salary;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification(salary);
        }
    }
}