using KidsChoreApp.Models;
using KidsChoreApp.Services;
using System.Collections.ObjectModel;


namespace KidsChoreApp.Pages.Chores
{
    public partial class AssignChoresPage : ContentPage
    {
        private readonly ChoreDatabase _choreDatabase;
        private readonly ChildService _childService;

        public ObservableCollection<Chore> Chores { get; set; }
        public ObservableCollection<Child> Children { get; set; }

        public AssignChoresPage(ChoreDatabase choreDatabase, ChildService childService)
        {
            InitializeComponent();
            _choreDatabase = choreDatabase;
            _childService = childService;

            LoadChores();
            LoadChildren();
        }


        private async void LoadChores()
        {
            var chores = await _choreDatabase.GetChoresAsync();
            Chores = new ObservableCollection<Chore>(chores);
            ChoresListView.ItemsSource = Chores;
        }

        private async void LoadChildren()
        {
            var children = await _childService.GetAllChildrenAsync();
            //Children = new ObservableCollection<FamilyMember>(children.Where(c => c.Role == "Child"));
        }

        private async void OnAddChoreClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddChorePage(_choreDatabase, _childService));
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            //var button = sender as Button;
            //var chore = button.BindingContext as Chore;
            //await Navigation.PushAsync(new EditChorePage(_choreDatabase, chore));
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var chore = button.BindingContext as Chore;
            var confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this chore?", "Yes", "No");
            if (confirm)
            {
                await _choreDatabase.DeleteChoreAsync(chore);
                Chores.Remove(chore);
            }
        }
    }
}
