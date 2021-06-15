using NUnit.Framework;
using Payroll.DBTransaction;

namespace Payroll.Tests.Transactions
{
    public class TestDeleteEmployees : TestSetupTransactions
    {

        [Test]
        public void TestDeleteAnEmployee()
        {

            AddSalaryEmployee t = new AddSalaryEmployee(EmpId, Name, Address, 100);
            t.Execute();
            Assert.NotNull(PayrollDB.GetEmployee(EmpId));
            DeleteEmployee deleteEmployee = new DeleteEmployee(EmpId);
            deleteEmployee.Execute();
            Employee e = PayrollDB.GetEmployee(EmpId);
            Assert.IsTrue(e.isNull);
        }

        [Test]
        public void DeletingANonExistingEmployee()
        {
            DeleteEmployee deleteEmployee = new DeleteEmployee(EmpId);
            Assert.Throws<EmployeeNotFound>(() => deleteEmployee.Execute());
        }
    }
}