namespace Payroll.Tests.Transactions
{
    public abstract class ChangeAffiliation:ChangeEmployeeTransaction
    {
        protected int memberId;

        protected ChangeAffiliation(InMemoryDB database, int empId, int memberId) : base(database, empId)
        {
            this.memberId = memberId;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Affiliation = MakeAffiliation();
            ModifyMembership();
        }

        protected abstract Affiliation MakeAffiliation();

        protected abstract void ModifyMembership();
    }
}