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
            // throw new System.NotImplementedException();
        }

        private void AddEmployeeTransationForm_Load(object sender, EventArgs e)
        {
            EnterState(AddEmployeeStates.Hourly);
            // throw new System.NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void commisionedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnterState(AddEmployeeStates.Commisioned);
        }

        private void salariedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnterState(AddEmployeeStates.Salaried);
        }

        private void hourlyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnterState(AddEmployeeStates.Hourly);
        }

        private void NumberOnly(TextBox textBox, char seperator = '0')
        {
            textBox.Text = new string(textBox.Text.Where(c => char.IsDigit(c) || c == seperator).ToArray());
        }

        private void EnterState(AddEmployeeStates state)
        {
            HourlyRateTextBox.ReadOnly = state != AddEmployeeStates.Hourly;
            SalariedSalaryTextBox.ReadOnly = state != AddEmployeeStates.Salaried;
            CommsionedRateBox.ReadOnly = state != AddEmployeeStates.Commisioned;
            CommisionedSalaryBox.ReadOnly = state != AddEmployeeStates.Commisioned;
        }

        private void HourlyRateTextBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(HourlyRateTextBox, '.');
        }

        private void SalariedSalaryTextBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(SalariedSalaryTextBox, '.');
        }

        private void CommsionedRateBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(CommsionedRateBox, '.');
        }

        private void CommisionedSalaryBox_TextChanged(object sender, EventArgs e)
        {
            NumberOnly(CommisionedSalaryBox, '.');
        }

        public void UpdateSubmitButton(bool allInfoCollected)
        {
            submitButton.Enabled = allInfoCollected;
        }
    }

    internal enum AddEmployeeStates
    {
        Hourly,
        Salaried,
        Commisioned
    }
}