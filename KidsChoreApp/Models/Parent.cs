using SQLite;


namespace KidsChoreApp.Models
{
    public class Parent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public string Passcode { get; set; }
    }
}
