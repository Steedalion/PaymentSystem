using NUnit.Framework;
using WindowsFormsUI;

namespace PayrollFormsTest
{
    [TestFixture]
    public class PayrollWindowTests
    {
        private PayrollWindowForm window;
        private MockPayrollPresenter presenter;

        [SetUp]
        public void Setup()
        {
            CreateWindow();
            presenter = new MockPayrollPresenter();
            window.Presenter = presenter;
            window.Show();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownWindow();
        }

        public void TearDownWindow()
        {
            window.Dispose();
        }

        public void CreateWindow()
        {
            window = new PayrollWindowForm();
        }

        [Test]
        public void SetTransactionText()
        {
            window.TransactionText = "abc, 123";

            Assert.AreEqual("abc, 123", window.pendingTransactions.Items[0]);
        }

        [Test]
        public void SetEmployeeText()
        {
            window.EmployeeText = "John B Good";

            Assert.AreEqual("John B Good", window.employeesTextbox.Items[0]);
        }

        [Test]
        public void AddEmployeeAction()
        {
            window.addEmployeeButton.PerformClick();
            Assert.IsTrue(presenter.addEmployeeActionInvoked);
        }

        [Test]
        public void RuningTransactionsFromUI()
        {
            window.runTransactions.PerformClick();
            Assert.True(presenter.runTransactionsInvoked);
        }

        [Test]
        public void CloseWindowShouldEndProcess()
        {
            window.Close();
            Assert.Fail("Need to check for window closed.");
        }
    }
}