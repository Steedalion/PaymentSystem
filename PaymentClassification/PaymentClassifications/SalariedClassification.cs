using PayrollDomains;

namespace PaymentClassification.PaymentClassifications
{
    public class SalariedClassification : PayrollDomains.PaymentClassification
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