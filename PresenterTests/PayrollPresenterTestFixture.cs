using NUnit.Framework;
using PayrollDatabase;
using Presenter;

namespace PresenterTests
{
    [TestFixture]
    public class PayrollPresenterTestFixture
    {
        protected MockPayrollPayrollView PayrollView;
        protected PayrollPresenter presenter;
        protected IPayrollDb database;
        protected MockViewLoader viewLoader;

        [SetUp]
        public void CreateSetup()
        {
            PayrollView = new MockPayrollPayrollView();
            database = new InMemoryDB();
            viewLoader = new MockViewLoader();
            presenter = new PayrollPresenter(PayrollView, database, viewLoader);
        }
    }
}