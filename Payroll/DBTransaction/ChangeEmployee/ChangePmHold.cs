namespace Payroll.Tests
{
    public class ChangePmHold : ChangePmTransaction
    {
        public ChangePmHold(InMemoryDB database, int empId) : base(database, empId)
        {
        }

        protected override PaymentMethod SetPaymentMethod()
        {
            return new HoldMethod();
        }
    }
}