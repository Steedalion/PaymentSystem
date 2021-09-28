using System;
using System.Text;
using PayrollDB;
using PayrollDomain;
using Transactions;

namespace Presenters
{
    public class PayrollPresenter : IPayrollPresenter
    {
        public IPayrollView PayrollView;
        public readonly IPayrollDb database;
        private readonly IViewLoader viewLoader;

        public PayrollPresenter(IPayrollView payrollView, IPayrollDb database, IViewLoader viewLoader)
        {
            this.PayrollView = payrollView;
            this.database = database;
            this.viewLoader = viewLoader;
            transactionContainer = new TransactionContainer();
            transactionContainer.OnAddExecute(UpdateTransactionText);
        }

        public TransactionContainer transactionContainer { get; set; }

        public void UpdateTransactionText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (DbTransaction transaction in transactionContainer.transactions)
            {
                builder.Append(transaction.ToString());
                builder.Append(Environment.NewLine);
            }

            PayrollView.TransactionText = builder.ToString();
        }

        public void AddEmployeeActionInvoked()
        {
            viewLoader.LoadAddEmployerView(transactionContainer);
        }

        public void RunTransactions()
        {
            transactionContainer.RunTransactions();
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