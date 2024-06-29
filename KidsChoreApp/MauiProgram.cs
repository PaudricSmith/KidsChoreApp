using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Pages.Chores;
using KidsChoreApp.Pages.Family;
using KidsChoreApp.Services;
using Microsoft.Extensions.Logging;
using SQLite;
using MauiIcons.Fluent;
using KidsChoreApp.Pages;


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
                })
                .UseFluentMauiIcons();


            // Initialize SQLitePCLRaw
            SQLitePCL.Batteries_V2.Init();

            // Sqlite DB
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "kidschoreapp.db3");
            builder.Services.AddSingleton<SQLiteAsyncConnection>(s => new SQLiteAsyncConnection(dbPath));

            //if (File.Exists(dbPath))
            //{
            //    File.Delete(dbPath);
            //}

            // Register Services
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<ParentService>();
            builder.Services.AddSingleton<ChildService>();
            builder.Services.AddSingleton<ChoreDatabase>();

            // Register pages
            builder.Services.AddTransient<RegisterLoginPage>();
            builder.Services.AddTransient<SetupPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<AddChildPage>();
            builder.Services.AddTransient<CreateChorePage>();
            builder.Services.AddTransient<ViewChoresPage>();
            builder.Services.AddTransient<ChoreDetailsPage>();
            builder.Services.AddTransient<CreateFamilyMemberPage>();
            builder.Services.AddTransient<ViewFamilyMembersPage>();

            var mauiApp = builder.Build();

            return mauiApp;
        }
    }
}
