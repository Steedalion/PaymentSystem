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
            PayrollDatabase.AddUnionMember(memberId, id);
        }
    }
}