using NUnit.Framework;

namespace DatabaseTests.DatabaseTests
{
    public class RemoveEmployeeTests : AddEmployeeTest
    {
        [Test]
        public void AddAndRemoveEmployee()
        {
            database.AddEmployee(id,CreateEmployee());
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
            database.RemoveEmployee(id);
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
        }
    }
}