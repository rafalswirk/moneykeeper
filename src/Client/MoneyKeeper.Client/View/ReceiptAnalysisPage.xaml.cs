using MoneyKeeper.Client.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace MoneyKeeper.Client.View;

public partial class ReceiptAnalysisPage : ContentPage
{
    private const string ReceiptApiUrl = "http://localhost:5126/api/";
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly ReceiptInfoDto _uploadedImageInfo;
    private readonly IReadOnlyList<BudgetCategoryDto> _categories;
    private ReceiptDto _receiptDto;

    public ObservableCollection<string> Steps { get; set; } = new();

    public ReceiptAnalysisPage(DTO.ReceiptInfoDto uploadedImageInfo, IReadOnlyList<BudgetCategoryDto> categories)
	{
		InitializeComponent();
        lstSteps.ItemsSource = Steps;
		lblImage.Text = uploadedImageInfo.ToString();
        _uploadedImageInfo = uploadedImageInfo;
        _categories = categories;
        ctrlAddCompany.OnSave += CtrlAddCompany_OnSave;
        ctrlAddCompany.OnReject += CtrlAddCompany_OnReject;
    }

    private async void CtrlAddCompany_OnReject(object sender, EventArgs e)
    {
        ctrlAddCompany.IsVisible = false;
        await Navigation.PopToRootAsync();
    }

    private async void CtrlAddCompany_OnSave(object sender, EventArgs e)
    {
        try
        {
            var companyDto = new CompanyDto(0, ctrlAddCompany.TaxId, ctrlAddCompany.CompanyName, _categories[ctrlAddCompany.CategorySelectedIndex].Id);
            var result = await PreaparePostRequestAsync(ReceiptApiUrl + "receipt/companies", companyDto);
            if(result.IsSuccessStatusCode)
            {
                Steps.Add($"Added company: {companyDto.ToString()}");
            }
            ctrlAddCompany.IsVisible = false;
            await AddEntryToSpreadsheet(_receiptDto.TaxNumber, _receiptDto.Date, _receiptDto.Total);
        }
        catch (Exception ex)
        {
            Steps.Add(ex.Message);
        }
    }

    private async void btnStartAnalysis_Clicked(object sender, EventArgs e)
    {
        try
        {
            Steps.Add("Sending request for starting analysis");
            var response = await PreaparePostRequestAsync(ReceiptApiUrl + "receipt/analysis", _uploadedImageInfo.Id);

            if (!response.IsSuccessStatusCode)
            {
                Steps.Add("Error! Analysis terminated.");
                return;
            }

            _receiptDto = await response.Content.ReadAsAsync<ReceiptDto>();
            Steps.Add(_receiptDto.ToString());

            response = await PreapareGetRequestAsync($"{ReceiptApiUrl}receipt/companies/?taxId={_receiptDto.TaxNumber}");
            if (!response.IsSuccessStatusCode)
            {
                Steps.Add("Cannot find company with provided tax id! Analysis terminated.");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Steps.Add("Please add new company to proceed");
                ctrlAddCompany.IsVisible = true;
                ctrlAddCompany.TaxId = _receiptDto.TaxNumber;
                ctrlAddCompany.Categories = _categories.Select(c => c.Category).ToList();
                return;
            }
            var companyDto = await response.Content.ReadAsAsync<CompanyDto>();
            Steps.Add(companyDto.ToString());

            await AddEntryToSpreadsheet(_receiptDto.TaxNumber, _receiptDto.Date, _receiptDto.Total);
        }
        catch (Exception ex)
        {
            Steps.Add(ex.Message);
        }
    }

    private async Task AddEntryToSpreadsheet(string taxNumber, DateTime date, double total)
    {
        var dto = new BudgetEntryDto(taxNumber, date, total);
        var result = await PreaparePostRequestAsync($"{ReceiptApiUrl}budget", dto);
        if (!result.IsSuccessStatusCode) 
        {
            Steps.Add("Value cannot be added to spreadsheet");
        }
    }

    private async Task<HttpResponseMessage> PreaparePostRequestAsync<T>(string url, T data)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(url, jsonContent);
        return response;
    }

    private async Task<HttpResponseMessage> PreapareGetRequestAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return response;
    }
}