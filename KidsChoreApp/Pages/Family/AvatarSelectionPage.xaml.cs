using System.Windows.Input;


namespace KidsChoreApp.Pages.Family
{
    public partial class AvatarSelectionPage : ContentPage
    {
        public List<string> Avatars { get; set; }
        public ICommand SelectAvatarCommand { get; }

        public AvatarSelectionPage()
        {
            InitializeComponent();

            Avatars = new List<string>
            {
                "Resources/Images/Avatars/batgirl.png",
                "Resources/Images/Avatars/batboy.png",
                "Resources/Images/Avatars/supergirl.png",
                "Resources/Images/Avatars/superboy.png",
                "Resources/Images/Avatars/flashgirl.png",
                "Resources/Images/Avatars/flashboy.png",
                "Resources/Images/Avatars/hulkgirl.png",
                "Resources/Images/Avatars/hulkboy.png",
                "Resources/Images/Avatars/spidergirl.png",
                "Resources/Images/Avatars/spiderboy.png",
            };

            SelectAvatarCommand = new Command<string>(OnAvatarSelected);
            BindingContext = this;
        }

        private async void OnAvatarSelected(string selectedAvatar)
        {
            var confirm = await DisplayAlert("Confirm", "Are you sure you want to select this image?", "Yes", "No");
            if (confirm)
            {
                await Shell.Current.GoToAsync($"..?selectedAvatar={selectedAvatar}");
            }
        }
    }
}
