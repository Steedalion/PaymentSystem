using System;
using System.Collections.Generic;
using PayrollDomain;

namespace Affiliations
{
    public class UnionAffiliation : Affiliation
    {
        private List<ServiceCharge> charges = new List<ServiceCharge>();
        public double Dues { get; set; }

        public void AddServiceCharge(ServiceCharge sc)
        {
            charges.Add(sc);
        }

        public double CalculateDeductions()
        {
            throw new NotImplementedException();
        }

        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            foreach (ServiceCharge charge in charges)
            {
                if (charge.DATE == dateTime)
                {
                    return charge;
                }
            }

            throw new ServiceChargeNotFound();
        }
    }
    
}