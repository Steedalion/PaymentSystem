using PaymentClassifications.PaymentClassifications;
using PayrollDatabase;
using PayrollDomain;
using Schedules;

namespace PaymentClassifications.ChangeClassification
{
    public class ChangeSalaryEmployee : ChangeEmployeeClassification
    {
        private double salary;

        public ChangeSalaryEmployee(IPayrollDb database, int empId, double salary) : base(database, empId)
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