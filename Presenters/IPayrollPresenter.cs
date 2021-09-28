namespace Presenters
{
    public interface IPayrollPresenter
    {
        TransactionContainer transactionContainer { get; set; }
        void AddEmployeeActionInvoked();
        void RunTransactions();
  
    }
}