namespace Payroll
{
    public class Employee
    {
        public string name;
        public string address;
        public PaymentMethod method = new HoldMethod();

        public Employee(int id, string name, string address, double salary)
        {
            this.name = name;
            this.address = address;
        }

        public PaymentClassification Classification { get; set; }
    }
}