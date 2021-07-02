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
            SqliteCommand clearPM = new SqliteCommand("DELETE FROM DirectDepositAccount", con);
            clearPM.ExecuteNonQuery();
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
            addEmployeeMethod(new AccountPaymentMethod("New bank",accountNumber));
            string expectedCode = SqliteDB.PaymentMethods.Account;
            CompareSavedScheduleType(expectedCode);
            DataTable accountsTable = GetAccountsTable();
            Assert.AreEqual(1,accountsTable.Rows.Count);
            DataRow row = accountsTable.Rows[0];
            Assert.AreEqual(id,row["EmpID"]);
            Assert.AreEqual("New bank", row["Bank"]);
            Assert.AreEqual(accountNumber, row["Account"]);
        }

        private DataTable GetAccountsTable()
        {
            string getAccounts = "SELECT * FROM DirectDepositAccount";
            SqliteCommand cmd = new SqliteCommand(getAccounts, con);
            SqliteDataAdapter adapter = new SqliteDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        [Test]
        public void PostSaved()
        {
            addEmployeeMethod(new MailPaymentMethod("home"));
            string expectedCode = SqliteDB.PaymentMethods.Post;
            CompareSavedScheduleType(expectedCode);
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