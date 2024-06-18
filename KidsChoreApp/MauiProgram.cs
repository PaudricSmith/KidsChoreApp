using KidsChoreApp.Pages;
using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Pages.Chores;
using KidsChoreApp.Pages.Family;
using KidsChoreApp.Services;
using Microsoft.Extensions.Logging;
using SQLite;


namespace KidsChoreApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

#if DEBUG
            builder.Logging.AddDebug();
#endif


            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Initialize SQLitePCLRaw
            SQLitePCL.Batteries_V2.Init();

            // Sqlite DB
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "kidschoreapp.db3");
            builder.Services.AddSingleton<SQLiteAsyncConnection>(s => new SQLiteAsyncConnection(dbPath));

            // Register Services
            builder.Services.AddSingleton<AuthenticationService>();
            builder.Services.AddSingleton<ChoreDatabase>();
            builder.Services.AddSingleton<FamilyMemberDatabase>();

            // Register pages
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<CreateChorePage>();
            builder.Services.AddTransient<ViewChoresPage>();
            builder.Services.AddTransient<ChoreDetailsPage>();
            builder.Services.AddTransient<CreateFamilyMemberPage>();
            builder.Services.AddTransient<ViewFamilyMembersPage>();


            var mauiApp = builder.Build();

            App.ServiceProvider = mauiApp.Services;

            return mauiApp;
        }
    }
}
