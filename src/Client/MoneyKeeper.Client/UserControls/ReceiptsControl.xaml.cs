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


	public ReceiptsControl()
	{
		InitializeComponent();
	}
}