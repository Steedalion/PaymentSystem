using System;
using Affiliations;
using NUnit.Framework;
using PaymentClassifications;
using PayrollDB;
using PayrollDomain;

namespace TransactionTests.Tests.Transactions
{
    class TestAffiliationTransaction : TestSetupTransactions
    {
        [SetUp]
        public void AddSalaryEmployee()
        {
            AddSalaryEmployeeTransaction ae = new AddSalaryEmployeeTransaction(database,EmpId, Name, Address, 1000);
            ae.Execute();
        }

        [Test]
        public void AddUnionMember()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(database,EmpId, MemberId);
            am.Execute();
            Employee e = database.GetEmployee(EmpId);
            Employee unionMember = database.GetUnionMember(MemberId);

            Assert.AreEqual(e.Name, unionMember.Name);
            Assert.IsTrue(e.Affiliation is UnionAffiliation);
        }

        [Test]
        public void AddServiceChangeToUnaffiliatedEmployee()
        {
            AddUnionServiceCharge asc = new AddUnionServiceCharge(database,MemberId, new DateTime(2019, 8, 8), 20);
            Assert.Throws<UnionMemberNotFound>(() =>asc.Execute());
        }
        
       
        
        

        [Test]
        public void AddServiceChargeToEmployee()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(database,EmpId, MemberId);
            am.Execute();

            AddUnionServiceCharge asc = new AddUnionServiceCharge(database,MemberId, new DateTime(2019, 8, 8), 20);
            asc.Execute();

            Employee unionMember = database.GetUnionMember(MemberId);
            ServiceCharge sc = unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 8));

            Assert.AreEqual(20, sc.Amount);
        }

        [Test]
        public void UnfoundServiceChargeShouldThrowException()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(database,EmpId, MemberId);
            am.Execute();

            AddUnionServiceCharge asc = new AddUnionServiceCharge(database,MemberId, new DateTime(2019, 8, 8), 20);
            asc.Execute();

            Employee unionMember = database.GetUnionMember(MemberId);
            Assert.Throws<ServiceChargeNotFound>(() =>
                unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 9)));
        }

        [Test]
        public void Add2ServiceChargetToEmployee()
        {
            AddUnionMemberTransaction am = new AddUnionMemberTransaction(database,EmpId, MemberId);
            am.Execute();

            AddUnionServiceCharge asc = new AddUnionServiceCharge(database,MemberId, new DateTime(2019, 8, 8), 20);
            asc.Execute();
            AddUnionServiceCharge asc2 = new AddUnionServiceCharge(database,MemberId, new DateTime(2019, 8, 9), 25);
            asc2.Execute();

            Employee unionMember = database.GetUnionMember(MemberId);
            ServiceCharge sc = unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 8));

            Assert.AreEqual(20, sc.Amount);
            ServiceCharge sc2 = unionMember.Affiliation.GetServiceCharge(new DateTime(2019, 8, 9));
            Assert.AreEqual(25, sc2.Amount);
        }
    }
}