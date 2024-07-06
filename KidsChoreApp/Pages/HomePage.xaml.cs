using KidsChoreApp.Models;
using KidsChoreApp.Pages.Debugging;
using KidsChoreApp.Pages.Family;
using KidsChoreApp.Pages.Feedback;
using KidsChoreApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace KidsChoreApp.Pages
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private readonly ParentService _parentService;
        private readonly ChildService _childService;

        private User? _user;
        private Parent? _parent;
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

        public User? User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
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

        public ICommand OnChildTappedCommand { get; }


        public HomePage(UserService userService, ParentService parentService, ChildService childService)
        {
            InitializeComponent();
            _userService = userService;
            _parentService = parentService;
            _childService = childService;

            Children = new ObservableCollection<Child>();
            OnChildTappedCommand = new Command<Child>(async (child) => await OnChildTapped(child));

            BindingContext = this;

            Loaded += OnPageLoaded;
        }


        private async void OnPageLoaded(object? sender, EventArgs e)
        {
            await LoadData();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            User = await _userService.GetUserByIdAsync(UserId);
            _parent = await _parentService.GetParentByUserIdAsync(UserId);

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
                await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?userId={_userId}");
            }
            else
            {
                await DisplayAlert("Access Denied", "Unlock parental controls to access settings.", "OK");
            }
        }

        private async void OnFeedbackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FeedbackPage));
        }

        private async Task OnChildTapped(Child child)
        {
            if (child != null)
            {
                if (IsPadlockUnlocked)
                {
                    await Shell.Current.GoToAsync($"{nameof(ChildPage)}?childId={child.Id}");
                }
                else
                {
                    string result = await DisplayPromptAsync($"Enter {child.Name}'s Passcode", "", maxLength: 4, keyboard: Keyboard.Numeric);
                    if (result == child.Passcode)
                    {
                        await Shell.Current.GoToAsync($"{nameof(ChildPage)}?childId={child.Id}");
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

        public new event PropertyChangedEventHandler? PropertyChanged;
        protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
