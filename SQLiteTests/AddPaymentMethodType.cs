using System;
using System.Data;
using Domain;
using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDataBase;
using PayrollDomain;

namespace Payroll.Tests.SQLiteTests
{
    class AddPaymentMethodType : TestSqliteDB
    {
        [SetUp]
        public void ClearPaymentMethods()
        {
            ClearAllTables();
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

            Assert.AreEqual(1, GetDataTable(Tables.Account).Rows.Count);
            Assert.AreEqual(0, GetDataTable(Tables.Mail).Rows.Count,
                "Mail should not get added since employee failed");
        }

        [Test]
        public void HoldPaymentSaved()
        {
            addEmployeeMethod(new HoldMethod());
            string expectedCode = MethodCodes.Hold;
            CompareSavedScheduleType(expectedCode);
        }

        [Test]
        public void AccountGetsSaved()
        {
            int accountNumber = 12315;
            addEmployeeMethod(new AccountPaymentMethod("New bank", accountNumber));
            string expectedCode = MethodCodes.Account;
            CompareSavedScheduleType(expectedCode);
            DataTable accountsTable = GetDataTable(Tables.Account);
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
            string expectedCode = MethodCodes.Mail;
            CompareSavedScheduleType(expectedCode);
            DataTable paycheckAddresses = GetDataTable(Tables.Mail);
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

            DataTable employees = GetDataTable(Tables.Employee);
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