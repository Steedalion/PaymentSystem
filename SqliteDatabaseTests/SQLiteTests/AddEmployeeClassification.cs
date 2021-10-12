using NUnit.Framework;
using PaymentClassification.PaymentClassifications;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    public class AddEmployeeClassification : AddEmployeeTests
    {
        private Employee e;
        [Test]
        public void StartUp()
        {
            e = CreateEmployee();
        }
        [Test]
        public void AddWeekly()
        {
            e.Classification = new HourlyClassification(10.00);
            database.AddEmployee(id,e);
        }

        [Test]
        public void AddCommision()
        {
            e.Classification = new CommisionClassification(0.5f, 100);
            database.AddEmployee(id,e);
        }

        [Test]
        public void AddSalary()
        {
            e.Classification = new SalariedClassification(200);
            database.AddEmployee(id,e);
        }
    }
}