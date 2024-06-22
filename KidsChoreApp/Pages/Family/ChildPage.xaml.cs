using KidsChoreApp.Models;
using KidsChoreApp.Services;


namespace KidsChoreApp.Pages.Family
{
    public partial class ChildPage : ContentPage
    {
        //private readonly Child _child;
        private readonly ChoreDatabase _choreDatabase;

        //public ChildPage(Child child, ChoreDatabase choreDatabase)
        //{
        //    InitializeComponent();
        //    _child = child;
        //    _choreDatabase = choreDatabase;
        //    BindingContext = _child;
        //    LoadChores();
        //}

        public ChildPage(ChoreDatabase choreDatabase)
        {
            InitializeComponent();
            //_child = child;
            _choreDatabase = choreDatabase;
            //BindingContext = _child;
            LoadChores();
        }

        private async void LoadChores()
        {
            //var chores = await _choreDatabase.GetChoresForChildAsync(_child.Id);
            //ChoresListView.ItemsSource = chores;
        }

        private async void OnChoreCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var chore = checkBox.BindingContext as Chore;

            if (chore != null)
            {
                if (checkBox.IsChecked)
                {
                    //_child.LifetimeEarnings += chore.Worth;
                    //_child.WeeklyEarnings += chore.Worth;
                }
                else
                {
                    //_child.LifetimeEarnings -= chore.Worth;
                    //_child.WeeklyEarnings -= chore.Worth;
                }

                await _choreDatabase.SaveChoreAsync(chore);
                BindingContext = null;
                //BindingContext = _child;
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
