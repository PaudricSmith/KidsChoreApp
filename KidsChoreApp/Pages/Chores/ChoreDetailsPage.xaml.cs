using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages.Chores
{
    public partial class ChoreDetailsPage : ContentPage
    {
        private readonly ChoreDatabase _choreDatabase;
        private Chore _chore;


        public ChoreDetailsPage(ChoreDatabase choreDatabase, Chore chore)
        {
            InitializeComponent();

            _choreDatabase = choreDatabase;
            _chore = chore;

            BindingContext = _chore;
        }


        private async void OnMarkAsCompleteClicked(object sender, EventArgs e)
        {
            _chore.IsComplete = true;

            await _choreDatabase.SaveChoreAsync(_chore);
            await DisplayAlert("Success", "Chore marked as complete", "OK");

            await Navigation.PopAsync();
        }

        private async void OnMarkAsUncompleteClicked(object sender, EventArgs e)
        {
            _chore.IsComplete = false;

            await _choreDatabase.SaveChoreAsync(_chore);
            await DisplayAlert("Success", "Chore marked as uncomplete", "OK");

            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this chore?", "Yes", "No");
            if (confirm)
            {
                await _choreDatabase.DeleteChoreAsync(_chore);
                await DisplayAlert("Success", "Chore deleted", "OK");

                await Navigation.PopAsync();
            }
        }
    }
}
