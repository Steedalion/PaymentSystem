using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Payroll.Tests.Transactions
{
    public class TestPay : TestSetupTransactions
    {
    }

    public class TestPaySalaryEmployee : TestPay
    {
        [Test]
        public void MonthEndShouldPaySalaryEmployee()
        {
            AddSalariedEmployeeToDb();

            DateTime monthEnd = new DateTime(2001, 11, 30);
            PayDayTransaction payDay = new PayDayTransaction(monthEnd);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.AreEqual(Salary, payCheck.GrossPay, 0.001);
            Assert.AreEqual(monthEnd, payCheck.PayDate);
            Assert.AreEqual(0.0, payCheck.Deductions);
            Assert.AreEqual(Salary, payCheck.NetPay);
        }

        [Test]
        public void NotMonthEndShouldNotPaySalaryEmployee()
        {
            AddSalariedEmployeeToDb();

            DateTime notMonthEnd = new DateTime(2001, 11, 29);
            PayDayTransaction payDay = new PayDayTransaction(notMonthEnd);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);
            Assert.IsNull(payCheck);
        }
    }

    public class TestPayHourlyEmployee : TestPay
    {
        [Test]
        public void PayingHourlyWithNoTimeCards()
        {
            AddHourlyEmployeeToDB();
            DateTime friday = new DateTime(2001, 11, 9);
            PayDayTransaction payDay = new PayDayTransaction(friday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            ValidateHourlyPaycheck(payCheck, friday, 0);
        }

        private static void ValidateHourlyPaycheck(PayCheck payCheck, DateTime friday, double grossPay)
        {
            Assert.AreEqual(grossPay, payCheck.GrossPay, 0.001);
            Assert.AreEqual(friday, payCheck.PayDate);
            Assert.AreEqual(0.0, payCheck.Deductions);
            Assert.AreEqual(grossPay, payCheck.NetPay);
        }

        [Test]
        public void HourlyPaidWithOneCard()
        {
            AddHourlyEmployeeToDB();
            double hoursLogged = 8;


            DateTime friday = new DateTime(2001, 11, 9);
            AddTimeCard addTimeCard = new AddTimeCard(EmpId, friday, hoursLogged);
            addTimeCard.Execute();

            PayDayTransaction payDay = new PayDayTransaction(friday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.AreEqual(HourlyRate * hoursLogged, payCheck.GrossPay, 0.001);
            Assert.AreEqual(friday, payCheck.PayDate);
            Assert.AreEqual(0.0, payCheck.Deductions);
            Assert.AreEqual(HourlyRate * hoursLogged, payCheck.NetPay);
        }

        [Test]
        public void HourlyPaidWithOvertime()
        {
            AddHourlyEmployeeToDB();
            double hoursLogged = 12;
            double overtimePay = ((hoursLogged - 8) * 1.5 + 8) * HourlyRate;


            DateTime friday = new DateTime(2001, 11, 9);
            AddTimeCard addTimeCard = new AddTimeCard(EmpId, friday, hoursLogged);
            addTimeCard.Execute();

            PayDayTransaction payDay = new PayDayTransaction(friday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            
            Assert.AreEqual(overtimePay, payCheck.GrossPay, 0.001);
            Assert.AreEqual(friday, payCheck.PayDate);
            Assert.AreEqual(0.0, payCheck.Deductions);
            Assert.AreEqual(overtimePay, payCheck.NetPay,.001);
        }
        
        [Test]
        public void HourlyPaidWithOneCardAndOneNew()
        {
            AddHourlyEmployeeToDB();
            double hoursLogged = 8;


            DateTime friday = new DateTime(2001, 11, 9);
            AddTimeCard addTimeCard = new AddTimeCard(EmpId, friday.AddDays(-2), hoursLogged);
            addTimeCard.Execute();

            AddTimeCard aheadOfTime = new AddTimeCard(EmpId, friday.AddDays(8), hoursLogged);
            aheadOfTime.Execute();

            AddTimeCard oldTimeCard = new AddTimeCard(EmpId, friday.AddDays(-8), hoursLogged);
            oldTimeCard.Execute();

            PayDayTransaction payDay = new PayDayTransaction(friday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            Assert.AreEqual(HourlyRate * hoursLogged, payCheck.GrossPay, 0.001);
            Assert.AreEqual(friday, payCheck.PayDate);
            Assert.AreEqual(0.0, payCheck.Deductions);
            Assert.AreEqual(HourlyRate * hoursLogged, payCheck.NetPay);
        }

        [Test]
        public void HourlyShouldNotBePaidOldTimecard()
        {
            AddHourlyEmployeeToDB();
            double hoursLogged = 8;

            DateTime friday = new DateTime(2001, 11, 9);

            DateTime oldDate = friday.AddDays(-10);

            AddTimeCard addTimeCard = new AddTimeCard(EmpId, oldDate, hoursLogged);
            addTimeCard.Execute();

            PayDayTransaction payDay = new PayDayTransaction(friday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);

            ValidateHourlyPaycheck(payCheck, friday, 0);
        }

        [Test]
        public void NoPayOnOtherDays()
        {
            AddHourlyEmployeeToDB();

            DateTime notFriday = new DateTime(2001, 11, 10);
            PayDayTransaction payDay = new PayDayTransaction(notFriday);
            payDay.Execute();
            PayCheck payCheck = payDay.GetPayCheck(EmpId);
            Assert.IsNull(payCheck);
        }
    }

    public class PayDayTransaction : DbTransaction
    {
        private Dictionary<int, PayCheck> paychecks = new Dictionary<int, PayCheck>();
        private DateTime payDate;

        public PayDayTransaction(DateTime payDay)
        {
            payDate = payDay;
        }

        public void Execute()
        {
            int[] allEmployees = PayrollDB.GetEmployeeIds();
            foreach (int id in allEmployees)
            {
                Employee employee = PayrollDB.GetEmployee(id);
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

    public class PayCheck
    {
        public DateTime PayDate { get; set; }
        public double GrossPay { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }

        public PayCheck(DateTime payDate)
        {
            PayDate = payDate;
        }
    }
}