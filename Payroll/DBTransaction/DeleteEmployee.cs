namespace Payroll.DBTransaction
{
    public class DeleteEmployee : DbTransaction
    {
        private readonly int id;

        public DeleteEmployee(PayrollDB database, int id) : base(database)
        {
            this.id = id;
        }


        public override void Execute()
        {
            database.RemoveEmployee(id);
        }
    }
}