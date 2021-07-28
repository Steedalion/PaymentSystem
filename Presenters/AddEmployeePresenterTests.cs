using System;
using System.Data.Common;
using NUnit.Framework;
using PaymentClassification;
using PaymentClassification.PaymentClassifications;
using PayrollDB;
using Transactions.DBTransaction;

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
            container = new TransactionContainer();
            database = new InMemoryDB();
            view = new MockAddEmployee();
            presenter = new AddEmployeePresenter(view, container, database);
        }

        [Test]
        public void Creation()
        {
            Assert.AreSame(presenter.Container, container);
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
            presenter.isCommision = true;
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
            presenter.isCommision = true;
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
            presenter.isCommision = true;
            presenter.CommisionRate = 0.5;
            presenter.CommisionSalary = 100.00;
            Assert.IsTrue(presenter.CreateTransaction() is AddCommissionedEmployeeTransaction);
        }            
    }

    public class AddEmployeePresenter
    {
        private readonly MockAddEmployee view;
        public readonly TransactionContainer Container;
        private readonly IPayrollDb db;

        public int EmpId
        {
            get => empId;
            set
            {
                empId = value;
                UpdateView();
            }
        }

        public bool IsHourly
        {
            get => isHourly;
            set
            {
                isHourly = value;
                UpdateView();
            }
        }

        public bool IsSalary;
        public bool isCommision;
        private int empId;
        private bool isHourly;
        private string name;
        private string address;
        private double hourlyRate;

        private void UpdateView()
        {
            view.UpdateSubmitButton(AllInfoCollected());
        }

        public AddEmployeePresenter(MockAddEmployee view, TransactionContainer container, IPayrollDb database)
        {
            this.view = view;
            this.Container = container;
            db = database;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                UpdateView();
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                UpdateView();
            }
        }

        public double HourlyRate
        {
            get => hourlyRate;
            set
            {
                hourlyRate = value;
                UpdateView();
            }
        }

        public double Salary { get; set; }
        public double CommisionRate { get; set; }
        public double CommisionSalary { get; set; }

        public AddEmployeeTransaction CreateTransaction()
        {
            if (!AllInfoCollected())
            {
                throw new NotSupportedException();
            }

            if (IsHourly)
            {
                return new AddHourlyEmployeeTransaction(db, EmpId, Name, Address, HourlyRate);
            }

            if (IsSalary)
            {
                return new AddSalaryEmployeeTransaction(db, EmpId, Name, Address, Salary);
            }

            if (isCommision)
            {
                return new AddCommissionedEmployeeTransaction(db, EmpId, Name, Address, CommisionSalary,CommisionRate);
            }
            
             throw new NotSupportedException("Employee payment not defined.");
        }

        public bool AllInfoCollected()
        {
            bool result = EmpId > 0;
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
        public int Updates;
        public bool SubmitButtonEnabled;

        public void UpdateSubmitButton(bool allInfoCollected)
        {
            SubmitButtonEnabled = allInfoCollected;
            Updates++;
        }
    }
}