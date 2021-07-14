using PayrollDB;
using PayrollDomain;

namespace Transactions.DBTransaction.ChangeEmployee
{
    public class ChangeEmployeeNameTransaction : ChangeEmployeeTransaction
    {
        private string newName;

        public ChangeEmployeeNameTransaction(IPayrollDB database, int empId, string newName) : base(database, empId)
        {
            this.newName = newName;
        }



        protected override void ModifyEmployee(Employee employee)
        {
            employee.Name = newName;
        }
    }
}