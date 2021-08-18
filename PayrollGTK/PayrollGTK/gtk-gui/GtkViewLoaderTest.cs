using Gtk;
using NUnit.Framework;
using PayrollDB;

namespace PayrollGTK.gtk_gui
{
    [TestFixture]
    public class GtkViewLoaderTest
    {
        private IPayrollDb database;
        private GtkViewLoader viewLoader;

        [SetUp]
        public void SetUp()
        {
            database = new InMemoryDB();
            viewLoader = new GtkViewLoader(database);
        }

        // [Test]
        public void LoadPayrollView()
        {
            viewLoader.LoadPayrollView();
            Window window = viewLoader.LastLoadedView();
        }

        // [Test]
        public void LoadEmployeeView()
        {
            viewLoader.LoadPayrollView();
        }
    }
}