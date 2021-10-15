using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using PaymentClassification.ChangeClassification;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDataBase.Linq2SQL;
using PayrollDB;
using PayrollDomain;
using Schedules;
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
            db.Hourlies.DeleteAllOnSubmit(db.Hourlies.Select(h => h));
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
            var sched = GetSchedule(id, db, emp.ScheduleType);
            var classification = GetClassification(id, db, emp.PaymentClassificationType);
            var employee = emp.toEmployee();
            employee.Paymentmethod = pmethod;
            employee.Schedule = sched;
            employee.Classification = classification;

            return employee;
        }

        private PayrollDomain.PaymentClassification GetClassification(int id, EmployeeContext employeeContext,
            string empPaymentClassificationType)
        {
            if (empPaymentClassificationType == ClassificationCodes.Salary)
            {
                return new SalariedClassification(db.Salaries.Single(s => s.EmpID == id).Salary);
            }
            else if (empPaymentClassificationType == ClassificationCodes.Commision)
            {
                var c = db.Commsions.Single(commisionAdapter => commisionAdapter.EmpID == id);
                return new CommisionClassification(c.CommissionRate, c.Salary);
            }
            else if (empPaymentClassificationType == ClassificationCodes.Hourly)
            {
                return new HourlyClassification(db.Hourlies.Single(adapter => adapter.EmpID == id).HourlyRate);
            }

            throw new UnknownClassificationException("Failed to retrieve classification");
        }

        private PaymentSchedule GetSchedule(int i, EmployeeContext employeeContext, string empScheduleType)
        {
            if (empScheduleType == ScheduleCodes.Monthly)
            {
                return new MonthlyPaymentSchedule();
            }
            else if (empScheduleType == ScheduleCodes.Weekly)
            {
                return new WeeklySchedule();
            }
            else if (empScheduleType == ScheduleCodes.BiWeekly)
            {
                return new Biweekly();
            }

            throw new UnknownPaymentScheduleException("Could not retrieve the schudule codes");
        }
    }
}