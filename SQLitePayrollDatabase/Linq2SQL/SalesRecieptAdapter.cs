using System;
using System.Data.Linq.Mapping;
using PayrollDomain;

namespace PayrollDataBase.Linq2SQL
{
    [Table(Name = "SalesReceipts")]
    public class SalesRecieptAdapter
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Amount))] public float Amount;
        [Column(Name = nameof(Date))] public DateTime Date;

        public SalesRecieptAdapter(int id, SalesReciept reciept)
        {
            EmpID = id;
            Amount = (float)reciept.Amount;
            Date = reciept.DATE;
        }

        public SalesRecieptAdapter()
        {
        }
    }
}