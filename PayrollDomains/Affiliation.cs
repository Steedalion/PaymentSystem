using System;

namespace PayrollDomains
{
    public interface Affiliation
    {
        ServiceCharge GetServiceCharge(DateTime dateTime);
        void AddServiceCharge(ServiceCharge sc);
        double CalculateDeductions(PayCheck payCheck);
    }
}