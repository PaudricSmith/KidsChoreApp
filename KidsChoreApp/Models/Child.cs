using SQLite;


namespace KidsChoreApp.Models
{
    public class Child
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Passcode { get; set; }
        public decimal Money { get; set; }
        public decimal WeeklyEarnings { get; set; }
        public decimal LifetimeEarnings { get; set; }
    }
}
