namespace Payroll
{
    public interface DbTransaction
    {
        void Execute();
    }

    public abstract class AddEmployee : DbTransaction
    {
        protected int Id;
        protected string Name;
        protected string Address;
        protected Employee e;

        protected AddEmployee(int id, string name, string address)
        {
            this.Id = id;
            Name = name;
            Address = address;
            e = new Employee(id, name, address);
        }

        public void Execute()
        {
            e.Schedule = MakePaymentSchedule();
            e.Classification = MakeClassification();
            e.Paymentmethod = MakePaymentMethod();
            PayrollDB.AddEmployee(Id, e);
        }

        protected abstract PaymentSchedule MakePaymentSchedule();

        protected virtual PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }
        protected abstract PaymentClassification MakeClassification();
    }
}