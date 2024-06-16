using SQLite;
using KidsChoreApp.Models;


namespace KidsChoreApp.Services
{
    public class FamilyMemberDatabase
    {
        private readonly SQLiteAsyncConnection _database;


        public FamilyMemberDatabase(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<FamilyMember>().Wait();
        }


        public Task<List<FamilyMember>> GetFamilyMembersAsync()
        {
            return _database.Table<FamilyMember>().ToListAsync();
        }

        public Task<FamilyMember> GetFamilyMemberAsync(int id)
        {
            return _database.Table<FamilyMember>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveFamilyMemberAsync(FamilyMember member)
        {
            if (member.Id != 0)
            {
                return _database.UpdateAsync(member);
            }
            else
            {
                return _database.InsertAsync(member);
            }
        }

        public Task<int> DeleteFamilyMemberAsync(FamilyMember member)
        {
            return _database.DeleteAsync(member);
        }
    }
}
