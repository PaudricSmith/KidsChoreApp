using System.Security.Cryptography;
using System.Text;
using KidsChoreApp.Models;
using SQLite;


namespace KidsChoreApp.Services
{
    public class AuthenticationService
    {
        private readonly SQLiteAsyncConnection _database;


        public AuthenticationService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<User>().Wait();
        }


        public async Task<bool> RegisterAsync(string familyId, string password)
        {
            var user = await _database.Table<User>().Where(u => u.FamilyId == familyId).FirstOrDefaultAsync();
            if (user != null) return false; // User already exists

            var newUser = new User
            {
                FamilyId = familyId,
                PasswordHash = HashPassword(password)
            };

            await _database.InsertAsync(newUser);
            return true;
        }

        public async Task<bool> LoginAsync(string familyId, string password)
        {
            var user = await _database.Table<User>().Where(u => u.FamilyId == familyId).FirstOrDefaultAsync();
            if (user == null) return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == storedHash;
        }
    }
}
