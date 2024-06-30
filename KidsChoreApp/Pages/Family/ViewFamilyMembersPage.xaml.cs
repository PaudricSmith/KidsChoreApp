using KidsChoreApp.Services;
using KidsChoreApp.Models;


namespace KidsChoreApp.Pages.Family
{
    public partial class ViewFamilyMembersPage : ContentPage
    {
        private readonly ChildService _childService;


        public ViewFamilyMembersPage(ChildService childService)
        {
            InitializeComponent();
            _childService = childService;
            LoadFamilyMembers();
        }


        private async void LoadFamilyMembers()
        {
            //var children = await _childService.GetAllChildrenAsync();
            //FamilyMembersListView.ItemsSource = children;
        }

        private async void OnFamilyMemberTapped(object sender, ItemTappedEventArgs e)
        {
            var child = (Child)e.Item;

            bool deleteMember = await DisplayAlert("Delete child", "Do you want to delete this child?", "Yes", "No");
            if (deleteMember)
            {
                await _childService.DeleteChildAsync(child);
                LoadFamilyMembers();
            }
        }

        private async void OnAddFamilyMemberClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateFamilyMemberPage(_childService));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadFamilyMembers();
        }
    }
}
