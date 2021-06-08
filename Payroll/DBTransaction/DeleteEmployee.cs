namespace Payroll.DBTransaction
{
    public class DeleteEmployee : DbTransaction
    {
        private readonly int id;

        public DeleteEmployee(int empId)
        {
            id = empId;
        }

        public void Execute()
        {
            PayrollDatabase.RemoveEmployee(id);
        }
    }
}