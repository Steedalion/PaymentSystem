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

    public class AddSalariedEmployee
    {
        private int id;
        private string ename;
        private string eaddress;
        private double _salary;
        private Employee e;

        public AddSalariedEmployee(int id, string name, string address, double salary)
        {
            this.id = id;
            e = new Employee(id, name, address, salary);
            e.Classification = new SalariedClassification(salary);
            ename = name;
            eaddress = address;
            _salary = salary;
        }

        public void Execute()
        {
            PayrollDatabase.AddEmployee(id, e);
        }
    }
}