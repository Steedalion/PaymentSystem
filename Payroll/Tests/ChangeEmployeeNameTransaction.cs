namespace Payroll.Tests
{
    public class ChangeEmployeeNameTransaction : ChangeEmployeeTransaction
    {
        private string newName;

        public ChangeEmployeeNameTransaction(int empId, string newName) : base(empId)
        {
            this.empId = empId;
            this.newName = newName;
        }


        protected override void ModifyEmployee(Employee employee)
        {
            employee.Name = newName;
        }
    }
}