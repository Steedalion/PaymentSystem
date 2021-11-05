using PayrollDB;
using PayrollDomain;
using Transactions;

namespace PaymentMethods
{
    public class ChangePmMail : ChangePmTransaction
    {
        private string address;

        public ChangePmMail(PayrollDB.IPayrollDb database, int empId, string address) : base(database, empId)
        {
            this.address = address;
        }


        protected override IPaymentMethod SetPaymentMethod()
        {
            return new MailPaymentMethod(address);
        }
    }
}