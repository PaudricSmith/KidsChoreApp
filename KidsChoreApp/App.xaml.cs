using KidsChoreApp.Pages.Authentication;


namespace KidsChoreApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            //MainPage = new AppShell();


            // Uncomment this to show the login screen first *******************************
            ServiceProvider = serviceProvider;
            MainPage = new NavigationPage(ServiceProvider.GetService<LoginPage>());
        }
    }
}
