using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages
{
    public partial class CreateChorePage : ContentPage
    {
        private readonly ChoreDatabase _choreDatabase;


        public CreateChorePage(ChoreDatabase choreDatabase)
        {
            InitializeComponent();
            _choreDatabase = choreDatabase;
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var chore = new Chore
            {
                Name = ChoreNameEntry.Text,
                Description = DescriptionEditor.Text,
                AssignedTo = AssignedToEntry.Text,
                Deadline = DeadlinePicker.Date,
                Priority = (int)PriorityStepper.Value,
                IsComplete = false
            };

            await _choreDatabase.SaveChoreAsync(chore);
            await DisplayAlert("Success", "Chore created successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}
