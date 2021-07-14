using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace PaymentClassification.ChangeClassification
{
    public class ChangeSalaryEmployee : ChangeEmployeeClassification
    {
        private double salary;

        public ChangeSalaryEmployee(IPayrollDB database, int empId, double salary) : base(database, empId)
        {
            this.salary = salary;
        }


        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override PayrollDomain.PaymentClassification MakeClassification()
        {
            return new SalariedClassification(salary);
        }
    }
}