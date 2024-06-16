using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages
{
    public partial class CreateFamilyMemberPage : ContentPage
    {
        private readonly FamilyMemberDatabase _familyMemberDatabase;


        public CreateFamilyMemberPage(FamilyMemberDatabase familyMemberDatabase)
        {
            InitializeComponent();
            _familyMemberDatabase = familyMemberDatabase;
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var member = new FamilyMember
            {
                Name = FamilyMemberNameEntry.Text,
                Role = RolePicker.SelectedItem.ToString()
            };

            await _familyMemberDatabase.SaveFamilyMemberAsync(member);
            await DisplayAlert("Success", "Family member created successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}
