using SQLite;


namespace KidsChoreApp.Models
{
    public class Child
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Money { get; set; }
        public decimal LifetimeEarnings { get; set; }
        public string Passcode { get; set; }
    }
}
