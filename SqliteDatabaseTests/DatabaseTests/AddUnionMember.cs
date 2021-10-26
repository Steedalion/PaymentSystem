using NUnit.Framework;

namespace DatabaseTests.DatabaseTests
{
    public class AddUnionMember:AddEmployeeTest
    {
        [Test]
        public void AddSimpleEmployee()
        {
            var e = An.GenericEmployee;
            database.AddEmployee(100,e);
            database.AddUnionMember(1,100);
            var u = database.GetUnionMember(1);
            Assert.AreEqual(e,u);
        }
        
        [Test]
        public void EmployeeDoesNotExist()
        {
            database.AddUnionMember(1, 1);
        }

        [Test]
        public void GetNonExistingUMember()
        {
            var u=    database.GetUnionMember(1);
            Assert.NotNull(u);
        }
        
        
    }
}