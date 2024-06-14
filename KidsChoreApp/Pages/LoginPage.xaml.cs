using KidsChoreApp.Services;


namespace KidsChoreApp.Pages
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
            var familyId = FamilyIdEntry.Text;
            var password = PasswordEntry.Text;

            var success = await _authService.LoginAsync(familyId, password);
            if (success)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Error", "Invalid login", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var familyId = FamilyIdEntry.Text;
            var password = PasswordEntry.Text;

            var success = await _authService.RegisterAsync(familyId, password);
            if (success)
            {
                await DisplayAlert("Success", "Registration successful", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Registration failed", "OK");
            }
        }
    }
}
