using Payroll.Tests.Transactions;

namespace Payroll
{
    public interface PaymentClassification
    {
        double CalculatePay(PayCheck payCheck);
    }
}