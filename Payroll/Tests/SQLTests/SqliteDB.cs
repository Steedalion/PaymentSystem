using System;
using Mono.Data.Sqlite;

namespace Payroll.Tests
{
    public class SqliteDB : IPayrollDB
    {
        public static string connectionID = @"URI=file:/home/alex/RiderProjects/PaymentSystem/PayrollDB.sqlite";

        private SqliteConnection con;

        public Employee GetEmployee(int empId)
        {
            throw new System.NotImplementedException();
        }

        public void AddEmployee(int id, Employee employee)
        {
            con = new SqliteConnection(SqliteDB.connectionID);
            con.Open();

            SqliteTransaction transaction = con.BeginTransaction();

            string sql = "INSERT INTO Employee VALUES("
                         + "@EmpID"
                         + ",@Name"
                         + ",@Address"
                         + ",@ScheduleType"
                         + ",@PaymentMethodType"
                         + ",@PaymentClassificationType"
                         + ")";

            var command = new SqliteCommand(sql, con);
            var cmd = new SqliteCommand(command.CommandText, con);
            cmd.Parameters.AddWithValue("@EmpID", id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Address", employee.myAddress);
            cmd.Parameters.AddWithValue("@ScheduleType", ScheduleCode(employee.Schedule));
            cmd.Parameters.AddWithValue("@PaymentMethodType", PaymentMethodCode(employee.Paymentmethod));
            cmd.Parameters.AddWithValue("@PaymentClassificationType",
                PaymentClassificationCode(employee.Classification));

            try
            {
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                SavePaymentMethod(id, employee, transaction);
                transaction.Commit();
            }
            catch (SqliteException e)
            {
                transaction.Rollback();
                throw new EmployeeAlreadyExists(e.ToString());
            }

            con.Close();
        }

        private string PaymentClassificationCode(PaymentClassification employeeClassification)
        {
            return "unknown Payment Classification";
        }

        private void SavePaymentMethod(int id, Employee employee, SqliteTransaction sqliteTransaction)
        {
            PaymentMethod method = employee.Paymentmethod;
            SqliteCommand paymentMethod = null;

            if (method is HoldMethod)
            {
                return;
            }
            else if (method is AccountPaymentMethod)
            {
                AccountPaymentMethod acc = method as AccountPaymentMethod;

                string accountadd = "INSERT INTO DirectDepositAccount VALUES(" +
                                    "@id" +
                                    ", @Account" +
                                    ", @Bank" +
                                    ")";
                paymentMethod = new SqliteCommand(accountadd, con);
                paymentMethod.Parameters.AddWithValue("@id", id);
                paymentMethod.Parameters.AddWithValue("@Account", acc.AccountNumber);
                paymentMethod.Parameters.AddWithValue("@Bank", acc.bank);
               
            }
            else if (method is MailPaymentMethod)
            {
                MailPaymentMethod mail = method as MailPaymentMethod;
                string sql = "INSERT INTO PaycheckAddress VALUES (" +
                             "@id" +
                             ", @address" +
                             ")";
                paymentMethod = new SqliteCommand(sql, con);
                paymentMethod.Parameters.AddWithValue("@id", id);
                paymentMethod.Parameters.AddWithValue("@address", mail.Address);
            } 

            if (paymentMethod != null)
            {
                paymentMethod.Transaction = sqliteTransaction;
                paymentMethod.ExecuteNonQuery();
            } 

            if (paymentMethod == null)
            {
                throw new NullReferenceException();
            }
        }

        private string PaymentMethodCode(PaymentMethod employeePaymentmethod)
        {
            if (employeePaymentmethod is AccountPaymentMethod)
            {
                return PaymentMethods.Account;
            }

            if (employeePaymentmethod is HoldMethod)
            {
                return PaymentMethods.Hold;
            }

            if (employeePaymentmethod is MailPaymentMethod)
            {
                return PaymentMethods.Mail;
            }


            return "UnknownPayment";
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEmployee(int id)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public static class PaymentMethods
        {
            public static string Hold = "HoldPayment";
            public static string Account = "AccountPayment";
            public static string Mail = "PostPayment";
        }

        public static class ScheduleCodes
        {
            public static string BiWeekly = "BiWeekly";
            public static string Monthly = "Monthly";
            public static string Weekly = "Weekly";
        }

        public static string ScheduleCode(PaymentSchedule eSchedule)
        {
            if (eSchedule is Biweekly)
            {
                return ScheduleCodes.BiWeekly;
            }
            else if (eSchedule is MonthlyPaymentSchedule)
            {
                return ScheduleCodes.Monthly;
            }
            else if (eSchedule is WeeklySchedule)
            {
                return ScheduleCodes.Weekly;
            }

            return "UnknownSchedule";
        }

        public static class Tables
        {
            public static string mail = "PaycheckAddress";
            public static string account = "DirectDepositAccount";
            public static string employee = "Employee";
        }
    }
}