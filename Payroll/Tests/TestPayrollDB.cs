using NUnit.Framework;

namespace Payroll.Tests
{
    public class TestPayrollDb
    {

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
    }
}