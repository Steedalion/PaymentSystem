namespace Presenters
{
    public class MockPayrollView: IView
    {
        public string TransactionText;
        public string EmployeeText;
        private PayrollPresenter presenter;

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

    public interface IView
    {
        void SetPresenter(PayrollPresenter newPresenter);
        PayrollPresenter GetPresenter();
        void SetTransactionText(string transactionTable);
        string GetTransactionText();
        void SetEmployeeText(string employeeTable);
        string GetEmployeeText();
    }
}