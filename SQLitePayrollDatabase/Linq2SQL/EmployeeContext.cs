using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PaymentClassification.ChangeClassification;

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

    [Table(Name = "HourlyClassification")]
    public class HourlyAdapter
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(HourlyRate))] public float HourlyRate;

        public HourlyAdapter(int id, double hourlyRate)
        {
            EmpID = id;
            HourlyRate = (float)hourlyRate;
        }

        public HourlyAdapter()
        {
        }
    }

    [Table(Name = "CommissionedClassification")]
    public class CommisionAdapter
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Salary))] public float Salary;

        [Column(Name = nameof(CommissionRate))]
        public float CommissionRate;

        public CommisionAdapter(int id, double salary, double commisionRate)
        {
            EmpID = id;
            Salary = (float)salary;
            CommissionRate = (float)commisionRate;
        }

        public CommisionAdapter()
        {
        }
    }

    [Table(Name = "SalariedClassification")]
    public class SalaryAdapter
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Salary))] public float Salary;

        public SalaryAdapter(int id, double salSalary)
        {
            EmpID = id;
            Salary = (float)salSalary;
        }

        public SalaryAdapter()
        {
        }
    }
}