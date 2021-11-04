using Presenter;
using WinFormsApp1;

namespace PayrollFormsTest
{
    public class AddEmployeeWindowTestFix
    {
        protected AddEmployeeTransationForm window;
        protected AddEmployeePresenter presenter;
        protected TransactionContainer transactionContainer;

        protected void SetUpEnvironment()
        {
            window = new AddEmployeeTransationForm(false);
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