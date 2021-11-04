using System;
using NUnit.Framework;
using PayrollDomain;

namespace TransactionTests.Tests
{
    public class TestNoAffiliation
    {
        private Affiliation no;

        [SetUp]
        public void CreateNoAff()
        {
            no = new NoAffiliation();
        }

        [Test]
        public void GettingAServiceChangeFromNullWillAlwaysThrowError()
        {
            Assert.Throws<ServiceChargeNotFound>(() => no.GetServiceCharge(new DateTime(1, 1, 1)));
        }

        [Test]
        public void AddServiceChargeToNonWillThrowNull()
        {
            Assert.Throws<NullReferenceException>(() =>
                no.AddServiceCharge(new ServiceCharge(new DateTime(1, 1, 1), 20)));
        }
    }
}