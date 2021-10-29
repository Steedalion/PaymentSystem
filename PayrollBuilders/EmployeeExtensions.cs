using System;
using PaymentClassifications.PaymentClassifications;
using PayrollDomain;

namespace PayrollBuilders
{
    public static class EmployeeExtensions
    {
        public static HourlyClassification ClassificationAsHourly(this Employee employee)
        {
            return employee.Classification as HourlyClassification;
        }
        
        public static CommisionClassification ClassificationAsCommision(this Employee employee)
        {
            return employee.Classification as CommisionClassification;
        }
    }
}