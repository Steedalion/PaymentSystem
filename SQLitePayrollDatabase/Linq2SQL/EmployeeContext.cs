using System.Data;
using System.Data.Linq;

namespace DatabaseTests.SQLiteTests
{
    public class EmployeeContext : DataContext
    {
        public EmployeeContext(IDbConnection connection) : base(connection)
        {
        }

        public Table<EmployeeUnit> Employees;
    }
}