﻿using KidsChoreApp.Pages;
using KidsChoreApp.Pages.Authentication;
using KidsChoreApp.Pages.Debugging;
using KidsChoreApp.Pages.Family;


namespace KidsChoreApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterLoginPage), typeof(RegisterLoginPage));
            Routing.RegisterRoute(nameof(SetupPage), typeof(SetupPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(AddChildPage), typeof(AddChildPage));
            Routing.RegisterRoute(nameof(DebugPage), typeof(DebugPage));
        }
    }
}
