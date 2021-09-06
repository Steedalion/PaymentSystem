using Gtk;

namespace PayrollGTK
{
    public static class Program
    {
        public static MainWindow win { get; private set; }

        public static void Main(string[] args)
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
}