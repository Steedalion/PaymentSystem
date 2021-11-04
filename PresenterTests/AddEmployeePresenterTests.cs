using NUnit.Framework;
using PaymentClassification;
using PayrollDatabase;
using Presenter;

namespace PresenterTests
{
    [TestFixture]
    public class AddEmployeePresenterTests
    {
        private MockAddEmployee view;
        private TransactionContainer container;
        private IPayrollDb database;
        AddEmployeePresenter presenter;

        [SetUp]
        public void CreateAddEmp()
        {
            container = new TransactionContainer();
            database = new InMemoryDB();
            view = new MockAddEmployee();
            presenter = new AddEmployeePresenter(view, container, database);
        }

        [Test]
        public void Creation()
        {
            Assert.AreSame(presenter.TransactionContainer, container);
        }

        [Test]
        public void AliiInforIsCollected()
        {
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.EmpId = 1;
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.Name = "Bob";
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.Address = "Home";
            Assert.IsFalse(presenter.AllInfoCollected());

            presenter.IsHourly = true;
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.HourlyRate = 10.00;
            Assert.IsTrue(presenter.AllInfoCollected());

            presenter.IsHourly = false;
            presenter.IsSalary = true;
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.Salary = 1000.00;
            Assert.IsTrue(presenter.AllInfoCollected());

            presenter.IsSalary = false;
            presenter.IsCommision = true;
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.CommisionRate = .50;
            presenter.CommisionSalary = 500.00;
            Assert.IsTrue(presenter.AllInfoCollected());
        }

        [Test]
        public void ViewGetsUpdated()
        {
            presenter.EmpId = 1;
            CheckViewSubmit(false, 1);
            presenter.Name = "Bob";
            CheckViewSubmit(false, 2);
            presenter.Address = "Home";
            CheckViewSubmit(false, 3);

            presenter.IsHourly = true;
            CheckViewSubmit(false, 4);
            presenter.HourlyRate = 100.0;
            CheckViewSubmit(true, 5);

            presenter.IsHourly = false;
            CheckViewSubmit(false, 6);
            presenter.IsSalary = true;
            CheckViewSubmit(false, 7);
            presenter.Salary = 500.00;
            CheckViewSubmit(true, 8);

            presenter.IsSalary = false;
            CheckViewSubmit(false, 9);
            presenter.IsCommision = true;
            CheckViewSubmit(false, 10);
            presenter.CommisionRate = 0.5;
            CheckViewSubmit(false, 11);
            presenter.CommisionSalary = 100.00;
            CheckViewSubmit(true, 12);
        }

        private void CheckViewSubmit(bool submitButtonEnabled, int updateCounter)
        {
            Assert.AreEqual(view.SubmitButtonEnabled, submitButtonEnabled);
            Assert.AreEqual(updateCounter, view.Updates);
        }

        [Test]
        public void CreateTransactionWithoutAllFields()
        { presenter.EmpId = 1;
            presenter.Name = "Bob";
            presenter.Address = "Home";
            Assert.Throws<InsufficientInformationToAddEmployee>(() => presenter.CreateTransaction());
        }
        [Test]
        public void CreateTransaction()
        {
            presenter.EmpId = 1;
            presenter.Name = "Bob";
            presenter.Address = "Home";

            presenter.IsHourly = true;
            presenter.HourlyRate = 100.0;
            
            Assert.IsTrue(presenter.CreateTransaction() is AddHourlyEmployeeTransaction);

            presenter.IsHourly = false;
            presenter.IsSalary = true;
            presenter.Salary = 500.00;
            Assert.IsTrue(presenter.CreateTransaction() is AddSalaryEmployeeTransaction);

            presenter.IsSalary = false;
            presenter.IsCommision = true;
            presenter.CommisionRate = 0.5;
            presenter.CommisionSalary = 100.00;
            Assert.IsTrue(presenter.CreateTransaction() is AddCommissionedEmployeeTransaction);
        }            
    }
}