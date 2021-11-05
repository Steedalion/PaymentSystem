using System;

namespace PayrollDomain
{
    public interface IPaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
        DateTime GetStartDate(DateTime payDate);
    }
}