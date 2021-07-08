namespace Affiliations
{
    public class ChangeUnionMember : ChangeAffiliation
    {
        private int memberID;
        private double dues;

        public ChangeUnionMember(IPayrollDB database, int empId, int memberId, double dues) : base(database, empId, memberId)
        {
            memberID = memberId;
            this.dues = dues;
        }

        protected override Affiliation MakeAffiliation()
        {
            UnionAffiliation unionAffiliation = new UnionAffiliation();
            unionAffiliation.Dues = dues;
            return unionAffiliation;
        }

        protected override void ModifyMembership()
        {
            database.AddUnionMember(memberID,empId);
        }
    }
}