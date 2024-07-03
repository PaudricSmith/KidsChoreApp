using KidsChoreApp.Pages;
using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Pages.Debugging;
using KidsChoreApp.Pages.Family;
using KidsChoreApp.Pages.Feedback;


namespace KidsChoreApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterLoginPage), typeof(RegisterLoginPage));
            Routing.RegisterRoute(nameof(SetupPage), typeof(SetupPage));

            Routing.RegisterRoute(nameof(AddChildPage), typeof(AddChildPage));

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
            Routing.RegisterRoute(nameof(FeedbackFormPage), typeof(FeedbackFormPage));

            Routing.RegisterRoute(nameof(DebugPage), typeof(DebugPage));
        }
    }
}
