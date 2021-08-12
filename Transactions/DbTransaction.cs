using PayrollDB;

namespace Transactions
{
    public abstract class DbTransaction
    {
        protected PayrollDB.IPayrollDb database;

        public DbTransaction(PayrollDB.IPayrollDb database)
        {
            this.database = database;
        }

        public abstract void Execute();
    }
}