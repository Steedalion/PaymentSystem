namespace Payroll.DBTransaction
{
    public class DeleteEmployee : DbTransaction
    {
        private readonly int id;

        public DeleteEmployee(InMemoryDB database, int id) : base(database)
        {
            this.id = id;
        }


        public override void Execute()
        {
            database.RemoveEmployee(id);
        }
    }
}