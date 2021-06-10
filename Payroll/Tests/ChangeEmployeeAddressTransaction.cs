namespace Payroll.Tests
{
    public class ChangeEmployeeAddressTransaction : ChangeEmployeeTransaction
    {
        private string newAddress;

        public ChangeEmployeeAddressTransaction(int empId, string newAddress) : base(empId)
        {
            this.newAddress = newAddress;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.myAddress = newAddress;
        }
    }
}