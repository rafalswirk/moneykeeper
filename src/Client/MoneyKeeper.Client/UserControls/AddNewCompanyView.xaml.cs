namespace MoneyKeeper.Client.UserControls;

public partial class AddNewCompanyView : ContentView
{
    public string TaxId
    {
        get => entryTaxId.Text;
        set => entryTaxId.Text = value;
    }
    public string CompanyName
    {
        get => entryCompanyName.Text;
        set => entryCompanyName.Text = value;
    } 

    public event EventHandler OnSave;
    public event EventHandler OnReject;

    public AddNewCompanyView()
	{
		InitializeComponent();
	}

    

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        OnSave?.Invoke(this, EventArgs.Empty);
    }

    private void btnReject_Clicked(object sender, EventArgs e)
    {
        OnReject?.Invoke(this, EventArgs.Empty);
    }
}