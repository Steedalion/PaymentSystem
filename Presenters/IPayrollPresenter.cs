namespace Presenters
{
    public interface IPayrollPresenter
    {
        TransactionContainer transactionContainer { get; set; }
        IPayrollView PayrollView { get; set; }
        void AddEmployeeActionInvoked();
        void RunTransactions();
  
    }
}