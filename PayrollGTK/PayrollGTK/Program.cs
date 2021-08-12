using Gtk;

namespace PayrollGTK
{
    class MainClass
    {
        public static void Main(string[] args)
        {

             Application.Init();
            AddEmployee win = new AddEmployee();
            // MainWindow win = new MainWindow();
             win.Show();
            Application.Run();
            win.Destroy();
            Application.Quit();


        }
    }
}
