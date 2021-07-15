

using PayrollDomain;

namespace PaymentMethods
{
    public class AccountPaymentMethod : PaymentMethod
    {
        public int AccountNumber { get; }
        public string bank { get; }
        public AccountPaymentMethod(string bank,int accountNumber)
        {
            AccountNumber = accountNumber;
            this.bank = bank;
        }

        public void pay(PayCheck payCheck)
        {
            throw new System.NotImplementedException();
        }
    }
}