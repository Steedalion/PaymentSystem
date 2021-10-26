using PayrollDomain;

namespace PaymentClassifications.PaymentClassifications
{
    public class SalariedClassification : PayrollDomain.PaymentClassification
    {
        public readonly double Salary;

        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public double CalculatePay(PayCheck payCheck)
        {
            return Salary;
        }
    }
}