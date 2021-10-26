using PaymentClassifications.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace PaymentClassifications.ChangeClassification
{
    public class ChangeSalaryEmployee : ChangeEmployeeClassification
    {
        private double salary;

        public ChangeSalaryEmployee(PayrollDB.IPayrollDb database, int empId, double salary) : base(database, empId)
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