using System;
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
            InMemoryDB database = new InMemoryDB();
            FormsViewLoader loader =
                new FormsViewLoader(database);
            loader.LoadPayrollView();
            Application.Run();
        }
    }
}