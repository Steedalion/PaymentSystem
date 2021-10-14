using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    internal class UnregisteredClassification : PayrollDomain.PaymentClassification
    {
        public double CalculatePay(PayCheck payCheck)
        {
            return 0;
        }
    }
}