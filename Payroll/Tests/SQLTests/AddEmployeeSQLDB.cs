using System.Data;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests
{
    public
        class AddEmployeeSQLDB : TestSqliteDB
    {
       
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
            database.AddEmployee(id,e);

            Assert.AreEqual(1, EmployeeCount());
        }
        
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

        

  

     

        [Test]
        public void AddedScheduleType()
        {
            AddEmployee();
            string scmd = "SELECT * FROM Employee";
            SqliteCommand cmd = new SqliteCommand(scmd, con);

            SqliteDataAdapter sda = new SqliteDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            sda.Fill(dataSet);
            DataTable table = dataSet.Tables["table"];
            Assert.AreEqual(1,table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(SqliteDB.ScheduleCodes.BiWeekly, row["ScheduleType"]);
        }
    }
}