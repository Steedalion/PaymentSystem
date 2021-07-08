namespace Payroll.Tests.Transactions
{
    public class ChangeUnaffiliated : ChangeAffiliation
    {
        public ChangeUnaffiliated(IPayrollDB database, int empId, int memberId) : base(database, empId, memberId)
        {
        }

        protected override Affiliation MakeAffiliation()
        {
            return new NoAffiliation();
        }

        protected override void ModifyMembership()
        {
            database.RemoveUnionMember(memberId);
        }
    }
}