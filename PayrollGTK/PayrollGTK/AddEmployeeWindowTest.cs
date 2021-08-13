using System;
using System.Threading;
using System.Threading.Tasks;
using Gtk;
using NUnit.Framework;
using Presenters;

namespace PayrollGTK
{
    public class AddEmployeeWindowTest
    {
        private AddEmployee window;
        private AddEmployeePresenter presenter;
        private TransactionContainer container;

         // [Test]
        public void CreateWindow()
        {
             Application.Init();
            // AddEmployee win = new AddEmployee();
            // MainWindow win = new MainWindow();
             // win.Show();
            Application.Run();
            // win.Destroy();
            Application.Quit();
        }
    }
}