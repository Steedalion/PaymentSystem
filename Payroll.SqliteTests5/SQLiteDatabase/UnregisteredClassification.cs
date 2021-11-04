using PayrollDomain;

namespace DatabaseTests.SQLiteDatabase
{
    internal class UnregisteredClassification : PayrollDomain.PaymentClassification
    {
        public double CalculatePay(PayCheck payCheck)
        {
            return 0;
        }
    }
}