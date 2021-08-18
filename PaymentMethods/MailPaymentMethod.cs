using PayrollDomain;

namespace PaymentMethods
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
            payCheck.SetField("Disposition", "Mail");
            payCheck.SetField("Address", Address);
        }
    }
}