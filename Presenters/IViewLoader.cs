namespace Presenters
{
    public interface IViewLoader
    {
        void LoadAddEmployerView(TransactionContainer transactionContainer);
        void LoadPayrollView();
    }
}