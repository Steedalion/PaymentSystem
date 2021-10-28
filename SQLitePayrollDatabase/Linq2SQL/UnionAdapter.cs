using System.Data.Linq.Mapping;

namespace PayrollDataBase.Linq2SQL
{
    [Table(Name = "UnionMembers")]
    public class UnionAdapter
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;
        [Column(Name = nameof(MemberID))] public int MemberID;

        public UnionAdapter()
        {
        }

        public UnionAdapter(int empId, int memberId)
        {
            EmpID = empId;
            MemberID = memberId;
        }
    }
}