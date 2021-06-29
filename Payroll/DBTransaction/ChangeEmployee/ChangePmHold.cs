namespace Payroll.Tests
{
    public class ChangePmHold : ChangePmTransaction
    {
        public ChangePmHold(PayrollDB database, int empId) : base(database, empId)
        {
        }

        protected override PaymentMethod SetPaymentMethod()
        {
            return new HoldMethod();
        }
    }
}