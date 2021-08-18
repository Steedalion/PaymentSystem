using System;
using Affiliations;
using NUnit.Framework;
using PaymentClassification;
using PayrollDomain;
using Transactions.DBTransaction;

namespace TransactionTests.Tests.Transactions
{
    public class TestDeductions : TestPay
    {
        [Test]
        public void NoPayOnOtherDays()
        {
            AddHourlyEmployeeToDB();

            DateTime notFriday = new DateTime(2001, 11, 10);
            PayDayTransaction payDay = new PayDayTransaction(database, notFriday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);
            Assert.IsNull(payCheck);
        }

 
        [Test]
        public void HourlyUnionMemberDues()
        {
            AddHourlyEmployeeToDB();
            ChangeUnionMember unionMember = new ChangeUnionMember(database, EmpId, MemberId, 9.42);
            unionMember.Execute();
            DateTime paydate = new DateTime(2001, 11, 9);
            AddTimeCard timeCard = new AddTimeCard(database, EmpId, paydate, 8.0);
            timeCard.Execute();
            PayDayTransaction payDayTransaction = new PayDayTransaction(database, paydate);
            payDayTransaction.Execute();
            PayCheck payCheck = payDayTransaction.GetPayCheck(EmpId);
            Assert.AreEqual(paydate, payCheck.PayDate);
            Assert.AreEqual(8 * HourlyRate, payCheck.GrossPay);
            Assert.AreEqual("Hold",payCheck.GetField("Disposition"));
            Assert.AreEqual(9.42 , payCheck.Deductions);
            Assert.AreEqual((8 * HourlyRate - 9.42 ), payCheck.NetPay, 0.001,"NetPay");
        }
        
        [Test]
        public void HourlyUnionMemberServiceCharge()
        {
            AddHourlyEmployeeToDB();
            ChangeUnionMember unionMember = new ChangeUnionMember(database, EmpId, MemberId, 0);
            unionMember.Execute();
            DateTime paydate = new DateTime(2001, 11, 9);
            AddUnionServiceCharge serviceCharge = new AddUnionServiceCharge(database, MemberId, paydate, 19.42
            );
            serviceCharge.Execute();
            AddTimeCard timeCard = new AddTimeCard(database, EmpId, paydate, 8.0);
            timeCard.Execute();
            PayDayTransaction payDayTransaction = new PayDayTransaction(database, paydate);
            payDayTransaction.Execute();
            PayCheck payCheck = payDayTransaction.GetPayCheck(EmpId);
            Assert.AreEqual(paydate, payCheck.PayDate);
            Assert.AreEqual(8 * HourlyRate, payCheck.GrossPay);
            Assert.AreEqual("Hold",payCheck.GetField("Disposition"));
            Assert.AreEqual(19.42, payCheck.Deductions);
            Assert.AreEqual((8 * HourlyRate - 19.42), payCheck.NetPay, 0.001);
        }
        [Test]
        public void HourlyUnionMemberServiceChargeAndDues()
        {
            AddHourlyEmployeeToDB();
            ChangeUnionMember unionMember = new ChangeUnionMember(database, EmpId, MemberId, 9.42);
            unionMember.Execute();
            DateTime paydate = new DateTime(2001, 11, 9);
            AddUnionServiceCharge serviceCharge = new AddUnionServiceCharge(database, MemberId, paydate, 19.42
            );
            serviceCharge.Execute();
            AddTimeCard timeCard = new AddTimeCard(database, EmpId, paydate, 8.0);
            timeCard.Execute();
            PayDayTransaction payDayTransaction = new PayDayTransaction(database, paydate);
            payDayTransaction.Execute();
            PayCheck payCheck = payDayTransaction.GetPayCheck(EmpId);
            Assert.AreEqual(paydate, payCheck.PayDate);
            Assert.AreEqual(8 * HourlyRate, payCheck.GrossPay, "Gross pay incorrect");
            Assert.AreEqual("Hold",payCheck.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, payCheck.Deductions);
            Assert.AreEqual((8 * HourlyRate - (9.42 + 19.42)), payCheck.NetPay, 0.001, "Net pay incorrect");
        }

        [Test]
        public void DateTimeForLoop()
        {
            DateTime start = DateTime.Now;

            DateTime endDate= start.AddDays(7);
            Console.WriteLine(start);
            for (DateTime dat = start; dat <= endDate; dat = dat.AddDays(1))
            {
                Console.WriteLine("start :" +dat, "end "+endDate);
                
            }
        }
        
        [Test]
        public void ServiceChargeSpanningMultiplePayPeriods()
        {
            AddHourlyEmployeeToDB();
            ChangeUnionMember unionMember = new ChangeUnionMember(database, EmpId, MemberId, 9.42);
            unionMember.Execute();
            DateTime paydate = new DateTime(2001, 11, 9);
             DateTime early = new DateTime(2001, 11, 2);
              DateTime late = new DateTime(2001, 11, 16);
            AddUnionServiceCharge serviceCharge = new AddUnionServiceCharge(database, MemberId, paydate, 19.42
            );
            serviceCharge.Execute();
            AddUnionServiceCharge earlyCharge  = new AddUnionServiceCharge(database, MemberId, early, 10
            );
            earlyCharge.Execute();
             AddUnionServiceCharge lateCharge = new AddUnionServiceCharge(database, MemberId, late, 10
            );
            lateCharge.Execute();
            AddTimeCard timeCard = new AddTimeCard(database, EmpId, paydate, 8.0);
            timeCard.Execute();
            PayDayTransaction payDayTransaction = new PayDayTransaction(database, paydate);
            payDayTransaction.Execute();
            PayCheck payCheck = payDayTransaction.GetPayCheck(EmpId);
            Assert.AreEqual(paydate, payCheck.PayDate);
            Assert.AreEqual(8 * HourlyRate, payCheck.GrossPay, "Gross pay incorrect");
            Assert.AreEqual("Hold",payCheck.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, payCheck.Deductions);
            Assert.AreEqual((8 * HourlyRate - (9.42 + 19.42)), payCheck.NetPay, 0.001, "Net pay incorrect");
        }
    }
}