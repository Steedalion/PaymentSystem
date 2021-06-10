using System;
using NUnit.Framework;

namespace Payroll.Tests
{
    [TestFixture]
    public class TestSetup
    {
        protected int empID = 1;
        protected string name = "bob";
        protected string address = "home";
        protected double salary = 1000.00;
        protected int memberId = 112;
        protected DateTime otherDate = new DateTime(2019, 8, 8);
        private readonly TestUseCases testUseCases;

        [SetUp]
        public void ClearDataBase()
        {
            PayrollDB.Clear();
        }

        public void AddSalariedEmployeeToDB()
        {
            AddSalaryEmployee t = new AddSalaryEmployee(empID, name, address, salary);
            t.Execute();
        } 
       
    }
}