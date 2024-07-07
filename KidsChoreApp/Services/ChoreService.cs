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

        public async Task SaveChoreAsync(Chore chore)
        {
            if (chore.Id != 0)
            {
                await _database.UpdateAsync(chore);
            }
            else
            {
                await _database.InsertAsync(chore);
            }
        }

        public async Task<int> DeleteChoreAsync(Chore chore)
        {
            return await _database.DeleteAsync(chore);
        }
    }
}
