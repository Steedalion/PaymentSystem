using System;
namespace PayrollGTK
{
    public partial class AddEmployee : Gtk.Window
    {
        public AddEmployee() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            empidEntry.Changed += UpdateSubmitButton;
            HourlyState();
        }

     

        protected void UpdateSubmitButton(object sender, EventArgs e)
        {
            if(empidEntry.Text != "")
            {
                submit.Sensitive = true;
                return;
            }
            submit.Sensitive = false;
        }

        protected void OnHourlyToggled(object sender, EventArgs e)
        {
            HourlyState();

        }

        private void HourlyState()
        {
            hourlyRate.Sensitive = true;
            salaryValue.Sensitive = false;
            commisionRate.Sensitive = false;
            commisionSalary.Sensitive = false;
        }

        protected void OnSalaryToggled(object sender, EventArgs e)
        {
            SalaryState();
        }

        private void SalaryState()
        {
            hourlyRate.Sensitive = false;
            salaryValue.Sensitive = true;
            commisionRate.Sensitive = false;
            commisionSalary.Sensitive = false;
        }

        protected void OnCommisionToggled(object sender, EventArgs e)
        {
            CommisionState();
        }

        private void CommisionState()
        {
            hourlyRate.Sensitive = false;
            salaryValue.Sensitive = false;
            commisionRate.Sensitive = true;
            commisionSalary.Sensitive = true;
        }

        protected void OnEmpidEntryChanged(object sender, EventArgs e)
        {
        }

        protected void OnNameChanged(object sender, EventArgs e)
        {
        }

        protected void OnAddressChanged(object sender, EventArgs e)
        {
        }

        protected void OnHourlyRateChanged(object sender, EventArgs e)
        {
        }

        protected void OnSalaryValueChanged(object sender, EventArgs e)
        {
        }

        protected void OnCommisionRateChanged(object sender, EventArgs e)
        {
        }

        protected void OnCommisionSalaryChanged(object sender, EventArgs e)
        {
        }
    }
}
