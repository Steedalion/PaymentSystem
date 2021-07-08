using Payroll.Tests.Transactions;
using PayrollDomain.Payroll_Domain;

namespace Payroll
{
    public class SalariedClassification : PaymentClassification
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