using System;
using System.Linq;
using System.Windows.Forms;
using Presenter;

namespace WinFormsApp1
{
    public partial class AddEmployeeTransationForm : Form, AddEmployeeView
    {
        public bool EnabledPoppups = true;

        public AddEmployeeTransationForm(bool enabledPoppups = true)
        {
            EnabledPoppups = enabledPoppups;
            InitializeComponent();
            UpdateSubmitButton(false);
        }

        public AddEmployeePresenter Presenter { get; set; }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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
            string text = textBox.Text;
            if (text == "")
            {
                return;
            }

            if (text.Any(c => !char.IsDigit(c) || c == seperator))
            {
                if (EnabledPoppups)
                {
                    MessageBox.Show("Please enter numbers only");
                }

                // textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }

            textBox.Text = new string(text.Where(c => char.IsDigit(c) || c == seperator).ToArray());

            Console.WriteLine(text);
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

        private void empIdUpdated(object sender, EventArgs e)
        {
            

            NumberOnly(empIDTb);
            if (empIDTb.Text == "")
            {
                return;
            }
            Presenter.EmpId = Int32.Parse(empIDTb.Text);
        }

        private void HourlyRateTextBox_TextChanged(object sender, EventArgs e)
        {
          

            NumberOnly(HourlyRateTextBox, '.');
              if (HourlyRateTextBox.Text == "")
            {
                return;
            }
            Presenter.HourlyRate = Double.Parse(HourlyRateTextBox.Text);
        }

        private void SalariedSalaryTextBox_TextChanged(object sender, EventArgs e)
        {
                        NumberOnly(SalariedSalaryTextBox, '.');

            if (SalariedSalaryTextBox.Text == "")
            {
                return;
            }

            Presenter.Salary = Double.Parse(SalariedSalaryTextBox.Text);
        }

        private void CommsionedRateBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(CommsionedRateBox, '.');
            if (CommsionedRateBox.Text == "")
            {
                return;
            }

            Presenter.CommisionRate = Double.Parse(CommsionedRateBox.Text);
        }

        private void CommisionedSalaryBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(CommisionedSalaryBox, '.');
            if (CommisionedSalaryBox.Text == "")
            {
                return;
            }

            Presenter.CommisionSalary = Double.Parse(CommisionedSalaryBox.Text);
        }


        public void UpdateSubmitButton(bool allInfoCollected)
        {
            submitButton.Enabled = allInfoCollected;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            Presenter.Name = nameBox.Text;
        }


        private void CancelButtonClick(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}