namespace Payroll.Tests
{
    public class ChangePmAccount : ChangePmTransaction
    {
        private int accountNumber;

        public ChangePmAccount(int empId, int accountNumber) : base(empId)
        {
            this.accountNumber = accountNumber;
        }

        protected override PaymentMethod SetPaymentMethod()
        {
            return new AccountPaymentMethod(accountNumber);
        }
    }
}