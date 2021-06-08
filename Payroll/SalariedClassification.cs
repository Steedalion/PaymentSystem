namespace Payroll
{
    public class SalariedClassification : PaymentClassification
    {
        public readonly double Salary;

        public SalariedClassification(double salary)
        {
            Salary = salary;
        }
    }
}