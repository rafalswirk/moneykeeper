using MoneyKeeper.Client.DTO;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace MoneyKeeper.Client.View;

public partial class ReceiptAnalysisPage : ContentPage
{
    private const string ReceiptApiUrl = "http://localhost:5126/api/receipt/analysis";
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly ReceiptInfoDto _uploadedImageInfo;
    public ObservableCollection<string> Steps { get; set; } = new();

    public ReceiptAnalysisPage(DTO.ReceiptInfoDto uploadedImageInfo)
	{
		InitializeComponent();
        lstSteps.ItemsSource = Steps;
		lblImage.Text = uploadedImageInfo.ToString();
        _uploadedImageInfo = uploadedImageInfo;
    }

    private async void btnStartAnalysis_Clicked(object sender, EventArgs e)
    {
        Steps.Add("Sending request for starting analysis");
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(_uploadedImageInfo.Id),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.PostAsync(ReceiptApiUrl, jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var receiptDto = await response.Content.ReadAsAsync<ReceiptDto>();
            Steps.Add(receiptDto.ToString());
        }
        else
        {
            Steps.Add("Error! Analysis terminated.");
            return;
        }
    }
}