using KidsChoreApp.Models;
using KidsChoreApp.Services;
using System.Collections.ObjectModel;


namespace KidsChoreApp.Pages.Family
{
    [QueryProperty(nameof(ChildId), "childId")]
    public partial class ChildPage : ContentPage
    {
        private readonly ChildService _childService;
        private readonly ChoreService _choreService;
        private int _childId;
        private Child? _child;

        public ObservableCollection<Chore> Chores { get; set; }

        public int ChildId
        {
            get => _childId;
            set
            {
                _childId = value;
                LoadChildData(value);
            }
        }


        public ChildPage(ChildService childService, ChoreService choreService)
        {
            InitializeComponent();

            _childService = childService;
            _choreService = choreService;

            Chores = new ObservableCollection<Chore>();
            
            BindingContext = this;
        }


        private async void LoadChildData(int childId)
        {
            _child = await _childService.GetChildByIdAsync(childId);

            BindingContext = _child;
            
            LoadChores(childId);
        }

        private async void LoadChores(int childId)
        {
            var chores = await _choreService.GetChoresByChildIdAsync(childId);

            Chores.Clear();
            foreach (var chore in chores)
            {
                Chores.Add(chore);
            }

            ChoresListView.ItemsSource = Chores;
        }

        private async void OnChoreCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var chore = checkBox.BindingContext as Chore;

            if (chore != null)
            {
                if (checkBox.IsChecked)
                {
                    _child.LifetimeEarnings += chore.Worth;
                    _child.WeeklyEarnings += chore.Worth;
                }
                else
                {
                    _child.LifetimeEarnings -= chore.Worth;
                    _child.WeeklyEarnings -= chore.Worth;
                }

                chore.IsComplete = checkBox.IsChecked;

                await _choreService.UpdateChoreAsync(chore); // Updated Chore completed checkbox 
                await _childService.UpdateChildAsync(_child); // Updated Child earnings
            }
        }

        private async void OnChangeImageClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Choose an image", "Cancel", null, "Choose from library", "Take a photo", "Select from avatars");
            switch (action)
            {
                case "Choose from library":
                    // Implement photo library selection
                    break;
                case "Take a photo":
                    // Implement photo capture
                    break;
                case "Select from avatars":
                    // Implement avatar selection
                    break;
            }
        }
    }
}
