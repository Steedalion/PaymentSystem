using System;
using System.Data.SQLite;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDataBase.Linq2SQL;
using PayrollDomain;
using Schedules;

namespace PayrollDataBase
{
    public class SaveEmployeeOperation
    {
        private SQLiteConnection con;
        private int id;
        private Employee employee;
        private readonly EmployeeContext db;

        public SaveEmployeeOperation(int id, Employee employee, EmployeeContext db)
        {
            this.db = db;
            this.id = id;
            this.employee = employee;
        }

        public void Execute()
        {
            db.Employees.InsertOnSubmit(new EmployeeUnit(id, employee));
            SavePaymentMethod(id, employee, db);
            SavePaymentClassification(id, employee, db);
            SavePaymentSchedule(id, employee, db);
            db.SubmitChanges();
        }

        private void SavePaymentSchedule(int empId, Employee e, EmployeeContext db)
        {
            if (e.Schedule is WeeklySchedule)
            {
            }
            else if (e.Schedule is Biweekly)
            {
            }
            else if (e.Schedule is MonthlyPaymentSchedule)
            {
            }
            else
            {
                //todo these should be specific exceptions
                throw new UnknownPaymentScheduleException("Unknown Payment schedule" + e.Schedule.GetType());
            }
        }

        private void SavePaymentClassification(int id, Employee employee, EmployeeContext db)
        {
            if (employee.Classification is SalariedClassification)
            {
                SalariedClassification salary = employee.Classification as SalariedClassification;
                SalaryAdapter sal = new SalaryAdapter(id, salary.Salary);
                db.Salaries.InsertOnSubmit(sal);
            }
            else if (employee.Classification is CommisionClassification)
            {
                CommisionClassification commisionClassification = employee.Classification as CommisionClassification;
                CommisionAdapter com = new CommisionAdapter(id, commisionClassification.Salary,
                    commisionClassification.CommisionRate);
                db.Commsions.InsertOnSubmit(com);
            }

            else if (employee.Classification is HourlyClassification)
            {
                HourlyClassification hourly = employee.Classification as HourlyClassification;
                HourlyAdapter h = new HourlyAdapter(id, hourly.Rate);
                db.Hourlies.InsertOnSubmit(h);
            }
            else
            {
                throw new UnknownClassificationException("Unknown classification: " +
                                                         employee.Classification.GetType());
            }
        }


        private void SavePaymentMethod(int id, Employee employee, EmployeeContext db)
        {
            PaymentMethod method = employee.Paymentmethod;
            if (method is HoldMethod)
            {
                return;
            }

            if (method is AccountPaymentMethod)
            {
                AccountPaymentMethod acc = method as AccountPaymentMethod;
                db.DirectDepositAccounts.InsertOnSubmit(new Account(id, acc));
            }
            else if (method is MailPaymentMethod)
            {
                MailPaymentMethod mail = method as MailPaymentMethod;
                var pc = new MailAddress(id, mail);
                db.PaycheckAddresses.InsertOnSubmit(pc);
            }

            else
            {
                throw new UnknownPaymentMethodExcpetion("Unknown Payment methods:" + employee.Paymentmethod.GetType());
            }
        }
    }

    internal class UnknownPaymentMethodExcpetion : UnknownTypeException
    {
        public UnknownPaymentMethodExcpetion(string message) : base(message)
        {
        }
    }

    internal class UnknownClassificationException : UnknownTypeException
    {
        public UnknownClassificationException(string unknownClassification) : base(unknownClassification)
        {
        }
    }

    internal class UnknownTypeException : NotSupportedException
    {
        public UnknownTypeException(string message) : base(message)
        {
        }
    }

    internal class UnknownPaymentScheduleException : UnknownTypeException
    {
        public UnknownPaymentScheduleException(string message) : base(message)
        {
        }
    }
}