using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestChangePaymentMethods:TestSetup
    {
        [Test]
        public void ChangeEmployeePaymentMethodToHold()
        {
            AddSalariedEmployeeToDB();
            ChangePmTransaction toHold = new ChangePmHold(empID);
            toHold.Execute();
            Employee employee = PayrollDB.GetEmployee(empID);
            Assert.IsTrue(employee.Paymentmethod is HoldMethod);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToMail()
        {
            AddSalariedEmployeeToDB();
            ChangePmTransaction toMail = new ChangePmMail(empID, address);
            toMail.Execute();
            Employee employee = PayrollDB.GetEmployee(empID);

            Assert.IsTrue(employee.Paymentmethod is MailPaymentMethod);
            MailPaymentMethod ml = employee.Paymentmethod as MailPaymentMethod;
            Assert.AreEqual(address, ml.Address);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToAccount()
        {
            AddSalariedEmployeeToDB();
            int accountNumber = 1533352425;
            ChangePmTransaction toAccount = new ChangePmAccount(empID, accountNumber);
            toAccount.Execute();

            Employee employee = PayrollDB.GetEmployee(empID);
            Assert.IsTrue(employee.Paymentmethod is AccountPaymentMethod);
            AccountPaymentMethod accountPM = employee.Paymentmethod as AccountPaymentMethod;
            Assert.AreEqual(accountNumber, accountPM.AccountNumber);
        }
    }
}