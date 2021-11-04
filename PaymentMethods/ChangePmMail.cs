using PayrollDatabase;
using Transactions;

namespace PaymentMethods
{
    public class ChangePmMail : ChangePmTransaction
    {
        private string address;

        public ChangePmMail(IPayrollDb database, int empId, string address) : base(database, empId)
        {
            this.address = address;
        }


        protected override PayrollDomain.PaymentMethod SetPaymentMethod()
        {
            return new MailPaymentMethod(address);
        }
    }
}