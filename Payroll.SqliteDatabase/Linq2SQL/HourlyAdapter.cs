using System.Data.Linq.Mapping;

namespace PayrollDataBase.Linq2SQL
{
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
}