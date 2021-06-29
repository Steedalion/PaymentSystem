namespace Payroll.Tests
{
    public class ChangePmAccount : ChangePmTransaction
    {
        private int accountNumber;

        public ChangePmAccount(PayrollDB database, int empId, int accountNumber) : base(database, empId)
        {
            this.accountNumber = accountNumber;
        }


        protected override PaymentMethod SetPaymentMethod()
        {
            return new AccountPaymentMethod(accountNumber);
        }
    }
}