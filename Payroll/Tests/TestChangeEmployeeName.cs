using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestChangeEmployeeName : TestSetup
    {
        [Test]
        public void ChangeNotExistingEmployeeNameShouldThrowException()
        {
            string newName = "henruy";
            PayrollDB.Clear();
            ChangeEmployeeNameTransaction ce = new ChangeEmployeeNameTransaction(empID + 2, newName);
            Assert.Throws<EmployeeNotFound>(() => ce.Execute());
        }

        [Test]
        public void ChangeNameOfExistingEmployee()
        {
            AddSalariedEmployeeToDB();
            string newName = "henruy";
            ChangeEmployeeNameTransaction cn = new ChangeEmployeeNameTransaction(empID, newName);
            cn.Execute();
            Employee e = PayrollDB.GetEmployee(empID);
            Assert.AreEqual(newName, e.Name);
        }

        [Test]
        public void ChangeAddressOfExistingEmployee()
        {
            AddSalariedEmployeeToDB();
            string newAddress = "New home";
            ChangeEmployeeAddressTransaction ca = new ChangeEmployeeAddressTransaction(empID, newAddress);
            ca.Execute();
            Employee employee = PayrollDB.GetEmployee(empID);
            Assert.AreEqual(newAddress, employee.myAddress);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToHold()
        {
            AddSalariedEmployeeToDB();
            PaymentMethod pm = new HoldMethod();
            ChangePmTransaction hold = new ChangePmTransaction(empID, pm);
            hold.Execute();
            Employee employee = PayrollDB.GetEmployee(empID);
            Assert.IsTrue(employee.Paymentmethod is HoldMethod);
        }

        [Test]
        public void ChangeEmployeePaymentMethodToMail()
        {
            AddSalariedEmployeeToDB();
            PaymentMethod setPm = new MailPaymentMethod(address);
            ChangePmTransaction hold = new ChangePmTransaction(empID, setPm);
            hold.Execute();
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
            PaymentMethod paymentMethod = new AccountPaymentMethod(accountNumber);
            ChangePmTransaction changePM = new ChangePmTransaction(empID, paymentMethod);
            changePM.Execute();

            Employee employee = PayrollDB.GetEmployee(empID);
            Assert.IsTrue(employee.Paymentmethod is AccountPaymentMethod);
            AccountPaymentMethod asAccount = employee.Paymentmethod as AccountPaymentMethod;
            Assert.AreEqual(accountNumber, asAccount.AccountNumber);
        }
    }
}