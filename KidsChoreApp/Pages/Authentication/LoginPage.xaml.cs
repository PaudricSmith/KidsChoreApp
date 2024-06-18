using KidsChoreApp.Services;


namespace KidsChoreApp.Pages.Authentication
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthenticationService _authService;


        public LoginPage(AuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            FamilyIdErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;



            // *************************************************************************
            // Just for development to quickly login in 

                FamilyIdEntry.Text = "TestFamily";
                PasswordEntry.Text = "Password1";

            // *************************************************************************



            var familyId = FamilyIdEntry.Text;
            var password = PasswordEntry.Text;

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

            if (!isValid)
            {
                return;
            }

            var success = await _authService.LoginAsync(familyId, password);
            if (success)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Error", "Invalid login. Please check your Family ID and Password.", "OK");
            }
        }

        private async void OnGoToRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_authService));
        }
    }
}
