using NUnit.Framework;
using PaymentMethods;
using Payroll.TestBuilders;
using PayrollDataBase;
using PayrollDomain;

namespace DatabaseTests.DatabaseTests
{
    public class AddEmployeePaymentMethods : AddEmployeeTest
    {
        [Test]
        public void AddAccountPayment()
        {
            Employee e = An.GenericEmployee.Bank("Woori", 800).Build();
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void AddHoldPayment()
        {
            Employee e = An.GenericEmployee.HoldPM();
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void AddMailPayment()
        {
            Employee e = An.GenericEmployee.MailPM("Home");
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void MethodCannotBeNull()
        {
            Employee e = An.GenericEmployee.WithoutPaymentMethod().Build();
            Assert.Throws<UnknownPaymentMethodExcpetion>(code: () => database.AddEmployee(id, e));
            Assert.AreEqual(0, EmployeeCount());
        }

        [Test]
        public void GetHoldPayment()
        {
            AddHoldPayment();
            Employee got = database.GetEmployee(id);
            Assert.IsTrue(got.Paymentmethod is HoldMethod);
        }

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
            Assert.IsTrue(got.Paymentmethod is MailPaymentMethod);
            Assert.AreEqual("Home", acc.Address);
        }
    }
}