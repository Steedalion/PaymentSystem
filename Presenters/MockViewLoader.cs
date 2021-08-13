namespace Presenters
{
    public class MockViewLoader : IViewLoader
    {
        public bool addEmployeeViewWasLoaded;
        public bool parollyViewWasLoaded;

        public void LoadAddEmployerView()
        {
            addEmployeeViewWasLoaded = true;
        }

        public void LoadPayrollView()
        {
             parollyViewWasLoaded = true;
        }
    }
}