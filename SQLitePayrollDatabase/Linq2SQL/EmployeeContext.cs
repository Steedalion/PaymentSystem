using System.Data;
using System.Data.Linq;
using PaymentClassifications.ChangeClassification;

namespace PayrollDataBase.Linq2SQL
{
    public class EmployeeContext : DataContext
    {
        public EmployeeContext(IDbConnection connection) : base(connection)
        {
        }

        public Table<EmployeeUnit> Employees;
        public Table<Account> DirectDepositAccounts;
        public Table<MailAddress> PaycheckAddresses;
        public Table<SalaryAdapter> Salaries;
        public Table<CommisionAdapter> Commsions;
        public Table<HourlyAdapter> Hourlies;
    }
}