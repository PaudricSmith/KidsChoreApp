using KidsChoreApp.Pages.Authentication;


namespace KidsChoreApp
{
    public partial class App : Application
    {
        
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            //MainPage = new AppShell();

            // Uncomment this to show the Register/Login screen first *******************************
            MainPage = new NavigationPage(MauiProgram.ServiceProvider.GetService<RegisterLoginPage>());
        }
    }
}
