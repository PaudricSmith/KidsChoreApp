using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages.Family
{
    public partial class CreateFamilyMemberPage : ContentPage
    {
        private readonly ChildService _childService;


        public CreateFamilyMemberPage(ChildService childService)
        {
            InitializeComponent();
            _childService = childService;
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string childName = FamilyMemberNameEntry.Text;

            if (string.IsNullOrWhiteSpace(childName))
            {
                await DisplayAlert("Error", "Child name cannot be empty.", "OK");
                return;
            }
            if (await _childService.ChildExistsAsync(childName))
            {
                await DisplayAlert("Error", "A child with this name already exists.", "OK");
                return;
            }

            var member = new Child
            {
                Name = FamilyMemberNameEntry.Text,
                //Role = RolePicker.SelectedItem.ToString()
            };

            await _childService.SaveChildAsync(member);
            await DisplayAlert("Success", "Child created successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}
