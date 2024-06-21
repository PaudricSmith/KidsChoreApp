using KidsChoreApp.Services;
using System.Text.RegularExpressions;


namespace KidsChoreApp.Pages.Authentication
{
    public partial class RegisterLoginPage : ContentPage
    {
        private readonly AuthenticationService _authService;
        private bool _isRegistering = false;


        public RegisterLoginPage(AuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;

            ToggleLink.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ToggleRegisterLogin)
            });

            AppTip.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnHowToUseClicked)
            });

            ConfirmPasswordEntry.IsVisible = _isRegistering;
        }


        private void ToggleRegisterLogin()
        {
            _isRegistering = !_isRegistering;
            ConfirmPasswordEntry.IsVisible = _isRegistering;
            MainActionButton.Text = _isRegistering ? "Register" : "Login";
            ToggleLink.Text = _isRegistering ? "or Login" : "or Create a new account";
        }

        private void OnHowToUseClicked()
        {
            DisplayAlert("How to use app with whole family",
                "The parent installs first on their phone and sets all the passwords, " +
                "then on the child's phone after they install, simply click on the child lock icon.",
                "OK");
        }

        private async void OnMainActionButtonClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;
            string? confirmPassword = _isRegistering ? ConfirmPasswordEntry.Text : null;

            if (!ValidateInputs(email, password, confirmPassword))
            {
                return;
            }

            if (_isRegistering)
            {
                var success = await _authService.RegisterAsync(email, password);
                if (success)
                {
                    await DisplayAlert("Success", "Registration successful", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Email might already be taken.", "OK");
                    return;
                }
            }
            else
            {
                var success = await _authService.LoginAsync(email, password);
                if (success)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Error", "Invalid login. Please check your email and Password.", "OK");
                }
            }

            await Navigation.PushAsync(new HomePage());
        }

        private bool ValidateInputs(string email, string password, string? confirmPassword = null)
        {
            EmailErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;
            ConfirmPasswordErrorLabel.IsVisible = false;

            bool isValid = true;

            if (!ValidateEmail(email))
            {
                isValid = false;
            }

            if (!ValidatePassword(password))
            {
                isValid = false;
            }

            if (_isRegistering && password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Passwords do not match.";
                ConfirmPasswordErrorLabel.IsVisible = true;
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                EmailErrorLabel.Text = "Please enter a valid email address.";
                EmailErrorLabel.IsVisible = true;
                return false;
            }
            return true;
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
            {
                PasswordErrorLabel.Text = "Password must be at least 8 characters long and " +
                    "include at least one capital letter, one number and one special character.";
                PasswordErrorLabel.IsVisible = true;
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?""{}|<>])(?=.*[a-z])(?=.*[0-9]).{8,}$");
            return regex.IsMatch(password);
        }

        private void OnPasswordEyeToggleClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
        }
    }
}
