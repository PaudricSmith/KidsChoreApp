using KidsChoreApp.Models;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages.Family
{
    public partial class AddChildPage : ContentPage
    {
        private readonly ChildService _childService;
        private string _selectedImage = "default_avatar.png"; // Default image


        public AddChildPage(ChildService childService)
        {
            InitializeComponent();
            _childService = childService;
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
            if (string.IsNullOrWhiteSpace(ChildNameEntry.Text) ||
                string.IsNullOrWhiteSpace(ChildPasscodeEntry.Text) ||
                string.IsNullOrWhiteSpace(ConfirmChildPasscodeEntry.Text) ||
                !decimal.TryParse(WeeklyAllowanceEntry.Text, out decimal weeklyAllowance))
            {
                await DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
                return;
            }

            if (ChildPasscodeEntry.Text != ConfirmChildPasscodeEntry.Text)
            {
                await DisplayAlert("Error", "Passcodes do not match.", "OK");
                return;
            }

            var childInDatabase = await _childService.ChildExistsAsync(ChildNameEntry.Text);
            if (!childInDatabase)
            {
                var child = new Child
                {
                    Name = ChildNameEntry.Text,
                    Image = _selectedImage, // Replace with actual image selection logic
                    Money = weeklyAllowance,
                    Passcode = ChildPasscodeEntry.Text
                };

                await _childService.SaveChildAsync(child);
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                await DisplayAlert("Error", "Child already exists!", "OK");
                return;
            }
        }
    }
}
