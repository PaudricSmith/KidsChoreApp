using SQLite;


namespace KidsChoreApp.Models
{
    public class FamilyMember
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Money { get; set; }
        public string Passcode { get; set; } // Passcode for accessing the child's page
    }
}
