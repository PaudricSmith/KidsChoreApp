

namespace KidsChoreApp.Pages.Feedback
{
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }


        private async void OnLoveAppClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Rate this app", "Would you like to rate this app in the Google Play Store?", "Yes", "No");
            if (answer)
            {
                Uri uri = new("https://play.google.com/store/apps/details?id=com.yokee.piano.keyboard");
                bool isOpened = await Launcher.Default.TryOpenAsync(uri);

                if (!isOpened)
                {
                    await DisplayAlert("Error", "Unable to open the URL.", "OK");
                }
            }
        }

        private async void OnDislikeAppClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FeedbackFormPage));
        }
    }
}
