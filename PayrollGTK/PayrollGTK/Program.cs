using System;
using Gtk;
using PayrollDomain;

namespace PayrollGTK
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Affiliation affiliations = new NoAffiliation();
            Console.WriteLine(affiliations);
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();


        }
    }
}
