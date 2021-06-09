using System;
using Payroll.Tests;

namespace Payroll
{
    public interface Affiliation
    {
        ServiceCharge GetServiceCharge(DateTime dateTime);
        void AddServiceCharge(ServiceCharge sc);
    }

    public class ServiceChargeNotFound : Exception
    {
    }
        public class Unaffiliated : Exception
    {
    }
          public class AlreadyAffiliated : Exception
    {
    }

}