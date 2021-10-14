using NUnit.Framework;
using PaymentMethods;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    public class GetEmployeeMethods : AddEmployeePaymentMethods
    {
        [Test]
        public void GetAccountPayment()
        {
            AddAccountPayment();
            Employee got = database.GetEmployee(id);
            AccountPaymentMethod acc = got.Paymentmethod as AccountPaymentMethod;
            Assert.IsTrue(got.Paymentmethod is AccountPaymentMethod);
            Assert.AreEqual("Woori", acc.bank);
            Assert.AreEqual(800, acc.AccountNumber);
        }

        [Test]
        public void GetMailPayment()
        {
            AddMailPayment();
            Employee got = database.GetEmployee(id);
            MailPaymentMethod acc = got.Paymentmethod as MailPaymentMethod;
            Assert.IsTrue(got.Paymentmethod is AccountPaymentMethod);
            Assert.AreEqual("Home", acc.Address);
        }

        [Test]
        public void GetHoldPayment()
        {
            AddHoldPayment();
            Employee got = database.GetEmployee(id);
            HoldMethod acc = got.Paymentmethod as HoldMethod;
            Assert.IsTrue(got.Paymentmethod is HoldMethod);
        }
    }
}