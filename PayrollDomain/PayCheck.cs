using System;
using System.Collections.Generic;

namespace PayrollDomain
{
    public class PayCheck
    {
        private Dictionary<string, string> fields = new Dictionary<string, string>();
        private string Disposition;
        public DateTime PayDate { get; set; }
        public double GrossPay { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => PayDate;

        public PayCheck(DateTime startDate, DateTime payDate)
        {
            StartDate = startDate;
            PayDate = payDate;
        }

        public string GetField(string fieldName)
        {
            return fields[fieldName];
        }

        public void SetField(string field, string value)
        {
            fields[field] = value;
        }
    }
}