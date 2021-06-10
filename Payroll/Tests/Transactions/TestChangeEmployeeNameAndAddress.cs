using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestChangeEmployeeNameAndAddress : TestSetup
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

        
    }
}