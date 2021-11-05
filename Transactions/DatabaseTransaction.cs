using PayrollDB;

namespace Transactions
{
    public abstract class DatabaseTransaction
    {
        protected readonly IPayrollDb Database;

        protected DatabaseTransaction(IPayrollDb database)
        {
            this.Database = database;
        }

        public abstract void Execute();
    }
}