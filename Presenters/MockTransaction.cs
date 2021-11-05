using PayrollDB;
using Transactions;

namespace Presenters
{
    public class MockTransaction : DatabaseTransaction
    {
        public bool wasExecuted;

        public MockTransaction(IPayrollDb database) : base(database)
        {
            wasExecuted = true;
        }

        public override void Execute()
        {
        }
    }
}