using System.Windows.Forms;
using NUnit.Framework;
using PayrollDB;

namespace WinFormTest
{
    [TestFixture]
    public class AddEmployeeWindowEnablingSubmiting : AddEmployeeWindowTestFix
    {
        [SetUp]
        public void Setup()
        {
            SetUpEnvironment();
            AddEmployeeWindowSetPresenterFields borrowTests = new AddEmployeeWindowSetPresenterFields();
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
}