using PayrollDatabase;
using PayrollDomains;

namespace Transaction.DBTransaction.ChangeEmployee
{
    public abstract class ChangeEmployeeTransaction:DbTransaction
    {
        protected int empId;

        protected ChangeEmployeeTransaction(IPayrollDb database, int empId) : base(database)
        {
            this.empId = empId;
        }

        protected abstract void  ModifyEmployee(Employee employee);
        
         public override void Execute()
        {
            Employee e = database.GetEmployee(empId);
            if (e.isNull)
            {
                throw new EmployeeNotFound();
            }
            ModifyEmployee(e);
        }
    }
}