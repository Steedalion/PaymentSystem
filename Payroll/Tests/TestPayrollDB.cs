using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestPayrollDB
    {

        [Test]
        public void EmptyDBShouldNotContainUnionMember()
        {
            Assert.Throws<UnionMemberNotFound>(() =>PayrollDatabase.GetUnionMember(153));
        }

        [Test]
        public void AddingUnionMemberWithoutEmpShouldThrow()
        {
            Assert.Throws<EmployeeNotFound>(() => PayrollDatabase.AddUnionMember(1, 112));
        }
    }
}