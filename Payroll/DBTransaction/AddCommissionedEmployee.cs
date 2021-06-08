namespace Payroll
{
    public class AddCommissionedEmployee : AddEmployee
    {
        private readonly double _salary;
        private readonly float _commisionRate;

        public AddCommissionedEmployee(int empId, string name, string address, double salary, float commisionRate) :
            base(empId, name, address)
        {
            _salary = salary;
            _commisionRate = commisionRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new Biweekly();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommisionClassification(_commisionRate, _salary);
        }
    }

 
}