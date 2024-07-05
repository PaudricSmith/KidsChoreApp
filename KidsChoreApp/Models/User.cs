using SQLite;


namespace KidsChoreApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? PreferredCurrency { get; set; }
        public bool IsSetupCompleted { get; set; }
    }
}
