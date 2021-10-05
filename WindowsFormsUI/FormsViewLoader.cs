using System.Windows.Forms;
using PayrollDB;
using Presenters;

namespace WindowsFormsUI
{
    public class FormsViewLoader : IViewLoader
    {
        private readonly InMemoryDB DB;

        public FormsViewLoader(InMemoryDB database)
        {
            DB = database;
        }

        public Form LastLoadedView { get; set; }

        public void LoadAddEmployerView(TransactionContainer transactionContainer)
        {
            AddEmployeeTransationForm window = new AddEmployeeTransationForm();
            window.Presenter = new AddEmployeePresenter(window,
                transactionContainer, DB);
            LoadView(window as Form);
        }

   

        public void LoadPayrollView()
        {
            PayrollWindowForm window = new PayrollWindowForm();
            window.Presenter = new PayrollPresenter(window, DB, this);
            LoadView(window as Form);
        }

        private void LoadView(Form window)
        {
            LastLoadedView = window;
            window.Show();
        }
    }
}