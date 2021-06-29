using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestPayrollDb
    {
        protected InMemoryDB database = new InMemoryDB();

        [Test]
        public void AddEmployeeToDB()
        {
            int empid = 12;

            Employee empl = new Employee(empid, "John", "home");
            database.AddEmployee(12,empl);
            Assert.IsNotEmpty(database.GetEmployeeIds());
        }
        [Test]
        public void EmptyDbShouldNotContainUnionMember()
        {
            Assert.Throws<UnionMemberNotFound>(() =>database.GetUnionMember(153));
        }

        [Test]
        public void AddingUnionMemberWithoutEmpShouldThrow()
        {
            Assert.Throws<EmployeeNotFound>(() => database.AddUnionMember(1, 112));
        }

        [Test]
        public void RemoveUnionMemberThatDoesNotExistShouldThrow()
        {
            Assert.Throws<UnionMemberNotFound>(() => database.RemoveUnionMember(123));
        }
    }
}