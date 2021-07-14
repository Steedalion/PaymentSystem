using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction.ChangeEmployee;

namespace Transactions
{
    public abstract class ChangePmTransaction : ChangeEmployeeTransaction
    {
        protected ChangePmTransaction(IPayrollDB database, int empId) : base(database, empId)
        {
        }

        protected abstract PaymentMethod SetPaymentMethod();
        protected override void ModifyEmployee(Employee employee)
        {
            employee.Paymentmethod = SetPaymentMethod();
        }
    }

}