using KidsChoreApp.Models;
using SQLite;


namespace KidsChoreApp.Services
{
    public class ChildService
    {
        private readonly SQLiteAsyncConnection _database;


        public ChildService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Child>().Wait();
        }


        public async Task<List<Child>> GetAllChildrenByUserIdAsync(int userId)
        {
            return await _database.Table<Child>().Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Child> GetChildByIdAsync(int id)
        {
            return await _database.Table<Child>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveChildAsync(Child child)
        {
            if (child.Id != 0)
            {
                await _database.UpdateAsync(child);
            }
            else
            {
                await _database.InsertAsync(child);
            }
        }

        public async Task<int> DeleteChildAsync(Child child)
        {
            return await _database.DeleteAsync(child);
        }

        public async Task<bool> ChildExistsAsync(string name)
        {
            var child = await _database.Table<Child>().Where(c => c.Name == name).FirstOrDefaultAsync();
            return child != null;
        }
    }
}
