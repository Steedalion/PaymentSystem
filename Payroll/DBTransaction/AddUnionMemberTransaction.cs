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
            Employee e = PayrollDatabase.GetEmployee(id);
            e.Affiliation = new UnionAffiliation();
            PayrollDatabase.AddUnionMember(memberId, id);
            
        }
    }
}