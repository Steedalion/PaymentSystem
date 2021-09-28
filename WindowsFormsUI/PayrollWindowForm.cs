using System;
using System.Windows.Forms;
using Presenters;

namespace WindowsFormsUI
{
    public partial class PayrollWindowForm : Form, IPayrollView
    {
        public PayrollWindowForm()
        {
            InitializeComponent();
        }

        public string TransactionText
        {
            set => pendingTransactions.Text = value;
            get => pendingTransactions.Text;
        }

        public string EmployeeText
        {
            set => employeesTextbox.Text = value;
            get => employeesTextbox.Text;
        }

        public IPayrollPresenter Presenter { get; set; }


        private void runTransactions_Click(object sender, EventArgs e)
        {
            Presenter.RunTransactions();
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            Presenter.AddEmployeeActionInvoked();
        }
    }
}