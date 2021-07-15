using System;
using Domain;
using Microsoft.Data.Sqlite;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDB;
using PayrollDomain;

namespace PayrollDataBase
{
    public class SaveEmployeeOperation
    {
        private SqliteConnection con;
        private int id;
        private Employee employee;

        public SaveEmployeeOperation(int id, Employee employee)
        {
            this.id = id;
            this.employee = employee;
        }

        public void Execute()
        {
            con = new SqliteConnection(SqliteDB.connectionID);
            con.Open();

            SqliteTransaction transaction = con.BeginTransaction();


            try
            {
                SaveEmployee(id, employee, transaction);
                SavePaymentMethod(id, employee, transaction);
                SavePaymentClassification(id, employee, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();


                var sql = e as SqliteException;
                // if (sql.ErrorCode == sq.Constraint)
                // {
                //     throw new EmployeeAlreadyExists();
                // }

                throw e;
            }

            con.Close();
        }

        private void SavePaymentClassification(int id, Employee employee, SqliteTransaction transaction)
        {
            SqliteCommand classification = null;

            if (employee.Classification is SalariedClassification)
            {
                SalariedClassification sal = employee.Classification as SalariedClassification;
                string sql = "INSERT INTO " + Tables.Salary + " VALUES("
                             + "@EmpID"
                             + ",@Salary"
                             + ")";
                classification = new SqliteCommand(sql, con);
                classification.Parameters.AddWithValue("@EmpID", id);
                classification.Parameters.AddWithValue("@Salary", sal.Salary);
            }
            else if (employee.Classification is CommisionClassification)
            {
                CommisionClassification commision = employee.Classification as CommisionClassification;
                string sql = "INSERT INTO " + Tables.Commission + " VALUES("
                             + "@EmpID"
                             + ",@Salary"
                             + ",@CommisionRate"
                             + ")";
                classification = new SqliteCommand(sql, con);
                classification.Parameters.AddWithValue("@EmpID", id);
                classification.Parameters.AddWithValue("@Salary", commision.Salary);
                classification.Parameters.AddWithValue("@CommisionRate", commision.CommisionRate);
            }

            if (employee.Classification is HourlyClassification)
            {
                HourlyClassification hourly = employee.Classification as HourlyClassification;
                string sql = "INSERT INTO " + Tables.Hourly + " VALUES("
                             + "@EmpID"
                             + ",@HourlyRate"
                             + ")";
                classification = new SqliteCommand(sql, con);
                classification.Parameters.AddWithValue("@EmpID", id);
                classification.Parameters.AddWithValue("@HourlyRate", hourly.Rate);
            }

            if (classification == null)
            {
                throw new NullReferenceException("Classification Not matched");
            }

            classification.Transaction = transaction;
            classification.ExecuteNonQuery();
        }

        private void SaveEmployee(int id, Employee employee, SqliteTransaction sqliteTransaction)
        {
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
            cmd.Parameters.AddWithValue("@ScheduleType", ScheduleCodes.Code(employee.Schedule));
            cmd.Parameters.AddWithValue("@PaymentMethodType", MethodCodes.Code(employee.Paymentmethod));
            cmd.Parameters.AddWithValue("@PaymentClassificationType",
                ClassificationCodes.Code(employee.Classification));

            cmd.Transaction = sqliteTransaction;
            cmd.ExecuteNonQuery();
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

    
    }
}