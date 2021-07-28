using System.Data.Common;
using PayrollDB;
using PayrollDomain;
using DbTransaction = Transactions.DbTransaction;

namespace Affiliations
{
    public class AddUnionMemberTransaction:DbTransaction
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
            Employee e = database.GetEmployee(id);
            e.Affiliation = new UnionAffiliation();
            database.AddUnionMember(memberId, id);
            
        }
    }
}