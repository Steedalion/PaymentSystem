using System.Data.Linq.Mapping;

namespace PayrollDataBase.Linq2SQL
{
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
}