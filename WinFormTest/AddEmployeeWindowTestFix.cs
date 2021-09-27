using Presenters;
using WindowsFormsUI;

namespace WinFormTest
{
    public class AddEmployeeWindowTestFix
    {
        protected AddEmployeeTransationForm window;
        protected AddEmployeePresenter presenter;
        protected TransactionContainer transactionContainer;

        protected void SetUpEnvironment()
        {
            window = new AddEmployeeTransationForm();
            transactionContainer = new TransactionContainer();
            presenter = new AddEmployeePresenter(window, transactionContainer, null);
            window.Presenter = presenter;
            window.Visible = true;
        }

        protected bool SubmitButtonEnabled()
        {
            return window.submitButton.Enabled;
        }
    }
}