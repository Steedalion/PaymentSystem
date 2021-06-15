using System;
using System.Collections.Generic;
using Payroll.Tests;
using Payroll.Tests.Transactions;

namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        private List<TimeCard> myTimecards = new List<TimeCard>();
        public double Rate;

        public HourlyClassification(double rate)
        {
            Rate = rate;
        }

        public void AddTimeCard(TimeCard tc)
        {
            myTimecards.Add(tc);
        }

        public TimeCard GetTimeCard(DateTime dateTime)
        {
            foreach (TimeCard card in myTimecards)
            {
                if (card.Date.Equals(dateTime))
                {
                    return card;
                }
            }

            throw new TimeCardNotFound();
        }

        public double CalculatePay(PayCheck payCheck)
        {
            double pay = 0;
            foreach (TimeCard card in myTimecards)
            {
                if (InPayPeriod(card.Date, payCheck.PayDate))
                {
                    pay += card.Hours * Rate;
                }
            }

            return pay;
        }

        private bool InPayPeriod(DateTime cardDate, DateTime PayDate)
        {
            return cardDate >= PayDate.AddDays(-5) && cardDate <= PayDate;
        }
    }

    public class TimeCardNotFound : Exception
    {
    }
}