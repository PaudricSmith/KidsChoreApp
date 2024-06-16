using KidsChoreApp.Services;
using System.Text.RegularExpressions;


namespace KidsChoreApp.Pages
{
    public partial class RegisterPage : ContentPage
    {
        private readonly AuthenticationService _authService;


        public RegisterPage(AuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;
        }


        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            FamilyIdErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;
            ConfirmPasswordErrorLabel.IsVisible = false;

            var familyId = FamilyIdEntry.Text;
            var password = PasswordEntry.Text;
            var confirmPassword = ConfirmPasswordEntry.Text;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(familyId) || familyId.Length < 7)
            {
                FamilyIdErrorLabel.Text = "Family ID must be at least 7 characters long.";
                FamilyIdErrorLabel.IsVisible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 7)
            {
                PasswordErrorLabel.Text = "Password must be at least 7 characters long.";
                PasswordErrorLabel.IsVisible = true;
                isValid = false;
            }

            if (password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Passwords do not match.";
                ConfirmPasswordErrorLabel.IsVisible = true;
                isValid = false;
            }

            if (!isValid)
            {
                return;
            }

            var success = await _authService.RegisterAsync(familyId, password);
            if (success)
            {
                await DisplayAlert("Success", "Registration successful", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Registration failed. Family ID might already be taken.", "OK");
                //FamilyIdExistsLabel.Text = "This Family ID already exists.";
                //FamilyIdExistsLabel.IsVisible = true;
            }
        }

        private async void OnGoToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
