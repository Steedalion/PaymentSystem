using System;
using NUnit.Framework;
using Payroll.DBTransaction;

namespace Payroll.Tests
{
    [TestFixture]
    public class TestUseCaseTests
    {
        int empID = 1;
        string name = "bob";
        string address = "home";
        double salary = 1000.00;


        [SetUp]
        public void ClearDataBase()
        {
            PayrollDatabase.Clear();
        }

        [Test]
        public void TestAddSalariedEmployee()
        {
            AddSalaryEmployee t = new AddSalaryEmployee(empID, name, address, salary);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empID);
            Assert.AreEqual(name, e.Name);
            Assert.AreEqual(address, e.myAddress);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, 0.001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);
            PaymentMethod pm = e.Paymentmethod;
            Assert.IsTrue(pm is HoldMethod);
        }

        [Test]
        public void TestAddHourlyEmployee()
        {
            empID = 3;
            AddEmployee t = new AddHourlyEmployee(empID, name, address);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empID);
            Assert.AreEqual(name, e.Name);
            Assert.AreEqual(address, e.myAddress);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            PaymentSchedule sc = e.Schedule;
            Assert.IsTrue(sc is WeeklySchedule);
            PaymentMethod pm = e.Paymentmethod;
            Assert.IsTrue(pm is HoldMethod);
        }

        [Test]
        public void TestAddCommissionedEmployee()
        {
            empID = 4;
            float commisionRate = 0.14f;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empID, name, address, salary, commisionRate);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empID);
            Assert.NotNull(e);

            Assert.AreEqual(name, e.Name);
            Assert.AreEqual(address, e.myAddress);

            PaymentClassification pc = e.Classification;
            PaymentSchedule ps = e.Schedule;
            PaymentMethod pm = e.Paymentmethod;

            Assert.IsTrue(pm is HoldMethod);
            Assert.IsTrue(pc is CommisionClassification);
            CommisionClassification cc = pc as CommisionClassification;
            Assert.IsTrue(ps is Biweekly);

            Assert.AreEqual(salary, cc.Salary);
            Assert.AreEqual(commisionRate, cc.CommisionRate);
        }
        
        [Test]
        public void TestAddedSalesRecieptShouldExist()
        {
            int empId = 5;
            string name = "bob";
            string address = "Home";

            AddCommissionedEmployee ce = new AddCommissionedEmployee(empId, name, address, 1000, 0.1);
            ce.Execute();

            SalesRecieptTransaction sr = new SalesRecieptTransaction(empId, new DateTime(2005, 8, 8), 200);
            sr.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommisionClassification);
            CommisionClassification cc = pc as CommisionClassification;
            SalesReciept sp = cc.GetSalesReciept(new DateTime(2005, 8, 8));
            Assert.AreEqual(200,sp.Amount,0.001);
        }
        
        [Test]
        public void TestDeleteAnEmployee()
        {
            int empId = 5;
            string name = "bob";
            string address = "Home";
            
            AddSalaryEmployee t = new AddSalaryEmployee(empId, name, address, 100);
            t.Execute();
            Assert.NotNull(PayrollDatabase.GetEmployee(empId));
            DeleteEmployee deleteEmployee = new DeleteEmployee(empId);
            deleteEmployee.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNull(e);
        }
        
        [Test]
        public void AddedTimeCardShouldBe()
        {
            int empId = 5;
            string name = "bob";
            string address = "Home";
            AddHourlyEmployee addHourlyEmployee = new AddHourlyEmployee(empId, name, address);
            addHourlyEmployee.Execute();

            TimeCardTransaction timeCardTransaction = new TimeCardTransaction(empId, new DateTime(2005, 7, 31),  8.00);
            timeCardTransaction.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.AreEqual(8.0,tc.Hours,0.01);
        }
    }
}