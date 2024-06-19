using MauiIcons.Core;
using MauiIcons.Fluent;
using Microsoft.Maui.Controls;
using System;


namespace KidsChoreApp.Pages.Authentication
{
    public partial class RegisterLoginPage : ContentPage
    {
        private bool _isRegistering = true;
        private FluentIcons _eyeOpenedIcon;
        private FluentIcons _eyeClosedIcon;


        public RegisterLoginPage()
        {
            InitializeComponent();

            ToggleLink.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ToggleRegisterLogin)
            });

            AppTip.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnHowToUseClicked)
            });


            _eyeOpenedIcon = FluentIcons.Eye16;
            _eyeClosedIcon = FluentIcons.EyeOff16;

            PasswordToggle.Source = _eyeClosedIcon.ToImageSource();
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
            DisplayAlert("How to use app with whole family", "Tips on using the app with your family...", "OK");
        }

        private async void OnMainActionButtonClicked(object sender, EventArgs e)
        {
            if (_isRegistering)
            {
                if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match", "OK");
                    return;
                }

                // Register user logic here
            }
            else
            {
                // Login user logic here
            }

            await Navigation.PushAsync(new HomePage());
        }

        private void OnPasswordToggleClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
            PasswordToggle.Source = PasswordEntry.IsPassword ? _eyeClosedIcon.ToImageSource() : _eyeOpenedIcon.ToImageSource();
        }
    }
}
