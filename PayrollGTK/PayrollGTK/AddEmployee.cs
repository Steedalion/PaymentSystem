using System;
using Gtk;
using PayrollDB;
using Presenters;

namespace PayrollGTK
{
    public partial class AddEmployee : Gtk.Window, AddEmployeeView
    {
        private AddEmployeePresenter presenter;

        public AddEmployee() :
            base(Gtk.WindowType.Toplevel)
        {
            presenter =
                new AddEmployeePresenter(this, new TransactionContainer(), new InMemoryDB());
            Build();
            HourlyState();
        }

        public static AddEmployee StartWindowAsMain()
        {
            Application.Init();
            AddEmployee win = new AddEmployee();
            win.Show();
            Application.Run();
            return win;
        }


        protected void OnHourlyToggled(object sender, EventArgs e)
        {
            HourlyState();
        }

        private void HourlyState()
        {
            presenter.IsHourly = true;
            presenter.IsSalary = false;
            presenter.IsCommision = false;

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
            presenter.IsHourly = false;
            presenter.IsSalary = true;
            presenter.IsCommision = false;

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
            presenter.IsHourly = false;
            presenter.IsSalary = false;
            presenter.IsCommision = true;

            hourlyRate.Sensitive = false;
            salaryValue.Sensitive = false;
            commisionRate.Sensitive = true;
            commisionSalary.Sensitive = true;
        }

        protected void OnEmpidEntryChanged(object sender, EventArgs e)
        {
            presenter.EmpId = Int32.Parse(empidEntry.Text);
        }

        protected void OnNameChanged(object sender, EventArgs e)
        {
            presenter.Name = name.Text;
        }

        protected void OnAddressChanged(object sender, EventArgs e)
        {
            presenter.Address = address.Text;
        }

        protected void OnHourlyRateChanged(object sender, EventArgs e)
        {
            presenter.HourlyRate = Double.Parse(hourlyRate.Text);
        }

        protected void OnSalaryValueChanged(object sender, EventArgs e)
        {
            presenter.Salary = Double.Parse(salaryValue.Text);
        }

        protected void OnCommisionRateChanged(object sender, EventArgs e)
        {
            presenter.CommisionRate = Double.Parse(commisionRate.Text);
        }

        protected void OnCommisionSalaryChanged(object sender, EventArgs e)
        {
            presenter.CommisionSalary = Double.Parse(commisionSalary.Text);
        }

        public void UpdateSubmitButton(bool allInfoCollected)
        {
            submit.Sensitive = allInfoCollected;
        }
    }
}