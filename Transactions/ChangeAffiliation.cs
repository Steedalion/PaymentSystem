using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction.ChangeEmployee;

namespace Transactions
{
    public abstract class ChangeAffiliation:ChangeEmployeeTransaction
    {
        protected readonly int MemberId;

        protected ChangeAffiliation(IPayrollDb database, int empId, int memberId) : base(database, empId)
        {
            this.MemberId = memberId;
        }

        protected override void ModifyEmployee(Employee employee)
        {
            employee.Affiliation = MakeAffiliation();
            ModifyMembership();
        }

        protected abstract IAffiliation MakeAffiliation();

        protected abstract void ModifyMembership();
    }
}