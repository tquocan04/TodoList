using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ListService : IListService
    {
        private readonly ListContext _dbContext;
        public ListService(ListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<List>> GetListItemsAsync()
        {
            return await _dbContext.lists.ToListAsync();
        }
        public async Task<List?> GetListItemByIdAsync(int id)
        {
            return await _dbContext.lists.FindAsync(id);
        }
        public async Task<List> CreateListItemAsync(List item)
        {
            _dbContext.lists.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task UpdateListItemAsync(List item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteListItemAsync(int id)
        {
            var item = await _dbContext.lists.FindAsync(id);
            if (item != null)
            {
                _dbContext.lists.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
