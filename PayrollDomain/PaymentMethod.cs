namespace PayrollDomain
{
    public interface PaymentMethod
    {
        void pay(PayCheck payCheck);
    }
}