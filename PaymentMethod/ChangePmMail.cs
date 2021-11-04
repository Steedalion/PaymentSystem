using PayrollDatabase;
using Transaction;

namespace PaymentMethod
{
    public class ChangePmMail : ChangePmTransaction
    {
        private string address;

        public ChangePmMail(IPayrollDb database, int empId, string address) : base(database, empId)
        {
            this.address = address;
        }


        protected override PayrollDomains.PaymentMethod SetPaymentMethod()
        {
            return new MailPaymentMethod(address);
        }
    }
}