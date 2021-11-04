namespace Presenter
{
    public class MockPayrollPayrollView : IPayrollView
    {
        public string TransactionText { get; set; }
        public string EmployeeText { get; set; }
        private PayrollPresenter presenter;

        public IPayrollPresenter Presenter { get; set; }

     
    }
}