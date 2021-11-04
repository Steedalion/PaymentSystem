namespace PayrollDomains
{
    public interface PaymentMethod
    {
        void pay(PayCheck payCheck);
    }
}