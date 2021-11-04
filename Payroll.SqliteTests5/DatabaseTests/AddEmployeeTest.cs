using NUnit.Framework;
using PaymentClassifications.PaymentClassifications;
using PayrollDataBase;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.DatabaseTests
{
    public class AddEmployeeTest
    {
        protected SqliteDB database = new SqliteDB();
        protected int id = 1234;

        [SetUp]
        public void Setup()
        {
            database.Clear();
        }

        [TearDown]
        public void TakeDown()
        {
            database.Clear();
            
        }

        protected Employee CreateEmployee()
        {
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
            string name = "John";
            string address = "123 bird street";
            Employee e = new Employee(id, name, address);
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new SalariedClassification(1000);
            return e;
        }

        protected int EmployeeCount()
        {
            return database.GetEmployeeIds().Length;
        }
    }
}