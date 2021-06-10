using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    public class TestChangeEmployeeNameAndAddress : TestSetup
    {
        [Test]
        public void ChangeNotExistingEmployeeNameShouldThrowException()
        {
            string newName = "Henry";
            PayrollDB.Clear();
            ChangeEmployeeNameTransaction ce = new ChangeEmployeeNameTransaction(EmpId + 2, newName);
            Assert.Throws<EmployeeNotFound>(() => ce.Execute());
        }

        [Test]
        public void ChangeNameOfExistingEmployee()
        {
            AddSalariedEmployeeToDb();
            string newName = "H-James";
            ChangeEmployeeNameTransaction cn = new ChangeEmployeeNameTransaction(EmpId, newName);
            cn.Execute();
            Employee e = PayrollDB.GetEmployee(EmpId);
            Assert.AreEqual(newName, e.Name);
        }

        [Test]
        public void ChangeAddressOfExistingEmployee()
        {
            AddSalariedEmployeeToDb();
            string newAddress = "New home";
            ChangeEmployeeAddressTransaction ca = new ChangeEmployeeAddressTransaction(EmpId, newAddress);
            ca.Execute();
            Employee employee = PayrollDB.GetEmployee(EmpId);
            Assert.AreEqual(newAddress, employee.myAddress);
        }

        
    }
}