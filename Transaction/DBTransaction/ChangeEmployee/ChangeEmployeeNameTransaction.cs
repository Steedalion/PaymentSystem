using PayrollDatabase;
using PayrollDomains;

namespace Transaction.DBTransaction.ChangeEmployee
{
    public class ChangeEmployeeNameTransaction : ChangeEmployeeTransaction
    {
        private string newName;

        public ChangeEmployeeNameTransaction(IPayrollDb database, int empId, string newName) : base(database, empId)
        {
            this.newName = newName;
        }



        protected override void ModifyEmployee(Employee employee)
        {
            employee.Name = newName;
        }
    }
}