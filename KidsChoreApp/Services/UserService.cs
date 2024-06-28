﻿using System.Security.Cryptography;
using System.Text;
using KidsChoreApp.Models;
using SQLite;


namespace KidsChoreApp.Services
{
    public class UserService
    {
        private readonly SQLiteAsyncConnection _database;


        public UserService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<User>().Wait();
        }


        public async Task<bool> RegisterAsync(string email, string password)
        {
            var user = await _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user != null) return false; // User already exists

            var newUser = new User
            {
                Email = email,
                PasswordHash = HashPassword(password)
            };

            await _database.InsertAsync(newUser);
            return true;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null) return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
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