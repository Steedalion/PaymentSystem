namespace PayrollDataBase
{
    internal class UnknownPaymentMethodExcpetion : UnknownTypeException
    {
        public UnknownPaymentMethodExcpetion(string message) : base(message)
        {
        }
    }
}