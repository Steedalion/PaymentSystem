using NUnit.Framework;
using Payroll.TestBuilders;
using PayrollDB;

namespace DatabaseTests.DatabaseTests
{
    public class AddUnionMember : AddEmployeeTest
    {
        [Test]
        public void AddSimpleEmployee()
        {
            var e = An.GenericEmployee;
            database.AddEmployee(100, e);
            database.AddUnionMember(1, 100);
        } [Test]
        public void AddSimpleEmployee2()
        {
            var e = An.GenericEmployee.Build();
            database.AddEmployee(100, e);
            database.AddUnionMember(1, 100);
            var u = database.GetUnionMember(1);
            Assert.AreEqual(e.Name, u.Name);
        }

        [Test]
        public void GetNonExistingUnionMember()
        {
            Assert.Throws<UnionMemberNotFound>(() => database.GetUnionMember(1));
        }

    }
}