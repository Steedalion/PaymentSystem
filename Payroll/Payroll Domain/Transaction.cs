namespace Payroll
{
    public abstract class DbTransaction
    {
        protected PayrollDB database;

        public DbTransaction(PayrollDB database)
        {
            this.database = database;
        }

        public abstract void Execute();
    }
}