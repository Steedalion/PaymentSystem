using PaymentClassification.PaymentClassifications;
using PayrollDB;
using PayrollDomain;
using Schedules;
using Transactions.DBTransaction;

namespace PaymentClassification
{
    public class AddCommissionedEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly double _salary;
        private readonly double _commisionRate;

        public AddCommissionedEmployeeTransaction(PayrollDB.IPayrollDb database, int id, string name, string address, double salary, double commisionRate) : base(database, id, name, address)
        {
            _salary = salary;
            _commisionRate = commisionRate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new Biweekly();
        }

        protected override PayrollDomain.PaymentClassification MakeClassification()
        {
            return new CommisionClassification(_commisionRate, _salary);
        }
    }

 
}