using System.Collections.ObjectModel;

namespace MoneyKeeper.Client
{
    public partial class MainPage : ContentPage
    {
        private const string ApiUrl = "http://192.168.0.172:5126/api/images";
        private readonly HttpClient _httpClient = new HttpClient();

        public ObservableCollection<string> ImageUrls { get; } = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Fetch the list of image URLs from the API
            //var response = await _httpClient.GetAsync(ApiUrl);
            //if (response.IsSuccessStatusCode)
            //{
            //    var imageUrls = await response.Content.ReadAsAsync<List<string>>();
            //    foreach (var imageUrl in imageUrls)
            //    {
            //        ImageUrls.Add(imageUrl);
            //    }

            //}
            //else
            //{
            //    // Handle API error
            //}
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

            if (file != null)
            {
                try
                {
                    // Upload the file to the API
                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

                    var response = await _httpClient.PostAsync(ApiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var imageUrl = await response.Content.ReadAsStringAsync();
                        ImageUrls.Add(imageUrl);
                    }
                    else
                    {
                        // Handle API error
                    }
                }
                catch (Exception ex)
                {
                    // Handle upload error
                }
            }
        }
    }
}