using System;
using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    [TestFixture]
    public class TestSetupTransactions
    {
        protected InMemoryDB database = new InMemoryDB();
        protected int EmpId = 10;
        protected const string Name = "bob";
        protected const string Address = "home";
        protected const double Salary = 1000.00;
        protected const int MemberId = 112;
        protected DateTime OtherDate = new DateTime(2019, 8, 8);
        public const double HourlyRate = 20;
        public readonly float CommisionRate = 0.14f;

        [SetUp]
        public void ClearDataBase()
        {
            database.Clear();
        }

        protected void AddSalariedEmployeeToDb()
        {
            AddSalaryEmployee t = new AddSalaryEmployee(database,EmpId, Name, Address, Salary);
            t.Execute();
        } 
        protected void AddHourlyEmployeeToDB()
        {
            AddHourlyEmployee addHourlyEmployee = new AddHourlyEmployee(database,EmpId, Name, Address,HourlyRate );
            addHourlyEmployee.Execute();
        }

        protected void AddCommisionedEmployeeToDB()
        {
            // EmpId = 4;
            
            AddCommissionedEmployee t = new AddCommissionedEmployee(database,EmpId, Name, Address, Salary, CommisionRate);
            t.Execute();
        }

       
    }
}