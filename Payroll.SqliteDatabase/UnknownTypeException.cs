using System;

namespace PayrollDataBase
{
    internal class UnknownTypeException : NotSupportedException
    {
        public UnknownTypeException(string message) : base(message)
        {
        }
    }
}