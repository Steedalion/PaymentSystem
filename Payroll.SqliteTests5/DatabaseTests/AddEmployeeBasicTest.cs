using NUnit.Framework;
using PaymentClassifications.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.DatabaseTests
{
    public class AddEmployeeBasicTest : AddEmployeeTest
    {
        [SetUp]
        public void Startup()
        {
            database.Clear();
        }

        [TearDown]
        public void ClearOnTeardown()
        {
            database.Clear();
        }

        [Test]
        public void ShouldBeEmptyAtStart()
        {
            Assert.AreEqual(0, EmployeeCount());
        }


        [Test]
        public void AddEmployee()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
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
            Assert.AreEqual(1, EmployeeCount());
        }

    }
}