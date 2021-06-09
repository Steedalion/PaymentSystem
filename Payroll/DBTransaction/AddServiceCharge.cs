using System;

namespace Payroll.Tests
{
    public class AddServiceCharge:DbTransaction
    {
        public AddServiceCharge(int memberId, DateTime date, double amount)
        {
            this.memberId = memberId;
            this.date = date;
            this.amount = amount;
        }

        private int memberId;
        private DateTime date;
        private double amount;

        public void Execute()
        {
            Employee e = PayrollDatabase.GetUnionMember(memberId);
            ServiceCharge sc = new ServiceCharge(date, amount);
            UnionAffiliation ua = null;
            if (e.Affiliation is UnionAffiliation)
            {
                
                ua = e.Affiliation as UnionAffiliation;
                ua.AddServiceCharge(sc);
            }
            if (e.Affiliation == null)
            {
                ua = new UnionAffiliation();
                e.Affiliation = ua;
            }
            else
            {
                throw new AlreadyAffiliated();
            }
            ua.AddServiceCharge(sc);
        }
    }
}