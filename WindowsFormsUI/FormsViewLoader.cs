using Presenters;

namespace WindowsFormsUI
{
    public class FormsViewLoader:IViewLoader
    {
        public void LoadAddEmployerView(TransactionContainer transactionContainer)
        {
            throw new System.NotImplementedException();
        }

        IPayrollView IViewLoader.LoadPayrollView()
        {
            throw new System.NotImplementedException();
        }

        public void LoadPayrollView()
        {
            throw new System.NotImplementedException();
        }
    }
}