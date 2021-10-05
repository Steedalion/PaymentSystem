using Presenters;

namespace WinFormTest
{
    internal class MockPayrollPresenter: IPayrollPresenter
    {
        public bool addEmployeeActionInvoked;
        public bool runTransactionsInvoked;
        public bool transactionTextUpdated;
        public IPayrollView PayrollView { get; set; }

        public void AddEmployeeActionInvoked()
        {
            addEmployeeActionInvoked = true;
        }

        public void RunTransactions()
        {
            runTransactionsInvoked = true;
        }

        public void UpdateTransactionText()
        {
            transactionTextUpdated = true;
        }
    }
}