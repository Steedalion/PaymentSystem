using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestSqliteDB
    {
        protected SqliteDB database = new SqliteDB();
        protected int id = 1234;
        protected SqliteConnection con;

        [SetUp]
        public void ConnectAndClear()
        {
            con = new SqliteConnection(SqliteDB.connectionID);
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
           protected DataTable LoadEmployeeTable()
        {
            SqliteCommand getEmployees = new SqliteCommand( "SELECT * FROM Employee",con);
            SqliteDataAdapter adapter = new SqliteDataAdapter(getEmployees);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables["table"];
        }

    }
}