﻿using MoneyKeeper.Client.Core.Backend;
using MoneyKeeper.Client.Core.Backend.Storage;
using MoneyKeeper.Client.DTO;
using MoneyKeeper.Client.View;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace MoneyKeeper.Client
{
    public partial class MainPage : ContentPage
    {
        private readonly string ImagesApiUrl = $"{Consts.BaseApiUrl}images/";
        private readonly string ReceiptApiUrl = $"{Consts.BaseApiUrl}receipt/storage";
        private readonly string CategoriesApiUrl = $"{Consts.BaseApiUrl}budget/categories";


        private readonly HttpClient _httpClient = new HttpClient();
        private ReceiptInfoDto _uploadedImageInfo;
        public ObservableCollection<ReceiptInfoDto> Receipts { get; set; } = new ObservableCollection<ReceiptInfoDto>();

        public ObservableCollection<string> ImageUrls { get; } = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            receiptsControl.ReceiptSelected += ReceiptsControl_ReceiptSelected;
        }

        private async void ReceiptsControl_ReceiptSelected(object sender, ReceiptInfoDto e)
        {
            await Navigation.PushAsync(new ReceiptDetails(e));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Fetch the list of image URLs from the API
            try
            {
                var response = await _httpClient.GetAsync(ReceiptApiUrl + "/all");
                if (response.IsSuccessStatusCode)
                {
                    var allInfo = await response.Content.ReadAsAsync<IEnumerable<ReceiptInfoDto>>();
                    ImageUrls.Clear();
                    Receipts.Clear();
                    foreach (var receiptInfo in allInfo.Where(i => i.SpreadsheetEntered == false))
                    {
                        Receipts.Add(receiptInfo);
                    }

                }
                else
                {
                    // Handle API error
                }
            }
            catch (Exception)
            {

            }
        }

        private async void UploadButton_Clicked(object sender, EventArgs e)
        {
            // Show the file picker
            var file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.macOS, new[] { "public.image" } },
                    { DevicePlatform.iOS, new[] { "public.image" } },
                    { DevicePlatform.Android, new[] { "image/*" } },
                    { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" } }
                })
            });
            activIndicator.IsRunning = true;
            if (file != null)
            {
                try
                {
                    var storeReceipt = new StoreReceipt();
                    var dto = await storeReceipt.StoreAsync(file.FileName, await file.OpenReadAsync());
                    Receipts.Add(dto);
                    ImageUrls.Add(dto.ImageName);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "OK");    
                }
                finally
                {
                    activIndicator.IsRunning = false;
                }
            }
        }

        private async void btnIsAlive_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync(ImagesApiUrl + "isalive");
                if(!response.IsSuccessStatusCode)
                    throw new Exception("Server is not alive");
                await DisplayAlert("Alert", "Server is alive", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

        private async void btnTakePhoto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TakePhotoView());
        }
    }
}