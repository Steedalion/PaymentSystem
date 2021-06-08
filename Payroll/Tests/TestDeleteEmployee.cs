using NUnit.Framework;
using Payroll.DBTransaction;

namespace Payroll.Tests
{
    public class TestDeleteEmployee
    {
        [Test]
        public void DeleteAnEmployee()
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
    }
}