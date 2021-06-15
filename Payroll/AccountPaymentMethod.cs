using Payroll.Tests.Transactions;

namespace Payroll
{
    public class AccountPaymentMethod : PaymentMethod
    {
        public int AccountNumber { get; }

        public AccountPaymentMethod(int accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public void pay(PayCheck payCheck)
        {
            throw new System.NotImplementedException();
        }
    }
}