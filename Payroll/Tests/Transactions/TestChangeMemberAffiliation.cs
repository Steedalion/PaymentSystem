using System;
using System.Diagnostics;
using NUnit.Framework;

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

    public class NoAffiliation : Affiliation
    {
        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            throw new ServiceChargeNotFound();
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            throw new NullReferenceException();
        }

        public double CalculateDeductions()
        {

            return 0;
        }
    }

    public class ChangeUnionMember : ChangeAffiliation
    {
        private int memberID;
        private double dues;

        public ChangeUnionMember(PayrollDB database, int empId, int memberId, double dues) : base(database, empId, memberId)
        {
            memberID = memberId;
            this.dues = dues;
        }

        protected override Affiliation MakeAffiliation()
        {
            UnionAffiliation unionAffiliation = new UnionAffiliation();
            unionAffiliation.Dues = dues;
            return unionAffiliation;
        }

        protected override void ModifyMembership()
        {
            database.AddUnionMember(memberID,empId);
        }
    }
}