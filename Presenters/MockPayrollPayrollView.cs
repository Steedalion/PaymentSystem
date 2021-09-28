namespace Presenters
{
    public class MockPayrollPayrollView : IPayrollView
    {
        public string TransactionText { get; set; }
        public string EmployeeText { get; set; }
        private PayrollPresenter presenter;

        public IPayrollPresenter Presenter { get; set; }

        public void SetPresenter(PayrollPresenter newPresenter)
        {
            presenter = newPresenter;
        }

        public PayrollPresenter GetPresenter()
        {
            return presenter;
        }

        public void SetTransactionText(string transactionTable)
        {
            TransactionText = transactionTable;
        }

        public string GetTransactionText()
        {
            return TransactionText;
        }

        public void SetEmployeeText(string employeeTable)
        {
            EmployeeText = employeeTable;
        }

        public string GetEmployeeText()
        {
            return EmployeeText;
        }
    }
}