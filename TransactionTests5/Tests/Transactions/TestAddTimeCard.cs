using System;
using NUnit.Framework;
using PaymentClassifications;
using PaymentClassifications.PaymentClassifications;
using PayrollDatabase;
using PayrollDomain;

namespace TransactionTests.Tests.Transactions
{
    public class TestAddTimeCard : TestSetupTransactions
    {

        [Test]
        public void AddedTimeCardShouldBe()
        {
            AddHourlyEmployeeToDB();

            AddTimeCard addTimeCard = new AddTimeCard(database,EmpId, new DateTime(2005, 7, 31), 8.00);
            addTimeCard.Execute();

            Employee e = database.GetEmployee(EmpId);
            Assert.IsNotNull(e);

            PayrollDomain.PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.AreEqual(8.0, tc.Hours, 0.01);

            Assert.Throws<TimeCardNotFound>(() =>hc.GetTimeCard(OtherDate));
        }

        
        [Test]
        public void AddTimecardWhenEmployeeDoesnotExist()
        {
            AddTimeCard addTimeCard = new AddTimeCard(database,EmpId, new DateTime(2005, 7, 31), 8.00);
            Assert.Throws<EmployeeNotFound>(() => addTimeCard.Execute());
        }
    }
}