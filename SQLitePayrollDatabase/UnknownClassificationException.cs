namespace PayrollDataBase
{
    public class UnknownClassificationException : UnknownTypeException
    {
        public UnknownClassificationException(string unknownClassification) : base(unknownClassification)
        {
        }
    }
}