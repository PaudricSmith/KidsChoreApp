using KidsChoreApp.Services;
using KidsChoreApp.Models;


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
            // Navigate to chore detail or edit page
            // await Navigation.PushAsync(new EditChorePage(_choreDatabase, chore));
        }
    }
}
