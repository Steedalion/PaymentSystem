using PayrollDomains;

namespace PaymentMethod
{
    public class MailPaymentMethod : PayrollDomains.PaymentMethod
    {
        public string Address { get; }

        public MailPaymentMethod(string address)
        {
            Address = address;
        }

        public void pay(PayCheck payCheck)
        {
            payCheck.SetField("Disposition", "Mail");
            payCheck.SetField("Address", Address);
        }
    }
}