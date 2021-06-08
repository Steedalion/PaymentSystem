using System;

namespace Payroll
{
    public class Employee
    {
        public string Name;
        public string myAddress;
        public PaymentMethod Paymentmethod;
        private readonly int myID;

        public Employee(int id, string name, string address)
        {
            myID = id;
            this.Name = name;
            myAddress = address;
        }

        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
    }
}