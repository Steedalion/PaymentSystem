using System;
using NUnit.Framework;
using PayrollDB;
using PayrollDomain;
using Transactions;

namespace Presenters
{
    public class PayrollPresenterTest : PayrollPresenterTestFixture

    {
        [SetUp]
        public void ResetDB()
        {
            database = new InMemoryDB();
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
            Assert.AreSame(PayrollView, presenter.PayrollView);
            Assert.AreSame(database, presenter.database);
            Assert.IsNotNull(presenter.TransactionContainer);
        }

        [Test]
        public void AddAction()
        {
            TransactionContainer transactionContainer = presenter.TransactionContainer;
            DatabaseTransaction transaction = new MockTransaction(presenter.database);
            transactionContainer.Add(transaction);
            string expected = transaction.ToString() + Environment.NewLine;
            Assert.AreEqual(expected, PayrollView.TransactionText);
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
            presenter.TransactionContainer.Add(transaction);
            presenter.RunTransactions();
            Assert.IsTrue(transaction.wasExecuted);
            Assert.AreEqual("", PayrollView.TransactionText);
        }

        [Test]
        public void RunTransactionsUpdatesEmployeesList()
        {
            Employee employee = new Employee(123, "John", "123 forth street");
            database.AddEmployee(123, employee);
            presenter.RunTransactions();
            Assert.AreEqual(employee.ToString() + Environment.NewLine, PayrollView.EmployeeText);
        }

        [Test]
        public void RunTransactionContainerEmptiesIt()
        {
            Employee employee = new Employee(123, "John", "123 forth street");
            database.AddEmployee(123, employee);
            presenter.RunTransactions();
            Assert.IsTrue(presenter.TransactionContainer.Size() == 0);
            presenter.RunTransactions();
        }

        [Test]
        public void EmptyTransactionShouldDoNothing()
        {
            presenter.RunTransactions();
            Assert.AreEqual(0, database.GetEmployeeIds().Length);
        }

        [Test]
        public void RunTransactionTheSecondTimeShouldDoNothing()
        {
            Employee employee = new Employee(123, "John", "123 forth street");
            database.AddEmployee(123, employee);
            presenter.RunTransactions();
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
            presenter.RunTransactions();
            Assert.AreEqual(1, database.GetEmployeeIds().Length);
        }
    }
}