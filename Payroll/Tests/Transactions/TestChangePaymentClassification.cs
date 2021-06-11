using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    public class TestChangePaymentClassification : TestSetupTransactions
    {

        [Test]
        public void ChangeToSalaryPMC()
        {
            AddHourlyEmployeeToDB();
            ChangeSalaryEmployee salary = new ChangeSalaryEmployee(EmpId, Salary);
            salary.Execute();
            Employee employee = PayrollDB.GetEmployee(EmpId);
            Assert.IsFalse(employee.isNull);
            PaymentClassification p = employee.Classification;
            Assert.IsTrue(p is SalariedClassification);
            SalariedClassification salariedClassification = p as SalariedClassification;
            Assert.AreEqual(Salary, salariedClassification.Salary, 0.001);
        }

        [Test]
        public void ChangetoHourly()
        {
            AddSalariedEmployeeToDb();
            double hourlyRate = 25;
            ChangeHourlyEmployee hourlyEmployee = new ChangeHourlyEmployee(EmpId, hourlyRate);
            hourlyEmployee.Execute();

            Employee e = PayrollDB.GetEmployee(EmpId);
            PaymentClassification pm = e.Classification;
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
            ChangeEmployeeTransaction commision = new ChangeCommisionTransaction(EmpId, newSalary, commisionRate);
            commision.Execute();

            Employee e = PayrollDB.GetEmployee(EmpId);
            PaymentClassification pm = e.Classification;
            Assert.IsTrue(pm is CommisionClassification);
            CommisionClassification hc = pm as CommisionClassification;
            Assert.AreEqual(commisionRate, hc.CommisionRate);
            Assert.AreEqual(newSalary, hc.Salary);
            Assert.IsTrue(e.Schedule is Biweekly);
        }
        
        
    }

    public class ChangeCommisionTransaction : ChangeEmployeeClassification
    {
        private double salary;
        private double commisionRate;

        public ChangeCommisionTransaction(int empId, double salary, double commisionRate) : base(empId)
        {
            this.salary = salary;
            this.commisionRate = commisionRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new Biweekly();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommisionClassification(commisionRate, salary);
        }
    }

    public class ChangeHourlyEmployee:ChangeEmployeeClassification
    {
        private double hourlyRate;

        public ChangeHourlyEmployee(int empId, double hourlyRate) : base(empId)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }
    }

    public class ChangeSalaryEmployee : ChangeEmployeeClassification
    {
        private double salary;

        public ChangeSalaryEmployee(int empId, double salary) : base(empId)
        {
            this.salary = salary;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification(salary);
        }
    }

    public abstract class ChangeEmployeeClassification : ChangeEmployeeTransaction
    {
        protected ChangeEmployeeClassification(int empId) : base(empId)
        {
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Classification = MakeClassification();
            employee.Schedule = MakePaymentSchedule();
        }

        protected abstract PaymentSchedule MakePaymentSchedule();

        protected abstract PaymentClassification MakeClassification();
    }
}