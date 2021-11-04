using PayrollDatabase;

namespace Transactions.DBTransaction
{
    public class DeleteEmployee : DbTransaction
    {
        private readonly int id;

        public DeleteEmployee(IPayrollDb database, int id) : base(database)
        {
            this.id = id;
        }


        public override void Execute()
        {
            database.RemoveEmployee(id);
        }
    }
}