using MoneyKeeper.Client.Core.Backend;
using MoneyKeeper.Client.Core.Exceptions;
using MoneyKeeper.Client.DTO;
using System.Globalization;

namespace MoneyKeeper.Client.View;

public partial class ReceiptDetails : ContentPage
{
    private readonly string ImagesApiUrl = $"{Consts.BaseApiUrl}images/";
    private readonly string ReceiptApiUrl = $"{Consts.BaseApiUrl}receipt/storage";
    private readonly string CategoriesApiUrl = $"{Consts.BaseApiUrl}budget/categories";

    public ReceiptInfoDto Info { get; }
    public double Value { get; set; }

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
            stack.IsVisible = false;
            activIndicator.IsVisible = true;
            activIndicator.IsRunning = true;
            var category = (pckCategories.SelectedItem as BudgetCategoryDto);
            var transaction = new TransactionCommit();
            await transaction.CommitTransactionAsync(
                new TransactionData(Value, SetDateWithDefaultHour(), category.Id, Info.Id));
        }
        catch (MoneyKeeperException)
        {
            await DisplayAlert("Error", "Error during transaction creation!", "OK");
        }
        catch (Exception)
        {
            throw;
        }
        finally 
        {
            stack.IsVisible = true;
            activIndicator.IsVisible = false;
            activIndicator.IsRunning = false;
        }
    }

    private DateTime SetDateWithDefaultHour()
    {
        return new DateTime(dpTransactionDate.Date.Year, dpTransactionDate.Date.Month, dpTransactionDate.Date.Day,
            12, 0, 0, DateTimeKind.Local);
    }

    private async void control_Appearing(object sender, EventArgs e)
    {
        var categoriesSource = new TransactionCategories();
        var categories = await categoriesSource.GetCategories();
        pckCategories.ItemsSource = categories.ToList();
    }
}