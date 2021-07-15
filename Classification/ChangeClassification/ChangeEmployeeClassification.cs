using Domain;
using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction.ChangeEmployee;

namespace PaymentClassification.ChangeClassification
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

        protected abstract PayrollDomain.PaymentClassification MakeClassification();
    }
}