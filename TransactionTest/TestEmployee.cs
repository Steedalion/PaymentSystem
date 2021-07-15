using Domain;
using NUnit.Framework;
using PayrollDomain;

namespace Payroll.Tests
{
    public class EmployeeTest
    {
        private int id = 1;
        private string name = "Tester";
        private string address = "his House";
        private Employee e;

        [SetUp]
        public void CreatEmployee()
        {
            e = new Employee(id, name, address);
        }

        [Test]
        public void CreatedEmployeeIsnNotNull()
        {
            Assert.IsNotNull(e);
            Assert.False(e.isNull);
        }

        [Test]
        public void DefaultPaymentMethodIsHold()
        {
            Assert.IsTrue(e.Paymentmethod is HoldMethod);
        }
        

    }
}