using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDataBase;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.SQLiteTests
{
    public class RemoveEmployeeTests : TestSqliteDB
    {
        [Test]
        public void AddAndRemoveEmployee()
        {
            AddEmployee();
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
            database.RemoveEmployee(id);
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
        }
    }

    public class AddEmployeeTests
    {
        protected SqliteDB database = new SqliteDB();
        protected int id = 1234;

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

        private int EmployeeCount()
        {
            return database.GetEmployeeIds().Length;
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

        private Employee CreateEmployee()
        {
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
            string name = "John";
            string address = "123 bird street";
            Employee e = new Employee(id, name, address);
            return e;
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

        [Test]
        public void AddEmployeeSavesSchedule()
        {
            AddEmployee();
            Employee employee = database.GetEmployee(id);
            Assert.IsTrue(employee.Schedule is Biweekly);
        }

        [Test]
        public void AddEmployeeSavesClassification()
        {
            AddEmployee();
            Employee e = database.GetEmployee(id);
            Assert.IsTrue(e.Classification is HourlyClassification);
        }

        [Test]
        public void AddAccountPayment()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new AccountPaymentMethod("Woori", 800);
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void AddHoldPayment()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new HoldMethod();
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }

        [Test]
        public void AddMailPayment()
        {
            Employee e = CreateEmployee();
            e.Schedule = new Biweekly();
            e.Paymentmethod = new MailPaymentMethod("Home");
            e.Classification = new CommisionClassification(0.5, 1000);
            database.AddEmployee(id, e);
            Assert.AreEqual(1, EmployeeCount());
        }


        [Test]
        public void AddEmployeeSavesMethod()
        {
            AddEmployee();
            Employee e = database.GetEmployee(id);
            Assert.IsTrue(e.Paymentmethod is MailPaymentMethod);
        }
    }

    public class GetEmployeeTests : AddEmployeeTests
    {
        [Test]
        public void GetAccountPayment()
        {
            AddAccountPayment();
            Employee got = database.GetEmployee(id);
            AccountPaymentMethod acc = got.Paymentmethod as AccountPaymentMethod;
            Assert.IsTrue(got.Paymentmethod is AccountPaymentMethod);
            Assert.AreEqual("Woori", acc.bank);
            Assert.AreEqual(800, acc.AccountNumber);
        }

        [Test]
        public void GetMailPayment()
        {
            AddMailPayment();
            Employee got = database.GetEmployee(id);
            MailPaymentMethod acc = got.Paymentmethod as MailPaymentMethod;
            Assert.IsTrue(got.Paymentmethod is AccountPaymentMethod);
            Assert.AreEqual("Home", acc.Address);
        }

        [Test]
        public void GetHoldPayment()
        {
            AddHoldPayment();
            Employee got = database.GetEmployee(id);
            HoldMethod acc = got.Paymentmethod as HoldMethod;
            Assert.IsTrue(got.Paymentmethod is HoldMethod);
        }
    }
}