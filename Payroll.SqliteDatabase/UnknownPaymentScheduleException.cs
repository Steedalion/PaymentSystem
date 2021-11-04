namespace PayrollDataBase
{
    internal class UnknownPaymentScheduleException : UnknownTypeException
    {
        public UnknownPaymentScheduleException(string message) : base(message)
        {
        }
    }
}