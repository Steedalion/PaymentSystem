namespace PayrollDomain
{
    public class Employee
    {

        public string Name;
        public string myAddress;
        public PaymentMethod Paymentmethod;
        private readonly int myID; //employee should not know his database ID.
        public bool isNull => this == NULL;
        public static readonly Employee NULL = new NullEmployee(0,"name","nowhere");

        public Employee(int id, string name, string address)
        {
            myID = id;
            Name = name;
            myAddress = address;
            Paymentmethod = new HoldMethod();
        }

        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public Affiliation Affiliation { get; set; } = new NoAffiliation();

        private class NullEmployee : Employee
        {
            public NullEmployee(int id, string name, string address) : base(id, name, address)
            {
            }
        }

        public void CompletePaycheck(PayCheck payCheck)
        {
            payCheck.GrossPay = Classification.CalculatePay(payCheck);
            payCheck.Deductions = Affiliation.CalculateDeductions();
            payCheck.NetPay = payCheck.GrossPay - payCheck.Deductions;
            Paymentmethod.pay(payCheck);

        }
    }

}