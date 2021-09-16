using System;
using NUnit.Framework;
using Presenters;
using WindowsFormsUI;

namespace WinFormTest
{
    public class windowTests
    {
        protected AddEmployeeTransationForm window;
        protected AddEmployeePresenter presenter;
        protected TransactionContainer transactionContainer;

        protected void SetUpEnvironment()
        {
            window = new AddEmployeeTransationForm();
            transactionContainer = new TransactionContainer();
            presenter = new AddEmployeePresenter(window, transactionContainer, null);
            window.Presenter = presenter;
            window.Visible = true;
        }

        protected bool SubmitButtonEnabled()
        {
            return window.submitButton.Enabled;
        }
    }

    [TestFixture]
    public class UITests : windowTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpEnvironment();
        }

        [Test]
        public void HourlyButton()
        {
            window.hourlyRadioButton.Checked = true;
            Assert.False(window.HourlyRateTextBox.ReadOnly);
            Assert.True(window.SalariedSalaryTextBox.ReadOnly);
            Assert.True(window.CommsionedRateBox.ReadOnly);
            Assert.True(window.CommisionedSalaryBox.ReadOnly);
        }

        [Test]
        public void SalariedButton()
        {
            window.salariedRadioButton.Checked = true;
            Assert.True(window.HourlyRateTextBox.ReadOnly);
            Assert.False(window.SalariedSalaryTextBox.ReadOnly);
            Assert.True(window.CommsionedRateBox.ReadOnly);
            Assert.True(window.CommisionedSalaryBox.ReadOnly);
        }

        [Test]
        public void CommisionedRadio()
        {
            window.commisionedRadioButton.Checked = true;
            Assert.True(window.HourlyRateTextBox.ReadOnly);
            Assert.True(window.SalariedSalaryTextBox.ReadOnly);
            Assert.False(window.CommsionedRateBox.ReadOnly);
            Assert.False(window.CommisionedSalaryBox.ReadOnly);
        }

        [Test]
        public void AddEmployee()
        {
            Assert.False(window.submitButton.Enabled);
            window.nameBox.Text = "Jonny";
            window.addressBox.Text = "Home";
            window.empIDTb.Text = "123";
            window.salariedRadioButton.Checked = true;
            window.SalariedSalaryTextBox.Text = "100";
            Assert.IsTrue(window.submitButton.Enabled);
            window.submitButton.PerformClick();
            Assert.IsFalse(window.Visible);
            Assert.AreEqual(1, transactionContainer.Size());
        }
    }

    [TestFixture]
    public class EnableSubmitingWhenAllFieldsAvailable : windowTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpEnvironment();
            PresenterFieldsAreSet borrowTests = new PresenterFieldsAreSet();
            window.nameBox.Text = "Jonny";
            window.addressBox.Text = "Home";
            window.empIDTb.Text = "123";
        }

        [Test]
        public void HourlyEnableSubmitButton()
        {
            window.hourlyRadioButton.Checked = true;
            window.HourlyRateTextBox.Text = "10.00";
            Assert.True(SubmitButtonEnabled());
        }

        [Test]
        public void Salaried()
        {
            window.salariedRadioButton.Checked = true;
            window.SalariedSalaryTextBox.Text = "2000";
            Assert.True(SubmitButtonEnabled());
        }

        [Test]
        public void Commisioned()
        {
            window.commisionedRadioButton.Checked = true;
            window.CommisionedSalaryBox.Text = "100";
            window.CommsionedRateBox.Text = "0.5";
            Assert.True(SubmitButtonEnabled());
        }

        [Test]
        public void CommisionedSalaryOnly()
        {
            window.commisionedRadioButton.PerformClick();
            window.CommisionedSalaryBox.Text = "100.0";
            Assert.False(SubmitButtonEnabled());
        }

        [Test]
        public void CommisionedRateOnly()
        {
            window.commisionedRadioButton.PerformClick();
            window.CommsionedRateBox.Text = "0.5";
            Assert.False(SubmitButtonEnabled());
        }
    }

    [TestFixture]
    public class PresenterFieldsAreSet : windowTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpEnvironment();
        }


        [Test]
        public void Starting()
        {
            Assert.AreSame(presenter, window.Presenter);
            Assert.AreSame(presenter.Container, transactionContainer);
            Assert.IsFalse(window.submitButton.Enabled);
        }

        [Test]
        public void EmpID()
        {
            window.empIDTb.Text = "123";
            Assert.AreEqual(123, presenter.EmpId);
            Assert.False(window.submitButton.Enabled);
        }

        [Test]
        public void Addresss()
        {
            window.addressBox.Text = "Home";
            Assert.AreEqual("Home", presenter.Address);
            Assert.False(window.submitButton.Enabled);
        }

        [Test]
        public void Name()
        {
            window.nameBox.Text = "John";
            Assert.AreEqual("John", presenter.Name);
            Assert.False(window.submitButton.Enabled);
        }


        [Test]
        public void Commisioned()
        {
            window.commisionedRadioButton.Checked = true;
            Assert.IsFalse(presenter.IsHourly);
            Assert.IsFalse(presenter.IsSalary);
            Assert.IsTrue(presenter.IsCommision);

            window.CommsionedRateBox.Text = "0.3";
            Assert.AreEqual(0.3, presenter.CommisionRate);
            window.CommisionedSalaryBox.Text = "100";
            Assert.AreEqual(100, presenter.CommisionSalary);
        }

        [Test]
        public void Salaried()
        {
            window.salariedRadioButton.Checked = true;
            Assert.IsFalse(presenter.IsHourly);
            Assert.IsTrue(presenter.IsSalary);
            Assert.IsFalse(presenter.IsCommision);
        }

        [Test]
        public void Hourly()
        {
            window.hourlyRadioButton.Checked = true;
            Assert.IsTrue(presenter.IsHourly);
            Assert.IsFalse(presenter.IsSalary);
            Assert.IsFalse(presenter.IsCommision);
        }
    }
}