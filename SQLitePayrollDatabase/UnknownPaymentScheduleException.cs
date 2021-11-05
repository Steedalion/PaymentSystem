namespace PayrollDataBase
{
    public class UnknownPaymentScheduleException : UnknownTypeException
    {
        public UnknownPaymentScheduleException(string message) : base(message)
        {
        }
    }
}