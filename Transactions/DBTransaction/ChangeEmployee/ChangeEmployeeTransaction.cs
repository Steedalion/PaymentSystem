using PayrollDB;
using PayrollDomain;

namespace Transactions.DBTransaction.ChangeEmployee
{
    public abstract class ChangeEmployeeTransaction:DatabaseTransaction
    {
        protected int empId;

        protected ChangeEmployeeTransaction(PayrollDB.IPayrollDb database, int empId) : base(database)
        {
            this.empId = empId;
        }

        protected abstract void  ModifyEmployee(Employee employee);
        
         public override void Execute()
        {
            Employee e = Database.GetEmployee(empId);
            if (e.isNull)
            {
                throw new EmployeeNotFound();
            }
            ModifyEmployee(e);
        }
    }
}