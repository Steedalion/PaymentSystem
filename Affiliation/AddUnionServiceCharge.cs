using System;
using PayrollDatabase;
using PayrollDomains;

namespace Affiliation
{
    public class AddUnionServiceCharge:Transaction.DbTransaction
    {
        public AddUnionServiceCharge(IPayrollDb database, int memberId, DateTime date, double amount) : base(database)
        {
            this.memberId = memberId;
            this.date = date;
            this.amount = amount;
        }

        private int memberId;
        private DateTime date;
        private double amount;

        public override void Execute()
        {
            Employee e = database.GetUnionMember(memberId);
            ServiceCharge sc = new ServiceCharge(date, amount);
            UnionAffiliation ua = null;
            
            if (e.Affiliation is UnionAffiliation)
            {
                ua = e.Affiliation as UnionAffiliation;
            }
            ua.AddServiceCharge(sc);
        }
    }
}