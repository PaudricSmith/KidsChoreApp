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


        public Task<List<Chore>> GetChoresByChildIdAsync(int childId)
        {
            return _database.Table<Chore>().Where(c => c.ChildId == childId).ToListAsync();
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
