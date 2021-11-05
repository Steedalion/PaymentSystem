using PayrollDB;
using PayrollDomain;

namespace Transactions.DBTransaction
{
    public abstract class AddEmployeeTransaction : DatabaseTransaction
    {
        protected int Id;
        protected string Name;
        protected string Address;

        protected AddEmployeeTransaction(PayrollDB.IPayrollDb database, int id, string name, string address) : base(database)
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
            Database.AddEmployee(Id, e);
        }

        protected abstract IPaymentSchedule MakePaymentSchedule();

        protected virtual IPaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }
        protected abstract IPaymentClassification MakeClassification();
        public override string ToString()
        {
            return "AddEmployee: " + Name + ":" + Id;
        }
    }
}