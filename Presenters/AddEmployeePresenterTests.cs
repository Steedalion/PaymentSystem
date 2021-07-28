using NUnit.Framework;
using PayrollDB;

namespace Presenters
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
            presenter = new AddEmployeePresenter(view, container, database);
        }

        [Test]
        public void Creation()
        {
            Assert.AreSame(presenter.container, container);
        }

        [Test]
        public void AliiInforIsCollected()
        {
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.EmpID = 1;
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
            presenter.isCommision = true;
            Assert.IsFalse(presenter.AllInfoCollected());
            presenter.CommisionRate = .50;
            presenter.CommisionSalary = 500.00;
            Assert.IsTrue(presenter.AllInfoCollected());
        }
    }

    public class AddEmployeePresenter
    {
        private readonly MockAddEmployee view;
        public readonly TransactionContainer container;
        private readonly IPayrollDb db;
        public int EmpID;
        public bool IsHourly;
        public bool IsSalary;
        public bool isCommision;

        public AddEmployeePresenter(MockAddEmployee view, TransactionContainer container, IPayrollDb database)
        {
            this.view = view;
            this.container = container;
            db = database;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public double HourlyRate { get; set; }
        public double Salary { get; set; }
        public double CommisionRate { get; set; }
        public double CommisionSalary { get; set; }

        public bool AllInfoCollected()
        {
            bool result = EmpID > 0;
            result &= Name != null;
            result &= Address != null;
            result &= IsHourly || IsSalary || isCommision;
            if (IsHourly)
            {
                result &= HourlyRate > 0;
            }

            if (IsSalary)
            {
                result &= Salary > 0;
            }

            if (isCommision)
            {
                result &= CommisionRate > 0;
                result &= CommisionSalary > 0;

            }

            return result;
        }
    }

    public class TransactionContainer
    {
    }

    public class MockAddEmployee
    {
    }
}