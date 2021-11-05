using System;

namespace PayrollDomain
{
    public class Employee
    {

        public string Name;
        public string myAddress;
        public IPaymentMethod Paymentmethod;
        public readonly int myID; //employee should not know his database ID.
        public bool isNull => this == NULL;
        public static readonly Employee NULL = new NullEmployee(0,"name","nowhere");

        public Employee(int id, string name, string address)
        {
            myID = id;
            Name = name;
            myAddress = address;
            Paymentmethod = new HoldMethod();
        }


        public IPaymentClassification Classification { get; set; }
        public IPaymentSchedule Schedule { get; set; }
        public IAffiliation Affiliation { get; set; } = new NoAffiliation();

        private class NullEmployee : Employee
        {
            public NullEmployee(int id, string name, string address) : base(id, name, address)
            {
            }
        }

        public void CompletePaycheck(PayCheck payCheck)
        {
            payCheck.GrossPay = Classification.CalculatePay(payCheck);
            payCheck.Deductions = Affiliation.CalculateDeductions(payCheck);
            payCheck.NetPay = payCheck.GrossPay - payCheck.Deductions;
            Paymentmethod.Pay(payCheck);

        }

        public bool IsPayDate(DateTime payDate)
        {
            return Schedule.IsPayDate(payDate);
        }

        public DateTime GetPaydateStartPeriod(DateTime payDate)
        {
            return Schedule.GetStartDate(payDate);
        }

        public override string ToString()
        {
            return myID+":" +Name +","+myAddress ;
        }

    }

}