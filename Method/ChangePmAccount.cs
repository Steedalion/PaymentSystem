using PayrollDB;
using PayrollDomain;
using Transactions;

namespace PaymentMethods
{
    public class ChangePmAccount : ChangePmTransaction
    {
        private int accountNumber;
        private string bank;

        public ChangePmAccount(IPayrollDB database, int empId,string bank, int accountNumber) : base(database, empId)
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