using SQLite;
using KidsChoreApp.Models;


namespace KidsChoreApp.Services
{
    public class ChoreDatabase
    {
        private readonly SQLiteAsyncConnection _database;


        public ChoreDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Chore>().Wait();
        }


        public Task<List<Chore>> GetChoresAsync()
        {
            return _database.Table<Chore>().ToListAsync();
        }

        public Task<int> SaveChoreAsync(Chore chore)
        {
            if (chore.Id != 0)
            {
                return _database.UpdateAsync(chore);
            }
            else
            {
                return _database.InsertAsync(chore);
            }
        }

        public Task<int> DeleteChoreAsync(Chore chore)
        {
            return _database.DeleteAsync(chore);
        }
    }
}
