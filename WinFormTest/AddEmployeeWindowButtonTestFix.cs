using NUnit.Framework;

namespace WinFormTest
{
    [TestFixture]
    public class AddEmployeeWindowButtonTestFix : AddEmployeeWindowTestFix
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
        public void PerformAddEmployee()
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
}