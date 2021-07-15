using Domain;
using PayrollDB;
using PayrollDomain;
using Transactions.DBTransaction.ChangeEmployee;

namespace Transactions
{
    public abstract class ChangeAffiliation:ChangeEmployeeTransaction
    {
        protected int memberId;

        protected ChangeAffiliation(IPayrollDB database, int empId, int memberId) : base(database, empId)
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