namespace Payroll.Tests
{
    public class ChangeEmployeeNameTransaction : ChangeEmployeeTransaction
    {
        private string newName;

        public ChangeEmployeeNameTransaction(InMemoryDB database, int empId, string newName) : base(database, empId)
        {
            this.newName = newName;
        }



        protected override void ModifyEmployee(Employee employee)
        {
            employee.Name = newName;
        }
    }
}