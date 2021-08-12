using NUnit.Framework;
using PaymentClassification;
using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction;

namespace TransactionTests.Tests.Transactions
{
    public class TestDeleteEmployees : TestSetupTransactions
    {

        [Test]
        public void TestDeleteAnEmployee()
        {

            AddSalaryEmployeeTransaction t = new AddSalaryEmployeeTransaction(database,EmpId, Name, Address, 100);
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