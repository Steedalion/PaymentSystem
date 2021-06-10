using System;
using System.Collections.Generic;

namespace Payroll.Tests
{
    public class UnionAffiliation : Affiliation
    {
        private List<ServiceCharge> charges = new List<ServiceCharge>();
        public void AddServiceCharge(ServiceCharge sc)
        {
            charges.Add(sc);
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