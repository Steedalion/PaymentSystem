using System;
using System.Collections.Generic;
using Payroll.Tests;

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
    }

    public class TimeCardNotFound : Exception
    {
    }
}