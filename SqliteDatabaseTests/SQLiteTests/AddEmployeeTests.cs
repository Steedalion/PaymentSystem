using System.Data;
using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PayrollDataBase;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.SQLiteTests
{
    public
        class AddEmployeeTests : TestSqliteDB
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
            Employee e = new Employee(id, name, address);
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
        }

        [Test]
        public void AddAndRemoveEmployee()
        {
            AddEmployee();
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
            database.RemoveEmployee(id);
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
        }

        [Test]
        public void AddAndClearEmployeeList()
        {
            AddEmployee();
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
            database.Clear();
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
        }


        [Test]
        public void AddingDuplicateEmployee()
        {
            Employee e = new Employee(id, "John", "homeelss");
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.Throws<EmployeeIdAlreadyExists>(() => database.AddEmployee(id, e));
        }

        [Test]
        public void AddedScheduleType()
        {
            AddEmployee();
            Employee employee = database.GetEmployee(id);
            Assert.IsTrue(employee.Schedule is Biweekly);
        }
    }
}