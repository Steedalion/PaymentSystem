using System;
using System.Collections.Generic;
using System.Linq;
using Transactions;

namespace Presenters
{
    public class TransactionContainer
    {
        public List<DatabaseTransaction> transactions = new List<DatabaseTransaction>();

        public delegate void OnAction();

        private OnAction OnAdd;

        public void OnAddExecute(OnAction onAdd)
        {
            OnAdd += onAdd;
        }

        public void Add(DatabaseTransaction transaction)
        {
            transactions.Add(transaction);
            OnAdd?.Invoke();
        }

        public void RunTransactions()
        {
            foreach (DatabaseTransaction transaction in transactions)
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

        public List<DatabaseTransaction> GetTransactions()
        {
            return transactions;
        }
    }
}