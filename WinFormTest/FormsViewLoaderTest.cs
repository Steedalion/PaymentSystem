using NUnit.Framework;
using WindowsFormsUI;

namespace WinFormTest
{
    [TestFixture]
    public class FormsViewLoaderTest
    {
        private FormsViewLoader viewloader;

        [Test]
        public void LoadPayrollView()
        {
            viewloader.LoadPayrollView();
        }
    }
}