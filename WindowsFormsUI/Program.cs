using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PayrollDB;

namespace WindowsFormsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FormsViewLoader loader =
                new FormsViewLoader(new InMemoryDB());
            loader.LoadPayrollView();
            Application.Run();
        }
    }
}