using System;
using System.Data;
using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PayrollDataBase;
using PayrollDomain;
using Schedules;

namespace Payroll.Tests.SQLiteTests
{
    class AddClassificationType : TestSqliteDB
    {
        [SetUp]
        public void ClearTables()
        {
            ClearAllTables();
        }

        [Test]
        public void SalariedClassificationGetsSave()
        {
            ClearTable(Tables.Salary);
            var classification = new SalariedClassification(100.00);
            AddClassificationEmp(classification);
            ComapreClassificationCode(ClassificationCodes.Salary);

            DataTable salariedEmps = GetDataTable(Tables.Salary);
            Assert.AreEqual(1, salariedEmps.Rows.Count);
            DataRow row = salariedEmps.Rows[0];
            Assert.AreEqual(100, row["Salary"]);
            Assert.AreEqual(id, row["EmpID"]);
        }

        private void ComapreClassificationCode(string salary)
        {
            DataTable employeetable = GetDataTable(Tables.Employee);
            Assert.AreEqual(1, employeetable.Rows.Count);
            Assert.AreEqual(ClassificationCodes.Salary, employeetable.Rows[0]["PaymentClassificationType"]);
        }

        [Test]
        public void CommisionClassificationGetsSave()
        {
            var salary = 50.00;
            var classification = new CommisionClassification(0.05, salary);
            AddClassificationEmp(classification);

            DataTable salariedEmps = GetDataTable(Tables.Commission);
            Assert.AreEqual(1, salariedEmps.Rows.Count);
            DataRow row = salariedEmps.Rows[0];
            Assert.AreEqual(id, row["EmpID"]);
            Assert.AreEqual(salary, row["Salary"]);
            Assert.AreEqual(0.05, row["CommissionRate"]);
        }

        [Test]
        public void HourlyClassificationGetsSave()
        {
            float rate = 15;
            var classification = new HourlyClassification(rate);
            AddClassificationEmp(classification);

            DataTable salariedEmps = GetDataTable(Tables.Hourly);
            Assert.AreEqual(1, salariedEmps.Rows.Count);
            DataRow row = salariedEmps.Rows[0];
            Assert.AreEqual(rate, row["HourlyRate"]);
            Assert.AreEqual(id, row["EmpID"]);
        }

        private void AddClassificationEmp(PayrollDomain.PaymentClassification classification)
        {
            Employee e = new Employee(id, "John", "123 bird ave")
            {
                Paymentmethod = new HoldMethod(),
                Schedule = new MonthlyPaymentSchedule()
            };
            e.Classification = classification;
            database.AddEmployee(id, e);
        }

        [Test]
        public void AddUnknownClassisifcationShouldNotFreezeOrLockDB()
        {
            Employee e = new Employee(id, "John", "123 bird ave")
            {
                Paymentmethod = new HoldMethod(),
                Schedule = new MonthlyPaymentSchedule()
            };
            e.Classification = new UnregisteredClassification();
            try
            {
                database.AddEmployee(id, e);
                Assert.Fail("Should throw an exception for adding an unknown classification");
            }
            catch (Exception exception)
            {
            }
            SalariedClassificationGetsSave();
        }
    }

    internal class UnregisteredClassification : PayrollDomain.PaymentClassification
    {
        public double CalculatePay(PayCheck payCheck)
        {
            return 0;
        }
    }
}