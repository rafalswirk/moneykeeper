using MoneyKeeper.Client.Core.Backend;
using MoneyKeeper.Client.DTO;

namespace MoneyKeeper.Client.View;

public partial class ReceiptDetails : ContentPage
{
    private readonly string ImagesApiUrl = $"{Consts.BaseApiUrl}images/";
    private readonly string ReceiptApiUrl = $"{Consts.BaseApiUrl}receipt/storage";
    private readonly string CategoriesApiUrl = $"{Consts.BaseApiUrl}budget/categories";

    public ReceiptInfoDto Info { get; }

    public ReceiptDetails(DTO.ReceiptInfoDto info)
	{
        Info = info;
		InitializeComponent();
        string apiUrl = $"{Consts.BaseApiUrl}images/{Info.ImageName}";

        imagePreview.Source = ImageSource.FromUri(new Uri(apiUrl));
    }

    private async void btnWriteToSpreadsheet_Clicked(object sender, EventArgs e)
    {
        try
        {
            var category = (pckCategories.SelectedItem as BudgetCategoryDto);
            var transaction = new TransactionCommit();
            await transaction.CommitTransactionAsync(new TransactionDto(dpTransactionDate.Date, category.Id, double.Parse(enSum.Text)));

        }
        catch (Exception)
        {
            throw;
        }
    }

    private async void control_Appearing(object sender, EventArgs e)
    {
        var categoriesSource = new TransactionCategories();
        var categories = await categoriesSource.GetCategories();
        pckCategories.ItemsSource = categories.ToList();
    }
}