namespace PayrollDataBase
{
    public class UnknownPaymentMethodExcpetion : UnknownTypeException
    {
        public UnknownPaymentMethodExcpetion(string message) : base(message)
        {
        }
    }
}