using PaymentClassification.PaymentClassifications;
using PayrollDatabase;
using PayrollDomains;
using Schedule;
using Transaction.DBTransaction;

namespace PaymentClassification
{
    public class AddSalaryEmployeeTransaction : AddEmployeeTransaction
    {
        private double mySalary;


        public AddSalaryEmployeeTransaction(IPayrollDb database, int id, string name, string address, double mySalary) : base(database, id, name, address)
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

        protected override PayrollDomains.PaymentClassification MakeClassification()
        {
            SalariedClassification sc = new SalariedClassification(mySalary);
            return sc;
        }
    }
}