namespace MoneyKeeper.Client.View;

public partial class ReceiptAnalysisPage : ContentPage
{
	public ReceiptAnalysisPage(DTO.ReceiptInfoDto _uploadedImageInfo)
	{
		InitializeComponent();
		lblImage.Text = _uploadedImageInfo.ToString();
	}
}