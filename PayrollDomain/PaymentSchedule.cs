using System;

namespace PayrollDomain
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
    }
}