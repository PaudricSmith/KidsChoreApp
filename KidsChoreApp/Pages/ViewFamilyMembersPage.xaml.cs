using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages
{
    public partial class ViewFamilyMembersPage : ContentPage
    {
        private readonly FamilyMemberDatabase _familyMemberDatabase;


        public ViewFamilyMembersPage(FamilyMemberDatabase familyMemberDatabase)
        {
            InitializeComponent();
            _familyMemberDatabase = familyMemberDatabase;
            LoadFamilyMembers();
        }


        private async void LoadFamilyMembers()
        {
            var members = await _familyMemberDatabase.GetFamilyMembersAsync();
            FamilyMembersListView.ItemsSource = members;
        }

        private async void OnFamilyMemberTapped(object sender, ItemTappedEventArgs e)
        {
            var member = (FamilyMember)e.Item;

            bool deleteMember = await DisplayAlert("Delete Family Member", "Do you want to delete this family member?", "Yes", "No");
            if (deleteMember)
            {
                await _familyMemberDatabase.DeleteFamilyMemberAsync(member);
                LoadFamilyMembers();
            }
        }

        private async void OnAddFamilyMemberClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateFamilyMemberPage(_familyMemberDatabase));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadFamilyMembers();
        }
    }
}
