using System.Data;
using System.Data.Linq;

namespace PayrollDataBase.Linq2SQL
{
    public class EmployeeContext : DataContext
    {
        public EmployeeContext(IDbConnection connection) : base(connection)
        {
        }

        public Table<EmployeeUnit> Employees;
        public Table<Account> DirectDepositAccounts;
        public Table<PaycheckAddress> PaycheckAddresses;
    }
}