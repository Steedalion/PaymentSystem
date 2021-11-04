using System;
using System.Data.Linq.Mapping;
using PayrollDomain;

namespace PayrollDataBase.Linq2SQL
{
    [Table(Name = "Timecards")]
    public class TimecardAdapter
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Hours))] public float Hours;
        [Column(Name = nameof(Date))] public DateTime Date;

        public TimecardAdapter(int id, TimeCard timeCard)
        {
            EmpID = id;
            Date = timeCard.Date;
            Hours = (float)timeCard.Hours;
        }

        public TimecardAdapter()
        {
        }
    }
}