namespace Payroll
{
    public class MailPaymentMethod : PaymentMethod
    {
        public string Address { get; }

        public MailPaymentMethod(string address)
        {
            Address = address;
        }
    }
}