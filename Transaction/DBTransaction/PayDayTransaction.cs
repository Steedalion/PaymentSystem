using System;
using System.Collections.Generic;
using Domain;
using PayrollDB;
using PayrollDomain;

namespace Transactions.DBTransaction
{
    public class PayDayTransaction : DbTransaction
    {
        private Dictionary<int, PayCheck> paychecks = new Dictionary<int, PayCheck>();
        private DateTime payDate;

        public PayDayTransaction(IPayrollDB database, DateTime payDate) : base(database)
        {
            this.payDate = payDate;
        }

        public override void Execute()
        {
            int[] allEmployees = database.GetEmployeeIds();
            foreach (int id in allEmployees)
            {
                    Employee employee = database.GetEmployee(id);
                if (employee.Schedule.IsPayDate(payDate))
                {
                    PayCheck payCheck = new PayCheck(payDate);
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