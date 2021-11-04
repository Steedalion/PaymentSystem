using NUnit.Framework;
using PayrollDatabase;
using Presenter;
using WinFormsApp1;

namespace PayrollFormsTest
{
    public class WindowIntegrationTests
    {
        private FormsViewLoader viewloader;
        private InMemoryDB database;

        [SetUp]
        public void CreateViewLoader()
        {
            database = new InMemoryDB();
            viewloader = new FormsViewLoader(database);
        }

        [Test]
        public void TransactionContainerIsMaintained()
        {
            viewloader.LoadPayrollView();
            PayrollWindowForm payrollWindowForm = viewloader.LastLoadedView as PayrollWindowForm;
            var firstPresenter = (payrollWindowForm.Presenter as PayrollPresenter).TransactionContainer;

            payrollWindowForm.addEmployeeButton.PerformClick();
            AddEmployeeTransationForm addEmp = viewloader.LastLoadedView as AddEmployeeTransationForm;
            var secondPresenter = addEmp.Presenter.TransactionContainer;
            Assert.AreSame(firstPresenter, secondPresenter);
        }

        [Test]
        public void AfterAddingEmployeeViewShouldUpdate()
        {
            viewloader.LoadPayrollView();
            PayrollWindowForm payrollWindowForm = viewloader.LastLoadedView as PayrollWindowForm;
            payrollWindowForm.addEmployeeButton.PerformClick();
            AddEmployeeTransationForm addEmp = viewloader.LastLoadedView as AddEmployeeTransationForm;
            addEmp.EnabledPoppups = false;
            addEmp.nameBox.Text = "John";
            addEmp.addressBox.Text = "Home";
            addEmp.empIDTb.Text = "123";
            addEmp.hourlyRadioButton.Checked = true;
            addEmp.HourlyRateTextBox.Text = "100.0";
            addEmp.submitButton.PerformClick();
            payrollWindowForm.updateTextButton.PerformClick();
            Assert.IsTrue(payrollWindowForm.pendingTransactions.Items[0].ToString().Contains("John"));
        }
    }
}