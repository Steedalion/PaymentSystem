using System;
using System.Data;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests.SQLiteTests
{
    class AddClassificationType :TestSqliteDB
    {
        [Test]
        public void SalariedClassificationGetsSave()
        {
            Employee e = new Employee(id, "John", "123 bird ave")
            {
                Paymentmethod = new HoldMethod(),
                Schedule = new MonthlyPaymentSchedule(),
                Classification = new SalariedClassification(100.00)
            };
            database.AddEmployee(id, e);

            DataTable salariedEmps = GetDataTable(SqliteDB.Tables.salary);
            Assert.AreEqual(1, salariedEmps.Rows.Count);
            DataRow row = salariedEmps.Rows[0];
            Assert.AreEqual(100, row["Salary"]);
        }
        
    }
    class AddPaymentMethodType : TestSqliteDB
    {
        [SetUp]
        public void ClearPaymentMethods()
        {
            ClearTable(SqliteDB.Tables.account);
            ClearTable(SqliteDB.Tables.mail);
        }

    


        [Test]
        public void EmployeeCannotHaveMoreThanOnePM()
        {
            AccountGetsSaved();
            try
            {
                MailPayGetsSaved();
                Assert.Fail("Should not be able to add the same employee with a different payment method");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.AreEqual(1, GetDataTable(SqliteDB.Tables.account).Rows.Count);
            Assert.AreEqual(0, GetDataTable(SqliteDB.Tables.mail).Rows.Count,
                "Mail should not get added since employee failed");
        }

        [Test]
        public void HoldPaymentSaved()
        {
            addEmployeeMethod(new HoldMethod());
            string expectedCode = SqliteDB.PaymentMethods.Hold;
            CompareSavedScheduleType(expectedCode);
        }

        [Test]
        public void AccountGetsSaved()
        {
            int accountNumber = 12315;
            addEmployeeMethod(new AccountPaymentMethod("New bank", accountNumber));
            string expectedCode = SqliteDB.PaymentMethods.Account;
            CompareSavedScheduleType(expectedCode);
            DataTable accountsTable = GetDataTable(SqliteDB.Tables.account);
            Assert.AreEqual(1, accountsTable.Rows.Count);
            DataRow row = accountsTable.Rows[0];
            Assert.AreEqual(id, row["EmpID"]);
            Assert.AreEqual("New bank", row["Bank"]);
            Assert.AreEqual(accountNumber, row["Account"]);
        }

        [Test]
        public void MailPayGetsSaved()
        {
            addEmployeeMethod(new MailPaymentMethod("home"));
            string expectedCode = SqliteDB.PaymentMethods.Mail;
            CompareSavedScheduleType(expectedCode);
            DataTable paycheckAddresses = GetDataTable(SqliteDB.Tables.mail);
            Assert.AreEqual(1, paycheckAddresses.Rows.Count);
            DataRow row = paycheckAddresses.Rows[0];
            Assert.AreEqual(id, row["EmpID"]);
            Assert.AreEqual("home", row["Address"]);
        }

        [Test]
        public void PMSaveIsTransactional()
        {
            AccountPaymentMethod method = new AccountPaymentMethod(null, 0);
            Employee employee = new Employee(id, "John", "home");
            employee.Paymentmethod = method;

            try
            {
                database.AddEmployee(id, employee);
                Assert.Fail("An exception should occure");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            DataTable employees = GetDataTable(SqliteDB.Tables.employee);
            Assert.AreEqual(0, employees.Rows.Count);
        }

        void addEmployeeMethod(PaymentMethod pm)
        {
            Employee e = new Employee(id, "ScheduleTester", "SQLtestdb");
            e.Paymentmethod = pm;
            e.Classification = new SalariedClassification(100);
            database.AddEmployee(id, e);
        }

        private void CompareSavedScheduleType(string expectedCode)
        {
            DataTable employeeTable = LoadEmployeeTable();
            DataRow row = employeeTable.Rows[0];
            Assert.AreEqual(expectedCode, row["PaymentMethodType"]);
        }
    }
}