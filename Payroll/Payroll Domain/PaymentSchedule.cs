using System;

namespace Payroll
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
    }
}