using Gtk;
using PayrollDB;
using PayrollGTK.gtk_gui;

namespace PayrollGTK
{
    static class StartClass
    {
        public static MainWindow win { get; private set; }

        public static void Launch(string[] args)
        {
            Application.Init();
            //AddEmployee win = new AddEmployee();
            win = new MainWindow();
            win.Show();
            Application.Run();
            win.Destroy();
            Application.Quit();
        }
    }

    static class MainClass
    {
        public static MainWindow win { get; private set; }

        public static void Main(string[] args)
        {
            IPayrollDb database = new InMemoryDB();
            GtkViewLoader viewLoader = new GtkViewLoader(database);
            viewLoader.LoadPayrollView();
        }
    }
}