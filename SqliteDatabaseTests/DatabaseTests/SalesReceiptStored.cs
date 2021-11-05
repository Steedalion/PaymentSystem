using System;
using NUnit.Framework;
using PaymentClassifications.PaymentClassifications;
using Payroll.TestBuilders;
using PayrollDomain;

namespace DatabaseTests.DatabaseTests
{
    public class SalesReceiptStored : AddEmployeeTest
    {
        [Test]
        public void StoreSalesReceipt()
        {
            Employee e = An.GenericEmployee.Commision(1, 100);
            CommisionClassification commision = e.ClassificationAsCommision();
            commision.AddSalesReciept(DateTime.Today, 10);
            database.AddEmployee(1,e);
            var got = database.GetEmployee(1);
        }

        [Test]
        public void GetSalesReceipt()
        {
            Employee e = An.GenericEmployee.Commision(1, 100);
            CommisionClassification commision = e.ClassificationAsCommision();
            commision.AddSalesReciept(DateTime.Today, 10);
            database.AddEmployee(1,e);
            var got = database.GetEmployee(1);
            var saleReceipt = got.ClassificationAsCommision().GetSalesReciept(DateTime.Today);
            Assert.AreEqual(10,saleReceipt.Amount);
        }
    }
}