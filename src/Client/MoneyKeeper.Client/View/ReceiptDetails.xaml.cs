using MoneyKeeper.Client.DTO;

namespace MoneyKeeper.Client.View;

public partial class ReceiptDetails : ContentPage
{
    private const string BaseApiUrl = "http://localhost:5126/api/";
    private readonly string ImagesApiUrl = $"{BaseApiUrl}images/";
    private readonly string ReceiptApiUrl = $"{BaseApiUrl}receipt/storage";
    private readonly string CategoriesApiUrl = $"{BaseApiUrl}budget/categories";

    public ReceiptInfoDto Info { get; }

	public ReceiptDetails(DTO.ReceiptInfoDto info)
	{
        Info = info;
		InitializeComponent();
        string apiUrl = $"{BaseApiUrl}images/{Info.ImageName}";

        imagePreview.Source = ImageSource.FromUri(new Uri(apiUrl));
    }

}