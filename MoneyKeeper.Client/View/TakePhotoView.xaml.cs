using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
namespace MoneyKeeper.Client.View;

public partial class TakePhotoView : ContentPage
{
    public Image Photo { get; set; }
    public TakePhotoView()
	{
		InitializeComponent();
	}

    private async void btnCapturePhoto_Clicked(object sender, EventArgs e)
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
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                image.Source = ImageSource.FromFile(localFilePath);


            }
        }
    }
}