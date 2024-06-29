using KidsChoreApp.Models;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages
{
    public partial class SetupPage : ContentPage
    {
        private readonly UserService _userService;
        private readonly ParentService _parentService;
        //private readonly User _user;
        private List<Entry> _passcodeEntries;


        public SetupPage(ParentService parentService)
        {
            InitializeComponent();
            _parentService = parentService;
            
            _passcodeEntries = new List<Entry> { Digit1, Digit2, Digit3, Digit4 };

            // Adding TapGestureRecognizers to ensure focus is properly managed
            foreach (var entry in _passcodeEntries)
            {
                entry.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(OnDigitEntryClicked)
                });
            }
        }


        private void OnDigitEntryClicked()
        {
            KeypadViewComponent.IsVisible = true;
        }

        private void OnKeypadNumClicked(object sender, string digit)
        {
            foreach (var entry in _passcodeEntries)
            {
                if (string.IsNullOrEmpty(entry.Text))
                {
                    entry.Text = digit;
                    break;
                }
            }
        }

        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            for (int i = _passcodeEntries.Count - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(_passcodeEntries[i].Text))
                {
                    _passcodeEntries[i].Text = string.Empty;
                    break;
                }
            }
        }

        private async void OnSavePasscodeClicked(object sender, EventArgs e)
        {
            string passcode = string.Concat(_passcodeEntries.Select(entry => entry.Text));

            if (passcode.Length != 4)
            {
                await DisplayAlert("Error", "Please enter a 4-digit passcode.", "OK");
                return;
            }

            // Create a new parent account
            var parentAccount = new Parent
            {
                UserId = 1, //_user.Id,
                Passcode = passcode
            };

            await _parentService.SaveParentAsync(parentAccount);

            // Mark setup as completed here
            //_user.IsSetupCompleted = true;

            //await _userService.UpdateUserAsync(_user);

            Application.Current.MainPage = new AppShell(); // new AppShell will show the first shell content which is the 'HomePage'
        }
    }
}
