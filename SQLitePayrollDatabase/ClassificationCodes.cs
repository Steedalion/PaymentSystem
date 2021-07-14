using PaymentClassification.PaymentClassifications;

namespace PayrollDataBase
{
    public static class ClassificationCodes
    {
        public static string Salary = "Salary";
        
        public static string Code(PayrollDomain.PaymentClassification employeeClassification)
        {
            if (employeeClassification is SalariedClassification)
            {
                return "Salary";
            }

            return "unknown Payment Classification";
        }
    }
}