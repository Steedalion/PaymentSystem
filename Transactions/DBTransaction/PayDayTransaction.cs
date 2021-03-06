using System;
using System.Collections.Generic;
using PayrollDomain;

namespace Transactions.DBTransaction
{
    public class PayDayTransaction : DatabaseTransaction
    {
        private Dictionary<int, PayCheck> paychecks = new Dictionary<int, PayCheck>();
        private DateTime payDate;

        public PayDayTransaction(PayrollDB.IPayrollDb database, DateTime payDate) : base(database)
        {
            this.payDate = payDate;
        }

        public override void Execute()
        {
            int[] allEmployees = Database.GetEmployeeIds();
            foreach (int id in allEmployees)
            {
                Employee employee = Database.GetEmployee(id);
                if (employee.IsPayDate(payDate))
                {
                    DateTime startDate = employee.GetPaydateStartPeriod(payDate);
                    PayCheck payCheck = new PayCheck(startDate,payDate);
                    paychecks[id] = payCheck;
                    employee.CompletePaycheck(payCheck);
                }
            }
        }

        public PayCheck GetPayCheck(int empId)
        {
            if (paychecks.ContainsKey(empId))
            {
                return paychecks[empId];
            }
            else
            {
                return null;
            }
        }
    }
}