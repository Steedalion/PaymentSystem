using NUnit.Framework;

namespace DatabaseTests.ContextTests
{
    public class ClearsTables : ContextTests
    {
        [SetUp]
        public void ClearAll()
        {
            database.Clear();
        }
        [Test]
        public void ClearClearsCommision()
        {
            Assert.IsEmpty(db.Commsions);
        }

        [Test]
        public void ClearSalay()
        {
            Assert.IsEmpty(db.Salaries);
        }

        [Test]
        public void ClearsUnion()
        {
            Assert.IsEmpty(db.UnionMember);
        }

        [Test]
        public void DirectDepositions()
        {
            Assert.IsEmpty(db.DirectDepositAccounts);
        }

        [Test]
        public void ClearAccountPaymentMethod()
        {
            Assert.IsEmpty(db.DirectDepositAccounts);
        }

        [Test]
        public void ClearMailPaymentMethod()
        {
            Assert.IsEmpty(db.PaycheckAddresses);
        }

        [Test]
        public void ClearHourlies()
        {
            Assert.IsEmpty(db.Hourlies);
        }

        [Test]
        public void ClearsSalesTickets()
        {
            Assert.IsEmpty(db.SalesReceipts);
        }

        [Test]
        public void ClearTimeCards()
        {
            Assert.IsEmpty(db.Timecards);
        }

    }
}