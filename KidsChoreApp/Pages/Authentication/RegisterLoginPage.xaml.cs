using KidsChoreApp.Models;
using KidsChoreApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace KidsChoreApp.Pages.Authentication
{
    public partial class RegisterLoginPage : ContentPage, INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private readonly ParentService _parentService;
        private readonly ChildService _childService;
        private bool _isRegistering;
        private bool _isPasswordVisible;
        private string _email;
        private string _password;
        private string _confirmPassword;


        public bool IsRegistering
        {
            get => _isRegistering;
            set
            {
                _isRegistering = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MainActionText));
                OnPropertyChanged(nameof(ToggleLinkText));
                ConfirmPasswordEntry.IsVisible = _isRegistering;
            }
        }

        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged();
                PasswordEntry.IsPassword = !_isPasswordVisible;
                ConfirmPasswordEntry.IsPassword = !_isPasswordVisible;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public string MainActionText => IsRegistering ? "Register" : "Login";
        public string ToggleLinkText => IsRegistering ? "or Login" : "or Create a new account";


        public RegisterLoginPage(UserService userService, ParentService parentService, ChildService childService)
        {
            InitializeComponent();
            _userService = userService;
            _parentService = parentService;
            _childService = childService;

            BindingContext = this;

            IsRegistering = false;
            IsPasswordVisible = false; 

            ToggleLink.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ToggleRegisterLogin)
            });

            AppTip.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnHowToUseClicked)
            });
        }


        private void ToggleRegisterLogin()
        {
            IsRegistering = !IsRegistering;
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
            // TESTING PURPOSES ONLY!!!!! ////////////////////////////////////////////////////////////////////
                                Email = "testemail@email.com";
                                Password = "Password1!";
            //////////////////////////////////////////////////////////////////////////////////////////////////


            if (!ValidateInputs(Email, Password, ConfirmPassword))
            {
                return;
            }

            if (IsRegistering)
            {
                var success = await _userService.RegisterAsync(Email, Password);
                if (success)
                {
                    // Get the newly created user
                    var user = await _userService.GetUserByEmailAsync(Email);

                    // Create a new parent account
                    var parentAccount = new Parent
                    {
                        UserId = user.Id
                    };

                    await _parentService.SaveParentAsync(parentAccount);


                    //await DisplayAlert("Success", "Registration successful", "OK");
                    Application.Current.MainPage = new AppShell();


                    await Shell.Current.GoToAsync($"//{nameof(SetupPage)}?userId={user.Id}");

                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Email might already be taken.", "OK");
                    return;
                }
            }
            else // Logging In
            {
                var success = await _userService.LoginAsync(Email, Password);
                if (success)
                {
                    var user = await _userService.GetUserByEmailAsync(Email);
                    if (!user.IsSetupCompleted)
                    {
                        await Shell.Current.GoToAsync($"//{nameof(SetupPage)}?userId={user.Id}");
                    }
                    else
                    {
                        Application.Current.MainPage = new AppShell();
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Invalid login. Please check your email and Password.", "OK");
                    return;
                }
            }
        }

        private bool ValidateInputs(string email, string password, string confirmPassword = null)
        {
            EmailErrorLabel.IsVisible = false;
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

            if (IsRegistering && password != confirmPassword)
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
                DisplayAlert("Error", "Password must be at least 8 characters long and include at least one capital letter, one number and one special character.", "OK");

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
            IsPasswordVisible = !IsPasswordVisible;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
