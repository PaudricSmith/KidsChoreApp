using KidsChoreApp.Models;
using KidsChoreApp.Pages.Family;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace KidsChoreApp.Pages
{
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        private bool _isPadlockUnlocked = false;
        private string _parentalPasscode = "1234"; // Example passcode, should be securely stored and retrieved


        public ObservableCollection<FamilyMember> Children { get; set; }
        public bool IsPadlockUnlocked
        {
            get => _isPadlockUnlocked;
            set
            {
                _isPadlockUnlocked = value;
                OnPropertyChanged();
            }
        }

        public HomePage()
        {
            InitializeComponent();
            LoadChildren();

            BindingContext = this;
        }


        private void LoadChildren()
        {
            // Load children from database
            Children = new ObservableCollection<FamilyMember>
            {
                new FamilyMember { Name = "Kimberly", Image = "child1.png", Money = 10 },
                new FamilyMember { Name = "Edward", Image = "child2.png", Money = 15 }
            };
        }

        private async void OnPadlockToggleClicked(object sender, EventArgs e)
        {
            if (IsPadlockUnlocked)
            {
                IsPadlockUnlocked = false;
            }
            else
            {
                string result = await DisplayPromptAsync("Enter your Parental Passcode", "", maxLength: 4, keyboard: Keyboard.Numeric);
                if (result == _parentalPasscode)
                {
                    IsPadlockUnlocked = true;
                }
                else
                {
                    await DisplayAlert("Error", "Incorrect passcode", "OK");
                }
            }
        }

        private async void OnAddChildClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddChildPage());
        }

        private async void OnAssignChoresClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AssignChoresPage());
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            if (IsPadlockUnlocked)
            {
                //await Navigation.PushAsync(new SettingsPage());
            }
            else
            {
                await DisplayAlert("Access Denied", "Unlock parental controls to access settings.", "OK");
            }
        }

        private async void OnFeedbackClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new FeedbackPage());
        }

        private async void OnChildSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedChild = e.CurrentSelection[0] as FamilyMember;
            if (selectedChild != null)
            {
                if (IsPadlockUnlocked)
                {
                    //await Navigation.PushAsync(new ChildPage(selectedChild));
                }
                else
                {
                    string result = await DisplayPromptAsync($"Enter {selectedChild.Name}'s Passcode", "", maxLength: 4, keyboard: Keyboard.Numeric);
                    if (result == selectedChild.Passcode) // Assuming each FamilyMember has a Passcode property
                    {
                        //await Navigation.PushAsync(new ChildPage(selectedChild));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Incorrect passcode", "OK");
                    }
                }
            }
        }
    }
}
