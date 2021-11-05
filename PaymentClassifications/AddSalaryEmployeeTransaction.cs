using PaymentClassifications.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace PaymentClassifications
{
    public class AddSalaryEmployeeTransaction : AddEmployeeTransaction
    {
        private double mySalary;


        public AddSalaryEmployeeTransaction(PayrollDB.IPayrollDb database, int id, string name, string address, double mySalary) : base(database, id, name, address)
        {
            this.mySalary = mySalary;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }

        protected override IPaymentMethod MakePaymentMethod()
        {
            return new HoldMethod();
        }

        protected override PayrollDomain.IPaymentClassification MakeClassification()
        {
            SalariedClassification sc = new SalariedClassification(mySalary);
            return sc;
        }
    }
}