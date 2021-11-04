using PayrollDatabase;
using PayrollDomains;
using Transaction.DBTransaction.ChangeEmployee;

namespace PaymentClassification.ChangeClassification
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

        protected abstract PayrollDomains.PaymentClassification MakeClassification();
    }
}