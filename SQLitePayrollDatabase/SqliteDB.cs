using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using PayrollDataBase.Linq2SQL;
using PayrollDB;
using PayrollDomain;

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
            GetEmployeeOperation getEmp = new GetEmployeeOperation(empId, db);
            Employee emp = getEmp.Retrieve();
            return emp;
        }


        public void AddEmployee(int id, Employee employee)
        {
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
            db.Employees.DeleteAllOnSubmit(db.Employees);
            db.Commsions.DeleteAllOnSubmit(db.Commsions.Select(c => c));
            db.Salaries.DeleteAllOnSubmit(db.Salaries.Select(s => s));
            db.DirectDepositAccounts.DeleteAllOnSubmit(db.DirectDepositAccounts.Select(s => s));
            db.PaycheckAddresses.DeleteAllOnSubmit(db.PaycheckAddresses.Select(a => a));
            db.DirectDepositAccounts.DeleteAllOnSubmit(db.DirectDepositAccounts.Select(d => d));
            db.Hourlies.DeleteAllOnSubmit(db.Hourlies.Select(h => h));
            db.SubmitChanges();
        }

        public void RemoveEmployee(int id)
        {
            db.Employees.DeleteOnSubmit(db.Employees.Single(e => e.EmpID == id));
            db.Commsions.DeleteAllOnSubmit(db.Commsions.Where(e => e.EmpID == id));
            db.Salaries.DeleteAllOnSubmit(db.Salaries.Where(e => e.EmpID == id));
            db.DirectDepositAccounts.DeleteAllOnSubmit(db.DirectDepositAccounts.Where(e => e.EmpID == id));
            db.PaycheckAddresses.DeleteAllOnSubmit(db.PaycheckAddresses.Where(e => e.EmpID == id));
            db.Hourlies.DeleteAllOnSubmit(db.Hourlies.Where(e => e.EmpID == id));
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