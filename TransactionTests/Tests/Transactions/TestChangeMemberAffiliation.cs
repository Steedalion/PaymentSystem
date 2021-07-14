using Affiliations;
using NUnit.Framework;
using PayrollDomain;
using Transactions;

namespace Payroll.Tests.Transactions
{
    public class TestChangeMemberAffiliation:TestSetupTransactions
    {

        [Test]
        public void TestChangeToUnionMember()
        {
            AddSalariedEmployeeToDb();
            double dues = 2;
            ChangeAffiliation ca = new ChangeUnionMember(database,EmpId, MemberId, dues);
            ca.Execute();

            Employee e = database.GetEmployee(EmpId);
            Affiliation affiliation = e.Affiliation;
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation ua = affiliation as UnionAffiliation;
            Assert.AreEqual(dues, ua.Dues);
            Assert.AreSame(database.GetEmployee(EmpId), database.GetUnionMember(MemberId));
        }

        [Test]
        public void TestChangeAffiliatedToUnaffiliated()
        {
            TestChangeToUnionMember();
            ChangeAffiliation ca = new ChangeUnaffiliated(database,EmpId, MemberId);
            ca.Execute();

            Employee e = database.GetEmployee(EmpId);
            Affiliation affiliation = e.Affiliation;
            Assert.IsTrue(affiliation is NoAffiliation);
            
        
        }

    }
}