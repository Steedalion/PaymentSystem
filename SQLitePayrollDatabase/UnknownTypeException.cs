using System;

namespace PayrollDataBase
{
    public class UnknownTypeException : NotSupportedException
    {
        public UnknownTypeException(string message) : base(message)
        {
        }
    }
}