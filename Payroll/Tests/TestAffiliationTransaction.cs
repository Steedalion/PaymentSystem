using System;
using NUnit.Framework;

namespace Payroll.Tests
{
    class TestAffiliationTransaction : TestSetup
    {
        [SetUp]
        public void AddSalaryEmployee()
        {
            AddSalaryEmployee ae = new AddSalaryEmployee(empID, name, address, 1000);
            ae.Execute();
        }

        [Test]
        public void AddUnionMember()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(empID, memberId);
            am.Execute();
            Employee e = PayrollDB.GetEmployee(empID);
            Employee unionMember = PayrollDB.GetUnionMember(memberId);

            Assert.AreEqual(e.Name, unionMember.Name);
            Assert.IsTrue(e.Affiliation is UnionAffiliation);
        }

        [Test]
        public void AddServiceChangeToUnaffiliatedEmployee()
        {
            AddUnionServiceCharge asc = new AddUnionServiceCharge(memberId, new DateTime(2019, 8, 8), 20);
            Assert.Throws<UnionMemberNotFound>(() =>asc.Execute());
        }
        
       
        
        

        [Test]
        public void AddServiceChargeToEmployee()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(empID, memberId);
            am.Execute();

            AddUnionServiceCharge asc = new AddUnionServiceCharge(memberId, new DateTime(2019, 8, 8), 20);
            asc.Execute();

            Employee unionMember = PayrollDB.GetUnionMember(memberId);
            ServiceCharge sc = unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 8));

            Assert.AreEqual(20, sc.Amount);
        }

        [Test]
        public void UnfoundServiceChargeShouldThrowException()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(empID, memberId);
            am.Execute();

            AddUnionServiceCharge asc = new AddUnionServiceCharge(memberId, new DateTime(2019, 8, 8), 20);
            asc.Execute();

            Employee unionMember = PayrollDB.GetUnionMember(memberId);
            Assert.Throws<ServiceChargeNotFound>(() =>
                unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 9)));
        }

        [Test]
        public void Add2ServiceChargetToEmployee()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(empID, memberId);
            am.Execute();

            AddUnionServiceCharge asc = new AddUnionServiceCharge(memberId, new DateTime(2019, 8, 8), 20);
            asc.Execute();
            AddUnionServiceCharge asc2 = new AddUnionServiceCharge(memberId, new DateTime(2019, 8, 9), 25);
            asc2.Execute();

            Employee unionMember = PayrollDB.GetUnionMember(memberId);
            ServiceCharge sc = unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 8));

            Assert.AreEqual(20, sc.Amount);
            ServiceCharge sc2 = unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 9));
            Assert.AreEqual(25, sc2.Amount);
        }
    }
}