using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;

namespace PaymentClassification.ChangeClassification
{
    public class ChangeCommisionTransaction : ChangeEmployeeClassification
    {
        private double salary;
        private double commisionRate;

        public ChangeCommisionTransaction(PayrollDB.IPayrollDb database, int empId, double salary, double commisionRate) : base(database, empId)
        {
            this.salary = salary;
            this.commisionRate = commisionRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new Biweekly();
        }

        protected override PayrollDomain.PaymentClassification MakeClassification()
        {
            return new CommisionClassification(commisionRate, salary);
        }
    }
}