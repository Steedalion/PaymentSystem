using DatabaseTests.DatabaseTests;
using NUnit.Framework;
using PayrollDB;
using PayrollDomain;

namespace InMemoryDBTest
{
    public class PayrollExceptionsTest
    {
        public IPayrollDb database = new InMemoryDB();

        [Test]
        public void AddEmployeeToDB()
        {
            database.Clear();
            int empid = 12;

            Employee empl = An.GenericEmployee;
            database.AddEmployee(12,empl);
            Assert.IsNotEmpty(database.GetEmployeeIds());
        }
        [Test]
        public void EmptyDbShouldNotContainUnionMember()
        {
             database.Clear();
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