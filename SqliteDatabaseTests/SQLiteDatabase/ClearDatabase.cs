using NUnit.Framework;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    public class ClearDatabase : TestSqliteDB
    {
        [Test]
        public void AddOneRemove()
        {
            database.Clear();
            string name = "John";
            string address = "123 bird street";
            Employee e = new Employee(id, name, address);
            database.AddEmployee(id, e);
            Employee e2 = new Employee(id + 1, "Second person", "Home is where ,,,");
            database.AddEmployee(id+1, e2);
            Assert.AreEqual(2, EmployeeCount());
            database.Clear();
            Assert.AreEqual(0, EmployeeCount());
        }

        [Test]
        public void ClearEmployees()
        {
            database.Clear();
            Assert.AreEqual(0,database.GetEmployeeIds().Length);
        }
    }
}