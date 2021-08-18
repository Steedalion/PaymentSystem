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

        public void LoadPayrollView()
        {
            
            StartClass.Launch(null);
            lastWindow = StartClass.win;
        }

        public Window LastLoadedView()
        {
            return lastWindow;
        }
    }
}