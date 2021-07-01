using System;
using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests
{
    public
        class TestSQLDB : TestSQLliteSB
    {
        [Test]
        public void AddingDuplicateEmployee()
        {
            Employee e = new Employee(id, "John", "homeelss");
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.Throws<EmployeeAlreadyExists>(() => database.AddEmployee(id, e));
        }
    }

    public class TestSQLliteSB
    {
        protected SQLLSB database = new SQLLSB();
        protected int id = 1234;
        private SqliteConnection con;

        [SetUp]
        public void ConnectAndClear()
        {
            con = new SqliteConnection(SQLLSB.connectionID);
            con.Open();
            
            string sql = "DELETE FROM Employee";
            SqliteCommand command = new SqliteCommand(sql, con);
            
            command.ExecuteNonQuery();
        }

        [TearDown]
        public void CloseConnection()
        {
            con.Close();
        }

        [Test]
        public void ShouldBeEmptyAtStart()
        {
            Assert.AreEqual(0, EmployeeCount());
        }

        [Test]
        public void AddEmployee()
        {
            string name = "John";
            string address = "123 bird street";
            Employee e = new Employee(id,name, address);
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);

            // database.AddEmployee(id,e);
            SqliteConnection con = new SqliteConnection(SQLLSB.connectionID);
            con.Open();

            string sql = "INSERT INTO Employee VALUES("
                         + "@EmpID"
                         +",@Name"
                         +",@Address"
                         +",@ScheduleType"
                         +",@PaymentMethodType"
                         +",@PaymentClassificationType"
                         + ")";

            var command = new SqliteCommand(sql, con);
            var cmd = new SqliteCommand(command.CommandText, con);
            cmd.Parameters.AddWithValue("@EmpID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@ScheduleType", e.Schedule.GetType().ToString());
            cmd.Parameters.AddWithValue("@PaymentMethodType", e.Paymentmethod.GetType().ToString());
            cmd.Parameters.AddWithValue("@PaymentClassificationType", e.Classification.GetType().ToString());
            cmd.ExecuteNonQuery();

            Assert.AreEqual(1, EmployeeCount());
        }

        protected int EmployeeCount()
        {
            string getEmployeescmd = "SELECT * FROM Employee";
            SqliteCommand getEmployees = new SqliteCommand(getEmployeescmd, con);
            DataAdapter adapter = new SqliteDataAdapter(getEmployees);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable employeeTable = dataSet.Tables["table"];
            int numEmployees = employeeTable.Rows.Count;
            return numEmployees;
        }

        [Test]
        public void EmployeeWasAddedToDB()
        {
            AddEmployee();
            string cmdText = "SELECT * FROM Employee";
            SqliteCommand get = new SqliteCommand(cmdText, con);
            SqliteDataAdapter adapter = new SqliteDataAdapter(get);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables["table"];
            Assert.AreEqual(1, dataTable.Rows.Count);
            DataRow row = dataTable.Rows[0];
            Assert.AreEqual("monthly", row["Schedule"]);
        }

        [Test]
        public void GetVersion()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SqliteConnection(cs);
            con.Open();

            var cmd = new SqliteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");
        }

        [Test]
        public void TestCars()
        {
            string cs = @"URI=file:/home/alex/RiderProjects/PaymentSystem/PayrollDB.sqlite";

            SqliteConnection con = new SqliteConnection(cs);
            con.Open();

            SqliteCommand cmd = new SqliteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS cars";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
            name TEXT, price INT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT * FROM cars";

            var reader = cmd.ExecuteReader();
            string response = "";

            while (reader.Read())
            {
                response += reader.GetInt16(0);
            }

            Console.WriteLine("Table cars created" + response);
        }

        [Test]
        public void GetTables()
        {
            string cs = SQLLSB.connectionID;
            string stm = "SELECT * from Employee;";

            var con = new SqliteConnection(cs);
            con.Open();

            var cmd = new SqliteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");
        }
    }

    public class SQLLSB : IPayrollDB
    {
        public static string connectionID = @"URI=file:/home/alex/RiderProjects/PaymentSystem/PayrollDB.sqlite";

        public Employee GetEmployee(int empId)
        {
            throw new System.NotImplementedException();
        }

        public void AddEmployee(int id, Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEmployee(int id)
        {
            throw new System.NotImplementedException();
        }

        public void AddUnionMember(int memberId, int id)
        {
            throw new System.NotImplementedException();
        }

        public Employee GetUnionMember(int memberId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new System.NotImplementedException();
        }

        public int[] GetEmployeeIds()
        {
            throw new System.NotImplementedException();
        }
    }
}