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
}