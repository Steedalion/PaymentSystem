using System;
using System.Windows.Forms;
using PayrollDatabase;

namespace WinFormsApp1
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