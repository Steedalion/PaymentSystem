using PayrollDomain.Payroll_Domain;

namespace Payroll.Tests.Transactions
{
    public class ChangeCommisionTransaction : ChangeEmployeeClassification
    {
        private double salary;
        private double commisionRate;

        public ChangeCommisionTransaction(IPayrollDB database, int empId, double salary, double commisionRate) : base(database, empId)
        {
            this.salary = salary;
            this.commisionRate = commisionRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new Biweekly();
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommisionClassification(commisionRate, salary);
        }
    }
}