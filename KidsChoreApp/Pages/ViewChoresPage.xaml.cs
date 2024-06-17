using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages
{
    public partial class ViewChoresPage : ContentPage
    {
        private readonly ChoreDatabase _choreDatabase;
        private readonly FamilyMemberDatabase _familyMemberDatabase;


        public ViewChoresPage(ChoreDatabase choreDatabase, FamilyMemberDatabase familyMemberDatabase)
        {
            InitializeComponent();

            _choreDatabase = choreDatabase;
            _familyMemberDatabase = familyMemberDatabase;

            LoadChores();
        }


        private async void LoadChores()
        {
            var chores = await _choreDatabase.GetChoresAsync();
            ChoresListView.ItemsSource = null;
            ChoresListView.ItemsSource = chores;
        }

        private async void OnChoreTapped(object sender, ItemTappedEventArgs e)
        {
            var chore = (Chore)e.Item;
            await Navigation.PushAsync(new ChoreDetailsPage(_choreDatabase, chore));
        }

        //private async void OnChoreTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var chore = (Chore)e.Item;

        //    var action = await DisplayActionSheet("Select Action", "Cancel", null, "Chore Completed", "Chore Not Completed", "Delete");
        //    if (action == "Chore Completed")
        //    {
        //        chore.IsComplete = true;
        //        await _choreDatabase.SaveChoreAsync(chore);
        //    }
        //    else if (action == "Chore Not Completed")
        //    {
        //        chore.IsComplete = false;
        //        await _choreDatabase.SaveChoreAsync(chore);
        //    }
        //    else if(action == "Delete")
        //    {
        //        await _choreDatabase.DeleteChoreAsync(chore);
        //    }

        //    LoadChores();
        //}

        private async void OnAddChoreClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateChorePage(_choreDatabase, _familyMemberDatabase));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadChores();
        }
    }
}
