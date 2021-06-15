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
            ChangeAffiliation ca = new ChangeUnionMember(EmpId, MemberId, dues);
            ca.Execute();

            Employee e = PayrollDB.GetEmployee(EmpId);
            Affiliation affiliation = e.Affiliation;
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation ua = affiliation as UnionAffiliation;
            Assert.AreEqual(dues, ua.Dues);
            Assert.AreSame(PayrollDB.GetEmployee(EmpId), PayrollDB.GetUnionMember(MemberId));
        }

        [Test]
        public void TestChangeAffiliatedToUnaffiliated()
        {
            TestChangeToUnionMember();
            ChangeAffiliation ca = new ChangeUnaffiliated(EmpId, MemberId);
            ca.Execute();

            Employee e = PayrollDB.GetEmployee(EmpId);
            Affiliation affiliation = e.Affiliation;
            Assert.IsTrue(affiliation is NoAffiliation);
            
        
        }

    }

    public class ChangeUnaffiliated : ChangeAffiliation
    {
        public ChangeUnaffiliated(int empId, int memberId) : base(empId, memberId)
        {
        }

        protected override Affiliation MakeAffiliation()
        {
            return new NoAffiliation();
        }

        protected override void ModifyMembership()
        {
            PayrollDB.RemoveUnionMember(memberId);
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

        public ChangeUnionMember(int empId, int memberId, double dues) : base(empId, memberId)
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
            PayrollDB.AddUnionMember(memberID,empId);
        }
    }

    public abstract class ChangeAffiliation:ChangeEmployeeTransaction
    {
        protected int memberId;

        protected ChangeAffiliation(int empId, int memberId) : base(empId)
        {
            this.memberId = memberId;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Affiliation = MakeAffiliation();
            ModifyMembership();
        }

        protected abstract Affiliation MakeAffiliation();

        protected abstract void ModifyMembership();
    }
}