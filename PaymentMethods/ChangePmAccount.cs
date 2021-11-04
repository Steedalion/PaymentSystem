using PayrollDatabase;
using Transactions;

namespace PaymentMethods
{
    public class ChangePmAccount : ChangePmTransaction
    {
        private int accountNumber;
        private string bank;

        public ChangePmAccount(IPayrollDb database, int empId,string bank, int accountNumber) : base(database, empId)
        {
            this.bank = bank;
            this.accountNumber = accountNumber;
        }


        protected override PayrollDomain.PaymentMethod SetPaymentMethod()
        {
            return new AccountPaymentMethod(bank, accountNumber);
        }
    }
}