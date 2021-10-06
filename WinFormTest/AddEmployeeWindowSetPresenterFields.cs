using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace WinFormTest
{
    public class AddEmployeeWindowFiltersIncorrectFields : AddEmployeeWindowTestFix
    {
        [SetUp]
        public void Setup()
        {
            SetUpEnvironment();
        }

        [Test]
        public void EmployeeIDAsString()
        {
            window.Show();
            window.empIDTb.Text = "1a1";
            Assert.AreEqual("11", window.empIDTb.Text);
        }

        [Test]
        public void SetFieldsAsANameonly()
        {
            setunsetAll("a");
        }

        private void setunsetAll(string s)
        {
            SetUnsetTextBox(window.empIDTb, s);
            SetUnsetTextBox(window.addressBox, s);
            SetUnsetTextBox(window.nameBox, s);
            SetUnsetTextBox(window.CommsionedRateBox, s);
            SetUnsetTextBox(window.CommisionedSalaryBox, s);
            window.salariedRadioButton.Checked = true;
            SetUnsetTextBox(window.SalariedSalaryTextBox, s);
            window.hourlyRadioButton.Checked = true;
            SetUnsetTextBox(window.HourlyRateTextBox, s);
        }

        [Test]
        public void ClearAfterWriting()
        {
            window.Show();
            SetUnsetTextBox(window.empIDTb);
            SetUnsetTextBox(window.addressBox);
            SetUnsetTextBox(window.nameBox);
            window.commisionedRadioButton.Checked = true;
            SetUnsetTextBox(window.CommsionedRateBox, "1.0");
            SetUnsetTextBox(window.CommisionedSalaryBox, "1.0");
            window.salariedRadioButton.Checked = true;
            SetUnsetTextBox(window.SalariedSalaryTextBox, "1.0");
            window.hourlyRadioButton.Checked = true;
            SetUnsetTextBox(window.HourlyRateTextBox, "1.0");
        }


        private void SetUnsetTextBox(TextBox tex, string set = "1")
        {
            tex.Text = set;
            tex.Text = "";
        }
    }

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
            Assert.AreSame(presenter.TransactionContainer, transactionContainer);
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