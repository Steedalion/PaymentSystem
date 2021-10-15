using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using PaymentMethods;
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
            db.SubmitChanges();
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

    public class GetEmployeeOperation
    {
        private EmployeeContext db;
        private int id;

        public GetEmployeeOperation(int empId, EmployeeContext dataBase)
        {
            id = empId;
            this.db = dataBase;
        }

        private PaymentMethod GetPaymentMethod(int empId, EmployeeContext db, string empPaymentMethodType)
        {
            if (empPaymentMethodType == PaymentMethodCodes.Hold)
            {
                return new HoldMethod();
            }
            else if (empPaymentMethodType == PaymentMethodCodes.Account)
            {
                var acc = db.DirectDepositAccounts.Single(account => account.EmpID.Equals(empId));
                return new AccountPaymentMethod(acc.Bank, acc.AccountNumber);
            }
            else if (empPaymentMethodType == PaymentMethodCodes.Mail)
            {
                var mail = db.PaycheckAddresses.Single(address => address.EmpID.Equals(empId));
                return new MailPaymentMethod(mail.Address);
            }

            throw new UnknownPaymentMethodExcpetion("Could not find the employees payment method");
        }

        public Employee Retrieve()
        {
            var emp = db.Employees.Single(unit => unit.EmpID.Equals(id));
            var pmethod = GetPaymentMethod(id, db, emp.PaymentMethodType);
            var employee = emp.toEmployee();
            employee.Paymentmethod = pmethod;
            return employee;
        }
    }
}