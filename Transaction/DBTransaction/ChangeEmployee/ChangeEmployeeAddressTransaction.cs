using PayrollDatabase;
using PayrollDomains;

namespace Transaction.DBTransaction.ChangeEmployee
{
    public class ChangeEmployeeAddressTransaction : ChangeEmployeeTransaction
    {
        private string newAddress;

        public ChangeEmployeeAddressTransaction(IPayrollDb database, int empId, string newAddress) : base(database, empId)
        {
            this.newAddress = newAddress;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.myAddress = newAddress;
        }
    }
}