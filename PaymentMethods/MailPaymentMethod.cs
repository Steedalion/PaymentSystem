using PayrollDomain;

namespace PaymentMethods
{
    public class MailPaymentMethod : IPaymentMethod
    {
        public string Address { get; }

        public MailPaymentMethod(string address)
        {
            Address = address;
        }

        public void Pay(PayCheck payCheck)
        {
            payCheck.SetField("Disposition", "Mail");
            payCheck.SetField("Address", Address);
        }
    }
}