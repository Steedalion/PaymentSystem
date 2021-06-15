using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestPayrollDb
    {

        [Test]
        public void AddEmployeeToDB()
        {
            int empid = 12;

            Employee empl = new Employee(empid, "John", "home");
            PayrollDB.AddEmployee(12,empl);
            Assert.IsNotEmpty(PayrollDB.GetEmployeeIds());
        }
        [Test]
        public void EmptyDbShouldNotContainUnionMember()
        {
            Assert.Throws<UnionMemberNotFound>(() =>PayrollDB.GetUnionMember(153));
        }

        [Test]
        public void AddingUnionMemberWithoutEmpShouldThrow()
        {
            Assert.Throws<EmployeeNotFound>(() => PayrollDB.AddUnionMember(1, 112));
        }

        [Test]
        public void RemoveUnionMemberThatDoesNotExistShouldThrow()
        {
            Assert.Throws<UnionMemberNotFound>(() => PayrollDB.RemoveUnionMember(123));
        }
    }
}