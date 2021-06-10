namespace Payroll
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