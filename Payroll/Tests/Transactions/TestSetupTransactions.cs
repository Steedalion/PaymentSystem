using System;
using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    [TestFixture]
    public class TestSetupTransactions
    {
        protected int EmpId = 10;
        protected const string Name = "bob";
        protected const string Address = "home";
        protected const double Salary = 1000.00;
        protected const int MemberId = 112;
        protected DateTime OtherDate = new DateTime(2019, 8, 8);
        public const double HourlyRate = 20;

        [SetUp]
        public void ClearDataBase()
        {
            PayrollDB.Clear();
        }

        protected void AddSalariedEmployeeToDb()
        {
            AddSalaryEmployee t = new AddSalaryEmployee(EmpId, Name, Address, Salary);
            t.Execute();
        } 
        protected void AddHourlyEmployeeToDB()
        {
            AddHourlyEmployee addHourlyEmployee = new AddHourlyEmployee(EmpId, Name, Address,HourlyRate );
            addHourlyEmployee.Execute();
        }

       
    }
}