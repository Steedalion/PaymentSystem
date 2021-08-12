using System;
using System.Text;
using NUnit.Framework;
using PayrollDB;
using PayrollDomain;
using Transactions;

namespace Presenters
{
    [TestFixture]
    public class PayrollPresenterTest
    {
        protected MockPayrollView view;
        protected PayrollPresenter presenter;
        protected IPayrollDb database;
        protected MockViewLoader viewLoader;

        [SetUp]
        public void CreateSetup()
        {
            view = new MockPayrollView();
            database = new InMemoryDB();
            viewLoader = new MockViewLoader();
            presenter = new PayrollPresenter(view, database, viewLoader);
        }

        [Test]
        public void Creation()
        {
            Assert.AreSame(view, presenter.view);
            Assert.AreSame(database, presenter.database);
            Assert.IsNotNull(presenter.transactionContainer);
        }

        [Test]
        public void AddAction()
        {
            TransactionContainer transactionContainer = presenter.transactionContainer;
            DbTransaction transaction = new MockTransaction(presenter.database);
            transactionContainer.Add(transaction);
            string expected = transaction.ToString() + Environment.NewLine;
            Assert.AreEqual(expected, view.transactionText);
        }

        [Test]
        public void AddEmployeeAction()
        {
            presenter.AddEmployeeActionInvoked();
            Assert.IsTrue(viewLoader.addEmployeeViewWasLoaded);
        }

        [Test]
        public void RunTransactionsUpdatesTransactionList()
        {
            MockTransaction transaction = new MockTransaction(database);
            presenter.transactionContainer.Add(transaction);
            presenter.RunTransactions();
            Assert.IsTrue(transaction.wasExecuted);
            Assert.AreEqual("", view.transactionText);
        }
        [Test]
        public void RunTransactionsUpdatesEmployeesList()
        {
            Employee employee = new Employee(123, "John","123 forth street");
            database.AddEmployee(123,employee);
            presenter.RunTransactions();
            Assert.AreEqual(employee.ToString() + Environment.NewLine, view.employeeText);
        }
        
    }

    public class MockTransaction : DbTransaction
    {
        public bool wasExecuted;

        public MockTransaction(IPayrollDb database) : base(database)
        {
            wasExecuted = true;
        }

        public override void Execute()
        {
        }
    }


    public class MockViewLoader : IViewLoader
    {
        public bool addEmployeeViewWasLoaded;

        public void LoadAddEmployerView()
        {
            addEmployeeViewWasLoaded = true;
        }
    }

    public class PayrollPresenter
    {
        public MockPayrollView view;
        public readonly IPayrollDb database;
        private readonly IViewLoader viewLoader;

        public PayrollPresenter(MockPayrollView view, IPayrollDb database, IViewLoader viewLoader)
        {
            this.view = view;
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

            view.transactionText = builder.ToString();
        }

        public void AddEmployeeActionInvoked()
        {
            viewLoader.LoadAddEmployerView();
        }

        public void RunTransactions()
        {
            transactionContainer.RunTransactions();
            UpdateTransactionText();
            UpdateEmployeeText();
        }

        private void UpdateEmployeeText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (int employeeId in database.GetEmployeeIds())
            {
                Employee employee = database.GetEmployee(employeeId);
                builder.Append(employee.ToString());
                builder.Append(Environment.NewLine);
            }
            view.employeeText = builder.ToString();
        }
    }

    public interface IViewLoader
    {
        void LoadAddEmployerView();
    }

    public class MockPayrollView
    {
        public string transactionText;
        public string employeeText;
    }
}