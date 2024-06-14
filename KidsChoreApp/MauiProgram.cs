using KidsChoreApp.Pages;
using KidsChoreApp.Services;
using Microsoft.Extensions.Logging;


namespace KidsChoreApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            // Sqlite DB
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "chores.db3");
            builder.Services.AddSingleton<ChoreDatabase>(s => ActivatorUtilities.CreateInstance<ChoreDatabase>(s, dbPath));


            // Register pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<CreateChorePage>();
            builder.Services.AddTransient<ViewChoresPage>();


            // Add Services here
            builder.Services.AddSingleton<AuthenticationService>();
            //builder.Services.AddSingleton<ChoreService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            var mauiApp = builder.Build();

            App.ServiceProvider = mauiApp.Services;

            return mauiApp;
        }
    }
}
