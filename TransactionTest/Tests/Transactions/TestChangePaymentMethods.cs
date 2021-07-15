using Domain;
using NUnit.Framework;
using PaymentMethods;
using PayrollDomain;
using Transactions;

namespace Payroll.Tests.Transactions
{
    public class TestChangePaymentMethods : TestSetupTransactions
    {
        [Test]
        public void ChangeEmployeePaymentMethodToHold()
        {
            AddSalariedEmployeeToDb();
            ChangePmTransaction toHold = new ChangePmHold(database, EmpId);
            toHold.Execute();
            Employee employee = database.GetEmployee(EmpId);
            Assert.IsTrue(employee.Paymentmethod is HoldMethod);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToMail()
        {
            AddSalariedEmployeeToDb();
            ChangePmTransaction toMail = new ChangePmMail(database, EmpId, Address);
            toMail.Execute();
            Employee employee = database.GetEmployee(EmpId);

            Assert.IsTrue(employee.Paymentmethod is MailPaymentMethod);
            MailPaymentMethod ml = employee.Paymentmethod as MailPaymentMethod;

            Assert.NotNull(ml);
            Assert.AreEqual(Address, ml.Address);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToAccount()
        {
            AddSalariedEmployeeToDb();
            int accountNumber = 1533352425;
            ChangePmTransaction toAccount = new ChangePmAccount(database, EmpId, "bank", accountNumber);
            toAccount.Execute();

            Employee employee = database.GetEmployee(EmpId);
            Assert.IsTrue(employee.Paymentmethod is AccountPaymentMethod);
            AccountPaymentMethod accountPm = employee.Paymentmethod as AccountPaymentMethod;

            Assert.NotNull(accountPm);
            Assert.AreEqual("bank", accountPm.bank);
            Assert.AreEqual(accountNumber, accountPm.AccountNumber);
        }
    }
}