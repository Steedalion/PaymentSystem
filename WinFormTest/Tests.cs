using System;
using NUnit.Framework;
using WindowsFormsUI;

namespace WinFormTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            AddEmployeeTransationForm window = new AddEmployeeTransationForm();
            window.empIDTb.Text = 123 + "";
        }
    }
}