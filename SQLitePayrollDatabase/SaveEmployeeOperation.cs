using System;
using System.Data.SQLite;
using PaymentClassification.PaymentClassifications;
using PaymentMethods;
using PayrollDataBase.Linq2SQL;
using PayrollDB;
using PayrollDomain;
using PaymentClassification = PayrollDomain.PaymentClassification;

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
            // db.Methods.InsertOnSubmit(new PaymentMethodUnit(id, employee.Paymentmethod));
            // db.Classifications.InsertOnSubmit(new PaymentClassificationUnit(id, employee.Classification));
            // db.Schedules.InsertOnSubmit(new PaymentScheduleUnit(id, employee.Schedule));
            db.SubmitChanges();
            // con = new SQLiteConnection(SqliteDB.connectionID);
            // con.Open();
            //
            // SqliteTransaction transaction = con.BeginTransaction();
            //
            //
            // try
            // {
            //     SaveEmployee(id, employee, transaction);
            //     SavePaymentMethod(id, employee, transaction);
            //     SavePaymentClassification(id, employee, transaction);
            //     transaction.Commit();
            // }
            // catch (Exception e)
            // {
            //     transaction.Rollback();
            //
            //
            //     // var sql = e as SqliteException;
            //     // if (sql.ErrorCode == SQLiteErrorCode.Constraint)
            //     // {
            //     //     throw new EmployeeIdAlreadyExists();
            //     // }
            //
            //     throw e;
            // }
            //
            // con.Close();
        }

        // private void SavePaymentClassification(int id, Employee employee, SqliteTransaction transaction)
        // {
        //     SqliteCommand classification = null;
        //
        //     if (employee.Classification is SalariedClassification)
        //     {
        //         SalariedClassification sal = employee.Classification as SalariedClassification;
        //         string sql = "INSERT INTO " + Tables.Salary + " VALUES("
        //                      + "@EmpID"
        //                      + ",@Salary"
        //                      + ")";
        //         classification = new SqliteCommand(sql, con);
        //         classification.Parameters.AddWithValue("@EmpID", id);
        //         classification.Parameters.AddWithValue("@Salary", sal.Salary);
        //     }
        //     else if (employee.Classification is CommisionClassification)
        //     {
        //         CommisionClassification commision = employee.Classification as CommisionClassification;
        //         string sql = "INSERT INTO " + Tables.Commission + " VALUES("
        //                      + "@EmpID"
        //                      + ",@Salary"
        //                      + ",@CommisionRate"
        //                      + ")";
        //         classification = new SqliteCommand(sql, con);
        //         classification.Parameters.AddWithValue("@EmpID", id);
        //         classification.Parameters.AddWithValue("@Salary", commision.Salary);
        //         classification.Parameters.AddWithValue("@CommisionRate", commision.CommisionRate);
        //     }
        //
        //     if (employee.Classification is HourlyClassification)
        //     {
        //         HourlyClassification hourly = employee.Classification as HourlyClassification;
        //         string sql = "INSERT INTO " + Tables.Hourly + " VALUES("
        //                      + "@EmpID"
        //                      + ",@HourlyRate"
        //                      + ")";
        //         classification = new SqliteCommand(sql, con);
        //         classification.Parameters.AddWithValue("@EmpID", id);
        //         classification.Parameters.AddWithValue("@HourlyRate", hourly.Rate);
        //     }
        //
        //     if (classification == null)
        //     {
        //         throw new NullReferenceException("Classification Not matched");
        //     }
        //
        //     classification.Transaction = transaction;
        //     classification.ExecuteNonQuery();
        // }
        //
        // private void SaveEmployee(int id, Employee employee, SqliteTransaction sqliteTransaction)
        // {
        //     string sql = "INSERT INTO Employee VALUES("
        //                  + "@EmpID"
        //                  + ",@Name"
        //                  + ",@Address"
        //                  + ",@ScheduleType"
        //                  + ",@PaymentMethodType"
        //                  + ",@PaymentClassificationType"
        //                  + ")";
        //
        //     var command = new SqliteCommand(sql, con);
        //     var cmd = new SqliteCommand(command.CommandText, con);
        //     cmd.Parameters.AddWithValue("@EmpID", id);
        //     cmd.Parameters.AddWithValue("@Name", employee.Name);
        //     cmd.Parameters.AddWithValue("@Address", employee.myAddress);
        //     cmd.Parameters.AddWithValue("@ScheduleType", ScheduleCodes.Code(employee.Schedule));
        //     cmd.Parameters.AddWithValue("@PaymentMethodType", MethodCodes.Code(employee.Paymentmethod));
        //     cmd.Parameters.AddWithValue("@PaymentClassificationType",
        //         ClassificationCodes.Code(employee.Classification));
        //
        //     cmd.Transaction = sqliteTransaction;
        //     cmd.ExecuteNonQuery();
        // }
        //
        //
        //
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
                db.DirectDepositAccounts.InsertOnSubmit(new Account(id,acc));
            }
            else if (method is MailPaymentMethod)
            {
                MailPaymentMethod mail = method as MailPaymentMethod;
                var pc = new PaycheckAddress(id, mail);
                db.PaycheckAddresses.InsertOnSubmit(pc);
            }
        
            if (method == null)
            {
                throw new NullReferenceException();
            }
        }


    }
}
