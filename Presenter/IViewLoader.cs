namespace Presenter
{
    public interface IViewLoader
    {
        void LoadAddEmployerView(TransactionContainer transactionContainer);
        void LoadPayrollView();
    }
}