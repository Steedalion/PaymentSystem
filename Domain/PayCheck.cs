using System;

namespace PayrollDomain
{
    public class PayCheck
    {
        public DateTime PayDate { get; set; }
        public double GrossPay { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }

        public PayCheck(DateTime payDate)
        {
            PayDate = payDate;
        }
    }
}