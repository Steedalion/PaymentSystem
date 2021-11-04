using System.Linq;
using NUnit.Framework;

namespace DatabaseTests.DatabaseTests
{
    public class GetEmpIdsTest : AddEmployeeTest
    {
        [Test]
        public void GetSingleID()
        {
            var e1 = CreateEmployee();
            database.AddEmployee(1, e1);
            var ids = database.GetEmployeeIds();
            Assert.AreEqual(1, ids.Length);
            Assert.AreEqual(1, ids[0]);
        }

        [Test]
        public void GetMultipleIds()
        {
            var e1 = CreateEmployee();
            database.AddEmployee(1, e1);
            database.AddEmployee(10, e1);
            database.AddEmployee(100, e1);
            var ids = database.GetEmployeeIds();
            Assert.AreEqual(3, ids.Length);
            Assert.Contains(1, ids);
            Assert.Contains(10, ids);
            Assert.Contains(100, ids);
        }

        [Test]
        public void RemovedIDs()
        {
            var e1 = CreateEmployee();
            database.AddEmployee(1, e1);
            database.AddEmployee(10, e1);
            database.AddEmployee(100, e1);
            database.RemoveEmployee(10);
            var ids = database.GetEmployeeIds();
            Assert.AreEqual(2, ids.Length);
            Assert.Contains(1, ids);
            Assert.False(ids.Contains(10));
            Assert.Contains(100, ids);
        }

        [Test]
        public void AddRemoveAdd()
        {
            var e1 = CreateEmployee();
            database.AddEmployee(1, e1);
            database.RemoveEmployee(1);
            database.AddEmployee(1, e1);
        }
    }
}