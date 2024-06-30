using KidsChoreApp.Models;
using KidsChoreApp.Pages.Debugging;
using KidsChoreApp.Pages.Family;
using KidsChoreApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace KidsChoreApp.Pages
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        private readonly ParentService _parentService;
        private readonly ChildService _childService;

        private Parent _parent;
        private int _userId;
        private bool _isPadlockUnlocked = false;

        public ObservableCollection<Child> Children { get; set; }

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
            }
        }

        public bool IsPadlockUnlocked
        {
            get => _isPadlockUnlocked;
            set
            {
                _isPadlockUnlocked = value;
                OnPropertyChanged();
            }
        }


        public HomePage(ParentService parentService, ChildService childService)
        {
            InitializeComponent();
            _parentService = parentService;
            _childService = childService;

            Children = new ObservableCollection<Child>();

            BindingContext = this;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {          
            var children = await _childService.GetAllChildrenByUserIdAsync(UserId);

            Children.Clear();
            foreach (var child in children)
            {
                Children.Add(child);
            }
        }

        private async void OnPadlockToggleClicked(object sender, EventArgs e)
        {
            if (IsPadlockUnlocked)
            {
                IsPadlockUnlocked = false;
            }
            else
            {
                _parent = await _parentService.GetParentByUserIdAsync(UserId);

                string result = await DisplayPromptAsync("Enter your Parental Passcode", "", maxLength: 4, keyboard: Keyboard.Numeric);
                if (result == _parent?.Passcode)
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
            await Shell.Current.GoToAsync($"{nameof(AddChildPage)}?userId={_userId}");
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
            var selectedChild = e.CurrentSelection[0] as Child;
            if (selectedChild != null)
            {
                if (IsPadlockUnlocked)
                {
                    //await Navigation.PushAsync(new ChildPage(selectedChild));
                }
                else
                {
                    string result = await DisplayPromptAsync($"Enter {selectedChild.Name}'s Passcode", "", maxLength: 4, keyboard: Keyboard.Numeric);
                    if (result == selectedChild.Passcode)
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

        private async void OnDebugClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DebugPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
