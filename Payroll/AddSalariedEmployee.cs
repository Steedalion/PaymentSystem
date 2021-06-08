namespace Payroll
{
    public interface DbTransaction
    {
        void Execute();
    }

    public abstract class AddEmployee : DbTransaction
    {
        protected int id;
        protected string Name;
        protected string Address;
        protected Employee e;

        public AddEmployee(int id, string name, string address)
        {
            this.id = id;
            this.Name = name;
            this.Address = address;
            e = new Employee(id, name, address);
        }

        public void Execute()
        {
            e.Schedule = MakePaymentSchedule();
            e.Classification = MakeClassification();
            e.Paymentmethod = MakePaymentMethod();
            PayrollDatabase.AddEmployee(id, e);
        }

        protected abstract PaymentSchedule MakePaymentSchedule();

        protected virtual PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }
        protected abstract PaymentClassification MakeClassification();
    }

    public class AddSalaryEmployee : AddEmployee
    {
        private double _salary;

        public AddSalaryEmployee(int id, string name, string address, double salary) : base(id, name, address)
        {
            this.id = id;
            Name = name;
            Address = address;
            e = new Employee(id, name, address);
            _salary = salary;
        }


        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlySchedule();
        }

        protected override PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }

        protected override PaymentClassification MakeClassification()
        {
            SalariedClassification sc = new SalariedClassification(_salary);
            return sc;
        }
    }
}