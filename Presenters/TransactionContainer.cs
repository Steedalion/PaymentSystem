using System;
using System.Collections.Generic;
using System.Linq;
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
}