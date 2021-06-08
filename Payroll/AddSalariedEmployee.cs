namespace Payroll
{
    public interface DbTransaction
    {
        void Execute();
    }

    public class AddSalariedEmployeeTransaction : DbTransaction
    {
        protected int id;
        protected string ename;
        protected string eaddress;
        protected Employee e;

        public void Execute()
        {
            PayrollDatabase.AddEmployee(id, e);
        }
    }

    public class AddSalariedEmployee : AddSalariedEmployeeTransaction
    {
        public AddSalariedEmployee(int id, string name, string address, double salary)
        {
            this.id = id;
            ename = name;
            eaddress = address;
            e = new Employee(id, name, address, salary);
            e.Classification = new SalariedClassification(salary);
        }
    }
}