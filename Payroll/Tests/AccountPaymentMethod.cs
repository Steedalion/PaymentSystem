namespace Payroll.Tests
{
    public class AccountPaymentMethod : PaymentMethod
    {
        public int AccountNumber { get; }

        public AccountPaymentMethod(int accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}