using PayrollDomain;

namespace DatabaseTests.SQLiteDatabase
{
    internal class UnregisteredClassification : PayrollDomain.IPaymentClassification
    {
        public double CalculatePay(PayCheck payCheck)
        {
            return 0;
        }
    }
}