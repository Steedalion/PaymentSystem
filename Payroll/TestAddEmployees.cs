using System;
using NUnit.Framework;

namespace Payroll
{
    [TestFixture]
    public class TestAddEmployees
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
            Assert.AreEqual(1000.00, sc.salary, 0.001);
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
    }

    class Biweekly : PaymentSchedule
    {
    }

    class CommisionClassification : PaymentClassification
    {
        public CommisionClassification(double commisionRate, double salary)
        {
            CommisionRate = commisionRate;
            Salary = salary;
        }

        public double CommisionRate{ get; }
        public double Salary{ get; }
    }

    public class AddCommissionedEmployee : AddEmployee
    {
        private readonly double _salary;
        private readonly float _commisionRate;

        public AddCommissionedEmployee(int empId, string name, string address, double salary, float commisionRate) :
            base(empId, name, address)
        {
            _salary = salary;
            _commisionRate = commisionRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new Biweekly();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommisionClassification(_commisionRate, _salary);
        }
    }


    public class HourlyClassification : PaymentClassification
    {
    }

    public class WeeklySchedule : PaymentSchedule
    {
    }

    public class AddHourlyEmployee : AddEmployee
    {
        public AddHourlyEmployee(int id, string name, string address) : base(id, name, address)
        {
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification();
        }
    }

    public interface PaymentMethod
    {
    }

    class HoldMethod : PaymentMethod
    {
    }

    public class MonthlySchedule : PaymentSchedule
    {
    }

    public interface PaymentSchedule
    {
    }

    public class SalariedClassification : PaymentClassification
    {
        public double salary;

        public SalariedClassification(double Salary)
        {
            salary = Salary;
        }
    }

    public interface PaymentClassification
    {
    }
}