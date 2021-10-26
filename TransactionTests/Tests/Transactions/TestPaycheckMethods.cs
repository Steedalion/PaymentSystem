using System;
using NUnit.Framework;
using PaymentClassifications;
using PaymentMethods;
using PayrollDomain;
using Transactions.DBTransaction;

namespace TransactionTests.Tests.Transactions
{
    public class TestPaycheckMethods : TestPay
    {
        [Test]
        public void TestAccountPaymentMethod()
        {
            AddSalariedEmployeeToDb();
            ChangePmAccount account = new ChangePmAccount(database, EmpId, "GoodBank", 134);
            account.Execute();
            DateTime payday = new DateTime(2021, 08, 31);
            PayDayTransaction payDayTransaction = new PayDayTransaction(database, payday);
            payDayTransaction.Execute();
            PayCheck payCheck = payDayTransaction.GetPayCheck(EmpId);
            Assert.AreEqual("Account",payCheck.GetField("Disposition"));
            Assert.AreEqual("GoodBank",payCheck.GetField("Bank"));
            Assert.AreEqual("134",payCheck.GetField("AccountNumber"));
        }
        
         [Test]
        public void TestMailPaymentMethod()
        {
            AddSalariedEmployeeToDb();
            ChangePmMail mail = new ChangePmMail(database, EmpId, "123 Home street.");
            mail.Execute();
            DateTime payday = new DateTime(2021, 08, 31);
            PayDayTransaction payDayTransaction = new PayDayTransaction(database, payday);
            payDayTransaction.Execute();
            PayCheck payCheck = payDayTransaction.GetPayCheck(EmpId);
            Assert.AreEqual("Mail",payCheck.GetField("Disposition"));
            Assert.AreEqual("123 Home street.",payCheck.GetField("Address"));
        }
    }
}