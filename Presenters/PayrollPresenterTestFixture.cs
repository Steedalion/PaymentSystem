using NUnit.Framework;
using PayrollDB;

namespace Presenters
{
    [TestFixture]
    public class PayrollPresenterTestFixture
    {
        protected MockPayrollPayrollView PayrollView;
        protected PayrollPresenter presenter;
        protected IPayrollDb database;
        protected MockViewLoader viewLoader;

    }
}