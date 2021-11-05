using System;
using System.Linq;
using System.Windows.Forms;
using Presenters;

namespace WindowsFormsUI
{
    public partial class PayrollWindowForm : Form, IPayrollView
    {
        public PayrollWindowForm()
        {
            InitializeComponent();
            Closed += (sender, args) => { Application.Exit(); };
        }

        public string TransactionText
        {
            set
            {
                pendingTransactions.Items.Clear();
                foreach (string entry in value.Split(Environment.NewLine.ToCharArray()).Where(s => s!="" ))
                {
                    pendingTransactions.Items.Add(entry);
                }
            }
            get => (string)pendingTransactions.Items[0];
        }

        public string EmployeeText
        {
            set
            {
                employeesTextbox.Items.Clear();
                foreach (string s in value.Split(Environment.NewLine.ToCharArray()).Where(s => s!=""))
                {
                    employeesTextbox.Items.Add(s);
                }
            }
            get => (string)employeesTextbox.Items[0];
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

        private void UpdateTextBottonClicked(object sender, EventArgs e)
        {
            Presenter.UpdateTransactionText();
        }
    }
}