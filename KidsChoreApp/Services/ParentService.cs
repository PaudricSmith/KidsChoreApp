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


        public Task<Parent> GetParentByUserIdAsync(int userId)
        {
            return _database.Table<Parent>().Where(p => p.UserId == userId).FirstOrDefaultAsync();
        }

        public Task<int> SaveParentAsync(Parent parent)
        {
            if (parent.Id != 0)
            {
                return _database.UpdateAsync(parent);
            }
            else
            {
                return _database.InsertAsync(parent);
            }
        }

        public async Task UpdateParentAsync(Parent parent)
        {
            await _database.UpdateAsync(parent);
        }

        public Task<int> DeleteParentAsync(Parent parent)
        {
            return _database.DeleteAsync(parent);
        }
    }
}
