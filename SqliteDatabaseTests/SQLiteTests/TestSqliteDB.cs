using System;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PayrollDataBase;
using PayrollDomain;
using Schedules;

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

    public class TestSqliteDB
    {
        protected SQLiteConnection connection;
        protected SqliteDB database = new SqliteDB();
        protected int id = 123;

        public void AddEmployee()
        {
            string name = "John";
            string address = "123 bird street";
            Employee e = new Employee(id, name, address);
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
        }



        protected int EmployeeCount()
        {
            // string getEmployeescmd = "SELECT * FROM Employee";
            // SqliteCommand getEmployees = new SqliteCommand(getEmployeescmd, con);
            // DataAdapter adapter = new SqliteDataAdapter(getEmployees);
            // DataSet dataSet = new DataSet();
            // adapter.Fill(dataSet);
            // DataTable employeeTable = dataSet.Tables["table"];
            // int numEmployees = employeeTable.Rows.Count;
            // return numEmployees;
            return database.GetEmployeeIds().Length;
        }

        protected DataTable LoadEmployeeTable()
        {
            // SqliteCommand getEmployees = new SqliteCommand( "SELECT * FROM Employee",con);
            // SqliteDataAdapter adapter = new SqliteDataAdapter(getEmployees);
            // DataSet dataset = new DataSet();
            // adapter.Fill(dataset);
            // return dataset.Tables["table"];
            return null;
        }

        protected void ClearTable(string tableName)
        {
            // SqliteCommand clearPM = new SqliteCommand("DELETE FROM " + tableName, con);
            // clearPM.ExecuteNonQuery();
        }

        protected DataTable GetDataTable(string Table)
        {
            // string getAccounts = "SELECT * FROM " + Table;
            // SqliteCommand cmd = new SqliteCommand(getAccounts, con);
            // SqliteDataAdapter adapter = new SqliteDataAdapter(cmd);
            // DataTable table = new DataTable();
            // adapter.Fill(table);
            // return table;
            return null;
        }
         public void ClearAllTables()
        {
            ClearTable(Tables.Account);
            ClearTable(Tables.Mail);
            ClearTable(Tables.Commission);
            ClearTable(Tables.Salary);
            ClearTable(Tables.Hourly);
        }
    }
}