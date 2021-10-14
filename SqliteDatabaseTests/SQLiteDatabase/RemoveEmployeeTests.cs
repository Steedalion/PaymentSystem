using NUnit.Framework;

namespace DatabaseTests.SQLiteTests
{
    public class RemoveEmployeeTests : TestSqliteDB
    {
        [Test]
        public void AddAndRemoveEmployee()
        {
            AddEmployee();
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
            database.RemoveEmployee(id);
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
        }
    }
}