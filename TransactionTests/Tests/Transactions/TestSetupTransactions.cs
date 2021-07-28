using System;
using NUnit.Framework;
using PaymentClassification;
using PayrollDB;

namespace Payroll.Tests.Transactions
{
    [TestFixture]
    public class TestSetupTransactions
    {
        protected PayrollDB.IPayrollDb database = new InMemoryDB();
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
            AddSalaryEmployeeTransaction t = new AddSalaryEmployeeTransaction(database,EmpId, Name, Address, Salary);
            t.Execute();
        } 
        protected void AddHourlyEmployeeToDB()
        {
            AddHourlyEmployeeTransaction addHourlyEmployeeTransaction = new AddHourlyEmployeeTransaction(database,EmpId, Name, Address,HourlyRate );
            addHourlyEmployeeTransaction.Execute();
        }

        protected void AddCommisionedEmployeeToDB()
        {
            // EmpId = 4;
            
            AddCommissionedEmployeeTransaction t = new AddCommissionedEmployeeTransaction(database,EmpId, Name, Address, Salary, CommisionRate);
            t.Execute();
        }

       
    }
}