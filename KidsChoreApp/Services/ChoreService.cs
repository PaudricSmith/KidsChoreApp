using KidsChoreApp.Models;


namespace KidsChoreApp.Services
{
    public class ChoreService
    {
        private readonly List<Chore> _chores = new();


        public Task<List<Chore>> GetChoresAsync()
        {
            return Task.FromResult(_chores);
        }


        public Task AddChoreAsync(Chore chore)
        {
            _chores.Add(chore);
            return Task.CompletedTask;
        }

        public Task UpdateChoreAsync(Chore chore)
        {
            var existingChore = _chores.Find(c => c.Name == chore.Name);
            if (existingChore != null)
            {
                existingChore.Description = chore.Description;
                existingChore.AssignedTo = chore.AssignedTo;
                existingChore.Deadline = chore.Deadline;
                existingChore.Priority = chore.Priority;
                existingChore.IsComplete = chore.IsComplete;
            }
            return Task.CompletedTask;
        }

        public Task DeleteChoreAsync(Chore chore)
        {
            _chores.Remove(chore);
            return Task.CompletedTask;
        }
    }
}
