using PayrollDB;
using PayrollDomain;

namespace Transactions.DBTransaction.ChangeEmployee
{
    public class ChangeEmployeeAddressTransaction : ChangeEmployeeTransaction
    {
        private string newAddress;

        public ChangeEmployeeAddressTransaction(PayrollDB.IPayrollDb database, int empId, string newAddress) : base(database, empId)
        {
            this.newAddress = newAddress;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.myAddress = newAddress;
        }
    }
}