namespace Presenters
{
    public interface IPayrollPresenter
    {
        IPayrollView PayrollView { get; set; }
        void AddEmployeeActionInvoked();
        void RunTransactions();
  
    }
}