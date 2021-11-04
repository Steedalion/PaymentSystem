using PayrollDatabase;
using PayrollDomains;

namespace Affiliation
{
    public class AddUnionMemberTransaction:Transaction.DbTransaction
    {
        private int id;
        private int memberId;

        public AddUnionMemberTransaction(IPayrollDb database, int id, int memberId) : base(database)
        {
            this.id = id;
            this.memberId = memberId;
        }

        public override void Execute()
        {
            Employee e = database.GetEmployee(id);
            e.Affiliation = new UnionAffiliation();
            database.AddUnionMember(memberId, id);
            
        }
    }
}