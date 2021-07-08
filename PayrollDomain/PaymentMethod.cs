using Payroll.Tests.Transactions;

namespace Payroll
{
    public interface PaymentMethod
    {
        void pay(PayCheck payCheck);
    }
}