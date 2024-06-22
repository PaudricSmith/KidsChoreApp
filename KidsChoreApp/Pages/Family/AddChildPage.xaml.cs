

namespace KidsChoreApp.Pages.Family
{
    public partial class AddChildPage : ContentPage
    {
        public AddChildPage()
        {
            InitializeComponent();
        }


        private async void OnChildImageClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Choose an image", "Cancel", null, "Choose from library", "Take a photo", "Select from avatars");
            switch (action)
            {
                case "Choose from library":
                    // Implement photo library selection
                    break;
                case "Take a photo":
                    // Implement photo capture
                    break;
                case "Select from avatars":
                    // Implement avatar selection
                    break;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (ChildPasscodeEntry.Text != ConfirmChildPasscodeEntry.Text)
            {
                await DisplayAlert("Error", "Passcodes do not match", "OK");
                return;
            }

            // Save child logic here
            await DisplayAlert("Success", "Child added successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}
