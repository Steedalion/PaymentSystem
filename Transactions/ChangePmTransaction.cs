using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction.ChangeEmployee;

namespace Transactions
{
    public abstract class ChangePmTransaction : ChangeEmployeeTransaction
    {
        protected ChangePmTransaction(IPayrollDb database, int empId) : base(database, empId)
        {
        }

        protected abstract IPaymentMethod SetPaymentMethod();
        protected override void ModifyEmployee(Employee employee)
        {
            employee.Paymentmethod = SetPaymentMethod();
        }
    }

}