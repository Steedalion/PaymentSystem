using PayrollDatabase;
using PayrollDomains;
using Transaction;

namespace PaymentMethod
{
    public class ChangePmHold : ChangePmTransaction
    {
        public ChangePmHold(IPayrollDb database, int empId) : base(database, empId)
        {
        }

        protected override PayrollDomains.PaymentMethod SetPaymentMethod()
        {
            return new HoldMethod();
        }
    }
}