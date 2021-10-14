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
    public class ClearsTables : ContextTests
    {
        [Test]
        public void ClearClearsCommision()
        {
            database.Clear();
            Assert.AreEqual(0, db.Commsions.Count());
        }

        [Test]
        public void ClearSalay()
        {
            database.Clear();
            Assert.AreEqual(0,db.Salaries.Count());
        }

        [Test]
        public void DirectDepositions()
        {
            database.Clear();
            Assert.AreEqual(0,db.DirectDepositAccounts);
        }

    }

    public class DirectContextTests : ContextTests
    {
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


    }
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
            if (employees.Count() > 0)
            {
                db.Employees.DeleteAllOnSubmit(employees);
                db.SubmitChanges();
            }
        }

       

        [TearDown]
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}