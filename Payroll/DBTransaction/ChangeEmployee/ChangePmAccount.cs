namespace Payroll.Tests
{
    public class ChangePmAccount : ChangePmTransaction
    {
        private int accountNumber;
        private string bank;

        public ChangePmAccount(InMemoryDB database, int empId,string bank, int accountNumber) : base(database, empId)
        {
            this.bank = bank;
            this.accountNumber = accountNumber;
        }


        protected override PaymentMethod SetPaymentMethod()
        {
            return new AccountPaymentMethod(bank, accountNumber);
        }
    }
}