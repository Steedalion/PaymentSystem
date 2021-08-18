using PayrollDB;
using PayrollDomain;
using Transactions;

namespace PaymentMethods
{
    public class ChangePmHold : ChangePmTransaction
    {
        public ChangePmHold(PayrollDB.IPayrollDb database, int empId) : base(database, empId)
        {
        }

        protected override PaymentMethod SetPaymentMethod()
        {
            return new HoldMethod();
        }
    }
}