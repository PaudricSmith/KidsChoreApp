namespace KidsChoreApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();



            // Debug option visibility toggle (for testing)
#if DEBUG
            var debugItem = Items.FirstOrDefault(item => item.Route == "debug");
            if (debugItem != null)
            {
                debugItem.IsVisible = true;
            }
#endif
        }
    }
}
