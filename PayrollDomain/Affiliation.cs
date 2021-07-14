using System;

namespace PayrollDomain
{
    public interface Affiliation
    {
        ServiceCharge GetServiceCharge(DateTime dateTime);
        void AddServiceCharge(ServiceCharge sc);
        double CalculateDeductions();
    }

    public class ServiceChargeNotFound : Exception
    {
    }
        public class UnaffiliatedException : Exception
    {
    }
          public class AlreadyAffiliated : Exception
    {
    }
          public class ServiceCharge
    {
        public DateTime DATE;

        public double Amount { get; set; }

        public ServiceCharge(DateTime date, double amount)
        {
            DATE = date;
            Amount = amount;
        }
    }

}