using System;
using System.Data.Linq;
using System.Linq;
using NUnit.Framework;
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

            var emps = db.GetTable<EmployeeUnit>();
            foreach (EmployeeUnit emp in emps)
            {
                Console.WriteLine(emp);
            }
        }

        [Test]
        public void PrintEmployeesAsPayrollContext()
        {
            foreach (EmployeeUnit employee in db.Employees)
            {
                Console.WriteLine(employee);
            }
        }

        [Test]
        public void AddEmployees()
        {
            Employee emp = new Employee(123, "John", "Home");
            EmployeeUnit unit =
                new EmployeeUnit(123, emp);
            db.Employees.InsertOnSubmit(unit);
            db.SubmitChanges();
            Assert.AreEqual(1, db.Employees.Count());
        }
    }
}