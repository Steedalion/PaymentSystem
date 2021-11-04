using PayrollDatabase;
using PayrollDomain;
using Transactions.DBTransaction.ChangeEmployee;

namespace PaymentClassifications.ChangeClassification
{
    public abstract class ChangeEmployeeClassification : ChangeEmployeeTransaction
    {
        protected ChangeEmployeeClassification(IPayrollDb database, int empId) : base(database, empId)
        {
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Classification = MakeClassification();
            employee.Schedule = MakePaymentSchedule();
        }

        protected abstract PaymentSchedule MakePaymentSchedule();

        protected abstract PayrollDomain.PaymentClassification MakeClassification();
    }
}