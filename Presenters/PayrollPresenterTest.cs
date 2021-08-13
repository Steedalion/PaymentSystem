using System;
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
        public void StartCreatesAPayrollView()
        {
            presenter.Start();
            Assert.IsTrue(viewLoader.parollyViewWasLoaded);
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
}