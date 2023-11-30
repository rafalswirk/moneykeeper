using MoneyKeeper.Client.DTO;

namespace MoneyKeeper.Client.UserControls;

public partial class ReceiptsControl : ContentView
{
	public IList<ReceiptInfoDto> Receipts
	{
		get { return (IList<ReceiptInfoDto>)GetValue(ReceiptsProperty); }
		set { SetValue(ReceiptsProperty, value); }
	}

	public static readonly BindableProperty ReceiptsProperty =
		BindableProperty.Create("Receipts", typeof(IList<ReceiptInfoDto>), typeof(ReceiptsControl));

	public event EventHandler<ReceiptInfoDto> ReceiptSelected;

	public ReceiptsControl()
	{
		InitializeComponent();
	}

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ReceiptSelected?.Invoke(this, e.SelectedItem as ReceiptInfoDto);
    }
}