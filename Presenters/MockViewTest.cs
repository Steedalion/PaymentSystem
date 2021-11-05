using NUnit.Framework;
using PayrollDB;

namespace Presenters
{
    public class MockViewTest:PayrollPresenterTestFixture
    {
         [SetUp]
        public void CreateSetup()
        {
            PayrollView = new MockPayrollPayrollView();
            database = new InMemoryDB();
            viewLoader = new MockViewLoader();
            presenter = new PayrollPresenter(PayrollView, database, viewLoader);
        }

        [Test]
        public void CreateNew()
        {
            PayrollView = new MockPayrollPayrollView();
        }

        [Test]
        public void SetPresenter()
        {
            PayrollView.Presenter=presenter;
        }

        [Test]
        public void FGetPresenter()
        {
            PayrollView.Presenter = presenter;
            Assert.AreSame(presenter,PayrollView.Presenter);
        }
    }
}