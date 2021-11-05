using System.Data.SQLite;
using System.Linq;
using PaymentClassifications.PaymentClassifications;
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
            var emp = new EmployeeUnit(id, employee);
            emp.PaymentClassificationType = ClassificationCodes.Code(employee.Classification);
            emp.ScheduleType = ScheduleCodes.Code(employee.Schedule);
            emp.PaymentMethodType = PaymentMethodCodes.Code(employee.Paymentmethod);
            db.Employees.InsertOnSubmit(emp);
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
                var salesReceipts = commisionClassification.GetAllSalesReceipts();
                    db.SalesReceipts.InsertAllOnSubmit(salesReceipts.Select(reciept => new SalesRecieptAdapter(id,reciept)));

            }

            else if (employee.Classification is HourlyClassification)
            {
                HourlyClassification hourly = employee.Classification as HourlyClassification;
                HourlyAdapter h = new HourlyAdapter(id, hourly.Rate);
                db.Hourlies.InsertOnSubmit(h);
                var timeCards = hourly.GetTimeCards();
                db.Timecards.InsertAllOnSubmit(timeCards.Select(t=> new TimecardAdapter(id,t)));
            }
            else
            {
                throw new UnknownClassificationException("Unknown classification: " +
                                                         employee.Classification.GetType());
            }
        }


        private void SavePaymentMethod(int id, Employee employee, EmployeeContext db)
        {
            IPaymentMethod method = employee.Paymentmethod;
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
}