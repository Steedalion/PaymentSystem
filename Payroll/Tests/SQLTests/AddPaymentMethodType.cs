using System;
using System.Data;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests
{
    class AddPaymentMethodType : TestSqliteDB
    {
        [SetUp]
        public void ClearPaymentMethods()
        {
            ClearTable(SqliteDB.Tables.account);
            ClearTable(SqliteDB.Tables.mail);
        }

        private void ClearTable(string tableName)
        {
            SqliteCommand clearPM = new SqliteCommand("DELETE FROM " + tableName, con);
            clearPM.ExecuteNonQuery();
        }

        private DataTable GetDataTable(string Table)
        {
            string getAccounts = "SELECT * FROM " + Table;
            SqliteCommand cmd = new SqliteCommand(getAccounts, con);
            SqliteDataAdapter adapter = new SqliteDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
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
                Console.WriteLine(e);
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