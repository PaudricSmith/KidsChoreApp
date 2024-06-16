using SQLite;


namespace KidsChoreApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FamilyId { get; set; }
        public string PasswordHash { get; set; }
    }
}
