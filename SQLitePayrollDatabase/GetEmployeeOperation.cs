using System.Linq;
using PaymentClassifications.PaymentClassifications;
using PaymentMethods;
using PayrollDataBase.Linq2SQL;
using PayrollDomain;
using Schedules;

namespace PayrollDataBase
{
    public class GetEmployeeOperation
    {
        private EmployeeContext db;
        private int id;

        public GetEmployeeOperation(int empId, EmployeeContext dataBase)
        {
            id = empId;
            db = dataBase;
        }

        private IPaymentMethod GetPaymentMethod(int empId, EmployeeContext db, string empPaymentMethodType)
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

        private PayrollDomain.IPaymentClassification GetClassification(int id, EmployeeContext employeeContext,
            string empPaymentClassificationType)
        {
            if (empPaymentClassificationType == ClassificationCodes.Salary)
            {
                return new SalariedClassification(db.Salaries.Single(s => s.EmpID == id).Salary);
            }
            else if (empPaymentClassificationType == ClassificationCodes.Commision)
            {
                var c = db.Commsions.Single(commisionAdapter => commisionAdapter.EmpID == id);
                var cd = new CommisionClassification(c.CommissionRate, c.Salary);
                foreach (SalesRecieptAdapter salesRecieptAdapter in db.SalesReceipts.Where(sale => sale.EmpID == id))
                {
                    cd.AddSalesReciept(salesRecieptAdapter.Date, salesRecieptAdapter.Amount);
                }

                return cd;
            }
            else if (empPaymentClassificationType == ClassificationCodes.Hourly)
            {
                var hourly = new HourlyClassification(db.Hourlies.Single(adapter => adapter.EmpID == id).HourlyRate);
                foreach (TimecardAdapter timeCard in db.Timecards.Where(t => t.EmpID.Equals(id)))
                {
                    hourly.AddTimeCard(new TimeCard(timeCard.Date,timeCard.Hours));
                }

                return hourly;
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