using PayrollDB;
using PayrollDomain;

namespace Transactions
{
    public class ChangeUnaffiliated : ChangeAffiliation
    {
        public ChangeUnaffiliated(IPayrollDb database, int empId, int memberId) : base(database, empId, memberId)
        {
        }

        protected override IAffiliation MakeAffiliation()
        {
            return new NoAffiliation();
        }

        protected override void ModifyMembership()
        {
            Database.RemoveUnionMember(MemberId);
        }
    }
}

