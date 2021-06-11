using System;

namespace Payroll.Tests
{
    public class AddSalesReceipt:DbTransaction
    {
        private int empid;
        private double ammount;
        private DateTime date;

        public AddSalesReceipt(int empid,  DateTime date, double ammount)
        {
            this.empid = empid;
            this.ammount = ammount;
            this.date = date;
        }

        public void Execute()
        {
            Employee e = PayrollDB.GetEmployee(empid);
            CommisionClassification cc = e.Classification as CommisionClassification;
            cc.AddSalesReciept(date, ammount);
        }
    }
}