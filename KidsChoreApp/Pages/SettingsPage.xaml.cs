using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class SettingsPage : ContentPage
    {
        private readonly UserService _userService;
        private readonly IServiceProvider _serviceProvider;
        private int _userId;

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
            }
        }


        public SettingsPage(IServiceProvider serviceProvider, UserService userService)
        {
            InitializeComponent();

            _userService = userService;
            _serviceProvider = serviceProvider;

            BindingContext = this;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CurrencyPicker.SelectedItem = await _userService.GetUserPreferredCurrency(UserId);
        }

        private async void OnCurrencySelected(object sender, EventArgs e)
        {
            if (CurrencyPicker.SelectedItem is string selectedCurrency)
            {
                await _userService.SetUserPreferredCurrency(UserId, selectedCurrency);
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await _userService.LogoutAsync();

            if (Application.Current != null)
                Application.Current.MainPage = new NavigationPage(_serviceProvider.GetService<RegisterLoginPage>());

        }
    }
}
