using System;
using System.Collections.Generic;
using PayrollDomains;

namespace PaymentClassification.PaymentClassifications
{
    public class CommisionClassification : PayrollDomains.PaymentClassification
    {
        private readonly List<SalesReciept> salesReciepts = new List<SalesReciept>();
        public double CommisionRate { get; }
        public double Salary { get; }

        public CommisionClassification(double commisionRate, double salary)
        {
            CommisionRate = commisionRate;
            Salary = salary;
        }


        public SalesReciept GetSalesReciept(DateTime dateTime)
        {
            foreach (SalesReciept salesReciept in salesReciepts)
            {
                if (salesReciept.DATE == dateTime)
                {
                    return salesReciept;
                }
            }

            throw new SalesReceiptNotFound();
        }

        public void AddSalesReciept(DateTime date, double ammount)
        {
            SalesReciept sale = new SalesReciept(date, ammount);
            salesReciepts.Add(sale);
        }

        public double CalculatePay(PayCheck payCheck)
        {
            return Salary + CalculateSalesRev(payCheck.PayDate);
        }

        private double CalculateSalesRev(DateTime payCheckPayDate)
        {
            double total = 0;
            foreach (SalesReciept receipt in salesReciepts)
            {
                if (isInPeriod(payCheckPayDate, receipt))
                {
                    total += receipt.Amount * CommisionRate;
                }
            }
            return total;
        }

        private bool isInPeriod(DateTime payCheckDate, SalesReciept receipt)
        {
            return payCheckDate >= receipt.DATE && receipt.DATE > payCheckDate.AddDays(-14);
        }

        public List<SalesReciept> GetAllSalesReceipts()
        {
            return salesReciepts;
        }
    }
}