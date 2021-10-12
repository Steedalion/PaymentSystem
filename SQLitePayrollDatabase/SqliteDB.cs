using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using PayrollDataBase.Linq2SQL;
using PayrollDB;
using PayrollDomain;
using PaymentClassification = PayrollDomain.PaymentClassification;

namespace PayrollDataBase
{
    public class SqliteDB : IPayrollDb
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
            // db.Employees.InsertOnSubmit(EmployeeUnit.FromEmployee(id, employee));
            SaveEmployeeOperation saveEmployee = new SaveEmployeeOperation(id, employee, db);


            try
            {
                saveEmployee.Execute();
            }
            catch (DuplicateKeyException e)
            {
                Console.WriteLine(e);
                throw new EmployeeIdAlreadyExists();
            }
        }

        public void Clear()
        {
            foreach (EmployeeUnit employeeUnit in db.Employees)
            {
                RemoveEmployee(employeeUnit.EmpID);
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