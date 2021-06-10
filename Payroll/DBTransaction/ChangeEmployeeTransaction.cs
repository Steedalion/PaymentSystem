namespace Payroll.Tests
{
    public abstract class ChangeEmployeeTransaction:DbTransaction
    {
        protected ChangeEmployeeTransaction(int empId)
        {
            this.empId = empId;
        }

        protected int empId;
        protected abstract void  ModifyEmployee(Employee employee);
        
         public void Execute()
        {
            Employee e = PayrollDB.GetEmployee(empId);
            if (e.isNull)
            {
                throw new EmployeeNotFound();
            }
            ModifyEmployee(e);
        }
    }
}