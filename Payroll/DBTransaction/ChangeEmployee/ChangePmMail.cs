namespace Payroll.Tests
{
    public class ChangePmMail : ChangePmTransaction
    {
        private string address;

        public ChangePmMail(int empId, string address) : base(empId)
        {
            this.address = address;
        }

        protected override PaymentMethod SetPaymentMethod()
        {
            return new MailPaymentMethod(address);
        }
    }
}