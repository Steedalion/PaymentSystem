using System;
using System.Collections.Generic;
using Payroll.Tests;

namespace Payroll
{
    class CommisionClassification : PaymentClassification
    {
        private readonly List<SalesReciept> salesReciepts = new List<SalesReciept>();
             public double CommisionRate{ get; }
        public double Salary{ get; }
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
    }

    internal class SalesReceiptNotFound : Exception
    {
    }
}