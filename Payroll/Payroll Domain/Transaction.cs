namespace Payroll
{
    public abstract class DbTransaction
    {
        protected IPayrollDB database;

        public DbTransaction(InMemoryDB database)
        {
            this.database = database;
        }

        public abstract void Execute();
    }
}