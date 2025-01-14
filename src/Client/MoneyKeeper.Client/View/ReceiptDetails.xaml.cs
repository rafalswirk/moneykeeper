using MoneyKeeper.Client.Core.Backend;
using MoneyKeeper.Client.Core.Exceptions;
using MoneyKeeper.Client.DTO;
using System.Globalization;
using System.Linq;

namespace MoneyKeeper.Client.View;

public partial class ReceiptDetails : ContentPage
{
    private readonly string ImagesApiUrl = $"{Consts.BaseApiUrl}images/";
    private readonly string ReceiptApiUrl = $"{Consts.BaseApiUrl}receipt/storage";
    private readonly string CategoriesApiUrl = $"{Consts.BaseApiUrl}budget/categories";

    public ReceiptInfoDto Info { get; }
    public string Value { get; set; }

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
                new TransactionData(ToDoubleWithDecimalSeparator(Value), SetDateWithDefaultHour(), category.Id, Info.Id));
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

    //Entry control with numeric keyboard is blocking to write values 
    //with comma separator, even if comma is decimal separator for current 
    //culture. Workaround is based on using string format to keep two numbers
    //after decimal separator and manual convert to double if needed
    public double ToDoubleWithDecimalSeparator(string textValue)
    {
        if (string.IsNullOrEmpty(textValue))
            return 0.0;
        if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
        {
            if (textValue.Contains(','))
                textValue = textValue.Replace(",", ".");
        }
        if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
        {
            if (textValue.Contains('.'))
                textValue = textValue.Replace(".", ",");
        }
        return double.Parse(textValue);
    }

    private async void btnDeleteReceipt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var delete = await Application.Current.MainPage.DisplayAlert("MoneyKeeper", "Do you want to remove receipt?", "Yes", "No");
            if (!delete)
                return;

        }
        catch (Exception)
        {
            await Application.Current.MainPage.DisplayAlert("MoneyKeeper", "Receipt delete failure", "Ok");
        }
    }

    //private void enSum_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    var entry = sender as Entry;
    //    if (e.OldTextValue is null || e.NewTextValue is null)
    //        return;
    //    var oldWithoutDecimalSeparator = e.OldTextValue.Replace(",", "").Replace(".", "");
    //    var newWithoutDecimalSeparator = e.NewTextValue.Replace(",", "").Replace(".", "");
    //    if (oldWithoutDecimalSeparator == newWithoutDecimalSeparator)
    //    {
    //        entry.CursorPosition = entry.Text.Length;
    //    }
    //    else
    //    {
    //        if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
    //        {
    //            if (e.NewTextValue.Contains(','))
    //            {
    //                var lastCursorPosition = entry.CursorPosition;
    //                entry.Text = e.NewTextValue.Replace(",", ".");
    //            }
    //            return;
    //        }
    //        if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
    //        {
    //            if (e.NewTextValue.Contains('.'))
    //            {
    //                entry.Text = e.NewTextValue.Replace(".", ",");
    //            }
    //            return;
    //        }
    //    }
    //}
}