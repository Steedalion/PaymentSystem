using System;

namespace PayrollDomain
{
    public class NoAffiliation : Affiliation
    {
        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            throw new ServiceChargeNotFound();
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            throw new NullReferenceException();
        }

        public double CalculateDeductions()
        {

            return 0;
        }
    }

}