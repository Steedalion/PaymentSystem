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

            string sql = "INSERT INTO Employee VALUES("
                         + "@EmpID"
                         +",@Name"
                         +",@Address"
                         +",@ScheduleType"
                         +",@PaymentMethodType"
                         +",@PaymentClassificationType"
                         + ")";

            var command = new SqliteCommand(sql, con);
            var cmd = new SqliteCommand(command.CommandText, con);
            cmd.Parameters.AddWithValue("@EmpID", id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Address", employee.myAddress);
            cmd.Parameters.AddWithValue("@ScheduleType", ScheduleCode(employee.Schedule));
            cmd.Parameters.AddWithValue("@PaymentMethodType", PaymentMethodCode(employee.Paymentmethod));
            cmd.Parameters.AddWithValue("@PaymentClassificationType", employee.Classification.GetType().ToString());
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqliteException e)
            {
                throw new EmployeeAlreadyExists(e.ToString());
            }

            SavePaymentMethod(id,employee);
            con.Close();
        }

        private void SavePaymentMethod(int id,Employee employee)
        {
            PaymentMethod method = employee.Paymentmethod;
            if (method is AccountPaymentMethod)
            {
                 AccountPaymentMethod acc = method as AccountPaymentMethod;
                
                string accountadd = "INSERT INTO DirectDepositAccount VALUES(" +
                         "@id" +
                         ", @Account"+
                         ", @Bank"+
                         ")";
                
            var cmdAccount = new SqliteCommand(accountadd, con);
            cmdAccount.Parameters.AddWithValue("@id", id);
            cmdAccount.Parameters.AddWithValue("@Account", acc.AccountNumber);
            cmdAccount.Parameters.AddWithValue("@Bank", acc.bank);
            cmdAccount.ExecuteNonQuery();
            }
            else if (method is MailPaymentMethod)
            {
                MailPaymentMethod mail = method as MailPaymentMethod;
                string sql = "INSERT INTO PaycheckAddress VALUES (" +
                             "@id" +
                             ", @address" +
                             ")";
                var cmd = new SqliteCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@address", mail.Address);
                cmd.ExecuteNonQuery();
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
                return PaymentMethods.Post;
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
            public static string Post = "PostPayment";
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
    }
}