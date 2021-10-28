using InMemoryDBTest;
using NUnit.Framework;
using PayrollDataBase;

namespace DatabaseTests.DatabaseTests
{
    public class AddUnionMember : AddEmployeeTest
    {
        [Test]
        public void AddSimpleEmployee()
        {
            var e = An.GenericEmployee;
            database.AddEmployee(100, e);
            database.AddUnionMember(1, 100);
        } [Test]
        public void AddSimpleEmployee2()
        {
            var e = An.GenericEmployee.Build();
            database.AddEmployee(100, e);
            database.AddUnionMember(1, 100);
            var u = database.GetUnionMember(1);
            Assert.AreEqual(e.Name, u.Name);
        }

    }

    public class SQLiteExceptions : PayrollExceptionsTest
    {
        [SetUp]
        public void Setup()
        {
            database = new SqliteDB();
        }
    }

}