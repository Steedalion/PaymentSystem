using NUnit.Framework;
using PaymentClassifications.ChangeClassification;
using PaymentClassifications.PaymentClassifications;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction.ChangeEmployee;

namespace TransactionTests.Tests.Transactions
{
    public class TestChangePaymentClassification : TestSetupTransactions
    {

        [Test]
        public void ChangeToSalaryPMC()
        {
            AddHourlyEmployeeToDB();
            ChangeSalaryEmployee salary = new ChangeSalaryEmployee(database,EmpId, Salary);
            salary.Execute();
            Employee employee = database.GetEmployee(EmpId);
            Assert.IsFalse(employee.isNull);
            PayrollDomain.PaymentClassification p = employee.Classification;
            Assert.IsTrue(p is SalariedClassification);
            SalariedClassification salariedClassification = p as SalariedClassification;
            Assert.AreEqual(Salary, salariedClassification.Salary, 0.001);
        }

        [Test]
        public void ChangetoHourly()
        {
            AddSalariedEmployeeToDb();
            double hourlyRate = 25;
            ChangeHourlyEmployee hourlyEmployee = new ChangeHourlyEmployee(database,EmpId, hourlyRate);
            hourlyEmployee.Execute();

            Employee e = database.GetEmployee(EmpId);
            PayrollDomain.PaymentClassification pm = e.Classification;
            Assert.IsTrue(pm is HourlyClassification);
            HourlyClassification hc = pm as HourlyClassification;
            Assert.AreEqual(hourlyRate, hc.Rate);
            Assert.IsTrue(e.Schedule is WeeklySchedule);
        }
        
        [Test]
        public void ChangetoCommision()
        {
            AddSalariedEmployeeToDb();
            double hourlyRate = 25;

            double newSalary = 1250;
            double commisionRate = 0.01;
            ChangeEmployeeTransaction commision = new ChangeCommisionTransaction(database,EmpId, newSalary, commisionRate);
            commision.Execute();

            Employee e = database.GetEmployee(EmpId);
            PayrollDomain.PaymentClassification pm = e.Classification;
            Assert.IsTrue(pm is CommisionClassification);
            CommisionClassification hc = pm as CommisionClassification;
            Assert.AreEqual(commisionRate, hc.CommisionRate);
            Assert.AreEqual(newSalary, hc.Salary);
            Assert.IsTrue(e.Schedule is Biweekly);
        }
    }
}