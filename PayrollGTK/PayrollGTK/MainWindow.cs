using System;
using Atk;
using Gtk;
using PayrollGTK;
using Presenters;

public partial class MainWindow : Gtk.Window, IView
{
    private PayrollPresenter presenter;

    public void SetPresenter(PayrollPresenter newPresenter) => presenter = newPresenter;
    public PayrollPresenter GetPresenter() =>presenter;

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

    public TextBuffer GetTransactionTextArea()
    {
        return transactionsArea.Buffer;
    }
    protected void OnTransactionsAreaAdded(object o, AddedArgs args)
    {}

    public void SetTransactionText(string transactionTable)
    {
        transactionsArea.Buffer.Text = transactionTable;
    }

    public string GetTransactionText()
    {
        throw new NotImplementedException();
    }

    public void SetEmployeeText(string employeeTable)
    {
        throw new NotImplementedException();
    }

    public string GetEmployeeText()
    {
        throw new NotImplementedException();
    }
}
