using System;
using NUnit.Framework;

namespace WinFormTest
{
    [TestFixture]
    public class AddEmployeeWindowSetPresenterFields : AddEmployeeWindowTestFix
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