using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using NUnit.Framework;
using PayrollDataBase;
using PayrollDataBase.Linq2SQL;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    public class ContextTests : TestSqliteDB
    {
        private EmployeeContext db;

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
            if (employees.Count() > 0)
            {
                db.Employees.DeleteAllOnSubmit(employees);
                db.SubmitChanges();
            }
        }

        [Test]
        public void ConnectToDB()
        {
            DataContext db = new EmployeeContext(connection);
            string version = db.Connection.ServerVersion;
            Console.WriteLine(version);
            Assert.IsNotNull(version);
        }

        [Test]
        public void PrintEmployeesFromDataContext()
        {
            DataContext db = new EmployeeContext(connection);
            string version = db.Connection.ServerVersion;
            Console.WriteLine(version);
            Assert.IsNotNull(version);

            var emps = db.GetTable<EmployeeUnit>();
            foreach (EmployeeUnit emp in emps)
            {
                Console.WriteLine(emp);
            }
        }

        [Test]
        public void PrintEmployeesAsPayrollContext()
        {
            foreach (EmployeeUnit employee in db.Employees)
            {
                Console.WriteLine(employee);
            }
        }

        [Test]
        public void AddEmployees()
        {
            Employee emp = new Employee(123, "John", "Home");
            EmployeeUnit unit =
                new EmployeeUnit(123, emp);
            db.Employees.InsertOnSubmit(unit);
            db.SubmitChanges();
            Assert.AreEqual(1, db.Employees.Count());
        }

       

        [TearDown]
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}