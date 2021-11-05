namespace PayrollDomain
{
    public interface IPaymentMethod
    {
        void Pay(PayCheck payCheck);
    }
}