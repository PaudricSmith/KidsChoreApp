using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages.Chores
{
    public partial class CreateChorePage : ContentPage
    {
        private readonly ChoreService _choreService;
        private readonly ChildService _childService;


        public CreateChorePage(ChoreService choreDatabase, ChildService childService)
        {
            InitializeComponent();

            _choreService = choreDatabase;
            _childService = childService;

            LoadFamilyMembers();
        }

        private async void LoadFamilyMembers()
        {
            //var children = await _childService.GetAllChildrenAsync();

            //AssignedToPicker.ItemsSource = children;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ChoreNameEntry.Text))
            {
                await DisplayAlert("Error", "Please choose a name for this chore.", "OK");
                return;
            }
            if (AssignedToPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please assign the chore to a family member.", "OK");
                return;
            }

            var chore = new Chore
            {
                Name = ChoreNameEntry.Text,
                Description = DescriptionEditor.Text,
                AssignedTo = AssignedToPicker.SelectedItem.ToString(),
                Deadline = DeadlinePicker.Date,
                Priority = (int)PriorityStepper.Value,
                IsComplete = false
            };

            await _choreService.CreateChoreAsync(chore);
            await DisplayAlert("Success", "Chore created successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}
