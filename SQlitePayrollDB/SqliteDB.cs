using Domain;
using Microsoft.Data.Sqlite;
using PayrollDB;
using PayrollDomain;

namespace PayrollDataBase
{
    public class SqliteDB : IPayrollDB
    {
        public static string connectionID = @"URI=file:/home/alex/RiderProjects/PaymentSystem/PayrollDB.sqlite";

        private SqliteConnection con;


        public Employee GetEmployee(int empId)
        {
            throw new System.NotImplementedException();
        }

        public void AddEmployee(int id, Employee employee)
        {
            SaveEmployeeOperation saveEmployeeOperation = new SaveEmployeeOperation(id, employee);
            saveEmployeeOperation.Execute();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEmployee(int id)
        {
            throw new System.NotImplementedException();
        }

        public void AddUnionMember(int memberId, int id)
        {
            throw new System.NotImplementedException();
        }

        public Employee GetUnionMember(int memberId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new System.NotImplementedException();
        }

        public int[] GetEmployeeIds()
        {
            throw new System.NotImplementedException();
        }
    }
}