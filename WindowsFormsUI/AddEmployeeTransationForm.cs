using System;
using System.Linq;
using System.Windows.Forms;
using Presenters;

namespace WindowsFormsUI
{
    public partial class AddEmployeeTransationForm : Form, AddEmployeeView
    {
        public AddEmployeeTransationForm()
        {
            InitializeComponent();
            UpdateSubmitButton(false);
        }

        public AddEmployeePresenter Presenter { get; set; }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void empIdUpdated(object sender, EventArgs e)
        {
            NumberOnly(empIDTb);
            Presenter.EmpId = Int32.Parse(empIDTb.Text);
        }


        private void addressBox_TextChanged(object sender, EventArgs e)
        {
            Presenter.Address = addressBox.Text;
        }

        private void AddEmployeeTransationForm_Load(object sender, EventArgs e)
        {
            EnterState(AddEmployeeStates.Hourly);
        }

      

        private void submitButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked");
            Presenter.AddEmployeeToTransactions();
            this.Close();
        }

        private void commisionedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.IsCommision = true;
            EnterState(AddEmployeeStates.Commissioned);
        }

        private void salariedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.IsSalary = true;
            EnterState(AddEmployeeStates.Salaried);
        }

        private void hourlyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.IsHourly = true;
            EnterState(AddEmployeeStates.Hourly);
        }

        private void NumberOnly(TextBox textBox, char seperator = '0')
        {
            textBox.Text = new string(textBox.Text.Where(c => char.IsDigit(c) || c == seperator).ToArray());
        }

        private void EnterState(AddEmployeeStates state)
        {
            bool hourly = state != AddEmployeeStates.Hourly,
                salary = state != AddEmployeeStates.Salaried,
                commissioned = state != AddEmployeeStates.Commissioned;
            
            HourlyRateTextBox.ReadOnly = hourly;
            SalariedSalaryTextBox.ReadOnly = salary;
            CommsionedRateBox.ReadOnly = commissioned;
            CommisionedSalaryBox.ReadOnly = commissioned;
            
        }

        private void HourlyRateTextBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(HourlyRateTextBox, '.');
            Presenter.HourlyRate = Double.Parse(HourlyRateTextBox.Text);
        }

        private void SalariedSalaryTextBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(SalariedSalaryTextBox, '.');
            Presenter.Salary = Double.Parse(SalariedSalaryTextBox.Text);
        }

        private void CommsionedRateBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(CommsionedRateBox, '.');
            Presenter.CommisionRate = Double.Parse(CommsionedRateBox.Text);
        }

  
        public void UpdateSubmitButton(bool allInfoCollected)
        {
            submitButton.Enabled = allInfoCollected;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            Presenter.Name = nameBox.Text;
        }

        private void CommisionedSalaryBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(CommisionedSalaryBox, '.');
            Presenter.CommisionSalary = Double.Parse(CommisionedSalaryBox.Text);
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}