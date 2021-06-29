namespace Payroll.Tests
{
    public class ChangePmMail : ChangePmTransaction
    {
        private string address;

        public ChangePmMail(PayrollDB database, int empId, string address) : base(database, empId)
        {
            this.address = address;
        }


        protected override PaymentMethod SetPaymentMethod()
        {
            return new MailPaymentMethod(address);
        }
    }
}