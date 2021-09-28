using NUnit.Framework;
using PayrollDB;

namespace Presenters
{
    [TestFixture]
    public class PayrollPresenterTestFixture
    {
        protected IPayrollView PayrollView;
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