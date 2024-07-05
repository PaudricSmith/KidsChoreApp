using KidsChoreApp.Models;
using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class SettingsPage : ContentPage
    {
        private readonly UserService _userService;
        private User _user;
        private int _userId;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
            }
        }


        public SettingsPage(UserService userService)
        {
            InitializeComponent();

            _userService = userService;

            BindingContext = this;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            User = await _userService.GetUserByIdAsync(UserId); 

            LoadCurrentCurrency();
        }

        private async void LoadCurrentCurrency()
        {
            var currency = await _userService.GetUserPreferredCurrency(UserId);
            CurrencyPicker.SelectedItem = currency;
        }

        private async void OnCurrencySelected(object sender, EventArgs e)
        {
            var selectedCurrency = CurrencyPicker.SelectedItem as string;
            if (selectedCurrency != null)
            {
                await _userService.SetUserPreferredCurrency(UserId, selectedCurrency);
            }
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
