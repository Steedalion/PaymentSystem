namespace Payroll.Tests
{
    public class AddUnionMemberTransaction:DbTransaction
    {
        private int id;
        private int memberId;

        public AddUnionMemberTransaction(int id, int memberId)
        {
            this.id = id;
            this.memberId = memberId;
        }

        public void Execute()
        {
            Employee e = PayrollDB.GetEmployee(id);
            e.Affiliation = new UnionAffiliation();
            PayrollDB.AddUnionMember(memberId, id);
            
        }
    }
}