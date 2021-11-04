using System.Data.Linq.Mapping;

namespace PayrollDataBase.Linq2SQL
{
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