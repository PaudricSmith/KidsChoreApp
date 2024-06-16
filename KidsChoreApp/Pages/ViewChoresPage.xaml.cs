using KidsChoreApp.Services;
using KidsChoreApp.Models;
using UIKit;


namespace KidsChoreApp.Pages
{
    public partial class ViewChoresPage : ContentPage
    {
        private readonly ChoreDatabase _choreDatabase;


        public ViewChoresPage(ChoreDatabase choreDatabase)
        {
            InitializeComponent();
            _choreDatabase = choreDatabase;
            LoadChores();
        }


        private async void LoadChores()
        {
            var chores = await _choreDatabase.GetChoresAsync();
            ChoresListView.ItemsSource = chores;
        }

        private async void OnChoreTapped(object sender, ItemTappedEventArgs e)
        {
            var chore = (Chore)e.Item;

            bool markAsComplete = await DisplayAlert("Complete Chore", "Do you want to mark this chore as complete?", "Yes", "No");
            if (markAsComplete)
            {
                chore.IsComplete = true;

                await _choreDatabase.SaveChoreAsync(chore);
                LoadChores();
            }
        }

        private async void OnAddChoreClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateChorePage(_choreDatabase));
        }
    }
}
