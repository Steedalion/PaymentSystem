using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestPayrollDB
    {

        [Test]
        public void EmptyDBShouldNotContainUnionMember()
        {
            Assert.Throws<UnionMemberNotFound>(() =>PayrollDB.GetUnionMember(153));
        }

        [Test]
        public void AddingUnionMemberWithoutEmpShouldThrow()
        {
            Assert.Throws<EmployeeNotFound>(() => PayrollDB.AddUnionMember(1, 112));
        }
    }
}