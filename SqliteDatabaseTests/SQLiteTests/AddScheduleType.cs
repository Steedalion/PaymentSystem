using System.Data;
using NUnit.Framework;

namespace Payroll.Tests.SQLiteTests
{
    class AddScheduleType : TestSqliteDB
    {

        [SetUp]
        public void EmptyTables()
        {
            ClearAllTables();
        }
        [Test]
        public void MonthlyScheduleGetsSaved()
        {
            addEmployeeSchedule(new MonthlyPaymentSchedule());
            string expectedCode = SqliteDB.ScheduleCodes.Monthly;
            CompareSavedScheduleType(expectedCode);
        }

        [Test]
        public void WeeklyScheduleGetsSaved()
        {
            addEmployeeSchedule(new WeeklySchedule());
            string expectedCode = SqliteDB.ScheduleCodes.Weekly;
            CompareSavedScheduleType(expectedCode);
        }

        [Test]
        public void BiweeklyScheduleGetsSaved()
        {
            addEmployeeSchedule(new Biweekly());
            string expectedCode = SqliteDB.ScheduleCodes.BiWeekly;
            CompareSavedScheduleType(expectedCode);
        }

        void addEmployeeSchedule(PaymentSchedule ps)
        {
            Employee e = new Employee(id, "ScheduleTester", "SQLtestdb");
            e.Schedule = ps;
            e.Classification = new SalariedClassification(100);
            database.AddEmployee(id, e);
        }

        private void CompareSavedScheduleType(string expectedCode)
        {
            DataTable employeeTable = LoadEmployeeTable();
            DataRow row = employeeTable.Rows[0];
            Assert.AreEqual(expectedCode, row["ScheduleType"]);
        }
    }
}