using NUnit.Framework;

namespace Presenters
{
    public class MockViewTest:PayrollPresenterTestFixture
    {

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