namespace Presenters
{
    public class MockAddEmployee: AddEmployeeView
    {
        public int Updates;
        public bool SubmitButtonEnabled;

        public void UpdateSubmitButton(bool allInfoCollected)
        {
            SubmitButtonEnabled = allInfoCollected;
            Updates++;
        }
    }
}