using ToDoList.Repositories;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
namespace ToDoList.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly ListContext _listContext;

        // Constructor để inject DblistContext
        public ListRepository(ListContext listContext)
        {
            _listContext = listContext;
        }
        public async Task<IEnumerable<List>> GetAllAsync()
        {
            return await _listContext.lists.ToListAsync();
        }

        public async Task<List?> GetByIdAsync(int id)
        {
            return await _listContext.lists.FindAsync(id);
        }

        public async Task AddAsync(List item)
        {
            await _listContext.lists.AddAsync(item);
            await _listContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List item)
        {
            _listContext.lists.Update(item);
            await _listContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _listContext.lists.FindAsync(id);
            if (item != null)
            {
                _listContext.lists.Remove(item);
                await _listContext.SaveChangesAsync();
            }
        }
    }
}
