using System;

namespace PayrollDomain
{
    public interface IAffiliation
    {
        ServiceCharge GetServiceCharge(DateTime dateTime);
        void AddServiceCharge(ServiceCharge sc);
        double CalculateDeductions(PayCheck payCheck);
    }
}