using System;
using Payroll.Tests;

namespace Payroll
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

}