using System;
using PaymentClassification.PaymentClassifications;
using PayrollDatabase;
using PayrollDomains;
using Transaction;

namespace PaymentClassification
{
    public class AddSalesReceipt:DbTransaction
    {
        private int empid;
                private DateTime date;
        private double ammount;


        public AddSalesReceipt(IPayrollDb database, int empid, DateTime date, double ammount) : base(database)
        {
            this.empid = empid;
            this.date = date;
            this.ammount = ammount;
        }

        public override void Execute()
        {
            Employee e = database.GetEmployee(empid);
            CommisionClassification cc = e.Classification as CommisionClassification;
            cc.AddSalesReciept(date, ammount);
        }
    }
}