namespace Presenter
{
    public interface AddEmployeeView
    {
       
        /// <summary>
        /// This method gets called whenever a field of the presenter is updated.
        /// It returns true when all the required information is present otherwise false.
        /// It is typically used to enable the submit button when all the needed info is present.
        /// </summary>
        /// <param name="allInfoCollected"></param>
        void UpdateSubmitButton(bool allInfoCollected);
    }
}