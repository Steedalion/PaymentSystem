using PayrollDatabase;
using Transaction;

namespace PresenterTests
{
    public class MockTransaction : DbTransaction
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