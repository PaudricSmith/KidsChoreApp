using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages
{
    public partial class CreateChorePage : ContentPage
    {
        private readonly ChoreDatabase _choreDatabase;
        private readonly FamilyMemberDatabase _familyMemberDatabase;


        public CreateChorePage(ChoreDatabase choreDatabase, FamilyMemberDatabase familyMemberDatabase)
        {
            InitializeComponent();

            _choreDatabase = choreDatabase;
            _familyMemberDatabase = familyMemberDatabase;

            LoadFamilyMembers();
        }

        private async void LoadFamilyMembers()
        {
            var members = await _familyMemberDatabase.GetFamilyMembersAsync();
            var children = members.Where(m => m.Role == "Child").Select(m => m.Name).ToList();

            AssignedToPicker.ItemsSource = children;
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

            await _choreDatabase.SaveChoreAsync(chore);
            await DisplayAlert("Success", "Chore created successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}
