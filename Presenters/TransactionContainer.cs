using System;
using System.Collections.Generic;
using NUnit.Framework;
using PayrollDB;
using Transactions;

namespace Presenters
{
    public class TransactionContainer
    {
        public List<DbTransaction> transactions = new List<DbTransaction>();

        public delegate void OnAction();

        private OnAction OnAdd;

        public void OnAddExecute(OnAction onAdd)
        {
            OnAdd += onAdd;
        }

        public void Add(DbTransaction transaction)
        {
            transactions.Add(transaction);
            OnAdd?.Invoke();
        }

        public void RunTransactions()
        {
            foreach (DbTransaction transaction in transactions)
            {
                transaction.Execute();
            }

            ClearTransactions();
        }

        private void ClearTransactions()
        {
            transactions.Clear();
        }

        public int Size()
        {
            return transactions.Count;
        }

        public List<DbTransaction> GetTransactions()
        {
            return transactions;
        }
    }

    [TestFixture]
    public class TestTransactionContainer
    {
        private TransactionContainer container;
        private bool actionExecuted;
        private DbTransaction transaction;

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