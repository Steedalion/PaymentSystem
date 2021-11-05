using NUnit.Framework;
using PayrollDB;
using Transactions;

namespace Presenters
{
    [TestFixture]
    public class TestTransactionContainer
    {
        private TransactionContainer container;
        private bool actionExecuted;
        private DatabaseTransaction transaction;

        [SetUp]
        public void SetUp()
        {
            actionExecuted = false;
            container = new TransactionContainer();
            container.OnAddExecute(SillyAction);
            transaction = new MockTransaction(new InMemoryDB());
        }

        [Test]
        public void Creation()
        {
            Assert.AreEqual(0, container.Size());
        }

        [Test]
        public void AddingTransaction()
        {
            container.Add(transaction);
            Assert.AreEqual(1,container.Size());
            Assert.AreSame(transaction,container.GetTransactions()[0]);
        }

        [Test]
        public void AddTransactionTriggersDeligate()
        {
            Assert.IsFalse(actionExecuted);
            container.Add(transaction);
            Assert.IsTrue(actionExecuted);
        }

        private void SillyAction()
        {
            actionExecuted = true;
        }
    }
}