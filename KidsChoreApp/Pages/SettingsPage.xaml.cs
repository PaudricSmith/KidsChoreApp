using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages
{
    public partial class SettingsPage : ContentPage
    {
        private readonly UserService _userService;


        public SettingsPage(UserService userService)
        {
            InitializeComponent();

            _userService = userService;
        }


        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Implement logout logic
            
            //await _userService.LogoutAsync();

            if (Application.Current != null) 
                Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync(nameof(RegisterLoginPage));
        }
    }
}
