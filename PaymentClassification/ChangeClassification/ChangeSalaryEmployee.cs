using PaymentClassification.PaymentClassifications;
using PayrollDatabase;
using PayrollDomains;
using Schedule;

namespace PaymentClassification.ChangeClassification
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

        protected override PayrollDomains.PaymentClassification MakeClassification()
        {
            return new SalariedClassification(salary);
        }
    }
}