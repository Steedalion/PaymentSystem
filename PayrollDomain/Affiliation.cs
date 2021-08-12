using System;

namespace PayrollDomain
{
    public interface Affiliation
    {
        ServiceCharge GetServiceCharge(DateTime dateTime);
        void AddServiceCharge(ServiceCharge sc);
        double CalculateDeductions();
    }
}