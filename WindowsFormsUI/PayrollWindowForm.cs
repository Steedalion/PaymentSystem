using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsUI
{
    public partial class PayrollWindowForm : Form
    {
        public PayrollWindowForm()
        {
            InitializeComponent();
        }

        public string TransactionText
        {
            set { pendingTransactions.Text = value; }
        }

        public string EmployeeText
        {
            set { employeesTextbox.Text = value; }
        }


        private void runTransactions_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

    }
}