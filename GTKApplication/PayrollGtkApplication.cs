using System;
using PayrollDB;
using Presenters;

namespace PayrollGTK.gtk_gui
{
    static class PayrollGtkApplication
    {
        public static MainWindow win { get; private set; }

        public static void Main(string[] args)
        {
            IPayrollDb database = new InMemoryDB();
            GtkViewLoader viewLoader = new GtkViewLoader(database);
            IView view = viewLoader.LoadPayrollView();
            PayrollPresenter presenter = new PayrollPresenter(view, database, viewLoader);
        }
    }
}

// using Gtk;
// using NUnit.Framework;
// using PayrollDB;
//
// namespace PayrollGTK.gtk_gui
// {
//     [TestFixture]
//     public class GtkViewLoaderTest
//     {
//         private IPayrollDb database;
//         private GtkViewLoader viewLoader;
//
//         [SetUp]
//         public void SetUp()
//         {
//             database = new InMemoryDB();
//             viewLoader = new GtkViewLoader(database);
//         }
//
//         // [Test]
//         public void LoadPayrollView()
//         {
//             viewLoader.LoadPayrollView();
//             Window window = viewLoader.LastLoadedView();
//         }
//
//         // [Test]
//         public void LoadEmployeeView()
//         {
//             viewLoader.LoadPayrollView();
//         }
//     }
// }