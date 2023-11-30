using MoneyKeeper.Client.DTO;

namespace MoneyKeeper.Client.View;

public partial class ReceiptDetails : ContentPage
{
    public ReceiptInfoDto Info { get; }

	public ReceiptDetails(DTO.ReceiptInfoDto info)
	{
        Info = info;
		InitializeComponent();
    }

}