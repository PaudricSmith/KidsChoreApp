using KidsChoreApp.Models;
using SQLite;


namespace KidsChoreApp.Services
{
    public class ParentService
    {
        private readonly SQLiteAsyncConnection _database;


        public ParentService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Parent>().Wait();
        }


        public async Task<Parent> GetParentByUserIdAsync(int userId)
        {
            return await _database.Table<Parent>().Where(p => p.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task CreateParentAsync(Parent parent)
        {
            await _database.InsertAsync(parent);
        }

        public async Task UpdateParentAsync(Parent parent)
        {
            await _database.UpdateAsync(parent);
        }

        public async Task<int> DeleteParentAsync(Parent parent)
        {
            return await _database.DeleteAsync(parent);
        }
    }
}
