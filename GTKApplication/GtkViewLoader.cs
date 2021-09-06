using Gtk;
using PayrollDB;
using Presenters;

namespace PayrollGTK.gtk_gui
{
    public class GtkViewLoader : IViewLoader
    {
        private Window lastWindow;
        private readonly IPayrollDb database;

        public GtkViewLoader(IPayrollDb database)
        {
            this.database = database;
        }

        public void LoadAddEmployerView()
        {
            throw new System.NotImplementedException();
        }

        public IView LoadPayrollView()
        {
            PayrollGTK.Program.Main(null);
            var mainWindow = Program.win;
            // mainWindow.
            lastWindow = mainWindow;
            // mainWindow.SetPresenter()
            return mainWindow;
        }

        public Window LastLoadedView()
        {
            return lastWindow;
        }
    }
}