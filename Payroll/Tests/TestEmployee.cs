using NUnit.Framework;

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

        

    }
}