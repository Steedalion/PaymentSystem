using System.Data.SQLite;
using System.Linq;
using DatabaseTests.SQLiteDatabase;
using NUnit.Framework;
using PayrollDataBase;
using PayrollDataBase.Linq2SQL;

namespace DatabaseTests.ContextTests
{
    public class ContextTests : TestSqliteDB
    {
        protected EmployeeContext db;

        [SetUp]
        public void ConnectAndClear()
        {
            connection = new SQLiteConnection(SqliteDB.connectionID);
            connection.Open();
            db = new EmployeeContext(connection);
            ClearEmployees();
        }


        protected void ClearEmployees()
        {
            IQueryable<EmployeeUnit> employees = from emp in db.Employees
                select emp;
            db.Employees.DeleteAllOnSubmit(employees);
            db.SubmitChanges();
        }


        [TearDown]
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}