using NUnit.Framework;

namespace Payroll
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestAddSalariedEmployee()
        {
            int empID = 1;
            string name = "bob";
            string address = "home";

            AddSalariedEmployee t = new AddSalariedEmployee(empID, name, address, 1000.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empID);

            Assert.AreEqual(name, e.name);
            Assert.AreEqual(address, e.address);
            PaymentClassification
                pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.salary, 0.001);
            PaymentSchedule ps = sc.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            PaymentMethod pm = e.method;
            Assert.IsTrue(pm is HoldMethod);
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
            this.salary = Salary;
            Schedule= new MonthlySchedule();
        }

        public PaymentSchedule Schedule { get; set; }
    }

    public interface PaymentClassification
    {
    }
}