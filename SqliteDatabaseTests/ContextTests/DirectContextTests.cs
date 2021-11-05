using System;
using System.Data.Linq;
using System.Linq;
using NUnit.Framework;
using Payroll.TestBuilders;
using PayrollDataBase.Linq2SQL;
using PayrollDomain;

namespace DatabaseTests.ContextTests
{
    public class DirectContextTests : ContextTests
    {
        [Test]
        public void ConnectToDB()
        {
            DataContext db = new EmployeeContext(connection);
            string version = db.Connection.ServerVersion;
            Console.WriteLine(version);
            Assert.IsNotNull(version);
        }

        [Test]
        public void PrintEmployeesFromDataContext()
        {
            DataContext db = new EmployeeContext(connection);
            string version = db.Connection.ServerVersion;
            Console.WriteLine(version);
            Assert.IsNotNull(version);
            AddRandomEmployee();

            var emps = db.GetTable<EmployeeUnit>();
            foreach (EmployeeUnit emp in emps)
            {
                Console.WriteLine(emp);
            }
        }

        [Test]
        public void PrintEmployeesAsPayrollContext()
        {
            AddRandomEmployee();
            foreach (EmployeeUnit employee in db.Employees)
            {
                employee.ToString();
            }
        }

        private void AddRandomEmployee()
        {
            db.Employees.InsertOnSubmit(new EmployeeUnit(1,An.GenericEmployee));
            db.SubmitChanges();
        }

        [Test]
        public void TryToCreateAnEmployeeAdapterWithoutScedule()
        {
            Employee emp = An.GenericEmployee.WithoutSchedule();
            Assert.Throws<PayrollDataBase.UnknownPaymentScheduleException>(() => new EmployeeUnit(123, emp));
            Assert.AreEqual(0, db.Employees.Count());
        }
    }
}