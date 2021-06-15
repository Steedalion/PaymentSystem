using Payroll.Tests.Transactions;

namespace Payroll
{
    public class MailPaymentMethod : PaymentMethod
    {
        public string Address { get; }

        public MailPaymentMethod(string address)
        {
            Address = address;
        }

        public void pay(PayCheck payCheck)
        {
            throw new System.NotImplementedException();
        }
    }
}