

using PayrollDomain;

namespace PaymentMethods
{
    public class AccountPaymentMethod : IPaymentMethod
    {
        public int AccountNumber { get; }
        public string bank { get; }
        public AccountPaymentMethod(string bank,int accountNumber)
        {
            AccountNumber = accountNumber;
            this.bank = bank;
        }

        public void Pay(PayCheck payCheck)
        {
            payCheck.SetField("Disposition", "Account");
            payCheck.SetField("Bank", bank);
            payCheck.SetField("AccountNumber", AccountNumber.ToString());
        }
    }
}