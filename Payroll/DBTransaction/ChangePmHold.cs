namespace Payroll.Tests
{
    public class ChangePmHold : ChangePmTransaction
    {
        public ChangePmHold(int empId) : base(empId)
        {
        }

        protected override PaymentMethod SetPaymentMethod()
        {
            return new HoldMethod();
        }
    }
}