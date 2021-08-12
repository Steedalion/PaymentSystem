using System.Data;
using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PayrollDataBase;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.SQLiteTests
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
            string expectedCode = ScheduleCodes.Monthly;
            CompareSavedScheduleType(expectedCode);
        }

        [Test]
        public void WeeklyScheduleGetsSaved()
        {
            addEmployeeSchedule(new WeeklySchedule());
            string expectedCode = ScheduleCodes.Weekly;
            CompareSavedScheduleType(expectedCode);
        }

        [Test]
        public void BiweeklyScheduleGetsSaved()
        {
            addEmployeeSchedule(new Biweekly());
            string expectedCode = ScheduleCodes.BiWeekly;
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