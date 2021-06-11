using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    public class TestChangePaymentMethods:TestSetupTransactions
    {
        [Test]
        public void ChangeEmployeePaymentMethodToHold()
        {
            AddSalariedEmployeeToDb();
            ChangePmTransaction toHold = new ChangePmHold(EmpId);
            toHold.Execute();
            Employee employee = PayrollDB.GetEmployee(EmpId);
            Assert.IsTrue(employee.Paymentmethod is HoldMethod);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToMail()
        {
            AddSalariedEmployeeToDb();
            ChangePmTransaction toMail = new ChangePmMail(EmpId, Address);
            toMail.Execute();
            Employee employee = PayrollDB.GetEmployee(EmpId);

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
            ChangePmTransaction toAccount = new ChangePmAccount(EmpId, accountNumber);
            toAccount.Execute();

            Employee employee = PayrollDB.GetEmployee(EmpId);
            Assert.IsTrue(employee.Paymentmethod is AccountPaymentMethod);
            AccountPaymentMethod accountPm = employee.Paymentmethod as AccountPaymentMethod;
            Assert.NotNull(accountPm);
            Assert.AreEqual(accountNumber, accountPm.AccountNumber);
        }
    }
}