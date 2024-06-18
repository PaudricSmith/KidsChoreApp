

namespace KidsChoreApp.Pages.Debugging
{
    public partial class DebugPage : ContentPage
    {

        public DebugPage()
        {
            InitializeComponent();
        }


        private void OnClearPreferencesClicked(object sender, EventArgs e)
        {
            Preferences.Clear();

            DisplayAlert("Success", "App preferences cleared!", "OK");
        }

        private void OnDeleteDatabaseClicked(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "kidschoreapp.db3");
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            DisplayAlert("Success", "App database file deleted!", "OK");
        }
    }
}
