using PayrollDB;
using PayrollDomain;

namespace Transactions.DBTransaction
{
    public abstract class AddEmployee : DbTransaction
    {
        protected int Id;
        protected string Name;
        protected string Address;

        protected AddEmployee(PayrollDB.IPayrollDb database, int id, string name, string address) : base(database)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public override void Execute()
        {
            Employee e = new Employee(Id, Name, Address);
            e.Schedule = MakePaymentSchedule();
            e.Classification = MakeClassification();
            e.Paymentmethod = MakePaymentMethod();
            database.AddEmployee(Id, e);
        }

        protected abstract PaymentSchedule MakePaymentSchedule();

        protected virtual PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }
        protected abstract PaymentClassification MakeClassification();
    }
}