using PayrollDomain.Payroll_Domain;

namespace Payroll.Tests.Transactions
{
    public abstract class ChangeEmployeeClassification : ChangeEmployeeTransaction
    {
        protected ChangeEmployeeClassification(IPayrollDB database, int empId) : base(database, empId)
        {
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Classification = MakeClassification();
            employee.Schedule = MakePaymentSchedule();
        }

        protected abstract PaymentSchedule MakePaymentSchedule();

        protected abstract PaymentClassification MakeClassification();
    }
}