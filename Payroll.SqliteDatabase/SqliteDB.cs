using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using PayrollDatabase;
using PayrollDataBase.Linq2SQL;
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
            db.UnionMember.DeleteAllOnSubmit(db.UnionMember.Select(h => h));
            db.Timecards.DeleteAllOnSubmit(db.Timecards.Select(h => h));
            db.SalesReceipts.DeleteAllOnSubmit(db.SalesReceipts.Select(h => h));
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

        public void AddUnionMember(int memberId, int empID)
        {
            if (!db.Employees.Select(unit => unit.EmpID).Contains(empID))
            {
                throw new EmployeeNotFound();
            }

            db.UnionMember.InsertOnSubmit(new UnionAdapter(empID, memberId));
            db.SubmitChanges();
        }

        public Employee GetUnionMember(int memberId)
        {
            try
            {
                var empId = db.UnionMember.Single(m => m.MemberID.Equals(memberId)).EmpID;
                return GetEmployee(empId);
            }
            catch (InvalidOperationException e)
            {
                throw new UnionMemberNotFound();
            }
        }

        public void RemoveUnionMember(int memberId)
        {
            var contains = db.UnionMember.Select(m => m.MemberID).Contains(memberId);
            if (!contains)
            {
                throw new UnionMemberNotFound();
            }

            db.UnionMember.DeleteOnSubmit(db.UnionMember.Single(u => u.MemberID.Equals(memberId)));
            db.SubmitChanges();
        }

        public int[] GetEmployeeIds()
        {
            return db.Employees.Select(emp => emp.EmpID).ToArray();
        }
    }
}