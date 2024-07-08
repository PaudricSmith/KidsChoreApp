using KidsChoreApp.Models;
using SQLite;


namespace KidsChoreApp.Services
{
    public class ChoreService
    {
        private readonly SQLiteAsyncConnection _database;


        public ChoreService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Chore>().Wait();
        }


        public async Task<List<Chore>> GetChoresByChildIdAsync(int childId)
        {
            return await _database.Table<Chore>().Where(c => c.ChildId == childId).ToListAsync();
        }

        public async Task<int> CreateChoreAsync(Chore chore)
        {
            return await _database.InsertAsync(chore);
        }

        public async Task<int> UpdateChoreAsync(Chore chore)
        {
            return await _database.UpdateAsync(chore);
        }

        public async Task<int> DeleteChoreAsync(Chore chore)
        {
            return await _database.DeleteAsync(chore);
        }
    }
}
