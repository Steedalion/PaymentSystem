using System;
using System.Collections.Generic;
using PayrollDomain;

namespace PaymentClassification.PaymentClassifications
{
    public class HourlyClassification : PayrollDomain.PaymentClassification
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
                    pay += CalculatePay(card);
                }
            }

            return pay;
        }

        private double CalculatePay(TimeCard card)
        {
            if (card.Hours <= 8)
            {
                return card.Hours * Rate;
            }

            double overtime = card.Hours - 8;

            return (8 + 1.5 * overtime) * Rate;
        }

        private bool InPayPeriod(DateTime cardDate, DateTime PayDate)
        {
            return cardDate >= PayDate.AddDays(-5) && cardDate <= PayDate;
        }
    }
}