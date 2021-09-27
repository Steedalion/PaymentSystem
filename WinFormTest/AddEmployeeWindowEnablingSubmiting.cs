using System.Windows.Forms;
using NUnit.Framework;
using PayrollDB;
using Presenters;
using WindowsFormsUI;

namespace WinFormTest
{
    [TestFixture]
    public class PayrollWindowTests
    {
        private PayrollWindowForm window;
        private MockParollPresenter presenter;

        [SetUp]
        public void Setup()
        {
            CreateWindow();
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

            Assert.AreEqual("abc, 123", window.pendingTransactions.Text);
        }
        [Test]
        public void SetEmployeeText()
        {
            window.EmployeeText = "John B Good";

            Assert.AreEqual("John B Good", window.employeesTextbox.Text);
        }

        [Test]
        public void AddEmployeeAction()
        {
            window.addEmployeeButton.PerformClick();
            Assert.IsTrue(presenter.addEmployeeActionInvoked);
        }
    }

    internal class MockParollPresenter
    {
        public bool addEmployeeActionInvoked;
    }

    [TestFixture]
    public class AddEmployeeWindowEnablingSubmiting : AddEmployeeWindowTestFix
    {
        [SetUp]
        public void Setup()
        {
            SetUpEnvironment();
            AddEmployeeWindowSetPresenterFields borrowTests = new AddEmployeeWindowSetPresenterFields();
            window.nameBox.Text = "Jonny";
            window.addressBox.Text = "Home";
            window.empIDTb.Text = "123";
        }

        [Test]
        public void HourlyEnableSubmitButton()
        {
            window.hourlyRadioButton.Checked = true;
            window.HourlyRateTextBox.Text = "10.00";
            Assert.True(SubmitButtonEnabled());
        }

        [Test]
        public void Salaried()
        {
            window.salariedRadioButton.Checked = true;
            window.SalariedSalaryTextBox.Text = "2000";
            Assert.True(SubmitButtonEnabled());
        }

        [Test]
        public void Commisioned()
        {
            window.commisionedRadioButton.Checked = true;
            window.CommisionedSalaryBox.Text = "100";
            window.CommsionedRateBox.Text = "0.5";
            Assert.True(SubmitButtonEnabled());
        }

        [Test]
        public void CommisionedSalaryOnly()
        {
            window.commisionedRadioButton.PerformClick();
            window.CommisionedSalaryBox.Text = "100.0";
            Assert.False(SubmitButtonEnabled());
        }

        [Test]
        public void CommisionedRateOnly()
        {
            window.commisionedRadioButton.PerformClick();
            window.CommsionedRateBox.Text = "0.5";
            Assert.False(SubmitButtonEnabled());
        }
    }
}