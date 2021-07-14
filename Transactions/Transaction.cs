using PayrollDB;

namespace Transactions
{
    public abstract class DbTransaction
    {
        protected IPayrollDB database;

        public DbTransaction(IPayrollDB database)
        {
            this.database = database;
        }

        public abstract void Execute();
    }
}