using System;

namespace PayrollDomains
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
        DateTime GetStartDate(DateTime payDate);
    }
}