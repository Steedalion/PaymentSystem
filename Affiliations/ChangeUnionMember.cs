using PayrollDB;
using PayrollDomain;
using Transactions;

namespace Affiliations
{
    public class ChangeUnionMember : ChangeAffiliation
    {
        private int memberID;
        private double dues;

        public ChangeUnionMember(PayrollDB.IPayrollDb database, int empId, int memberId, double dues) : base(database, empId, memberId)
        {
            memberID = memberId;
            this.dues = dues;
        }

        protected override IAffiliation MakeAffiliation()
        {
            UnionAffiliation unionAffiliation = new UnionAffiliation();
            unionAffiliation.Dues = dues;
            return unionAffiliation;
        }

        protected override void ModifyMembership()
        {
            Database.AddUnionMember(memberID,empId);
        }
    }
}