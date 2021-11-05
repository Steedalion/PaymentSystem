using System;
using PaymentClassifications.PaymentClassifications;
using PayrollDomain;

namespace Payroll.TestBuilders
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