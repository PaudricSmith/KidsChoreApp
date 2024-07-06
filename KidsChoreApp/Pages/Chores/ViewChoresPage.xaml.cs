using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages.Chores
{
    public partial class ViewChoresPage : ContentPage
    {
        private readonly ChoreService _choreService;
        private readonly ChildService _familyMemberDatabase;


        public ViewChoresPage(ChoreService choreService, ChildService familyMemberDatabase)
        {
            InitializeComponent();

            _choreService = choreService;
            _familyMemberDatabase = familyMemberDatabase;

            LoadChores();
        }


        private async void LoadChores()
        {
            var chores = await _choreService.GetChoresByChildIdAsync(1);
            ChoresListView.ItemsSource = null;
            ChoresListView.ItemsSource = chores;
        }

        private async void OnChoreTapped(object sender, ItemTappedEventArgs e)
        {
            var chore = (Chore)e.Item;
            await Navigation.PushAsync(new ChoreDetailsPage(_choreService, chore));
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
            await Navigation.PushAsync(new CreateChorePage(_choreService, _familyMemberDatabase));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadChores();
        }
    }
}
