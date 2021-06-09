namespace Payroll
{
    public class Employee
    {

        public string Name;
        public string myAddress;
        public PaymentMethod Paymentmethod;
        private readonly int myID;
        public bool isNull => this == NULL;
        public static readonly Employee NULL = new NullEmployee(0,"name","nowhere");

        public Employee(int id, string name, string address)
        {
            myID = id;
            Name = name;
            myAddress = address;
        }

        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public Affiliation Affiliation { get; set; } = null;

        private class NullEmployee : Employee
        {
            public NullEmployee(int id, string name, string address) : base(id, name, address)
            {
            }
        }
    }
}