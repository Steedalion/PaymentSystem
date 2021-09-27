namespace Presenters
{
    public class MockViewLoader : IViewLoader
    {
        public bool addEmployeeViewWasLoaded;
        public bool parollyViewWasLoaded;


        public void LoadAddEmployerView(TransactionContainer transactionContainer)
        {
            addEmployeeViewWasLoaded = true;
        }

        public IView LoadPayrollView()
        {
             parollyViewWasLoaded = true;
             return null;
        }
    }
}