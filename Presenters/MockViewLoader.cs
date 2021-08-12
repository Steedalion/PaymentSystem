namespace Presenters
{
    public class MockViewLoader : IViewLoader
    {
        public bool addEmployeeViewWasLoaded;

        public void LoadAddEmployerView()
        {
            addEmployeeViewWasLoaded = true;
        }
    }
}