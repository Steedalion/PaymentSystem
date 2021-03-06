
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;

	private global::Gtk.Action ActionsAction;

	private global::Gtk.Action addEmployee;

	private global::Gtk.Action removeEmployee;

	private global::Gtk.Action quitAction;

	private global::Gtk.VBox vbox1;

	private global::Gtk.MenuBar menubar1;

	private global::Gtk.VBox vbox2;

	private global::Gtk.Label label1;

	private global::Gtk.ScrolledWindow scrolledwindow1;

	private global::Gtk.TextView transactionsArea;

	private global::Gtk.VBox vbox3;

	private global::Gtk.Label label2;

	private global::Gtk.Statusbar statusbar1;

	private global::Gtk.Button runButton;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup("Default");
		this.ActionsAction = new global::Gtk.Action("ActionsAction", global::Mono.Unix.Catalog.GetString("Actions"), null, null);
		this.ActionsAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Actions");
		w1.Add(this.ActionsAction, null);
		this.addEmployee = new global::Gtk.Action("addEmployee", global::Mono.Unix.Catalog.GetString("_Add Employee"), null, "gtk-add");
		this.addEmployee.ShortLabel = global::Mono.Unix.Catalog.GetString("_AddEmployee");
		w1.Add(this.addEmployee, null);
		this.removeEmployee = new global::Gtk.Action("removeEmployee", global::Mono.Unix.Catalog.GetString("_Remove Employee"), null, "gtk-remove");
		this.removeEmployee.ShortLabel = global::Mono.Unix.Catalog.GetString("_RemoveEmployee");
		w1.Add(this.removeEmployee, null);
		this.quitAction = new global::Gtk.Action("quitAction", global::Mono.Unix.Catalog.GetString("Quit"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Quit");
		w1.Add(this.quitAction, null);
		this.UIManager.InsertActionGroup(w1, 0);
		this.AddAccelGroup(this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Payroll");
		this.Icon = global::Stetic.IconLoader.LoadIcon(this, "gtk-dialog-authentication", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString(@"<ui><menubar name='menubar1'><menu name='ActionsAction' action='ActionsAction'><menuitem name='addEmployee' action='addEmployee'/><menuitem name='removeEmployee' action='removeEmployee'/><menuitem name='quitAction' action='quitAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add(this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.label1 = new global::Gtk.Label();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("PendingTransactions");
		this.vbox2.Add(this.label1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.label1]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.scrolledwindow1 = new global::Gtk.ScrolledWindow();
		this.scrolledwindow1.CanFocus = true;
		this.scrolledwindow1.Name = "scrolledwindow1";
		this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child scrolledwindow1.Gtk.Container+ContainerChild
		this.transactionsArea = new global::Gtk.TextView();
		this.transactionsArea.Buffer.Text = global::Mono.Unix.Catalog.GetString("zxcvfd");
		this.transactionsArea.CanFocus = true;
		this.transactionsArea.Name = "transactionsArea";
		this.scrolledwindow1.Add(this.transactionsArea);
		this.vbox2.Add(this.scrolledwindow1);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.scrolledwindow1]));
		w5.Position = 1;
		this.vbox1.Add(this.vbox2);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.vbox2]));
		w6.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vbox3 = new global::Gtk.VBox();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.label2 = new global::Gtk.Label();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Employees");
		this.vbox3.Add(this.label2);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.label2]));
		w7.Position = 0;
		w7.Expand = false;
		w7.Fill = false;
		this.vbox1.Add(this.vbox3);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.vbox3]));
		w8.Position = 2;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar1 = new global::Gtk.Statusbar();
		this.statusbar1.Name = "statusbar1";
		this.statusbar1.Spacing = 6;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.runButton = new global::Gtk.Button();
		this.runButton.CanFocus = true;
		this.runButton.Name = "runButton";
		this.runButton.UseUnderline = true;
		this.runButton.Label = global::Mono.Unix.Catalog.GetString("Run Transactions");
		this.statusbar1.Add(this.runButton);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.statusbar1[this.runButton]));
		w9.Position = 1;
		w9.Expand = false;
		w9.Fill = false;
		this.vbox1.Add(this.statusbar1);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
		w10.Position = 3;
		w10.Expand = false;
		w10.Fill = false;
		this.Add(this.vbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 300;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.quitAction.Activated += new global::System.EventHandler(this.OnQuitActionActivated);
		this.transactionsArea.Added += new global::Gtk.AddedHandler(this.OnTransactionsAreaAdded);
	}
}
