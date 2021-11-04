
using PayrollDatabase;

namespace Transaction
{
    public abstract class DbTransaction
    {
        protected IPayrollDb database;

        public DbTransaction(IPayrollDb database)
        {
            this.database = database;
        }

        public abstract void Execute();
    }
}