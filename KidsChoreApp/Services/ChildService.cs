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


        public Task<List<Child>> GetAllChildrenAsync()
        {
            return _database.Table<Child>().ToListAsync();
        }

        public Task<Child> GetChildAsync(int id)
        {
            return _database.Table<Child>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveChildAsync(Child child)
        {
            if (child.Id != 0)
            {
                return _database.UpdateAsync(child);
            }
            else
            {
                return _database.InsertAsync(child);
            }
        }

        public Task<int> DeleteChildAsync(Child child)
        {
            return _database.DeleteAsync(child);
        }

        public async Task<bool> ChildExistsAsync(string name)
        {
            var child = await _database.Table<Child>().Where(c => c.Name == name).FirstOrDefaultAsync();
            return child != null;
        }
    }
}
