using KidsChoreApp.Models;
using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class SettingsPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserService _userService;
        private readonly ParentService _parentService;
        private Parent? _parent;
        private int _userId;


        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
            }
        }


        public SettingsPage(IServiceProvider serviceProvider, UserService userService, ParentService parentService)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _userService = userService;
            _parentService = parentService;

            BindingContext = this;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _parent = await _parentService.GetParentByUserIdAsync(UserId);
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

        private async void OnChangePasscodeClicked(object sender, EventArgs e)
        {
            // Ask for old passcode
            string oldPasscode = await DisplayPromptAsync("Change Parental Passcode", "Enter your old Parental Passcode", maxLength: 4, keyboard: Keyboard.Numeric);
            if (oldPasscode == _parent?.Passcode)
            {
                // Ask for new passcode
                string newPasscode = await DisplayPromptAsync("Change Parental Passcode", "Enter your new Parental Passcode", maxLength: 4, keyboard: Keyboard.Numeric);
                if (!string.IsNullOrEmpty(newPasscode))
                {
                    // Confirm new passcode
                    string confirmPasscode = await DisplayPromptAsync("Change Parental Passcode", "Confirm your new Parental Passcode", maxLength: 4, keyboard: Keyboard.Numeric);
                    if (newPasscode == confirmPasscode)
                    {
                        _parent.Passcode = newPasscode;
                        await _parentService.UpdateParentAsync(_parent);
                        await DisplayAlert("Success", "Parental Passcode changed successfully.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "New passcodes do not match.", "OK");
                    }
                    
                }
                else
                {
                    await DisplayAlert("Error", "New passcode cannot be empty.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Incorrect old passcode.", "OK");
            }
        }
    }
}
