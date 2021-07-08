using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    public class TestChangeEmployeeNameAndAddress : TestSetupTransactions
    {
        [Test]
        public void ChangeNotExistingEmployeeNameShouldThrowException()
        {
            string newName = "Henry";
            database.Clear();
            ChangeEmployeeNameTransaction ce = new ChangeEmployeeNameTransaction(database,EmpId + 2, newName);
            Assert.Throws<EmployeeNotFound>(() => ce.Execute());
        }

        [Test]
        public void ChangeNameOfExistingEmployee()
        {
            AddSalariedEmployeeToDb();
            string newName = "H-James";
            ChangeEmployeeNameTransaction cn = new ChangeEmployeeNameTransaction(database,EmpId, newName);
            cn.Execute();
            Employee e = database.GetEmployee(EmpId);
            Assert.AreEqual(newName, e.Name);
        }

        [Test]
        public void ChangeAddressOfExistingEmployee()
        {
            AddSalariedEmployeeToDb();
            string newAddress = "New home";
            ChangeEmployeeAddressTransaction ca = new ChangeEmployeeAddressTransaction(database,EmpId, newAddress);
            ca.Execute();
            Employee employee = database.GetEmployee(EmpId);
            Assert.AreEqual(newAddress, employee.myAddress);
        }

        
    }
}