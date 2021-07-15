using Domain;
using NUnit.Framework;
using PaymentClassification;
using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction;

namespace Payroll.Tests.Transactions
{
    public class TestDeleteEmployees : TestSetupTransactions
    {

        [Test]
        public void TestDeleteAnEmployee()
        {

            AddSalaryEmployee t = new AddSalaryEmployee(database,EmpId, Name, Address, 100);
            t.Execute();
            Assert.NotNull(database.GetEmployee(EmpId));
            DeleteEmployee deleteEmployee = new DeleteEmployee(database,EmpId);
            deleteEmployee.Execute();
            Employee e = database.GetEmployee(EmpId);
            Assert.IsTrue(e.isNull);
        }

        [Test]
        public void DeletingANonExistingEmployee()
        {
            DeleteEmployee deleteEmployee = new DeleteEmployee(database,EmpId);
            Assert.Throws<EmployeeNotFound>(() => deleteEmployee.Execute());
        }
    }
}