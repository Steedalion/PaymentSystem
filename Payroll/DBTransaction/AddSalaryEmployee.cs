namespace Payroll
{
    public class AddSalaryEmployee : AddEmployee
    {
        private double mySalary;

        public AddSalaryEmployee(int id, string name, string address, double salary) : base(id, name, address)
        {
            this.Id = id;
            Name = name;
            Address = address;
            e = new Employee(id, name, address);
            mySalary = salary;
        }


        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }

        protected override PaymentClassification MakeClassification()
        {
            SalariedClassification sc = new SalariedClassification(mySalary);
            return sc;
        }
    }
}