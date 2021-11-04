using System;
using System.Collections.Generic;
using PayrollDomain;

namespace Affiliations
{
    public class UnionAffiliation : PayrollDomain.Affiliation
    {
        private List<ServiceCharge> charges = new List<ServiceCharge>();
        public double Dues { get; set; }

        public void AddServiceCharge(ServiceCharge sc)
        {
            charges.Add(sc);
        }

        public double CalculateDeductions(PayCheck payCheck)
        {
            double totalDues = Fridays(payCheck.StartDate, payCheck.EndDate) * Dues;
            double totalServiceCharges = CalculateServiceCharges(payCheck.StartDate, payCheck.EndDate);
            return totalDues+totalServiceCharges;
        }

        private double CalculateServiceCharges(DateTime payCheckStartDate, DateTime payCheckEndDate)
        {
            double total = 0;
            foreach (ServiceCharge charge in charges)
            {
                if (payCheckStartDate<charge.DATE && charge.DATE <= payCheckEndDate)
                {
                    total += charge.Amount;
                }
            }
            return total;
        }

        private double Fridays(DateTime startDate, DateTime endDate)
        {
            int fridays = 0;
            for (DateTime day = startDate; day < endDate; day = day.AddDays(1))
            {
                if (day.DayOfWeek ==DayOfWeek.Friday)
                {
                    fridays++;
                }
            }
            return fridays;
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