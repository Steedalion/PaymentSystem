using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace PaymentClassification
{
    public class AddSalaryEmployee : AddEmployee
    {
        private double mySalary;


        public AddSalaryEmployee(PayrollDB.IPayrollDb database, int id, string name, string address, double mySalary) : base(database, id, name, address)
        {
            this.mySalary = mySalary;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override PaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }

        protected override PayrollDomain.PaymentClassification MakeClassification()
        {
            SalariedClassification sc = new SalariedClassification(mySalary);
            return sc;
        }
    }
}