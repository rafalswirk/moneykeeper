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
		BindingContext = this;
	}

    private async void btnCapturePhoto_Clicked(object sender, EventArgs e)
    {
        if (!MediaPicker.IsCaptureSupported)
        {
            await DisplayAlert("No Camera", ":( No camera available.", "OK");
            return;
        }
        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
        if(photo != null) 
        {
            // Display the photo
            using (var stream = await photo.OpenReadAsync())
            {
                image.Source = ImageSource.FromStream(()=>stream);

            }
            return;
        }
        await DisplayAlert("No Photo", ":( No photo available.", "OK");
    }
}