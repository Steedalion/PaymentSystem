using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using DatabaseTests.SQLiteTests;
using PayrollDB;
using PayrollDomain;

namespace PayrollDataBase
{
    public class SqliteDB : PayrollDB.IPayrollDb
    {
        public static string connectionID = "Data Source=" +
                                            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName +
                                            @"\PayrollDB.sqlite";

        private SQLiteConnection con = new SQLiteConnection(connectionID);
        private EmployeeContext db;

        public SqliteDB()
        {
            db = new EmployeeContext(con);
        }

        public Employee GetEmployee(int empId)
        {
            Employee emp = db.Employees.Single(unit => unit.EmpID == empId).toEmployee();
            return emp;
        }

        public void AddEmployee(int id, Employee employee)
        {
            db.Employees.InsertOnSubmit(EmployeeUnit.FromEmployee(id, employee));

            try
            {
                db.SubmitChanges();

            }
            catch (DuplicateKeyException e)
            {
                Console.WriteLine(e);
                throw new EmployeeIdAlreadyExists();
            }
        }

        public void Clear()
        {
            foreach (int id in GetEmployeeIds())
            {
                RemoveEmployee(id);
            }
        }

        public void RemoveEmployee(int id)
        {
            db.Employees.DeleteOnSubmit(db.Employees.Single(e => e.EmpID == id));
            db.SubmitChanges();
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
            return db.Employees.Select(emp => emp.EmpID).ToArray();
        }
    }
}