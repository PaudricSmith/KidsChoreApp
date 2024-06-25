using KidsChoreApp.Models;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages.Family
{
    public partial class AddChildPage : ContentPage
    {
        private readonly ChildService _childService;
        private string _selectedImage = "default_avatar.png"; // Default image

        private List<Entry> _passcodeEntries;


        public AddChildPage(ChildService childService)
        {
            InitializeComponent();
            _childService = childService;
            _passcodeEntries = new List<Entry> { Digit1, Digit2, Digit3, Digit4 };

            // Adding TapGestureRecognizers to ensure focus is properly managed
            foreach (var entry in _passcodeEntries)
            {
                entry.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(OnDigitEntryClicked)
                });
            }
        }


        private async void OnChildImageClicked(object sender, EventArgs e)
        {
            KeypadViewComponent.IsVisible = false;

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
            KeypadViewComponent.IsVisible = false;

            string passcode = string.Concat(_passcodeEntries.Select(entry => entry.Text));

            if (string.IsNullOrWhiteSpace(ChildNameEntry.Text) ||
                string.IsNullOrWhiteSpace(passcode) ||
                !decimal.TryParse(WeeklyAllowanceEntry.Text, out decimal weeklyAllowance))
            {
                await DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
                return;
            }

            if (passcode.Length < 4)
            {
                await DisplayAlert("Error", "Please enter 4 numbers.", "OK");
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
                    Passcode = passcode
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

        private void OnDigitEntryClicked()
        {
            KeypadViewComponent.IsVisible = true;

            ChildNameEntry.Unfocus();
            WeeklyAllowanceEntry.Unfocus();
        }

        private void OnKeypadNumClicked(object sender, string digit)
        {
            foreach (var entry in _passcodeEntries)
            {
                if (string.IsNullOrEmpty(entry.Text))
                {
                    entry.Text = digit;
                    break;
                }
            }
        }

        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            for (int i = _passcodeEntries.Count - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(_passcodeEntries[i].Text))
                {
                    _passcodeEntries[i].Text = string.Empty;
                    break;
                }
            }
        }

        private void OnOtherEntryFocused(object sender, FocusEventArgs e)
        {
            KeypadViewComponent.IsVisible = false;
        }
    }
}
