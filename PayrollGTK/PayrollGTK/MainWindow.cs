using System;
using Gtk;
using PayrollDomain;
using PayrollGTK;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        addEmployee.Activated += printmsg;
        addEmployee.Activated += OpenAddemployee;
    }

    private void printmsg(object sender, EventArgs e)
    {
        Console.WriteLine(sender);
    }

    private void OpenAddemployee(object sender, EventArgs e)
    {

        Window addemployee = new AddEmployee();
        addemployee.Visible = true;
    }

    protected void NewAffiliation(object obj, EventArgs args)
    {
        Affiliation affiliations = new NoAffiliation();
        Console.WriteLine(affiliations);
        Console.WriteLine("Clicked");

    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnQuitActionActivated(object sender, EventArgs e)
    {
        Application.Quit();
    }

}
