using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MoneyKeeper.Client.Core.Backend.Storage;

namespace MoneyKeeper.Client.View;

public partial class TakePhotoView : ContentPage
{
    public Image Photo { get; set; }
    public TakePhotoView()
	{
		InitializeComponent();
        Dispatcher.DispatchAsync(async () => await TakePhoto());
	}

    private async void btnCapturePhoto_Clicked(object sender, EventArgs e)
    {
        await TakePhoto();
    }

    private async Task TakePhoto()
    {
        if (!MediaPicker.IsCaptureSupported)
        {
            await DisplayAlert("No Camera", ":( No camera available.", "OK");
            return;
        }
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                using Stream sourceStream = await photo.OpenReadAsync();
                var receiptStorage = new StoreReceipt();
                await receiptStorage.StoreAsync(photo.FileName, sourceStream);
                await Navigation.PopAsync(true);
            }
        }
    }
}