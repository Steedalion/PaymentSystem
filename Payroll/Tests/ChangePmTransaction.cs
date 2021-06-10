namespace Payroll.Tests
{
    public class ChangePmTransaction : ChangeEmployeeTransaction
    {
        private PaymentMethod newPm;

        public ChangePmTransaction(int empId, PaymentMethod newPaymentMethod) : base(empId)
        {
            newPm = newPaymentMethod;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Paymentmethod = newPm;
        }
    }
}