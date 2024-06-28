using KidsChoreApp.Pages.Authentication;


namespace KidsChoreApp
{
    public partial class App : Application
    {
        
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Uncomment this to show the home page first *******************************
            //MainPage = new AppShell();

            MainPage = new NavigationPage(serviceProvider.GetService<RegisterLoginPage>());
        }
    }
}
