namespace PayrollDataBase
{
    internal class UnknownClassificationException : UnknownTypeException
    {
        public UnknownClassificationException(string unknownClassification) : base(unknownClassification)
        {
        }
    }
}