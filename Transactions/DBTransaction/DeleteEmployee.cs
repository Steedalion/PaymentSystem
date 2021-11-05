using PayrollDB;

namespace Transactions.DBTransaction
{
    public class DeleteEmployee : DatabaseTransaction
    {
        private readonly int id;

        public DeleteEmployee(PayrollDB.IPayrollDb database, int id) : base(database)
        {
            this.id = id;
        }


        public override void Execute()
        {
            Database.RemoveEmployee(id);
        }
    }
}