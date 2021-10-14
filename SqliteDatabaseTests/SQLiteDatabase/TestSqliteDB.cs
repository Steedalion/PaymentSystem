using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using PaymentClassification.PaymentClassifications;
using PayrollDataBase;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.SQLiteTests
{
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