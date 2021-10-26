using PaymentClassifications.PaymentClassifications;

namespace PayrollDataBase
{
    public static class ClassificationCodes
    {
        public static string Salary = "Salary";
        public static string Commision = "Commision";
        public static string Hourly = "Hourly";

        public static string Code(PayrollDomain.PaymentClassification employeeClassification)
        {
            if (employeeClassification is SalariedClassification)
            {
                return Salary;
            }
            else if (employeeClassification is CommisionClassification)
            {
                return Commision;
            }
            else if (employeeClassification is HourlyClassification)
            {
                return Hourly;
            }

            throw new UnknownClassificationException("Classification Code is not specified");
        }
    }
}