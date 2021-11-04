using PayrollDatabase;
using PayrollDomain;
using Transactions;

namespace PaymentMethods
{
    public class ChangePmHold : ChangePmTransaction
    {
        public ChangePmHold(IPayrollDb database, int empId) : base(database, empId)
        {
        }

        protected override PayrollDomain.PaymentMethod SetPaymentMethod()
        {
            return new HoldMethod();
        }
    }
}