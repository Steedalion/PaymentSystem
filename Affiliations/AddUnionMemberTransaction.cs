using PayrollDB;
using PayrollDomain;
using Transactions;

namespace Affiliations
{
    public class AddUnionMemberTransaction:DatabaseTransaction
    {
        private int id;
        private int memberId;

        public AddUnionMemberTransaction(PayrollDB.IPayrollDb database, int id, int memberId) : base(database)
        {
            this.id = id;
            this.memberId = memberId;
        }

        public override void Execute()
        {
            Employee e = Database.GetEmployee(id);
            e.Affiliation = new UnionAffiliation();
            Database.AddUnionMember(memberId, id);
            
        }
    }
}