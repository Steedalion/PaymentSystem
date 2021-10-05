using System;
using System.Text;
using PayrollDB;
using PayrollDomain;
using Transactions;

namespace Presenters
{
    public class PayrollPresenter : IPayrollPresenter
    {
        // public IPayrollView PayrollView;
        public IPayrollView PayrollView { get; set; }
        public readonly IPayrollDb database;
        private readonly IViewLoader viewLoader;

        public PayrollPresenter(IPayrollView payrollView, IPayrollDb database, IViewLoader viewLoader)
        {
            this.PayrollView = payrollView;
            this.database = database;
            this.viewLoader = viewLoader;
            TransactionContainer = new TransactionContainer();
            TransactionContainer.OnAddExecute(UpdateTransactionText);
        }

        public TransactionContainer TransactionContainer { get; set; }

        public void UpdateTransactionText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (DbTransaction transaction in TransactionContainer.transactions)
            {
                builder.Append(transaction);
                builder.Append(Environment.NewLine);
            }

            PayrollView.TransactionText = builder.ToString();
        }

        public void AddEmployeeActionInvoked()
        {
            viewLoader.LoadAddEmployerView(TransactionContainer);
        }

        public void RunTransactions()
        {
            TransactionContainer.RunTransactions();
            UpdateTransactionText();
            UpdateEmployeeText();
        }

        public void UpdateEmployeeText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (int employeeId in database.GetEmployeeIds())
            {
                Employee employee = database.GetEmployee(employeeId);
                builder.Append(employee.ToString());
                builder.Append(Environment.NewLine);
            }
            PayrollView.EmployeeText = builder.ToString();
        }

        public void Start()
        {
            viewLoader.LoadPayrollView();
        }
    }
}