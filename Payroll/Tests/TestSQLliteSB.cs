using System;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestSQLliteSB
    {
        private IPayrollDB database = new SQLLSB();

        [Test]
        public void AddEmployee()
        {
            int id = 123;
            Employee e = new Employee(id, "John", "homeelss");
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);

            // database.AddEmployee(id,e);
            SqliteConnection con = new SqliteConnection(@"Data Source=PayrollDB.sqlite");
            con.Open();
            var command = con.CreateCommand();
            command.CommandText = @"insert into Employee values("
                                  + "123"
                                  + ")";
var cmd = new SqliteCommand(command.CommandText, con);
cmd.ExecuteNonQuery();
            // command.CommandText = "create table Employee(EmpId int);";


            // command.Parameters.AddWithValue("@EmpId", id);
            command.ExecuteNonQuery();
            command.ExecuteReader();
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
        public void GetTables()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT * from Employee;";

            var con = new SqliteConnection(cs);
            con.Open();

            var cmd = new SqliteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");
        }
    }

    internal class SQLLSB : IPayrollDB
    {
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