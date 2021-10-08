using System;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.Linq;
using NUnit.Framework;
using PayrollDataBase;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    public class TestSqliteDB
    {
        protected SqliteDB database = new SqliteDB();
        protected int id = 1234;
        protected SQLiteConnection connection;
        private EmployeeContext db;

        [SetUp]
        public void ConnectAndClear()
        {
            connection = new SQLiteConnection(SqliteDB.connectionID);
            connection.Open();
            db = new EmployeeContext(connection);

            ClearEmployees();
        }

        private void ClearEmployees()
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
                new EmployeeUnit(emp);
            db.Employees.InsertOnSubmit(unit);
            db.SubmitChanges();
            Assert.AreEqual(1, db.Employees.Count());
        }

        public void ClearAllTables()
        {
            ClearTable(Tables.Account);
            ClearTable(Tables.Mail);
            ClearTable(Tables.Commission);
            ClearTable(Tables.Salary);
            ClearTable(Tables.Hourly);
        }

        [TearDown]
        public void CloseConnection()
        {
            connection.Close();
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
            return db.Employees.Count();
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
    }

    public class EmployeeContext : DataContext
    {
        public EmployeeContext(IDbConnection connection) : base(connection)
        {
        }

        public Table<EmployeeUnit> Employees;
    }

    [Table(Name = "Employees")]
    public class EmployeeUnit
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Name))] public string Name;
        [Column(Name = nameof(Address))] public string Address;
        [Column(Name = nameof(ScheduleType))] public string ScheduleType;

        [Column(Name = nameof(PaymentMethodType))]
        public string PaymentMethodType;

        [Column(Name = nameof(PaymentClassificationType))]
        public string PaymentClassificationType;

        public EmployeeUnit()
        {
        }

        public EmployeeUnit(Employee emp)
        {
            EmpID = emp.myID;
            Name = emp.Name;
            Address = emp.myAddress;
            ScheduleType = ScheduleCodes.Code(emp.Schedule);
            PaymentMethodType = PaymentMethodCodes.Code(emp.Paymentmethod);
            PaymentClassificationType = ClassificationCodes.Code(emp.Classification);
        }
    }
}