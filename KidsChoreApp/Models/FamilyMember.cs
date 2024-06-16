using SQLite;


namespace KidsChoreApp.Models
{
    public class FamilyMember
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // Parent or Child
    }
}
